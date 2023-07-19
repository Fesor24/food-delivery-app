using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> GetUserFromClaimsPrincipalAsync(this UserManager<ApplicationUser> userManager, 
            ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);

            return await userManager.FindByEmailAsync(email);
        }
    }
}
