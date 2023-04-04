using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.ViewModels
{
    public class BaseCommandViewModel<T> : BaseViewModel<T>
    {
        public string? UserID { get; set; }
    }
}
