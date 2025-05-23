using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Services;

namespace Talabat.APIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CachingService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedData = await CachingService.GetCacheResponseAsync(cacheKey);


            if (!string.IsNullOrEmpty(cachedData))
            {
                var responseData = new ContentResult()
                {
                    Content = cachedData,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = responseData;
                return;
            }


            var responseAfterEndPoint = await next.Invoke();

            if (responseAfterEndPoint.Result is OkObjectResult okObjectResult)
            {
                await CachingService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }




        }


        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();

            KeyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy( item => item.Key ))
            {
                KeyBuilder.Append($"|{key}-{value}");
            }

            return KeyBuilder.ToString();
        }
    }
}
