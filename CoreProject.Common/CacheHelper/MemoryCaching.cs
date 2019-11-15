﻿using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Common.CacheHelper
{
    public class MemoryCaching : ICaching
    {
        private  IMemoryCache _cache;

        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
        }

       
        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(7200));
        }
    }
}
