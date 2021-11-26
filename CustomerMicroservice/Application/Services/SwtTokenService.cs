using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SwtTokenService:ISwtTokenService
    {
        private readonly SecretKeyModel _secretkeydata;
        public SwtTokenService(IOptionsMonitor<SecretKeyModel> optionsMonitor) 
        {
            _secretkeydata = optionsMonitor.CurrentValue;
        }
        public string JwtGenerator(string customerid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customerid),
                    new Claim(ClaimTypes.Role, "customer")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretkeydata.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

    public class SecretKeyModel 
    {
        public string Secret { get; set; }
    }
}
