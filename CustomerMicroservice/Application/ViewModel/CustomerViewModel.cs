using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class CustomerViewModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public RefferalInformation RefferalInformation { get; set; }
        public List<Address> CustomerAddresses { get; set; }
        public List<BankCard> CustomerBankCards { get; set; }
        public string Code { get; set; }
        public string FirebaseToken { get; set; }
        public string LastMobileToken { get; set; }
    }
}
