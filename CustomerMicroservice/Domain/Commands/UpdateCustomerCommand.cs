using Domain.Commands.Validations;
using System;

namespace Domain.Commands
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public UpdateCustomerCommand(string id, string name, string email, DateTime birthDate)
        {
            ID = id;
            FullName = name;
            Email = email;
            BirthDate = birthDate;
        }

        public bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}