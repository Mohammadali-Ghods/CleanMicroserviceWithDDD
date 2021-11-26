
namespace Domain.Commands.Validations
{
    public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
            ValidateMobileNumber();
            ValidateGenderString();
            ValidateFirebaseToken();
        }
    }
}