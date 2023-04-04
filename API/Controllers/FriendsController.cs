using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class FriendsController : ApiController
    {
        private IMediator _mediator;
        public FriendsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Friend/SendRequest/{userid:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> SendRequest(string userid)
        {
            var request = new AddRequest() { UserID = userid };
            request.SetUser(User.Identity.Name);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpPost]
        [Route("Friend/DisconnectRequest/{userid:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DisconnectRequest(string userid)
        {
            var request = new DisconnectRequest() { UserID = userid };
            request.SetUser(User.Identity.Name);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpPost]
        [Route("Friend/AcceptRequest/{userid:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AcceptRequest(string userid)
        {
            var request = new AcceptRequest() { UserID = userid };
            request.SetUser(User.Identity.Name);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpPost]
        [Route("Friend/RejectRequest/{userid:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RejectRequest(string userid)
        {
            var request = new RejectRequest() { UserID = userid };
            request.SetUser(User.Identity.Name);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpPost]
        [Route("Friend/CancelRequest/{userid:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CancelRequest(string userid)
        {
            var request = new CancelRequest() { UserID = userid };
            request.SetUser(User.Identity.Name);
            return CustomResponse(await _mediator.Send(request));
        }

        [HttpGet]
        [Route("Friend/GetReceivedRequests")]
        [Authorize(Roles = "Customer")]
        public async Task<GetReceivedByUserResponse> GetReceivedRequests()
        {
            var request = new GetReceivedByUserRequest();
            request.SetUser(User.Identity.Name);
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("Friend/GetSentRequests")]
        [Authorize(Roles = "Customer")]
        public async Task<GetSentByUserResponse> GetSentRequests()
        {
            var request = new GetSentByUserRequest();
            request.SetUser(User.Identity.Name);
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("Friend/GetFriends")]
        [Authorize(Roles = "Customer")]
        public async Task<GetFriendsByUserResponse> GetFriends()
        {
            var request = new GetFriendByUserRequest();
            request.SetUser(User.Identity.Name);
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("Friend/Status/{userid:guid}")]
        [Authorize(Roles = "Customer")]
        public async Task<string> Status(string userid)
        {
            var request = new GetStatusBetweenUsersRequest() { UserID = userid };
            request.SetUser(User.Identity.Name);
            return await _mediator.Send(request);
        }

    }
}
