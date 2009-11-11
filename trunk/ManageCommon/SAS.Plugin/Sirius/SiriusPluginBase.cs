using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Plugin.Sirius
{
    /// <summary>
    /// Sirius studio 插件基类
    /// </summary>
    public abstract class SiriusPluginBase : PluginBase
    {
        /// <summary>
        /// 创建团队信息
        /// </summary>
        /// <param name="teaminfo">信息实体</param>
        /// <param name="members">成员信息反馈</param>
        /// <returns></returns>
        public abstract int CreateTeamInfo(TeamInfo teaminfo, out string members);

        /// <summary>
        /// 获得所有团队信息
        /// </summary>
        /// <returns></returns>
        public abstract List<TeamInfo> GetAllTeamList();
    }
}
