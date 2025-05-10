using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare> logger , IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex )
            {
                _logger.LogError(ex , ex.Message);


                context.Response.ContentType = "application/json";
                context.Response.StatusCode =  (int) HttpStatusCode.InternalServerError;


                var ExceptionMess = _env.IsDevelopment() ?
                    new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse(500);

                var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(ExceptionMess,option);


               await context.Response.WriteAsync(json);
            }
        }
    }
}
