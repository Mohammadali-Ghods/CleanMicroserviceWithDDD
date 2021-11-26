using MediatR;
using System;

namespace Domain.Events
{
    public class CustomerRegisteredEvent : INotification
    {
        public CustomerRegisteredEvent(string id, string fullname,
            string email,string mobilenumber, DateTime birthDate,string code)
        {
            ID = id;
            FullName = fullname;
            Email = email;
            BirthDate = birthDate;
            Code = code;
        }
        public string ID { get; set; }
        public string FullName { get; private set; }
        public string Code { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string MobileNumber { get; private set; }
    }
}