using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Common;
using System.Threading.Tasks;

namespace Api.Web.Common
{
    /// <summary>
    /// 历史彩票信息
    /// </summary>
    public class HistoryLotteryHelper
    {
        static readonly string configFile = System.Web.HttpContext.Current.Server.MapPath("~/ConfigFiles/Sd11x5.txt");

        static HistoryLotteryHelper()
        {

        }

        public static string ReadFileContent()
        {
            string content = Utils.ReadFile(configFile);
            return content;
        }

        public static void SetContentToCache(string data = "")
        {
            if (string.IsNullOrEmpty(data))
            {
                data = ReadFileContent();
            }
            //缓存1天，避免一直占用内存。
            CacheHelper.Insert(ConstValues.CacheKey_HistoryLottery, data, 60 * 24 * 1);
        }

        public static string GetContentFromCache()
        {
            var content = CacheHelper.Get<string>(ConstValues.CacheKey_HistoryLottery);
            if (string.IsNullOrEmpty(content))
            {
                content = ReadFileContent();

                //与主线程不相关的操作使用异步完成
                new Task(() =>
                {
                    SetContentToCache(content);
                }).Start();
            }
            return content;
        }
    }
}