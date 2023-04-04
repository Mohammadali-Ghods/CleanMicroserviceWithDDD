using Application.Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserAPI
    {
        public Task<BriefUserViewModel> GetUser(string userid);
    }
}
