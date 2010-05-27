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
        /// 企业数据分页操作
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable GetCompanyListByPage(int pageindex, int pagesize, string condition)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyPageList(pageindex, pagesize, condition);
        }
        /// <summary>
        /// 返回企业数量
        /// </summary>
        /// <param name="conditions"></param>
        public static int GetCompanyCount(string conditions)
        {
            return SAS.Data.DataProvider.Companies.GetCompanyCountByCondition(conditions);
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
            Caches.ReSetCompanyTableList();
            return SAS.Data.DataProvider.Companies.UpdateCompanyStatus(enidlist, _status);
        }
    }
}
