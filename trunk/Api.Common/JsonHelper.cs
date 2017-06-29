using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Api.Common
{
    public class JsonHelper
    {

        private string _error = string.Empty;
        private bool _success = true;
        private long recordcount = 0;
        private IList<string> arrItem = new List<string>();
        private IList<string> arrData = new List<string>();

        public JsonHelper()
        {

        }

        /// <summary>
        /// 对应于JSON的Success成员
        /// </summary>
        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                //如设置为true则清空error
                if (Success) _error = string.Empty;
                _success = value;
            }
        }

        /// <summary>
        /// 对应于JSON的Error成员
        /// </summary>
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                //如设置error，则自动设置success为false
                if (value != "") _success = false;
                _error = value;
            }
        }

        /// <summary>
        /// 返回总记录条数
        /// </summary>
        public long TotalCount
        {
            get { return recordcount; }
            set { recordcount = value; }
        }

        /// <summary>
        /// 重置，每次新生成一个json对象时必须执行该方法
        /// </summary>
        public void Reset()
        {
            _success = true;
            _error = string.Empty;
            arrItem.Clear();
        }

        /// <summary>
        /// 向JSON添加记录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddItem(string name, string value)
        {
            arrItem.Add("\"" + name + "\":" + "\"" + value + "\"");
        }

        public void AddItem(string name, int value)
        {
            arrItem.Add("\"" + name + "\":" + value.ToString());
        }

        public void AddItem(string name, decimal value)
        {
            arrItem.Add("\"" + name + "\":" + value.ToString());
        }

        public void AddItem(string name, float value)
        {
            arrItem.Add("\"" + name + "\":" + value.ToString());
        }

        public void AddItem(string name, double value)
        {
            arrItem.Add("\"" + name + "\":" + value.ToString());
        }

        /// <summary>
        /// 记录数自加1
        /// </summary>
        public void ItemOk()
        {
            arrItem.Add("|");
            recordcount++;
        }

        /// <summary>
        /// 得到返回的JSON代码,以行为单位 例：{name:'ly',sex:'男'},{name:'lyp',sex:'男'}
        /// </summary>
        /// <returns></returns>
        public string ToList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (string val in arrItem)
            {
                if (val != "|")
                {
                    if (sb[sb.Length - 1] == '{')
                        sb.Append(val);
                    else
                        sb.Append("," + val);
                }
                else
                {
                    sb.Append("},{");
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 一个数据项为单位 ,例如：name:'ly',sex:'男'
        /// </summary>
        /// <returns></returns>
        public string ToData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string val in arrItem)
            {
                if (val != "|")
                {
                    if (sb.Length == 0)
                        sb.Append(val + ",");
                    else
                        sb.Append("," + val);
                }

            }
            return sb.ToString();
        }

        public string ToJsonTree()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + this.ToList() + "]");
            return sb.ToString();
        }

        public string ToJsonGrid()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("recordcount:" + recordcount.ToString() + ",");
            sb.Append("success:" + _success.ToString().ToLower() + ",");
            sb.Append("error:\"" + _error.Replace("\"", "\\\"") + "\",");
            sb.Append("item:[" + ToList() + "]}");
            return sb.ToString();
        }

        public string ToJsonForm()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("success:" + _success.ToString().ToLower() + ",");
            sb.Append("data:" + this.ToList() + "}");
            return sb.ToString();
        }

        public static string BuildJson(string result, string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"result\":\"" + result + "\",");
            sb.Append("\"msg\":\"" + msg + "\"}");
            return sb.ToString();
        }

        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeComplexType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        /// <summary>
        /// 反序列化JSON到给定的指定复杂对象
        /// </summary>
        /// <typeparam name="T">复杂对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>复杂对象</returns>
        public static T DeserializeComplexType<T>(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            T t = (T)serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            return t;
        }
    }
}
