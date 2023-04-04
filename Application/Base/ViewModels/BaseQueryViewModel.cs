using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.ViewModels
{
    public class BaseQueryViewModel
    {
        public List<BriefUserViewModel> Users { get; set; }
    }
    public class BriefUserViewModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public UserImage Image { get; set; }
        public DateTime Date { get; set; }
    }
    public class UserImage
    {
        public string ImageAddress { get; set; }
        public string ImageTitle { get; set; }
        public bool Default { get; set; }
    }
}
