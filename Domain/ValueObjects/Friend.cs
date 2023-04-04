using BaseDomain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Friend : ValueObject
    {
        public string UserID { get; private set; }
        public List<Status> Statuses { get; private set; }
        public Friend(string userid, StatusList status)
        {
            UserID = userid;
            Statuses = new List<Status>() { new Status(status) };
        }
        public void AddStatus(StatusList status) => Statuses.Add(new Status(status));
    }
}
