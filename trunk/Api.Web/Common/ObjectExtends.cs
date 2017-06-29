using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using Api.Common;
using Api.Entity.Enum;
using Api.Web.Models;

namespace Api.Web.Common
{
    public static class ObjectExtends
    {
        public static JsonResult ToJsonResult(this object obj)
        {
            return new JsonResult() { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public static object ToJsonObject(EnumRspStatus status, string msg = "", object values = null)
        {
            return new { status = status.GetHashCode(), msg = msg, values = values };
        }

        public static HttpResponseMessage ToHttpRspMsg(this object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var json = JsonHelper.SerializeObject(obj);
            return new HttpResponseMessage(statusCode) { Content = new StringContent(json) };
        }

        public static HttpResponseMessage ToHttpRspMsg(this object obj, EnumRspStatus status,
            string msg = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var jsonObj = ToJsonObject(status, msg, obj);
            return ToHttpRspMsg(jsonObj, statusCode);
        }

        public static HttpResponseMessage ToHttpRspMsgSuccess(this object obj, string msg = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var jsonObj = ToJsonObject(EnumRspStatus.Success, msg, obj);
            return ToHttpRspMsg(jsonObj, statusCode);
        }

        public static HttpResponseMessage ToHttpRspMsgError(string msg, EnumRspStatus status = EnumRspStatus.Fail,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var jsonObj = ToJsonObject(status, msg);
            return ToHttpRspMsg(jsonObj, statusCode);
        }

        public static ResultModel ReturnResult(string msg, bool status = false)
        {
            return new ResultModel(msg, status);
        }
    }
}