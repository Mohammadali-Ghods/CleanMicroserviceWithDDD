using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObject
{
    public class LastStatus
    {
        public LastStatus(Status status) 
        {
            Date = DateTime.Now;
            Status = status;
        }
        public DateTime Date { get; private set; }
        public Status Status { get; private set; }
    }
    public enum Status
    {
        WaitingForRefferal,
        RefferalRejected,
        WaitingForMobileVerification,
        RegisterationSucceed,
        RegisterationFailed,
        DisabledBySecurityReason,
        DisabledByAdmin,
        DeletedWithUserRequest,
        AutoLoggedOutForSecurityReason
    }
}
