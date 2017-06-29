using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Api.Entity.Enum;

namespace Api.Web.Common
{
    public class JsonHelper
    {
        public static object ToObject(EnumRspStatus result, string errorMsg = "", object value = null)
        {
            return new { result = result.GetHashCode(), errorMsg, value };
        }
    }
}
