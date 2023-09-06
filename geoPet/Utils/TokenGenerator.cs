using geoPet.Constants;
using geoPet.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace geoPet.Utils
{
    public class TokenGenerator
    {
        public string Generate(Ower request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(request),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(3)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private ClaimsIdentity AddClaims(Ower owerRequest)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, owerRequest.Name));
            claims.AddClaim(new Claim(ClaimTypes.Email, owerRequest.Email));
            return claims;
        }
    }
}
