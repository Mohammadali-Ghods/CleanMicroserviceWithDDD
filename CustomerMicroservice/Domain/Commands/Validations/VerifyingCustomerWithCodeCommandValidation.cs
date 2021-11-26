using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Validations
{
    class VerifyingCustomerWithCodeCommandValidation : CustomerValidation<VerifyingNewCustomerWithCodeCommand>
    {
        public VerifyingCustomerWithCodeCommandValidation() 
        {
            ValidateMobileNumber();
            ValidateCode();
            ValidateRefferalMobile();
        }
    }
}
