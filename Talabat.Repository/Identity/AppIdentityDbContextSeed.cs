using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManger) 
        {
            if (!userManger.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "MazenGamal",
                    Email = "mezog39@gmail.com?",
                    UserName = "mezog39",
                    PhoneNumber = "01016403394"
                };
               await userManger.CreateAsync(User, "P@ssw0rd");
            }
        
        }

    }
}
