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
        /// 类别企业信息缓存键值
        /// </summary>
        public const string SAS_COMPANY_Table_SUB = "/SAS/CompanyTableList/CompanyTableList_";
        public const string SAS_CARDCONFIG = "/SAS/CardConfig";
        /// <summary>
        /// 首页城市企业信息缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_CITY = "/SAS/CompanyIndex/City_";
        /// <summary>
        /// 最新企业信息缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_NEW = "/SAS/CompanyIndex/NEW";
        /// <summary>
        /// 最新审核企业信息缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_ENUPDATE = "/SAS/CompanyIndex/ENUPDATE";
        /// <summary>
        /// 最多访问的企业缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_ACCESSES = "/SAS/CompanyIndex/ACCESSES";
        /// <summary>
        /// 最多评论的企业缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_COMMENTS = "/SAS/CompanyIndex/COMMENTS";
        /// <summary>
        /// 企业信誉排行缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_CREDITS = "/SAS/CompanyIndex/CREDITS";
        /// <summary>
        /// 首页类型企业信息缓存键值
        /// </summary>
        public const string SAS_COMPANY_INDEX_ENTYPE = "/SAS/CompanyIndex/ENTYPE_";
        /// <summary>
        /// 活动缓存键值
        /// </summary>
        public const string SAS_ACTIVITY = "SAS/Activity";
    }
}
