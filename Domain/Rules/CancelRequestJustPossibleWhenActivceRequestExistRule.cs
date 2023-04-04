﻿using BaseDomain;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public class CancelRequestJustPossibleWhenActivceRequestExistRule : IBusinessRule
    {
        private readonly Friend? _friend;

        public CancelRequestJustPossibleWhenActivceRequestExistRule(Friend? friend)
        {
            _friend = friend;
        }

        public bool IsBroken()
        {
            if (_friend.Statuses[_friend.Statuses.Count - 1].Action == StatusList.RequestSent)
                return false;
            return true;
        }

        public string Message => "You send request to this user before and you are waiting for it!";
    }
}
