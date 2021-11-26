
namespace Domain.Commands.Validations
{
    public class VerifyingRegisteredCustomerWithCodeValidation:CustomerValidation<VerifyingRegisteredCustomerWithCode>
    {
        public VerifyingRegisteredCustomerWithCodeValidation()
        {
            ValidateMobileNumber();
            ValidateCode();
        }
    }
}
