using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class VerifyCodeViewModel
    {
        public string MobileNumber { get; set; }
        public string Code { get; set; }
        public RefferalInformation RefferalInformation { get; set; }
    }
}
