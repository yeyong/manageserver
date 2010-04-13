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
        /// <summary>
        /// 创建企业信息
        /// </summary>
        /// <param name="_companyInfo"></param>
        /// <returns></returns>
        public static int CreateCompanyInfo(Companys _companyInfo)
        {
            if (ExistCompanyName(_companyInfo.en_name) > 0) return 0;
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
            return (_companyInfo != null) ? _companyInfo.en_id : 0;
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
                if (cps.en_id == enid) return cps;
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
    }
}
