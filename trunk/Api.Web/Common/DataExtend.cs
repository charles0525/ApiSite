using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Api.Web.Common
{
    public static class DataExtend
    {
        public static JsonResult ToJsonResult(this object obj)
        {
            return new JsonResult() { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}