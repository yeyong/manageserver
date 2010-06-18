using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Cache
{
    public class CacheKeys
    {
        public const string FORUM_URLS = "/SAS/Urls";
        public const string FORUM_SETTING = "/SAS/Setting";
        public const string FORUM_ADMIN_GROUP_LIST = "/SAS/AdminGroupList";
        public const string FORUM_USER_GROUP_LIST = "/SAS/UserGroupList";
        public const string FORUM_STATISTICS = "/SAS/Statistics";
        public const string SAS_COMPANY_Table_LIST = "/SAS/CompanyTableList";
        /// <summary>
        /// 类别企业信息
        /// </summary>
        public const string SAS_COMPANY_Table_SUB = "/SAS/CompanyTableList/CompanyTableList_";
        public const string SAS_CARDCONFIG = "/SAS/CardConfig";
        /// <summary>
        /// 首页城市企业信息
        /// </summary>
        public const string SAS_COMPANY_INDEX_CITY = "/SAS/CompanyIndex/City_";
        /// <summary>
        /// 最新企业信息
        /// </summary>
        public const string SAS_COMPANY_INDEX_NEW = "/SAS/CompanyIndex/NEW";
        /// <summary>
        /// 最多访问的企业
        /// </summary>
        public const string SAS_COMPANY_INDEX_ACCESSES = "/SAS/CompanyIndex/ACCESSES";
        /// <summary>
        /// 首页类型企业信息
        /// </summary>
        public const string SAS_COMPANY_INDEX_ENTYPE = "/SAS/CompanyIndex/ENTYPE_";
    }
}
