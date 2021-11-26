using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Validations
{
    class CustomerLoginCommandValidation:CustomerValidation<CustomerLoginCommand>
    {
        public CustomerLoginCommandValidation()
        {
            ValidateMobileNumber();
            ValidateFirebaseToken();
        }
    }
}
