using System;
using NLog;

namespace Api.Common
{
    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public class LogHelper
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();
        static Logger _smsLogger = null;

        /// <summary>
        /// 记录说明、日志信息
        /// </summary>
        /// <param name="msg">内容</param>
        public static void Info(string msg)
        {
            _logger.Error(msg);
        }
        public static void Info<T>(T t)
        {
            _logger.Error<T>(t);
        }

        public static void Warn(string msg)
        {
            _logger.Warn(msg);
        }
        public static void Warn<T>(T t)
        {
            _logger.Warn(t);
        }

        public static void Error(string msg)
        {
            Error(msg, null);
        }
        public static void Error(Exception ex)
        {
            if (ex == null)
            {
                return;
            }
            string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            Error(msg, ex);
        }
        public static void Error(string msg, Exception ex)
        {
            _logger.ErrorException(msg, ex);
        }
        public static void Error<T>(T t)
        {
            _logger.Error<T>(t);
        }

        public static void Fatal(object obj)
        {
            _logger.Fatal(obj);
        }
        public static void Fatal(Exception ex)
        {
            if (ex == null)
            {
                return;
            }
            string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            Fatal(msg, ex);
        }
        public static void Fatal(string msg, Exception ex)
        {
            _logger.FatalException(msg, ex);
        }

        /// <summary>
        /// 记录验证码发送错误 内容
        /// </summary>
        /// <param name="mobile">号码</param>
        /// <param name="ip">ip</param>
        /// <param name="reqUrl">请求地址</param>
        public static void RecordSmsSendFail(string mobile, string ip, string reqUrl)
        {
            if (_smsLogger == null)
            {
                _smsLogger = LogManager.GetLogger("LogSmsVerifyError");
            }
            if (_smsLogger != null)
            {
                _smsLogger.Info(string.Format("手机号码 {0}，Ip {1}，请求地址 {2}", mobile, ip, reqUrl));
            }
        }
    }
}
