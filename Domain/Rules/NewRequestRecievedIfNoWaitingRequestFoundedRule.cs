using BaseDomain;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public class NewRequestRecievedIfNoWaitingRequestFoundedRule : IBusinessRule
    {
        private readonly Friend? _friend;

        public NewRequestRecievedIfNoWaitingRequestFoundedRule(Friend? friend)
        {
            _friend = friend;
        }

        public bool IsBroken()
        {
            if (_friend.Statuses[_friend.Statuses.Count - 1].Action == StatusList.RequestReceived)
                return true;
            return false;
        }

        public string Message => "This user send request before and it's not possible to send it again!";
    }
}
