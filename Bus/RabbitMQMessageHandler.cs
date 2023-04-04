using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bus
{
    public class RabbitMQMessageHandler<T> : INotificationHandler<T>
        where T : INotification
    {
        private readonly IBus _bus;
        public RabbitMQMessageHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification);
        }
    }
}
