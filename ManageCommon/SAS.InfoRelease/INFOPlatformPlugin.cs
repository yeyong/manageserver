using System;
using System.Text;

using SAS.Plugin.InfoPlatform;
using SAS.Common.Generic;
using SAS.Entity.InfoPlatform;

namespace SAS.InfoRelease
{
    /// <summary>
    /// 企业信息发布平台插件操作（执行）
    /// </summary>
    public class INFOPlatformPlugin : INFOPlatformPluginBase
    {
        /// <summary>
        /// 增加企业会员
        /// </summary>
        public override int InsertUser(UserInfo uinfo)
        {
            return INFOPlatform.InsertUser(uinfo);
        }
        /// <summary>
        /// 根据登录名获取会员信息
        /// </summary>
        public override UserInfo GetUserInfoByLoginName(string loginname)
        {
            return INFOPlatform.GetUserByName(loginname);
        }
        /// <summary>
        /// 修改企业会员信息
        /// </summary>
        public override int UpdateUser(UserInfo uinfo)
        {
            return INFOPlatform.UpdateUser(uinfo);
        }
    }
}
