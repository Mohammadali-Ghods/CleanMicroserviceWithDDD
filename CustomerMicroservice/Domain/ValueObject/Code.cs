using System;
using System.Collections.Generic;
namespace Domain.ValueObject
{
    public class Code
    {
        public Code(string verifycode) 
        {
            VerifyCode = verifycode;
            ExpireDate = DateTime.Now.AddMinutes(4);
            CreatedDate = DateTime.Now;
        }
        public string VerifyCode { get; private set; }
        private DateTime CreatedDate { get; set; }
        private DateTime ExpireDate { get; set; }

        public bool IsValid(string verifycode) 
        {
            if (DateTime.Now > ExpireDate) return false;
            if (verifycode != VerifyCode) return false;
            return true;
        }
    }
}
