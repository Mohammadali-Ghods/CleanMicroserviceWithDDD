using Application.Interface;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetStatusBetweenUsers : IRequestHandler<GetStatusBetweenUsersRequest, string>
    {
        private readonly IUserFriendsRepository _userFriendsRepository;
        public GetStatusBetweenUsers(IUserFriendsRepository userFriendsRepository,
            IUserAPI userAPI)
        {
            _userFriendsRepository = userFriendsRepository;
        }
        public async Task<string?> Handle(GetStatusBetweenUsersRequest request, CancellationToken cancellationToken)
        {
            var result = await _userFriendsRepository.GetById(request.GetUser());
            if (result == null) return "NotFriend";

            var getrelation = result.Friends.Where(x => x.UserID == request.UserID).FirstOrDefault();
            if (getrelation == null) return "NotFriend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
              Domain.ValueObjects.StatusList.AcceptSent) return "Friend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
             Domain.ValueObjects.StatusList.AcceptReceived) return "Friend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
           Domain.ValueObjects.StatusList.CancelSent) return "NotFriend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
          Domain.ValueObjects.StatusList.CancelReceived) return "NotFriend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
         Domain.ValueObjects.StatusList.RejectReceived) return "NotFriend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
        Domain.ValueObjects.StatusList.RejectSent) return "NotFriend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
      Domain.ValueObjects.StatusList.DisconnectReceived) return "NotFriend";

            if (getrelation.Statuses[getrelation.Statuses.Count - 1].Action ==
      Domain.ValueObjects.StatusList.DisconnectSent) return "NotFriend";

            return getrelation.Statuses[getrelation.Statuses.Count - 1].Action.ToString();
        }
    }
}
