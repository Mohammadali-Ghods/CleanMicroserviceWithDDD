using Domain.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Consumers
{
    public class CustomerRegisteredEventConsumer : IConsumer<CustomerRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<CustomerRegisteredEvent> context)
        {
            string A = context.Message.Email;
        }
    }
}
