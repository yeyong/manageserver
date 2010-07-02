using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Caching;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 企业信息操作类
    /// </summary>
    public class Companies
    {
        private static Predicate<Companys> marchPass = new Predicate<Companys>(delegate(Companys companyinfo) { return companyinfo.En_status == 2 && companyinfo.En_visble == 1; });
        /// <summary>
        /// 通用条件
        /// </summary>
        private const string COMMCONDITION = "[en_visble] = 1 AND [en_status] = 2";
        /// <summary>
        /// 通用排序
        /// </summary>
        private const string COMMSORT = "[en_credits] DESC,[en_accesses] DESC";
        
        /// <summary>
        /// 创建企业信息
        /// </summary>
        /// <param name="_companyInfo"></param>
        /// <returns></returns>
        public static int CreateCompanyInfo(Companys _companyInfo)
        {
            if (ExistCompanyName(_companyInfo.En_name) > 0) return 0;
            //缓存清理操作暂无
            int rows = SAS.Data.DataProvider.Companies.CreateCompany(_companyInfo);
            if (rows > 0) Catalogs.UpdateCatalogCompanyCount(_companyInfo.En_cataloglist, 1);
            return rows;
        }

        /// <summary>
        /// 更新企业信息
        /// </summary>
        /// <param name="_companyInfo"></param>
        /// <returns></returns>
        public static bool UpdateCompanyInfo(Companys _companyInfo)
        {
            return SAS.Data.DataProvider.Companies.UpdateCompany(_companyInfo);
        }

        /// <summary>
        /// 验证是否存在企业名称
        /// </summary>
        /// <param name="enname"></param>
        /// <returns>返回企业ID</returns>
        public static int ExistCompanyName(string enname)
        {
            Companys _companyInfo = SAS.Data.DataProvider.Companies.GetCompanyInfoByName(enname);
            return (_companyInfo != null) ? _companyInfo.En_id : 0;
        }

        /// <summary>
        /// 获取企业信息实体
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static Companys GetCompanyInfo(int enid)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyInfoByID(enid);
        }

        /// <summary>
        /// 获取企业信息缓存实体
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static Companys GetCompanyCacheInfo(int enid)
        {
            DataRow[] dr = GetCompanyTableList().Select(COMMCONDITION + " AND en_id = " + enid);
            if (dr.Length > 0)
            {
                return SAS.Data.DataProvider.Companies.LoadCompanyInfo(dr[0]);
            }
            return null;
        }

        /// <summary>
        /// 根据条件取得公司总数
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static int GetCompanyCount(int catalogid, string conditions)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyCountByCatalog(catalogid, conditions);
        }

        ///// <summary>
        ///// 企业数据分页操作
        ///// </summary>
        ///// <param name="catalogid">类别</param>
        ///// <param name="pageindex">当前页</param>
        ///// <param name="pagesize">页面尺寸</param>
        ///// <param name="ordertype">排序方式</param>
        ///// <param name="conditions">条件</param>
        public static List<Companys> GetCompanyPageList(int catalogid, int pageindex, int pagesize, int ordertype, string conditions)
        {
            string theordertype = "";
            string theordercolumn = "";

            switch (ordertype)
            {
                case 1:
                    theordertype = "";
                    theordercolumn = "en_accesses";
                    break;
                case 2:
                    theordertype = "desc";
                    theordercolumn = "en_accesses";
                    break;
                case 3:
                    theordertype = "";
                    theordercolumn = "en_sell";
                    break;
                case 4:
                    theordertype = "desc";
                    theordercolumn = "en_sell";
                    break;
                case 5:
                    theordertype = "desc";
                    theordercolumn = "en_createdate";
                    break;
                case 6:
                    theordertype = "";
                    theordercolumn = "en_createdate";
                    break;
                default:
                    theordertype = "desc";
                    theordercolumn = "en_credits";
                    break;
            }

            return GetCompanyPageList(catalogid, pageindex, pagesize, theordercolumn, theordertype, conditions);
        }

        ///// <summary>
        ///// 企业数据分页操作
        ///// </summary>
        ///// <param name="catalogid">类别</param>
        ///// <param name="pageindex">当前页</param>
        ///// <param name="pagesize">页面尺寸</param>
        ///// <param name="ordercolumn">排序列名</param>
        ///// <param name="ordertype">排序方式</param>
        ///// <param name="conditions">条件</param>
        public static List<Companys> GetCompanyPageList(int catalogid, int pageindex, int pagesize, string ordercolumn, string ordertype, string conditions)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyListPage(catalogid, pagesize, pageindex, ordercolumn, ordertype, conditions);
        }

        ///// <summary>
        ///// 企业数据分页操作
        ///// </summary>
        ///// <param name="catalogid">类别</param>
        ///// <param name="pageindex">当前页</param>
        ///// <param name="pagesize">页面尺寸</param>
        ///// <param name="ordercolumn">排序列名</param>
        ///// <param name="ordertype">排序方式</param>
        ///// <param name="conditions">条件</param>
        //public static DataRow[] GetCompanyPageList(int catalogid, int pageindex, int pagesize, string ordercolumn, string ordertype, string conditions)
        //{
        //    DataTable companylist = new DataTable();
        //    if (catalogid > 0) companylist = GetCompanyTableListByCatalog(catalogid);
        //    else companylist = GetCompanyTableList();

        //    List<DataRow> redatarow = new List<DataRow>();

        //    string defoder = "[en_status] DESC";
        //    if (ordercolumn != "") defoder += "," + ordercolumn + " " + ordertype;
        //    redatarow.AddRange(companylist.Select(conditions, defoder));
        //    if (redatarow.Count > 0)
        //    {
        //        if (pageindex * pagesize > redatarow.Count) pagesize = pagesize - (pagesize * pageindex - redatarow.Count);
        //        DataRow[] newdatarow = new DataRow[pagesize];
        //        redatarow.CopyTo((pageindex - 1) * pagesize, newdatarow, 0, pagesize);

        //        return newdatarow;
        //    }
        //    return new DataRow[0];
        //}

        /// <summary>
        /// 获取Table型企业信息集合（缓存）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanyTableList()
        {
            return GetCompanyTableListByCatalog(0);
        }
        /// <summary>
        /// 根据行业ID获取Table型企业信息集合（缓存）
        /// </summary>
        /// <param name="catalogid"></param>
        /// <returns></returns>
        public static DataTable GetCompanyTableListByCatalog(int catalogid)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = CacheKeys.SAS_COMPANY_Table_SUB + catalogid;
            DataTable companylist = cache.RetrieveObject(cachekey) as DataTable;
            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyListByCatalog(catalogid);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 1440;
                cache.AddObject(cachekey, companylist);
            }
            return companylist;
        }

        /// <summary>
        /// 获取最新加入企业
        /// </summary>
        /// <returns></returns>
        public static List<Companys> GetNewCompanyList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = CacheKeys.SAS_COMPANY_INDEX_NEW;
            List<Companys> companylist = cache.RetrieveObject(cachekey) as List<Companys>;
            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyListByOrder(10, "en_createdate", true);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 30;
                cache.AddObject(cachekey, companylist);
            }
            return companylist;
        }

        /// <summary>
        /// 根据企业类型获取企业信息
        /// </summary>
        /// <param name="ete">企业类型</param>
        /// <returns></returns>
        public static List<Companys> GetCompanyListByType(EnTypeEnum ete)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = CacheKeys.SAS_COMPANY_INDEX_ENTYPE + Convert.ToInt16(ete);
            List<Companys> companylist = cache.RetrieveObject(cachekey) as List<Companys>;
            if (companylist == null)
            {
                companylist = new List<Companys>();
                List<Companys> tempcompanylist = SAS.Data.DataProvider.Companies.GetCompanyListByType(Convert.ToInt16(ete), 10);
                foreach (Companys compinfo in tempcompanylist)
                {
                    if (compinfo.En_cataloglist.Length > 0)
                    {
                        compinfo.TempCatalogID = TypeConverter.StrToInt(compinfo.En_cataloglist.Split(',')[0]);
                        CatalogInfo _catalog = Catalogs.GetCatalogCacheInfo(compinfo.TempCatalogID);
                        if (_catalog != null) compinfo.CatalogName = _catalog.name;
                    }
                    companylist.Add(compinfo);
                }
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.AddObject(cachekey, companylist);
            }
            return companylist;
        }

        /// <summary>
        /// 根据访问排序，并返回企业信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Companys> GetCompanyListViews()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = CacheKeys.SAS_COMPANY_INDEX_ACCESSES;
            List<Companys> companylist = cache.RetrieveObject(cachekey) as List<Companys>;
            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyListByOrder(10, "en_accesses", true);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.AddObject(cachekey, companylist);
            }
            return companylist;
        }

        /// <summary>
        /// 根据市级信息获取企业信息
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public static List<Companys> GetCompanyListByCity(int cityid)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string cachekey = CacheKeys.SAS_COMPANY_INDEX_CITY + cityid;
            List<Companys> companylist = cache.RetrieveObject(cachekey) as List<Companys>;
            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyListByCity(cityid, 4);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.AddObject(cachekey, companylist);
            }
            return companylist;
        }

        /// <summary>
        /// 企业搜索条件（审核通过状态下）
        /// </summary>
        /// <param name="catalogid">行业类别ID</param>
        /// <param name="arealist">所在地区列表</param>
        /// <param name="typeid">企业类型ID</param>
        /// <param name="regyear">注册年限</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public static string GetCompanyCondition(string arealist, int typeid, int regyear, string keyword)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyCondition(arealist, typeid, regyear, keyword);
        }

        /// <summary>
        /// 更新企业评论数
        /// </summary>
        /// <param name="qyid"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public static void UpdateCompanyCommentCount(int qyid, int counts)
        {
            SAS.Data.DataProvider.Companies.UpdateCompanyCommentCount(qyid, counts);
        }
    }
}
