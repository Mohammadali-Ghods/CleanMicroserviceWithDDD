using Application.Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetStatusBetweenUsersRequest:BaseViewModel<string>
    {
        public string UserID { get; set; }
    }
}
