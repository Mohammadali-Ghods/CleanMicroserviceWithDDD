using Domain.Commands.Validations;

namespace Domain.Commands
{
    public class VerifyingRegisteredCustomerWithCode:CustomerCommand
    {
        public VerifyingRegisteredCustomerWithCode(string mobilenumber, string code)
        {
            MobileNumber = mobilenumber;
            Code = code;
        }
        public bool IsValid()
        {
            ValidationResult = new VerifyingRegisteredCustomerWithCodeValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
