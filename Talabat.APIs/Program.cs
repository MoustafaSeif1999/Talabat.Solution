using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Talabat.APIs.Extensions;
using Talabat.APIs.MiddleWare;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {

        public static async Task Main(string[] args)
        {


            #region Create cestrel

            var builder = WebApplication.CreateBuilder(args);

            #endregion


            #region Services Configration

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            }
            );

            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddAppServices();
            builder.Services.AddSwaggerServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            #endregion


            #region Build Cestrel

            var app = builder.Build();

            #endregion


            #region Applay_Migrations & Data Seeding

            var Services = app.Services ;

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                var Context = Services.GetRequiredService<StoreContext>();
                await Context.Database.MigrateAsync();

                var IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(Context, LoggerFactory);

                var userManager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, ex.Message);
            }

            #endregion


            #region Configure HTTP request Pipelines

            app.UseMiddleware<ExceptionMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #endregion


            #region Run Cestrel

            app.Run(); 
            #endregion



        }

    }
}
