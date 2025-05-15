using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Identity;

namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {

        public static IServiceCollection AddIdentityServices( this IServiceCollection services , IConfiguration _Configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequiredLength = 12 ;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>();


            services.AddAuthentication( options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = _Configuration["JWT:ValidAudiance"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]))
                    };
                });    

            return services;
        }

    }
}
