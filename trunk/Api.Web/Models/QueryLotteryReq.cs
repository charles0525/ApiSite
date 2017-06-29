using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web.Models
{
    public class QueryLotteryReq : BaseReq
    {
        public string LotteryType { get; set; }
        public string LotteryCode { get; set; }
    }
}