using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Xml.Serialization;
using System.Data;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Entity;
using SAS.Cache.CacheDependencyFactory;

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
        private static DataCacheConfigInfo dataconfig = DataCacheConfigs.GetConfig();
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
        /// 获取行业类别实体（缓存）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CatalogInfo GetCatalogCacheInfo(int id)
        {
            CatalogInfo cinfo = new CatalogInfo();
            DataTable dt = GetAllCatalog();
            foreach (DataRow dr in dt.Select("[id] = " + id))
            {
                cinfo.id = TypeConverter.StrToInt(dr["id"].ToString(), 0);
                cinfo.name = dr["name"].ToString();
                cinfo.parentid = TypeConverter.StrToInt(dr["parentid"].ToString(), 0);
                cinfo.parentlist = dr["parentlist"].ToString();
                cinfo.sort = TypeConverter.StrToInt(dr["sort"].ToString(), 0);
                cinfo.cllogo = dr["cllogo"].ToString();
                cinfo.displayorder = TypeConverter.StrToInt(dr["displayorder"].ToString(), 0);
                cinfo.haschild = TypeConverter.StrToInt(dr["haschild"].ToString(), 0);
                cinfo.companycount = TypeConverter.StrToInt(dr["companycount"].ToString(), 0);
                return cinfo;
            }
            return null;
        }

        /// <summary>
        /// 获取全部行业类别信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCatalog()
        {
            DataTable dt = new DataTable();
            string cachekeys = "CompanyCategories";
            if (dataconfig.EnableCaching != 1)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                dt = cache.RetrieveObject("/SAS/" + cachekeys) as DataTable;
                if (dt == null)
                {
                    dt = SAS.Data.DataProvider.Catalogies.GetAllCatalogList();
                    cache.AddObject("/SAS/" + cachekeys, dt);
                }
            }
            else
            {
                SAS.Cache.SASDataCache datacache = SAS.Cache.SASDataCache.GetCacheService();
                dt = datacache.GetDataCache("CompanyCategories") as DataTable;
                if (dt == null)
                {
                    dt = SAS.Data.DataProvider.Catalogies.GetAllCatalogList();
                    AggregateCacheDependency cd = DependencyFacade.GetCatalogDependency();
                    datacache.SetDataCache(cachekeys, dt, cd);
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据级别获取行业类别集合（缓存）
        /// </summary>
        /// <returns></returns>
        public static DataRow[] GetAllCatalogBySort(int sortnum)
        {
            DataTable dt = GetAllCatalog();
            return dt.Select("[sort] = " + sortnum);
        }

        /// <summary>
        /// 根据父ID获取行业类别集合（带缓存）
        /// </summary>
        /// <returns></returns>
        public static DataRow[] GetAllCatalogByPid(int pid)
        {
            DataTable dt = GetAllCatalog();
            return dt.Select("[parentid] = " + pid);
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
        /// 取得相关类别下的企业数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static int ReturnCompanyCountById(int id, string conditions)
        //{
        //    int num = 0;
        //    DataTable dt = GetAllCatalog();
        //    foreach (DataRow dr in dt.Select("(','+[parentlist]+',' like '%," + id + ",%' AND [haschild] = 0) OR [id] = " + id))
        //    {
        //        string querycondition = "','+[en_cataloglist]+',' like '%," + dr["id"].ToString() + ",%'";
        //        if (conditions != "") querycondition = "','+[en_cataloglist]+',' like '%," + dr["id"].ToString() + ",%' AND " + conditions;
        //        num += TypeConverter.ObjectToInt(Companies.GetCompanyTableList().Compute("COUNT(en_id)", querycondition), 0);
        //    }
        //    return num;
        //}

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
        /// 更新行业企业数量
        /// </summary>
        public static void UpdateCatalogCompanyCount()
        {
            DataTable dt = GetAllCatalogNoCache();
            foreach (DataRow dr in dt.Rows)
            {
                CatalogInfo cif = SAS.Data.DataProvider.Catalogies.LoadSingleCatalogInfo(dr);
                cif.companycount = Companies.GetCompanyCount(cif.id, "en_visble = 1");
                UpdateCatalogInfo(cif);
            }
        }

        /// <summary>
        /// 更新行业企业数量
        /// </summary>
        public static void UpdateCatalogCompanyCount(string catalogids, int companycount)
        {
            SAS.Data.DataProvider.Catalogies.UpdateCatalogCompanyCount(catalogids, companycount);
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
