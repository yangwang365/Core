using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using CoreProject.Common.CacheHelper;

namespace CoreProject.API.AOP
{
    public class MemoryCacheAOP : CacheAOPBase
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private readonly ICaching _cache;
        public MemoryCacheAOP(ICaching cache)
        {
            _cache = cache;
        }

        public override void Intercept(IInvocation invocation)
        {
            #region 不加特性验证
            ////获取自定义缓存键
            //var cacheKey = CustomCacheKey(invocation);
            ////根据key获取相应的缓存值
            //var cacheValue = _cache.Get(cacheKey);
            //if (cacheValue != null)
            //{
            //    //将当前获取到的缓存值，赋值给当前执行方法
            //    invocation.ReturnValue = cacheValue;
            //    return;
            //}
            ////去执行当前的方法
            //invocation.Proceed();
            ////存入缓存
            //if (!string.IsNullOrWhiteSpace(cacheKey))
            //{
            //    _cache.Set(cacheKey, invocation.ReturnValue);
            //}
            #endregion
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法特性验证
            var qCachingAttribute = method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) as CachingAttribute;
            if (qCachingAttribute != null)
            {
                var cacheKey = CustomCacheKey(invocation);
                var cacheValue = _cache.Get(cacheKey);
                if (cacheValue!=null)
                {
                    invocation.ReturnValue = cacheValue;
                    return;
                }
                invocation.Proceed();
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    _cache.Set(cacheKey, invocation.ReturnValue);
                }
            }
        }
    }
}
