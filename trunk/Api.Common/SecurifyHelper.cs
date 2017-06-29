using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Api.Common
{
    public class SecurifyHelper
    {
        #region 创建签名

        //创建签名SHA1
        public static string createSHA1Sign(SortedList<string, string> signParams)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            IEnumerator<KeyValuePair<string, string>> enumerator = signParams.OrderBy(x => x.Key).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (i > 0)
                {
                    sb.Append("&");
                }
                String k = enumerator.Current.Key;
                String v = enumerator.Current.Value;
                sb.Append(k + "=" + v);

                i++;
            }

            return FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "SHA1").ToLower();
        }

        public static string CreateSign(SortedList<string, string> packageParams, string key)
        {
            StringBuilder sb = new StringBuilder();
            packageParams.OrderBy(x => x.Key).ToList().ForEach(x =>
            {
                string k = x.Key;
                string v = x.Value;
                if (!string.IsNullOrEmpty(v) && k != "sign" && k != "key")
                {
                    sb.Append(k + "=" + v + "&");
                }
            });

            sb.Append("key=" + key);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "MD5").ToUpper();
        }

        #endregion
    }
}
