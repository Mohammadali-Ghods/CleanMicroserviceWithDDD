using Domain.Commands.Validations;
using System.Collections.Generic;

namespace Domain.Commands
{
    public class AddNewAddressCommand: CustomerCommand
    {
        public AddNewAddressCommand(List<Address> addresslist,string customerid) 
        {
            CustomerAddresses = addresslist;
            ID = customerid;
        }
        public bool IsValid()
        {
            ValidationResult = new AddNewAddressCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
