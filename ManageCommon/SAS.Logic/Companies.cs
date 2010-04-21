using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

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
        private static Predicate<Companys> marchPass = new Predicate<Companys>(delegate(Companys companyinfo) { return companyinfo.En_status == 2; });
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
            foreach (Companys cps in GetCompanyList())
            {
                if (cps.En_id == enid) return cps;
            }
            return null;
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
        /// 企业搜索条件
        /// </summary>
        /// <param name="catalogid">行业类别ID</param>
        /// <param name="arealist">所在地区列表</param>
        /// <param name="typeid">企业类型ID</param>
        /// <param name="regyear">注册年限</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public static string GetCompanyCondition(int catalogid, string arealist, int typeid, int regyear,string keyword)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyCondition(catalogid, arealist, typeid, regyear, keyword);
        }
    }
}
