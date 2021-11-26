using Domain.Commands.Validations;

namespace Domain.Commands
{
    public class ConfirmNewCustomerAsRefferalCommand:CustomerCommand
    {
        public ConfirmNewCustomerAsRefferalCommand(string refferalid,string customerid)
        {
            RefferalInformation = new RefferalInformation();
            RefferalInformation.RefferalCustomerID = refferalid;
            ID = customerid;
        }

        public bool IsValid()
        {
            ValidationResult = new ConfirmNewCustomerAsRefferalCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
