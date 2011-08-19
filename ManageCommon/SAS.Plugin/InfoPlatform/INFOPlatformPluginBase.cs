using System;
using System.Text;
using System.IO;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity.InfoPlatform;

namespace SAS.Plugin.InfoPlatform
{
    /// <summary>
    /// 企业信息发布平台插件操作
    /// </summary>
    public abstract class INFOPlatformPluginBase : PluginBase
    {
        /// <summary>
        /// 增加企业会员
        /// </summary>
        public abstract int InsertUser(UserInfo uinfo);

        /// <summary>
        /// 根据登录名获取会员信息
        /// </summary>
        public abstract UserInfo GetUserInfoByLoginName(string loginname);

        /// <summary>
        /// 修改企业会员信息
        /// </summary>
        public abstract int UpdateUser(UserInfo uinfo);
    }
}
