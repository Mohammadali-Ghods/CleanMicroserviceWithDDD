using Application.Interfaces;
using Application.Services;
using Domain.Commands;
using Domain.Events;
using Domain.Interfaces;
using Data.Repository;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Entities;
using Bus;
using MassTransit;
using System;
using Domain.ResultModel;

namespace IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<ISwtTokenService,SwtTokenService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<PushRequestedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<SMSRequestedEvent>, CustomerEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, Result>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, Result>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, Result>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<VerifyingNewCustomerWithCodeCommand, Result>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<ConfirmNewCustomerAsRefferalCommand, Result>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<CustomerLoginCommand, Result>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<VerifyingRegisteredCustomerWithCode, Result>, CustomerCommandHandler>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Infra - Bus
            services.AddScoped<IMessageBus,RabbitMQMessageHandler>();
        }
    }
}