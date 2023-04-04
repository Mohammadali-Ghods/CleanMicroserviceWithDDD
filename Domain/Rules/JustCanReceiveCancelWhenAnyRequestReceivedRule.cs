using BaseDomain;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public class JustCanReceiveCancelWhenAnyRequestReceivedRule : IBusinessRule
    {
        private readonly Friend? _friend;

        public JustCanReceiveCancelWhenAnyRequestReceivedRule(Friend? friend)
        {
            _friend = friend;
        }

        public bool IsBroken()
        {
            if (_friend.Statuses[_friend.Statuses.Count - 1].Action == StatusList.RequestReceived)
                return false;
            return true;
        }

        public string Message => "You don't have any request from this user!";
    }
}
