using Domain.Commands.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands
{
    public class CustomerLoginCommand: CustomerCommand
    {
        public CustomerLoginCommand(string mobilenumber, string firebasetoken)
        {
            MobileNumber = mobilenumber;
            FirebaseToken = firebasetoken;
        }

        public bool IsValid()
        {
            ValidationResult = new CustomerLoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
