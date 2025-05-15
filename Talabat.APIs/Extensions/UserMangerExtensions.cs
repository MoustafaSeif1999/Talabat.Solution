using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserMangerExtensions
    {

        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> _userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U =>  U.Email == email);

            return user;
        }

    }
}
