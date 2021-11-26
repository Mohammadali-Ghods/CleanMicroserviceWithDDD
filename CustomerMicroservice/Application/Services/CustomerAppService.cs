using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Commands;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        private readonly ISwtTokenService _swtservice;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediator mediator,
                                  ISwtTokenService swtservice)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _mediator = mediator;
            _swtservice = swtservice;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAll());
        }

        public async Task<CustomerViewModel> GetById(string id)
        {
            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetById(id.ToString()));
        }

        public async Task<ResultModel> Register(RegisterViewModel registerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(registerViewModel);
            var DomainModel= await _mediator.Send(registerCommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);
            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }

        public async Task<ResultModel> VerifyingCustomerWithCode(VerifyCodeViewModel verifyCodeViewModel)
        {
            var verifyingCommand = _mapper.Map<VerifyingNewCustomerWithCodeCommand>(verifyCodeViewModel);
            var DomainModel = await _mediator.Send(verifyingCommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);
            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }
        public async Task<ResultModel> ConfirmCustomerAsRefferal(ConfirmAsRefferalViewModel confirmAsRefferalViewModel) 
        {
            var confirmcustomercommand = _mapper.Map<ConfirmNewCustomerAsRefferalCommand>(confirmAsRefferalViewModel);
            var DomainModel = await _mediator.Send(confirmcustomercommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);
            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }

        public async Task<ResultModel> Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            var DomainModel = await _mediator.Send(updateCommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);
            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }

        public async Task<ResultModel> Remove(string id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            var DomainModel = await _mediator.Send(removeCommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);
            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ResultModel> Login(LoginViewModel customerViewModel)
        {
            var loginCommand = new CustomerLoginCommand(customerViewModel.MobileNumber,"Nothing");
            var DomainModel = await _mediator.Send(loginCommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);
            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }

        public async Task<ResultModel> VerifyingRegisteredCustomerWithCode(VerifyCodeViewModel customerViewModel)
        {
            var VerifyingRegisteredCustomerCommand = new VerifyingRegisteredCustomerWithCode(customerViewModel.MobileNumber, customerViewModel.Code);
            var DomainModel = await _mediator.Send(VerifyingRegisteredCustomerCommand);
            var ViewModel = _mapper.Map<CustomerViewModel>(DomainModel.SucceedResult);

            return new ResultModel() { FailedResults = DomainModel.FailedResults, SucceedResult = ViewModel };
        }
    }
}
