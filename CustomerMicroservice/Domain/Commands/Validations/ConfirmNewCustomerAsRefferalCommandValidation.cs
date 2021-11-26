using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.Validations
{
    class ConfirmNewCustomerAsRefferalCommandValidation: CustomerValidation<ConfirmNewCustomerAsRefferalCommand>
    {
        public ConfirmNewCustomerAsRefferalCommandValidation()
        {
            ValidateRefferalID();
        }
    }
}
