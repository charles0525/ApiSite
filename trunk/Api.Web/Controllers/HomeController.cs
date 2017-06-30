using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.Common;
using Api.Web.Common;
using Api.Interface.Demo;
using Autofac;
using Api.Entity.DB;

namespace Api.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISiteInfoDal _siteInfo = AutofacConfig.instance.Container.Resolve<ISiteInfoDal>();

        /// <summary>
        /// 测试页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.CacheData = CacheHelper.Get<string>(ConstValues.CacheKey_HistoryLottery);
            var siteInfo = new SiteInfoEntity();// _siteInfo.Get(0);
            var siteName = _siteInfo.GetName();
            return View(siteInfo);
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