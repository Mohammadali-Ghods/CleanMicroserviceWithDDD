using BaseDomain;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    internal class ThisUserAlsoHaveRequestThatUserWaitingForRule : IBusinessRule
    {
        private readonly Friend? _friend;

        public ThisUserAlsoHaveRequestThatUserWaitingForRule(Friend? friend)
        {
            _friend = friend;
        }

        public bool IsBroken()
        {
            if (_friend.Statuses[_friend.Statuses.Count - 1].Action == StatusList.RequestReceived)
                return false;
            return true;
        }

        public string Message => "This user is not waiting for your response!!";
    }
}
