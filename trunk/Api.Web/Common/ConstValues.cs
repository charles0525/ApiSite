using Api.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web.Common
{
    public class ConstValues
    {
        /// <summary>
        /// 记录页面发生异常 httpcode cookie key
        /// </summary>
        public static readonly string CookieKey_ErrorHttpCode = nameof(CookieKey_ErrorHttpCode);

        /// <summary>
        /// 历史开奖缓存Key
        /// </summary>
        public static readonly string CacheKey_HistoryLottery = nameof(CacheKey_HistoryLottery);

        /// <summary>
        /// 自定义路由规则配置
        /// </summary>
        public static readonly List<RouteItem> ListRouteItems = new List<RouteItem>() {
           new RouteItem() { PageTitle="页面/news/01", ControllerName="news", ActionName="01" ,Url="news/01"},
           new RouteItem() { PageTitle="页面/*11x5/kj", ControllerName="*11x5", ActionName="kj",Url="*11x5/kj" }
        };
    }
}