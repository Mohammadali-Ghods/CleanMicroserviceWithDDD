using System;
using MediatR;

namespace Domain.Events
{
    public class CustomerUpdatedEvent : INotification
    {
        public CustomerUpdatedEvent(string id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
        public string Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}