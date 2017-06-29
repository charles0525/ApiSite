using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Web.Common;
using Api.Common;

namespace Api.Web.Areas.Demo.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// 完全开放
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUser()
        {
            var user = GetUserObj();
            return user.ToHttpRspMsgSuccess();
        }

        /// <summary>
        /// （1）接口参数加密(基础加密)
        /// 通过签名匹配校验
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUserBySign(string mobile, string appKey, string sign)
        {
            var dic = new SortedList<string, string>();
            dic.Add("mobile", mobile);
            dic.Add("appKey", appKey);
            var currentSign = SecurifyHelper.CreateSign(dic, appKey);
            if (currentSign != sign)
            {
                return ObjectExtends.ToHttpRspMsgError("非法调用");
            }

            var user = GetUserObj();
            return user.ToHttpRspMsgSuccess();
        }

        /// <summary>
        /// （2）通过以上方式+时效性
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="ticks"></param>
        /// <param name="appKey"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetUserByTicks(string mobile, long ticks, string appKey, string sign)
        {
            var dic = new SortedList<string, string>();
            dic.Add("mobile", mobile);
            dic.Add("ticks", ticks.ToString());
            dic.Add("appKey", appKey);
            var currentSign = SecurifyHelper.CreateSign(dic, appKey);
            //判断签名是否一致
            if (currentSign != sign)
            {
                return ObjectExtends.ToHttpRspMsgError("非法请求");
            }
            //判断是否过期,30s有效期
            if (new DateTime(ticks).AddSeconds(30) < DateTime.Now)
            {
                return ObjectExtends.ToHttpRspMsgError("无效请求");
            }

            var user = GetUserObj();
            return user.ToHttpRspMsgSuccess();
        }

        /// <summary>
        /// （3）通过以上方式+私钥
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="ticks"></param>
        /// <param name="appKey"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public HttpResponseMessage GetUserBySecretKey(string token, long ticks, string appKey, string sign)
        {
            //判断是否过期,30s有效期
            if (new DateTime(ticks).AddSeconds(30) < DateTime.Now)
            {
                return ObjectExtends.ToHttpRspMsgError("无效请求");
            }
            var secretKey = GetSecretByKey(appKey);
            //判断签名是否一致
            var dic = new SortedList<string, string>();
            dic.Add("token", token);
            dic.Add("ticks", ticks.ToString());
            dic.Add("appKey", appKey);
            dic.Add("secretKey", secretKey);
            var currentSign = SecurifyHelper.CreateSign(dic, appKey);
            if (currentSign != sign)
            {
                return ObjectExtends.ToHttpRspMsgError("非法请求");
            }

            var user = GetUserObj();
            return user.ToHttpRspMsgSuccess();
        }

        static object GetUserObj()
        {
            return new
            {
                name = "charles",
                mobile = "15268596512",
                address = "Hangzhou ,Zhejiang province"
            };
        }

        static string GetSecretByKey(string key)
        {
            var dic = new Dictionary<string, string>()
            {
                {"charles","3333" },
                {"zhangfj","4444" }
            };
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            return string.Empty;
        }
    }
}
