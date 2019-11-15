using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Common.CacheHelper
{
  public  interface ICaching
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue);
    }
}
