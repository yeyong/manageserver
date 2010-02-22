using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;
using System.Xml.Serialization;
using System.Data;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 行业类别操作
    /// </summary>
    public class Catalogs : WriteFile
    {
        #region 私有变量
        private static volatile Catalogs instance = null;
        private static object lockHelper = new object();
        private static string jsonPath = "";
        #endregion

        #region 返回唯一实例
        private Catalogs()
        {
            jsonPath = Utils.GetMapPath(BaseConfigs.GetSitePath + "\\javascript\\companycategories.js");
        }

        public static Catalogs GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Catalogs();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 创建新行业类别
        /// </summary>
        /// <param name="cli"></param>
        /// <returns></returns>
        public static int CreateCatalogInfo(CatalogInfo cli)
        {
            return SAS.Data.DataProvider.Catalogies.CreateCatalogInfo(cli);
        }

        /// <summary>
        /// 获取行业类别信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogInfo GetCatalogInfo(int id)
        {
            return SAS.Data.DataProvider.Catalogies.GetCatalogInfo(id);
        }

        /// <summary>
        /// 获取全部行业类别信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCatalog()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/SAS/CompanyCategories") as DataTable;
            if (dt == null)
            {
                dt = SAS.Data.DataProvider.Catalogies.GetAllCatalogList();
                cache.AddObject("/SAS/CompanyCategories", dt);
            }
            return dt;
        }

        /// <summary>
        /// 获取非缓存行业信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCatalogNoCache()
        {
            return SAS.Data.DataProvider.Catalogies.GetAllCatalogList();
        }

        /// <summary>
        /// 返回Ajax字符串
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static string ReturnCalalogList(int parentid)
        {
            string returnmessage = "";
            DataTable dt = GetAllCatalog();

            foreach (DataRow dr in dt.Select("[parentid] = " + parentid))
            {
                returnmessage += "<option value=\"" + dr["id"] + "\">" + dr["name"] + "</option>";
            }

            return returnmessage;
        }

        /// <summary>
        /// 更新行业类别信息
        /// </summary>
        /// <param name="_catalog"></param>
        public static void UpdateCatalogInfo(CatalogInfo _catalog)
        {
            SAS.Data.DataProvider.Catalogies.UpdateCatalogInfo(_catalog);
        }

        /// <summary>
        /// 生成商品分类表的JSON文件
        /// </summary>
        /// <returns>是否写入成功</returns>
        public override bool WriteJsonFile()
        {
            StringBuilder sb_categories = new StringBuilder("var cats = ");

            sb_categories.Append(
                Utils.DataTableToJSON(
                      SAS.Data.DataProvider.Catalogies.GetCategoriesTableToJson()));

            return base.WriteJsonFile(jsonPath, sb_categories);
        }
    }
}
