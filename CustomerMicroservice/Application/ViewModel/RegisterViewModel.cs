using System;
using Domain.Commands;

namespace Application.ViewModel
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirebaseToken { get; set; }
    }
}
