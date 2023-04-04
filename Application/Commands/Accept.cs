using Application.Base.ViewModels;
using BaseDomain;
using Domain.Interfaces;
using FluentValidation.Results;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class Accept : IRequestHandler<AcceptRequest, ResultModel>
    {
        private readonly IUserFriendsRepository _userFriendsRepository;
        private readonly IMediator _mediator;
        public Accept(IUserFriendsRepository userFriendsRepository, IMediator mediator)
        {
            _userFriendsRepository = userFriendsRepository;
            _mediator = mediator;
        }
        public async Task<ResultModel> Handle(AcceptRequest request, CancellationToken cancellationToken)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.FailedResults = new ValidationResult();

            var from = await _userFriendsRepository.GetById(request.GetUser());
            var to = await _userFriendsRepository.GetById(request.UserID);

            try
            {
                from.SendAccept(request.UserID);
                to.ReceiveAccept(request.GetUser());
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
