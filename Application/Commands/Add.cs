using Application.Base.ViewModels;
using BaseDomain;
using Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Application.Commands
{
    public class Add : IRequestHandler<AddRequest, ResultModel>
    {
        protected readonly IUserFriendsRepository _userFriendsRepository;
        protected readonly IMediator _mediator;
        public Add(IUserFriendsRepository userFriendsRepository, IMediator mediator)
        {
            _userFriendsRepository = userFriendsRepository;
            _mediator = mediator;
        }
        public async Task<ResultModel> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            ResultModel resultModel = new ResultModel();
            resultModel.FailedResults = new ValidationResult();

            var from = await _userFriendsRepository.GetById(request.GetUser());
            var to = await _userFriendsRepository.GetById(request.UserID);

            if (from == null)
            {
                from = new Domain.Entities.UserFriends(request.GetUser());
                await _userFriendsRepository.Insert(from);
            }
            if (to == null)
            {
                to = new Domain.Entities.UserFriends(request.UserID);
                await _userFriendsRepository.Insert(to);
            }

            try
            {
                from.SendRequest(request.UserID);
                to.ReceiveRequest(request.GetUser());
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
