using Application.Base;
using Application.Base.ViewModels;
using Application.Commands;
using Application.Interface;
using Application.Queries;
using Bus;
using Data.Repository;
using Domain.Events;
using Domain.Interfaces;
using ExternalApi.Api;
using ExternalApi.TokenService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IRequestHandler<AcceptRequest, ResultModel>, Accept>();
            services.AddScoped<IRequestHandler<AddRequest, ResultModel>, Add>();
            services.AddScoped<IRequestHandler<RejectRequest, ResultModel>, Reject>();
            services.AddScoped<IRequestHandler<CancelRequest, ResultModel>, Cancel>();
            services.AddScoped<IRequestHandler<DisconnectRequest, ResultModel>, Disconnect>();
            services.AddScoped<IRequestHandler<GetSentByUserRequest, GetSentByUserResponse>, GetSentByUser>();
            services.AddScoped<IRequestHandler<GetReceivedByUserRequest, GetReceivedByUserResponse>, GetReceivedByUser>();
            services.AddScoped<IRequestHandler<GetFriendByUserRequest, GetFriendsByUserResponse>, GetFriendsByUser>();
            services.AddScoped<IRequestHandler<GetStatusBetweenUsersRequest, string>, GetStatusBetweenUsers>();


            //Events
            services.AddScoped<INotificationHandler<SentRequestDomainEvent>, RabbitMQMessageHandler<SentRequestDomainEvent>>();
            services.AddScoped<INotificationHandler<SentAcceptDomainEvent>, RabbitMQMessageHandler<SentAcceptDomainEvent>>();
            services.AddScoped<INotificationHandler<SentCancelDomainEvent>, RabbitMQMessageHandler<SentCancelDomainEvent>>();
            services.AddScoped<INotificationHandler<SentRejectDomainEvent>, RabbitMQMessageHandler<SentRejectDomainEvent>>();

            // Data
            services.AddScoped<IUserFriendsRepository, UserFriendsRepository>();

            // ExternalAPI
            services.AddScoped<IUserAPI, UserAPI>();
            services.AddScoped<ISwtToken, SwtToken>();
        }
    }
}