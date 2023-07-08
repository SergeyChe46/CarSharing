using CarSharing.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace CarSharing.Services
{
    public interface ITokenCreationService
    {
        public AuthentificationResponse CreateToken(IdentityUser user);
    }
}
