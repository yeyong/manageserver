using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 管理员企业操作类
    /// </summary>
    public class AdminCompanies : Companies
    {
        /// <summary>
        /// 获取企业信息集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static DataTable GetCompanyList()
        {
            DataTable dt = SAS.Data.DataProvider.Companies.GetCompanyAllList();
            return dt;
        }

        /// <summary>
        /// 批量开启企业
        /// </summary>
        /// <param name="enidlist"></param>
        public static void StartCompany(string enidlist)
        {
            UpdateCompanyListStatus(enidlist, 1);
        }

        /// <summary>
        /// 批量暂停企业
        /// </summary>
        /// <param name="enidlist"></param>
        public static void PauseCompany(string enidlist)
        {
            UpdateCompanyListStatus(enidlist, 0);
        }

        /// <summary>
        /// 更新企业列表状态信息
        /// </summary>
        /// <param name="_status"></param>
        /// <returns></returns>
        public static bool UpdateCompanyListStatus(string enidlist, int _status)
        {
            return SAS.Data.DataProvider.Companies.UpdateCompanyStatus(enidlist, _status);
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
    }
}
