using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObject
{
    public class Invitation
    {
        public Invitation(string customerid,string customername) 
        {
            CustomerID = customerid;
            CreatedDate = DateTime.Now;
            AcceptingStatus = RefferalStatus.Waiting;
        }
        public string CustomerID { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CustomerName { get; private set; }
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
}
