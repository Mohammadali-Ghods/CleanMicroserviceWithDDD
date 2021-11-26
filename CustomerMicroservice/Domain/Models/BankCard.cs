using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BankCard
    {
        public BankCard(string cardname, string cardnumber, string expiremonth, string expireyear)
        {
            ID = new Guid().ToString();
            CardName = cardname;
            CardNumber = cardnumber;
            ExpiredMonth = expiremonth;
            ExpireYear = expireyear;
            Enable = true;
        }

        public string ID { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpiredMonth { get; private set; }
        public string ExpireYear { get; private set; }
        public bool Enable { get; private set; }
    }
}
