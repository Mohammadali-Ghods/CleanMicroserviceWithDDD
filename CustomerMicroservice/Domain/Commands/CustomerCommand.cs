using MediatR;
using System;
using FluentValidation.Results;
using System.Collections.Generic;
using Domain.ResultModel;

namespace Domain.Commands
{
    public abstract class CustomerCommand : IRequest<Result>
    {
        public string ID { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Gender { get; protected set; }
        public string MobileNumber { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public RefferalInformation RefferalInformation { get; protected set; }
        public ValidationResult ValidationResult { get; set; }
        public List<Address> CustomerAddresses { get; protected set; }
        public List<BankCard> CustomerBankCards { get; protected set; }
        public string Code { get; protected set; }
        public string FirebaseToken { get; protected set; }
    }
    public class RefferalInformation 
    {
        public string RefferalCustomerID { get; set; }
        public string RefferalMobileNumber { get; set; }
    }
    public class Address 
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string AddressContent { get; set; }
        public string Floor { get; set; }
        public string Type { get; set; }
        public string AddressDirection { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public bool EnableStatus { get; set; }
    }
    public class BankCard 
    {
        public string ID { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiredMonth { get; set; }
        public string ExpireYear { get; set; }
        public bool Enable { get; set; }
    }
}