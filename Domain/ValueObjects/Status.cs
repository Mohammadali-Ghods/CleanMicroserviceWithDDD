using BaseDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Status : ValueObject
    {
        public DateTime? ActionDate { get; private set; }
        public StatusList Action { get; private set; }
        public Status(StatusList action)
        {
            Action = action;
            ActionDate = DateTime.Now;
        }
    }

}
