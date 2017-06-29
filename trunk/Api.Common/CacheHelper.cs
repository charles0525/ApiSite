using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Api.Common
{
    public class CacheHelper
    {
        private static List<string> listCacheKeys = new List<string>();


        /// <summary>
        /// 清除所有缓存客户
        /// </summary>
        public static void ClearAllCache()
        {
            foreach (string key in listCacheKeys)
            {
                object obj = GetCacheObject(key);
                if (obj != null)
                    HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache(string key)
        {
            object obj = GetCacheObject(key);
            if (obj != null)
                HttpRuntime.Cache.Remove(key);
        }
        /// <summary>
        /// 保存对象缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheContent"></param>
        /// <param name="cacheTimeSec">如果为null，则使用默认值：5秒</param>
        public static void SaveObjectToCache(string key, object cacheContent, Int32? cacheTimeSec)
        {
            int cacheTime = 5;
            if (cacheTimeSec.HasValue)
            {
                if (cacheTimeSec.Value > 0)
                    cacheTime = cacheTimeSec.Value;
            }
            if (cacheContent != null)
            {
                HttpRuntime.Cache.Add(key, cacheContent, null, DateTime.Now.AddSeconds(cacheTime),
                  System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);

                if (!listCacheKeys.Contains(key))
                    listCacheKeys.Add(key);
            }
        }

        /// <summary>
        /// 找对象窗体缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCacheObject(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //创建缓存依赖项
            CacheDependency dep = new CacheDependency(fileName);
            //创建缓存
            HttpRuntime.Cache.Insert(key, obj, dep);
        }

        /// <summary>
        /// 创建缓存项过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void Insert(string key, object obj, int expires)
        {
            //HttpRuntime.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
            HttpRuntime.Cache.Insert(key, obj, null, DateTime.Now.AddMinutes(expires), Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>object对象</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <typeparam name="T">T对象</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            object obj = Get(key);
            return obj == null ? default(T) : (T)obj;
        }
    }
}
