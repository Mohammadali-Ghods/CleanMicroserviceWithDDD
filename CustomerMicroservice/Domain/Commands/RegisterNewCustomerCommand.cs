using Domain.Commands.Validations;
using System;
using FluentValidation.Results;

namespace Domain.Commands
{
    public class RegisterNewCustomerCommand : CustomerCommand
    {
        public RegisterNewCustomerCommand(string fullname, string email,string mobilenumber,
            DateTime birthDate,string firebasetoken)
        {
            FullName = fullname;
            Email = email;
            BirthDate = birthDate;
            MobileNumber = mobilenumber;
            FirebaseToken = firebasetoken;
        }

        public bool IsValid()
        {
            ValidationResult = new RegisterNewCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}