using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
     public class AppIdentityDbContextSeed
    {

        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if ( !userManager.Users.Any() )
            {
                var user = new AppUser()
                {
                    DisplayName = "Moustafa Seif" ,
                    Email = "mostafamohammed2026@gmail.com",
                    UserName = "Moustafa.Mouhammed",
                    PhoneNumber = "01091518140"

                };

                 await userManager.CreateAsync(user,"P@ssw0rd");

                

                
            }
        }

    }
}
