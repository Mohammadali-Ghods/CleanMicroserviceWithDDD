using Bus.ConfigurationModel;
using Domain.Events;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Bus
{
    public class RabbitMQMessageHandler : IMessageBus
    {
        private readonly IBus _bus;
        private readonly RabbitMQModel _rabbitmqdata;
        public RabbitMQMessageHandler(IBus bus, IOptionsMonitor<RabbitMQModel> optionsMonitor)
        {
            _bus = bus;
            _rabbitmqdata = optionsMonitor.CurrentValue;
        }
        public async Task SendCustomerRegisteredEvent(CustomerRegisteredEvent _customerregisteredevent)
        {
            Uri uri = new Uri(_rabbitmqdata.Address+ "/CustomerRegisteredEvent");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send<CustomerRegisteredEvent>(_customerregisteredevent);
        }

        public async Task SendCustomerRemovedEvent(CustomerRemovedEvent _customerremovedevent)
        {
            Uri uri = new Uri(_rabbitmqdata.Address + "/CustomerRemovedEvent");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send<CustomerRemovedEvent>(_customerremovedevent);
        }

        public async Task SendCustomerUpdatedEvent(CustomerUpdatedEvent _customerupdatedevent)
        {
            Uri uri = new Uri(_rabbitmqdata.Address + "/CustomerUpdatedEvent");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send<CustomerUpdatedEvent>(_customerupdatedevent);
        }

        public async Task SendPushRequestedEvent(PushRequestedEvent _smsrequestedevent)
        {
            Uri uri = new Uri(_rabbitmqdata.Address + "/PushRequestedEvent");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send<PushRequestedEvent>(_smsrequestedevent);
        }

        public async Task SendSMSRequestedEvent(SMSRequestedEvent _smsrequestedevent)
        {
            Uri uri = new Uri(_rabbitmqdata.Address + "/SMSRequestedEvent");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send<SMSRequestedEvent>(_smsrequestedevent);
        }
    }
}
