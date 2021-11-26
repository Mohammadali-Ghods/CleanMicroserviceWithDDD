using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using Domain.ResultModel;
using Domain.ValueObject;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Domain.Commands
{
    public class CustomerCommandHandler :
        IRequestHandler<RegisterNewCustomerCommand, Result>,
        IRequestHandler<UpdateCustomerCommand, Result>,
        IRequestHandler<RemoveCustomerCommand, Result>,
        IRequestHandler<VerifyingNewCustomerWithCodeCommand, Result>,
        IRequestHandler<ConfirmNewCustomerAsRefferalCommand,Result>,
        IRequestHandler<CustomerLoginCommand, Result>,
        IRequestHandler<VerifyingRegisteredCustomerWithCode,Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        private readonly ISwtTokenService _swtservice;

        public CustomerCommandHandler(ICustomerRepository customerRepository
            , IMediator mediator,
            ISwtTokenService swtservice)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
            _swtservice = swtservice;
        }

        public async Task<Result> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };

            GenderType Gender=GenderType.Female;
            if (request.Gender == "male") Gender = GenderType.Male;
            if (request.Gender == "female") Gender = GenderType.Female;

            var customer = await _customerRepository.GetByMobileOrEmail(request.MobileNumber, request.Email);

            if (customer != null && customer.LastStatus.Status==Status.RegisterationSucceed)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Mobile or email exist" + Guid.NewGuid().ToString(), "Another User with this Mobilenumber or Email Registered Before"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer != null && customer.LastStatus.Status == Status.DisabledBySecurityReason)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Disable by security reason " + Guid.NewGuid().ToString(), "Your account blocked because of security reason"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer != null && customer.LastStatus.Status == Status.DisabledByAdmin)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Disable by admin " + Guid.NewGuid().ToString(), "Your account blocked by admin"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer == null)
            {
                customer = new Customer(Guid.NewGuid().ToString(), request.FullName,
              request.Email, request.MobileNumber, request.BirthDate, Gender
              , new Random().Next(1000, 9999).ToString(), request.FirebaseToken);

                await _customerRepository.Add(customer);
            }

            await _mediator.Publish(new SMSRequestedEvent("Your Code is " + customer.LastCode.VerifyCode, customer.MobileNumber, customer.ID));

            return new Result();
        }

        public async Task<Result> Handle(VerifyingNewCustomerWithCodeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };

            var customer= await _customerRepository.GetByMobile(request.MobileNumber);

            if (customer == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("User not found " + Guid.NewGuid().ToString(), "User Not Found"));
                return new Result() { FailedResults = request.ValidationResult };
            }
            if (customer.LastStatus.Status == Status.DisabledBySecurityReason ||
                customer.LastStatus.Status == Status.DisabledByAdmin) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("User disabled " + Guid.NewGuid().ToString(), "Your account blocked"));
                return new Result() { FailedResults = request.ValidationResult };
            }
            if (!customer.LastCode.IsValid(request.Code))
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Code Expired" + Guid.NewGuid().ToString(), "Your Code Expired Please Try Again"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.RefferalInformationList.Where(x => x.AcceptingStatus
            == RefferalStatus.Rejected && 
            x.RefferalCustomerMobileNumber==request.RefferalInformation.RefferalMobileNumber).ToList().Count!=0) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Customer Reject before" + Guid.NewGuid().ToString(), "This customer" +
                    " rejected you before and you cannot access to request again"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.RefferalInformationList.Where(x => x.AcceptingStatus ==
             RefferalStatus.Waiting && x.RefferalCustomerMobileNumber ==
             request.RefferalInformation.RefferalMobileNumber).ToList().Count >=5) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Customer more than" +
                    " 5 times request" + Guid.NewGuid().ToString(), "You send " +
                  " request to this customer more than 5 times and you cannot access to send new request"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.RefferalInformationList.Where(x => x.AcceptingStatus == RefferalStatus.Rejected).ToList()
                .Count >= 5) 
            {
                customer.DisabledBySecurityReasonStatus();
                await _customerRepository.Update(customer);

                request.ValidationResult.Errors.Add(new ValidationFailure("5 times" +
                    " rejected" + Guid.NewGuid().ToString(), " You try to send " +
                " request to refferals for 5 times and you cannot access to send new request"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            var refferal = await _customerRepository.GetByMobile(request.RefferalInformation.RefferalMobileNumber);
            if (refferal == null) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Refferal" +
                    " mobile number not found " + Guid.NewGuid().ToString(), "Refferal mobilenumber not found"));
                return new Result() { FailedResults = request.ValidationResult };
            }
            if (refferal.InvitationCustomersList.Where(x=>x.AcceptingStatus==RefferalStatus.Accepted)
                .ToList().Count>=5) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Refferal" +
                    " limitation finished " + Guid.NewGuid().ToString(), "Refferal" +
                    " invitation limitation finished"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            customer.AddRefferal(new Models.RefferalInformation(refferal.MobileNumber,refferal.ID,RefferalStatus.Waiting));
            refferal.AddInvitation(new Invitation(customer.ID, customer.FullName));
            await _customerRepository.Update(customer);
            await _customerRepository.Update(refferal);

            await _mediator.Publish(new PushRequestedEvent("Test",
                "Test",refferal.FirebaseToken,refferal.ID,"Test","{notification}"));

            return new Result() { SucceedResult = customer };
        }

        public Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Handle(ConfirmNewCustomerAsRefferalCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };
            var customer= await _customerRepository.GetById(request.ID);
            if (customer == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Customer Not Found " + Guid.NewGuid().ToString(), "Customer Not Found"));
                return new Result() { FailedResults = request.ValidationResult };
            }
            var refferal = await _customerRepository.GetById(request.RefferalInformation.RefferalCustomerID);
            if (refferal == null) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Refferal Not Found " + Guid.NewGuid().ToString(), "Refferal with this ID Not Found"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            Invitation AcceptThisCustomer = new Invitation(customer.ID, customer.FullName);
            AcceptThisCustomer.Accepted();
            refferal.UpdateInvitation(AcceptThisCustomer);
            customer.UpdateRefferal(new Models.RefferalInformation(customer.MobileNumber, request.RefferalInformation.RefferalCustomerID, RefferalStatus.Accepted));
            customer.UpdateMobileToken(_swtservice.JwtGenerator(customer.ID));
            await _customerRepository.Update(customer);
            await _customerRepository.Update(refferal);

            await _mediator.Publish(new PushRequestedEvent("Refferal Accept You", "We have good news for you", customer.FirebaseToken, customer.ID, "Test", "{notification}"));
            return new Result();
        }

        public async Task<Result> Handle(CustomerLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };

            var customer = await _customerRepository.GetByMobile(request.MobileNumber);

            if (customer == null)
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("customer not found " + Guid.NewGuid().ToString(), "Customer with mobile number not found"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.LastStatus.Status == Status.DisabledByAdmin ||
                customer.LastStatus.Status == Status.DisabledBySecurityReason) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("customer disabled " + Guid.NewGuid().ToString(), "Customer disabled because of security reason"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.LastStatus.Status == Status.DeletedWithUserRequest) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("customer deleted with user request " + Guid.NewGuid().ToString(), "This account deleted by yourself"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.LastStatus.Status == Status.RegisterationSucceed) 
            {
                customer.UpdateCode(new Random().Next(1000, 9999).ToString());
                await _customerRepository.Update(customer);
                await _mediator.Publish(new SMSRequestedEvent("Your code is " + customer.LastCode.VerifyCode, customer.MobileNumber, customer.ID));
                return new Result();
            }

            request.ValidationResult.Errors.Add(new ValidationFailure("other problem " + Guid.NewGuid().ToString(), "This account has problem"));
            return new Result() { FailedResults = request.ValidationResult };
        }

        public async Task<Result> Handle(VerifyingRegisteredCustomerWithCode request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return new Result() { FailedResults = request.ValidationResult };
            var customer = await _customerRepository.GetByMobile(request.MobileNumber);

            if (customer == null) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("User not" +
                   " found" + Guid.NewGuid().ToString(), "User not found"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (!customer.LastCode.IsValid(request.Code)) 
            {
                request.ValidationResult.Errors.Add(new ValidationFailure("Code not" +
                " true" + Guid.NewGuid().ToString(), "Your code is wrong"));
                return new Result() { FailedResults = request.ValidationResult };
            }

            if (customer.LastStatus.Status == Status.RegisterationSucceed) 
            {
                customer.UpdateMobileToken(_swtservice.JwtGenerator(customer.ID));
                await _customerRepository.Update(customer);
                return new Result() { SucceedResult = customer };
            }

            request.ValidationResult.Errors.Add(new ValidationFailure("Another Problem"
                + Guid.NewGuid().ToString(), "Your account has problem"));
            return new Result() { FailedResults = request.ValidationResult };
        }
    }
}