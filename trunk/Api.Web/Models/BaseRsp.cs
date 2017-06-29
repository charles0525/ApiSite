using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web.Models
{
    [Serializable]
    public class BaseRsp<T> where T : class
    {
        public string result { get; set; }
        public string errorMsg { get; set; }
        public List<T> value { get; set; }
    }
}