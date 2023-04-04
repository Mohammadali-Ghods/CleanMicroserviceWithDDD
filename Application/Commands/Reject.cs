using Application.Base.ViewModels;
using BaseDomain;
using Domain.Interfaces;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class Reject : IRequestHandler<RejectRequest, ResultModel>
    {
        protected readonly IUserFriendsRepository _userFriendsRepository;
        protected readonly IMediator _mediator;
        public Reject(IUserFriendsRepository userFriendsRepository, IMediator mediator)
        {
            _userFriendsRepository = userFriendsRepository;
            _mediator = mediator;
        }
        public async Task<ResultModel> Handle(RejectRequest request, CancellationToken cancellationToken)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.FailedResults = new ValidationResult();

            var from = await _userFriendsRepository.GetById(request.GetUser());
            var to = await _userFriendsRepository.GetById(request.UserID);

            try
            {
                from.SendReject(request.UserID);
                to.ReceiveReject(request.GetUser());
            }
            catch (BusinessRuleValidationException ex)
            {
                resultModel.FailedResults.Errors.Add(new ValidationFailure(ex.GetType().ToString(),
                  ex.Message));
                return resultModel;
            }

            await _userFriendsRepository.Update(from);
            await _userFriendsRepository.Update(to);

            if (from.DomainEvents != null)
                foreach (var eachevent in from.DomainEvents)
                {
                    await _mediator.Publish(eachevent);
                }
            if (to.DomainEvents != null)
                foreach (var eachevent in to.DomainEvents)
                {
                    await _mediator.Publish(eachevent);
                }

            return resultModel;
        }
    }
}
