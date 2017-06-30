using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using Api.Common;
using Api.Web.Common;

namespace Api.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            AutofacConfig.instance.Init();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            HistoryLotteryHelper.SetContentToCache();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnRemoteCertificateValidationCallback);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            var httpError = lastError as HttpException;
            int errCode = 500;
            if (httpError != null)
            {
                errCode = httpError.GetHttpCode();
            }
            Response.StatusCode = errCode;

            Utils.WriteCookie(ConstValues.CookieKey_ErrorHttpCode, errCode.ToString());
            Response.SuppressContent = true;
            Response.TrySkipIisCustomErrors = true;
            Response.Redirect("/error");
            Server.ClearError();
        }

        /// <summary>
        /// 回调验证证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool OnRemoteCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }


}