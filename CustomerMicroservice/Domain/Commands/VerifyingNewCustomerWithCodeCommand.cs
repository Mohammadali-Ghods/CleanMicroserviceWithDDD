using Domain.Commands.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class VerifyingNewCustomerWithCodeCommand:CustomerCommand
    {
        public VerifyingNewCustomerWithCodeCommand(string mobilenumber,string code,
            string refferalcustomermobilenumber) 
        {
            MobileNumber = mobilenumber;
            Code = code;
            RefferalInformation = new RefferalInformation();
            RefferalInformation.RefferalMobileNumber = refferalcustomermobilenumber;
        }
        public bool IsValid()
        {
            ValidationResult = new VerifyingCustomerWithCodeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
