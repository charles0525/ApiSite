using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.BLL;
using Api.Common;
using Api.Web.Common;

namespace Api.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SiteInfoBll _bll = new SiteInfoBll();

        /// <summary>
        /// 测试页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //ViewBag.CacheData = CacheHelper.Get<string>(ConstValues.CacheKey_HistoryLottery);
            //var siteInfo = _bll.Get();
            //return View(siteInfo);
            return View();
        }

        /// <summary>
        /// 引发程序错误页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorPageDemo()
        {
            var j = 0;
            var i = 1 / j;

            return View();
        }

        /// <summary>
        /// 友好错误页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorPage()
        {
            ViewBag.ErrorCode = Utils.GetCookie(ConstValues.CookieKey_ErrorHttpCode);
            return View();
        }

        /// <summary>
        /// 子页面
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ChildPage()
        {
            var url = Request.RawUrl;
            ViewBag.PageTile = ConstValues.ListRouteItems.FirstOrDefault(x => url.IndexOf(x.Url) >= 0)?.PageTitle;

            return View();
        }

        /// <summary>
        /// 彩票信息查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            return View();
        }
    }
}