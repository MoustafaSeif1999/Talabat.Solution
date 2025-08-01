﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositries;
using Talabat.Repository.Data;
using Talabat.Repository;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using System.Linq;
using Talabat.Core.Services;
using Talabat.Services;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton( typeof ( IResponseCacheService ) , typeof ( ResponseCacheService ) );

            services.AddScoped( typeof (IPaymentServices), typeof (PaymentService) );

            services.AddScoped( typeof(IUnitOfWork) , typeof(UnitOfWork) );

            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(ITokenService), typeof(TokinService));

            services.AddScoped( typeof(IBasketRepository) , typeof(BasketRepository));

            services.AddScoped(typeof(IGenericReopositiory<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfiles));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                          .SelectMany(S => S.Value.Errors)
                                                          .Select(F => F.ErrorMessage).ToArray();
                    var Final_Message = new ApiValidationErrorResponse() { Errors = errors };

                    return new BadRequestObjectResult(Final_Message);
                };
            }


            );


            return services;
        }

    }
}
