using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.ViewModels
{
    public class BaseViewModel<T> : IRequest<T>
    {
        private string _userId;
        private string _role;
        private string _language;
        public void SetRole(string role) => _role = role;
        public void SetUser(string userId) => _userId = userId;
        public void SetLanguage(string language) => _language = language;
        public string GetRole() => _role;
        public string GetUser() => _userId;
        public string GetLanguage() => _language;
    }
}
