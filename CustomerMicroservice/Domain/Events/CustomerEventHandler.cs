using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CustomerEventHandler :
        INotificationHandler<CustomerRegisteredEvent>,
        INotificationHandler<CustomerUpdatedEvent>,
        INotificationHandler<CustomerRemovedEvent>,
        INotificationHandler<SMSRequestedEvent>,
        INotificationHandler<PushRequestedEvent>
    {
        IMessageBus _messagebus;
        public CustomerEventHandler(IMessageBus messagebus) 
        {
            _messagebus = messagebus;
        }
        public Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _messagebus.SendCustomerUpdatedEvent(notification);
            return Task.CompletedTask;
        }

        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _messagebus.SendCustomerRegisteredEvent(notification);
            return Task.CompletedTask;
        }

        public Task Handle(CustomerRemovedEvent notification, CancellationToken cancellationToken)
        {
            _messagebus.SendCustomerRemovedEvent(notification);
            return Task.CompletedTask;
        }

        public Task Handle(SMSRequestedEvent notification, CancellationToken cancellationToken)
        {
            _messagebus.SendSMSRequestedEvent(notification);
            return Task.CompletedTask;
        }

        public Task Handle(PushRequestedEvent notification, CancellationToken cancellationToken)
        {
            _messagebus.SendPushRequestedEvent(notification);
            return Task.CompletedTask;
        }
    }
}