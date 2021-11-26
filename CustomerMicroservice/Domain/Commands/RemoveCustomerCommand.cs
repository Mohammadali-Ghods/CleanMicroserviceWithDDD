using Domain.Commands.Validations;
using FluentValidation.Results;

namespace Domain.Commands
{
    public class RemoveCustomerCommand : CustomerCommand
    {
        public RemoveCustomerCommand(string id)
        {
            ID = id;
        }
        public bool IsValid()
        {
            ValidationResult = new RemoveCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}