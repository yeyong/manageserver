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
using SAS.Cache.CacheDependencyFactory;

namespace SAS.Logic
{
    /// <summary>
    /// 企业信息操作类
    /// </summary>
    public class Companies
    {
        private static Predicate<Companys> marchPass = new Predicate<Companys>(delegate(Companys companyinfo) { return companyinfo.En_status == 2 && companyinfo.En_visble == 1; });
        private const string COMMSORT = "[en_credits] DESC,[en_accesses] DESC";
        private static DataCacheConfigInfo dataconfig = DataCacheConfigs.GetConfig();
        
        /// <summary>
        /// 创建企业信息
        /// </summary>
        /// <param name="_companyInfo"></param>
        /// <returns></returns>
        public static int CreateCompanyInfo(Companys _companyInfo)
        {
            if (ExistCompanyName(_companyInfo.En_name) > 0) return 0;
            //缓存清理操作暂无
            return SAS.Data.DataProvider.Companies.CreateCompany(_companyInfo);
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
            return SAS.Data.DataProvider.Companies.GetCompanyInfo(enid);
        }

        /// <summary>
        /// 获取企业信息缓存实体
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static Companys GetCompanyCacheInfo(int enid)
        {
            return GetCompanyList().Find(new Predicate<Companys>(delegate(Companys companyinfo) { return companyinfo.En_id == enid; }));
        }

        /// <summary>
        /// 获取全部企业信息（带缓存）
        /// </summary>
        /// <returns></returns>
        public static List<Companys> GetCompanyList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<Companys> companylist = cache.RetrieveObject("/SAS/CompanyList") as List<Companys>;

            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyList();
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.LoadCacheStrategy(ica);
                cache.AddObject("/SAS/CompanyList", companylist);
                cache.LoadDefaultCacheStrategy();
            }
            return companylist;
        }

        /// <summary>
        /// 根据条件取得公司总数
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static int GetCompanyCount(int catalogid, string conditions)
        {
            DataTable dt = new DataTable();
            if (catalogid > 0)
            {
                dt = GetCompanyTableListByCatalog(catalogid);
            }
            else
            {
                dt = GetCompanyTableList();
            }
            return dt.Select(conditions).Length;
        }

        /// <summary>
        /// 企业数据分页操作
        /// </summary>
        /// <param name="catalogid">类别</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <param name="ordercolumn">排序列名</param>
        /// <param name="ordertype">排序方式</param>
        /// <param name="conditions">条件</param>
        public static DataRow[] GetCompanyPageList(int catalogid, int pageindex, int pagesize, string ordercolumn, string ordertype, string conditions)
        {
            DataTable companylist = new DataTable();
            if (catalogid > 0) companylist = GetCompanyTableListByCatalog(catalogid);
            else companylist = GetCompanyTableList();

            ArrayList redatarow = new ArrayList();

            redatarow.AddRange(companylist.Select(conditions, ordercolumn + " " + ordertype));
            if (redatarow.Count > 0)
            {
                if (pageindex * pagesize > redatarow.Count) pagesize = pagesize - (pagesize * pageindex - redatarow.Count);
                DataRow[] newdatarow = new DataRow[pagesize];
                redatarow.CopyTo((pageindex - 1) * pagesize, newdatarow, 0, pagesize);

                return newdatarow;
            }
            return new DataRow[0];
        }

        /// <summary>
        /// 获取Table型企业信息集合（缓存）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanyTableList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable companylist = cache.RetrieveObject("/SAS/CompanyTableList") as DataTable;

            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyALLList();
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 300;
                cache.LoadCacheStrategy(ica);
                cache.AddObject("/SAS/CompanyTableList", companylist);
                cache.LoadDefaultCacheStrategy();
            }
            return companylist;
        }
        /// <summary>
        /// 根据行业ID获取Table型企业信息集合（缓存）
        /// </summary>
        /// <param name="catalogid"></param>
        /// <returns></returns>
        public static DataTable GetCompanyTableListByCatalog(int catalogid)
        {
            if(dataconfig.EnableCaching != 1){
                return SAS.Data.DataProvider.Companies.GetCompanyListByCatalog(catalogid);
            }
            SAS.Cache.SASDataCache cache = SAS.Cache.SASDataCache.GetCacheService();
            string cachekey = "CompanyTableList_" + catalogid;
            DataTable companylist = cache.GetDataCache(cachekey) as DataTable;
            if (companylist == null)
            {
                companylist = SAS.Data.DataProvider.Companies.GetCompanyListByCatalog(catalogid);
                AggregateCacheDependency cd = DependencyFacade.GetCompanyDependency();
                cache.SetDataCache(cachekey, companylist, cd);
            }
            return companylist;
        }

        /// <summary>
        /// 获取最新加入企业
        /// </summary>
        /// <returns></returns>
        public static List<Companys> GetNewCompanyList()
        {
            List<Companys> companylist = new List<Companys>();
            int row = 0;
            foreach (Companys _compinfo in GetCompanyList().FindAll(marchPass))
            {
                if (row > 4) break;
                companylist.Add(_compinfo);
                row++;
            }
            return companylist;
        }

        /// <summary>
        /// 根据市级信息获取企业信息
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public static DataRow[] GetCompanyListByCity(int cityid)
        {
            DataTable dt = GetCompanyTableList();
            return dt.Select("[en_areas] IN (" + areas.GetDistrictIDByCity(cityid) + ")", COMMSORT);
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
    }
}
