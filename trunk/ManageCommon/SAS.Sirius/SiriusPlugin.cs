using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using SAS.Plugin.Sirius;
using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;

namespace SAS.Sirius
{
    /// <summary>
    /// Sirius studio 插件类
    /// </summary>
    public class SiriusPlugin : SiriusPluginBase
    {
        /// <summary>
        /// 创建团队信息
        /// </summary>
        /// <param name="teaminfo">信息实体</param>
        /// <param name="members">成员信息反馈</param>
        /// <param name="adminUid">管理员ID</param>
        /// <param name="adminUserName">管理员姓名</param>
        /// <param name="adminUserGruopId">管理组</param>
        /// <param name="adminUserGroupTitle">管理组名称</param>
        /// <param name="adminIp">管理员IP</param>
        /// <returns></returns>
        public override int CreateTeamInfo(TeamInfo teaminfo, out string members)
        {
            return Sirius.CreateTeam(teaminfo, out  members);
        }

        /// <summary>
        /// 获得所有团队信息
        /// </summary>
        /// <returns></returns>
        public override SAS.Common.Generic.List<TeamInfo> GetAllTeamList()
        {
            return Sirius.GetAllTeamInfoList();
        }

        public override TeamInfo GetTeamByTeamID(int teamID)
        {
            return Sirius.GetTeamInfoByTeamID(teamID);
        }
    }
}
