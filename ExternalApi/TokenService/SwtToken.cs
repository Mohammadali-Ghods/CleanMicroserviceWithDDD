using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ExternalApi.TokenService
{
    public class SwtToken : ISwtToken
    {

        private readonly SecretKeyModel _secretkeydata;
        public SwtToken(IOptionsMonitor<SecretKeyModel> optionsMonitor)
        {
            _secretkeydata = optionsMonitor.CurrentValue;
        }

        public string JwtGenerator()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "MicroserviceRelatinUser"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("http://schemas.xmlsoap.org/ws/2009/09/identity/claims/accessigstores","")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                (Encoding.ASCII.GetBytes(_secretkeydata.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        public string GetUserId(ClaimsPrincipal principal)
        {
            return
                principal.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        }
    }
    public class SecretKeyModel
    {
        public string Secret { get; set; }
    }
}
