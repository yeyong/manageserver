using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Entity;
using SAS.Config;

namespace SAS.Logic
{
    /// <summary>
    /// AdminUserGroupFactory 的摘要说明。
    /// 后台用户组管理操作类
    /// </summary>
    public class AdminUserGroups : UserGroups
    {
        public static string opresult = ""; //存储操作结果或返回给用户的信息

        /// <summary>
        /// 通过指定的用户组id得到相关的用户组信息
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static UserGroupInfo AdminGetUserGroupInfo(int groupid)
        {
            return UserGroups.GetUserGroupInfo(groupid);
        }
    }
}
