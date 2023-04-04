using API;
using API.Configurations;
using API.Controllers;
using Application.Commands;
using Application.Queries;
using AutoFixture;
using AutoMapper;
using Domain.Interfaces;
using ExternalApi.ConfigurationModel;
using FluentAssertions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class FriendsServiceTest : DI
    {
        public FriendsServiceTest()
        {
            GlobalSetup();
        }

        private string fromuser = "cab0bce2-ae37-4192-b7e4-cc648fbd9135";
        private string touser = "167041ee-89b3-49cb-90fe-33e6de91d2dc";

        [Fact]
        public async Task SendRequestTest()
        {
            var request = new AddRequest()
            {
                UserID = touser,
            };
            request.SetUser(fromuser);

            var result = await _mediator.Send(request);
        }

        [Fact]
        public async Task GetReceivedRequestTest()
        {
            var request = new GetReceivedByUserRequest();
            request.SetUser(touser);
            var result = await _mediator.Send(request);
        }

        [Fact]
        public async Task GetSentRequestTest()
        {
            var request = new GetSentByUserRequest();
            request.SetUser(fromuser);
            var result = await _mediator.Send(request);
        }

        [Fact]
        public async Task SendAcceptTest()
        {
            var request = new AcceptRequest()
            {
                UserID = fromuser,
            };
            request.SetUser(touser);

            var result = await _mediator.Send(request);
        }

        [Fact]
        public async Task GetFriendsTest()
        {
            var request = new GetFriendByUserRequest();
            request.SetUser(touser);
            var result = await _mediator.Send(request);
        }

    }

}
