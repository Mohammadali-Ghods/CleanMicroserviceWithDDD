using Domain.Events;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageBus
    {
        Task SendCustomerRegisteredEvent(CustomerRegisteredEvent _customerregisteredevent);
        Task SendCustomerRemovedEvent(CustomerRemovedEvent _customerremovedevent);
        Task SendCustomerUpdatedEvent(CustomerUpdatedEvent _customerupdatedevent);
        Task SendSMSRequestedEvent(SMSRequestedEvent _smsrequestedevent);
        Task SendPushRequestedEvent(PushRequestedEvent _smsrequestedevent);
    }
}
