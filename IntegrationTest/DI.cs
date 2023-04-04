using API;
using API.Configurations;
using API.Controllers;
using AutoFixture;
using AutoMapper;
using Bus.ConfigurationModel;
using Domain.Interfaces;
using ExternalApi.ConfigurationModel;
using FluentAssertions;
using FluentAssertions.Common;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class DI : IDisposable
    {
        protected IServiceProvider serviceProvider;
        protected IMapper _mapper;
        protected IMediator _mediator;

        protected IConfiguration Configuration { get; set; }
        protected void GlobalSetup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("AppSetting/appsettings.json", true, true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.Configure<RabbitMQModel>(Configuration.GetSection("RabbitMQ"));
            serviceCollection.Configure<ExretnalApiModel>(Configuration.GetSection("ExternalApi"));
            serviceCollection.Configure<ExternalApi.TokenService.SecretKeyModel>(Configuration.GetSection("Keys"));
            serviceCollection.AddDependencyInjectionConfiguration();
            serviceCollection.AddAutoMapperConfiguration();
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            serviceCollection.AddMemoryCache();
            serviceCollection.AddMassTransit(x =>
            {
                x.AddBus(provider => MassTransit.Bus.Factory.CreateUsingRabbitMq(confige =>
                {
                    confige.Host(new Uri("rabbitmq://194.163.169.59"), h =>
                    {
                        h.Username("admin");
                        h.Password("Aa@27437823");
                    });

                }));

            });

            serviceCollection.AddMassTransitHostedService();
            DB.InitAsync("Diffancy", "localhost", 27017);

            serviceProvider = serviceCollection.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _mediator = serviceProvider.GetService<IMediator>();
        }
        protected void IterationCleanup()
        {
            _mapper = null;
            _mediator = null;
        }

        protected void GlobalCleanup()
        {
            serviceProvider = null;
        }
        public void Dispose()
        {
            IterationCleanup();
            GlobalCleanup();
            GC.SuppressFinalize(this);
        }
    }
}
