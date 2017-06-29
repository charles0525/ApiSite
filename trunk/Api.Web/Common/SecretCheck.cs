using Api.Common;
using Api.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web.Common
{
    public class SecretCheck
    {
        public static ResultModel CheckSign(SortedList<string, string> dic, string sign)
        {
            long ticks = Utils.ObjToLong(dic["ticks"]);
            string appKey = Utils.ObjToStr(dic["appKey"]);
            //判断是否过期,30s有效期
            if (new DateTime(ticks).AddSeconds(30) < DateTime.Now)
            {
                return ObjectExtends.ReturnResult("无效请求", false);
            }
            var secretKey = GetSecretByKey(appKey);
            //判断签名是否一致
            dic.Add("secretKey", secretKey);
            var currentSign = SecurifyHelper.CreateSign(dic, appKey);
            if (currentSign != sign)
            {
                return ObjectExtends.ReturnResult("非法请求", false);
            }
            return ObjectExtends.ReturnResult("", true);
        }

        public static string GetSecretByKey(string key)
        {
            var dic = new Dictionary<string, string>()
            {
                {"charles","33" },
                {"zhangfj","44" }
            };
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            return string.Empty;
        }
    }
}