using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 行业类别数据操作
    /// </summary>
    public class Catalogies
    {
        /// <summary>
        /// 创建行业类目
        /// </summary>
        /// <param name="_cataloginfo"></param>
        /// <returns></returns>
        public static int CreateCatalogInfo(CatalogInfo _cataloginfo)
        {
            return DatabaseProvider.GetInstance().CreateCatalog(_cataloginfo);
        }

        /// <summary>
        /// 企业信息实体化
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static CatalogInfo LoadSingleCatalogInfo(IDataReader reader)
        {
            if (reader.Read())
            {
                CatalogInfo _cataloginfo = new CatalogInfo();
                _cataloginfo.id = TypeConverter.ObjectToInt(reader["id"]);
                _cataloginfo.parentid = TypeConverter.ObjectToInt(reader["parentid"]);
                _cataloginfo.sort = TypeConverter.ObjectToInt(reader["sort"]);
                _cataloginfo.parentlist = reader["parentlist"].ToString().Trim();
                _cataloginfo.displayorder = TypeConverter.ObjectToInt(reader["displayorder"]);
                _cataloginfo.name = reader["name"].ToString().Trim();
                _cataloginfo.haschild = reader["haschild"].ToString() == "True" ? 1 : 0;
                _cataloginfo.companycount = TypeConverter.ObjectToInt(reader["companycount"]);
                _cataloginfo.cllogo = reader["cllogo"].ToString();

                reader.Close();
                return _cataloginfo;
            }
            return null;
        }

        /// <summary>
        /// 企业信息实体化
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static CatalogInfo LoadSingleCatalogInfo(DataRow dr)
        {
            CatalogInfo _cataloginfo = new CatalogInfo();
            _cataloginfo.id = TypeConverter.ObjectToInt(dr["id"]);
            _cataloginfo.parentid = TypeConverter.ObjectToInt(dr["parentid"]);
            _cataloginfo.sort = TypeConverter.ObjectToInt(dr["sort"]);
            _cataloginfo.parentlist = dr["parentlist"].ToString().Trim();
            _cataloginfo.displayorder = TypeConverter.ObjectToInt(dr["displayorder"]);
            _cataloginfo.name = dr["name"].ToString().Trim();
            _cataloginfo.haschild = dr["haschild"].ToString() == "True" ? 1 : 0;
            _cataloginfo.companycount = TypeConverter.ObjectToInt(dr["companycount"]);
            _cataloginfo.cllogo = dr["cllogo"].ToString();
            return _cataloginfo;
        }

        /// <summary>
        /// 获取类别实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogInfo GetCatalogInfo(int id)
        {
            return LoadSingleCatalogInfo(DatabaseProvider.GetInstance().GetCatalogInfo(id));
        }

        /// <summary>
        /// 获取全部行业类别信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCatalogList()
        {
            return DatabaseProvider.GetInstance().GetAllCatalog();
        }

        /// <summary>
        /// 获取行业类别信息赋予json
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCategoriesTableToJson()
        {
            return DatabaseProvider.GetInstance().GetCategoriesTableToJson();
        }

        /// <summary>
        /// 更新行业类别信息
        /// </summary>
        /// <param name="_catalog"></param>
        public static void UpdateCatalogInfo(CatalogInfo _catalog)
        {
            DatabaseProvider.GetInstance().UpdateCatalogInfo(_catalog);
        }

        /// <summary>
        /// 更新行业类别下企业信息数量
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="counts"></param>
        public static void UpdateCatalogCompanyCount(string idlist, int counts)
        {
            DatabaseProvider.GetInstance().UpdateCatalogCompanyCount(idlist, counts);
        }
    }
}
