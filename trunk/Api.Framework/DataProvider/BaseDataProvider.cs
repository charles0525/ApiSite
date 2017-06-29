using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Api.Entity.Enum;
using Api.Entity;

namespace Api.Framework.DataProvider
{
    /// <summary>
    /// 数据访问声明
    /// by charles.he
    /// @2016.12.26
    /// </summary>
    public class BaseDataProvider
    {
        private EnumDataProviderType _emProvider;
        public EnumDataProviderType EmProvider { get { return _emProvider; } }

        public BaseDataProvider(EnumDataProviderType? emProvider)
        {
            if (emProvider == null)
            {
                throw new CustomException("数据库枚举未赋值");
            }

            _emProvider = emProvider.Value;
        }

        public IDbConnection GetIDbConnection()
        {
            var strCon = GetConStr();
            IDbConnection con = new SqlConnection(strCon);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            return con;
        }

        public string GetConStr()
        {
            switch (_emProvider)
            {
                case EnumDataProviderType.TestDb:
                    return ConfigurationManager.AppSettings["TestConn"];
                default:
                    throw new CustomException("获取数据库链接失败");
            }
        }
    }
}