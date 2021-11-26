using System;
using MediatR;

namespace Domain.Events
{
    public class CustomerRemovedEvent : INotification
    {
        public CustomerRemovedEvent(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}