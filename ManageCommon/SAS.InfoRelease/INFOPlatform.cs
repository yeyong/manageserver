using System;
using System.Text;

using SAS.Entity.InfoPlatform;
using SAS.Common.Generic;

namespace SAS.InfoRelease
{
    /// <summary>
    /// 企业信息发布平台操作
    /// </summary>
    public class INFOPlatform
    {
        /// <summary>
        /// 增加企业会员
        /// </summary>
        public static int InsertUser(UserInfo uinfo)
        {
            return Data.DbProvider.GetInstance().InsertUser(uinfo);
        }
        /// <summary>
        /// 根据登录名获取会员信息
        /// </summary>
        public static UserInfo GetUserByName(string lname)
        {
            return Data.DbProvider.GetInstance().GetUserByName(lname);
        }
    }
}
