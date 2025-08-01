﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync( string cacheKey, object response , TimeSpan timeSpan);
        

        Task<string> GetCacheResponseAsync( string cacheKey );

    }
}
