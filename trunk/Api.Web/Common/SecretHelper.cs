using Api.Common;
using Api.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Entity;

namespace Api.Web.Common
{
    public class SecretHelper
    {
        public static ResultModel CheckSign(SortedList<string, string> dic, string sign)
        {
            if (dic == null)
            {
                new CustomException("字典不能为空");
            }

            long timestamp = Utils.ObjToLong(dic["timestamp"]);
            string appKey = Utils.ObjToStr(dic["appKey"]);
            //判断是否过期
            if (new DateTime(timestamp).AddSeconds(ConstValues.ApiValidSeconds) < DateTime.Now)
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

        public static string GetSecretTokenByKey(string key)
        {
            //先更新
            //AppId SecretToken ExpiredTime
            //唯一标识    私钥(更新频率比较高) 有效时间（有效期比较短）
            //再返回

            return string.Empty;
        }
    }
}