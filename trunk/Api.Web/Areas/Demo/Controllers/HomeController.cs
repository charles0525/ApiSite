using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Web.Common;
using Api.Common;
using Api.Web.Models;

namespace Api.Web.Areas.Demo.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// 完全开放
        /// </summary>
        /// <returns></returns>
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
        /// <param name="timestamp"></param>
        /// <param name="appKey"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public HttpResponseMessage GetUserBytimestamp(string mobile, long timestamp, string appKey, string sign)
        {
            var dic = new SortedList<string, string>();
            dic.Add("mobile", mobile);
            dic.Add("timestamp", timestamp.ToString());
            dic.Add("appKey", appKey);
            var currentSign = SecurifyHelper.CreateSign(dic, appKey);
            //判断签名是否一致
            if (currentSign != sign)
            {
                return ObjectExtends.ToHttpRspMsgError("非法请求");
            }
            //判断是否过期,30s有效期
            if (new DateTime(timestamp).AddSeconds(30) < DateTime.Now)
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
        /// <param name="timestamp"></param>
        /// <param name="appKey"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public HttpResponseMessage GetUserBySecretKey(string token, long timestamp, string appKey, string sign)
        {
            var dic = new SortedList<string, string>();
            dic.Add("token", token);
            dic.Add("timestamp", timestamp.ToString());
            dic.Add("appKey", appKey);
            var chkResult = SecretHelper.CheckSign(dic, sign);
            if (!chkResult.Status)
            {
                return ObjectExtends.ToHttpRspMsgError(chkResult.Msg);
            }

            var user = GetUserObj();
            return user.ToHttpRspMsgSuccess();
        }

        public HttpResponseMessage GetUserByToken(string token, long timestamp, string appKey, string sign)
        {
            var user = GetUserObj();
            return user.ToHttpRspMsgSuccess();
        }

        /// <summary>
        /// 获取接口校验token，用于校验接口安全性
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="timestamp"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetSecretToken([FromBody]string appKey, long timestamp, string sign)
        {
            var dic = new SortedList<string, string>();
            dic.Add("timestamp", timestamp.ToString());
            dic.Add("appKey", appKey);
            var chkResult = SecretHelper.CheckSign(dic, sign);
            if (!chkResult.Status)
            {
                return ObjectExtends.ToHttpRspMsgError(chkResult.Msg);
            }
            //生成临时接口校验token
            var secretToken = SecretHelper.GetSecretTokenByKey(appKey);
            return new { secretToken = "" }.ToHttpRspMsgSuccess();
        }

        static object GetUserObj()
        {
            return new
            {
                token = "3344",
                name = "charles",
                mobile = "152*****512",
                address = "Hangzhou ,Zhejiang province"
            };
        }
    }
}
