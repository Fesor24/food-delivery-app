using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbSeed
    {
        public static async Task SeedUser(UserManager<ApplicationUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = "fesordev@mail.com",
                    UserName = "fesordev@mail.com",
                    Address = new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        City = "New York",
                        State = "NY",
                        Street = "9 Templar street",

                    }
                };

                await userManager.CreateAsync(user, "passw0rd");
            }
        }
    }
}
