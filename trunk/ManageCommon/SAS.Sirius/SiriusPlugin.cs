using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using SAS.Plugin.Sirius;
using SAS.Common;
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
        /// <returns></returns>
        public override int CreateTeamInfo(TeamInfo teaminfo,out string result)
        {
            return Sirius.CreateTeam(teaminfo, out result);
        }

        /// <summary>
        /// 获得所有团队信息
        /// </summary>
        /// <returns></returns>
        public override SAS.Common.Generic.List<TeamInfo> GetAllTeamList()
        {
            return Sirius.GetAllTeamInfoList();
        }

        /// <summary>
        /// 根据ID获取团队信息
        /// </summary>
        /// <param name="teamID"></param>
        /// <returns></returns>
        public override TeamInfo GetTeamByTeamID(int teamID)
        {
            return Sirius.GetTeamInfoByTeamID(teamID);
        }

        public override bool UpdateTeamInfo(TeamInfo teaminfo, out string result)
        {
            return Sirius.UpdateTeamInfo(teaminfo, out result);
        }
    }
}
