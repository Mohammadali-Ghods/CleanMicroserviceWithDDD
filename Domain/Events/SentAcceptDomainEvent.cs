using BaseDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class SentAcceptDomainEvent : DomainEventBase
    {
        public SentAcceptDomainEvent(string fromuserid, string touserid, DateTime senddate)
        {
            FromUserID = fromuserid;
            ToUserID = touserid;
            SendDate = senddate;
        }
        public string? FromUserID { get; }
        public string? ToUserID { get; }
        public DateTime? SendDate { get; }
    }
}
