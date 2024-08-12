using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using ProjektUrlopWeb.Models.Entities;
using ProjektUrlopWeb.TokenJwt;

namespace ProjektUrlopWeb.Token
{
    public class TokenService
    {
        private readonly TokenDto tokenDto;

        public TokenService(IOptions<TokenDto> jwtSettings)
        {
            tokenDto = jwtSettings.Value;
        }

        public string GenerateToken(Pracownik pracownik)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(tokenDto.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, pracownik.Id.ToString()),
                new Claim(ClaimTypes.Role, pracownik.IsAdmin ? "Admin" : "User"),
                new Claim(IdentityData.AdminUserClaimName, pracownik.IsAdmin.ToString().ToLower())
            }),
                Expires = DateTime.UtcNow.AddMinutes(tokenDto.ExpiresInMinutes),
                Issuer = tokenDto.Issuer,
                Audience = tokenDto.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
