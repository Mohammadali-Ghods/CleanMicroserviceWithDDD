using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public enum StatusList
    {
        RequestSent, AcceptSent, RejectSent, CancelSent, BlockSent, ReportSent
       , RequestReceived, AcceptReceived, RejectReceived, BlockReceived, ReportReceived, CancelReceived,
        DisconnectSent, DisconnectReceived
    }
}
