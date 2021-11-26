using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RefferalInformation
    {
        public RefferalInformation(string refferalmobilenumber,string refferalcustomerid
            ,RefferalStatus refferalstatus)
        {
            RefferalCustomerID = refferalcustomerid;
            RefferalCustomerMobileNumber = refferalmobilenumber;
            AcceptingStatus = refferalstatus;
        }

        public string RefferalCustomerID { get;private set; }
        public string RefferalCustomerMobileNumber { get;private set; }
        public RefferalStatus AcceptingStatus { get; private set; }

        public void Rejected() 
        {
            AcceptingStatus = RefferalStatus.Rejected;
        }
        public void Waiting()
        {
            AcceptingStatus = RefferalStatus.Waiting;
        }
        public void Accepted()
        {
            AcceptingStatus = RefferalStatus.Accepted;
        }
        public void Cancelled()
        {
            AcceptingStatus = RefferalStatus.Cancelled;
        }
    }

    public enum RefferalStatus 
    {
        Rejected,
        Waiting,
        Accepted,
        Cancelled
    }
}
