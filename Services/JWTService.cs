using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using CarSharing.Entities.Identity;

namespace CarSharing.Services
{
    public class JWTService : ITokenCreationService
    {
        private readonly IConfiguration _configuration;
        private const int EXPIRATION_TIME = 1000;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthentificationResponse CreateToken(IdentityUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_TIME);

            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthentificationResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration
            };
        }

        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration)
        {
            return new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
        }
        private Claim[] CreateClaims(IdentityUser user)
        {
            return new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                ), SecurityAlgorithms.HmacSha256);
        }
    }
}
