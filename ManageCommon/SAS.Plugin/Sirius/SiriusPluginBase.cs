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
        /// <param name="result">返回成员信息</param>
        public abstract int CreateTeamInfo(TeamInfo teaminfo, out string result);

        /// <summary>
        /// 创建团队活动
        /// </summary>
        public abstract int CreateAct(TeamActInfo tacinfo);

        /// <summary>
        /// 获取团队信息（非缓存）
        /// </summary>
        public abstract DataTable GetAllTeam();

        /// <summary>
        /// 获得所有团队信息
        /// </summary>
        public abstract List<TeamInfo> GetAllTeamList();

        /// <summary>
        /// 根据团队ID获取团队信息
        /// </summary>
        /// <param name="teamID">团队ID</param>
        public abstract TeamInfo GetTeamByTeamID(int teamID);

        /// <summary>
        /// 更新团队信息
        /// </summary>
        /// <param name="teaminfo"></param>
        public abstract bool UpdateTeamInfo(TeamInfo teaminfo,out string result);

        /// <summary>
        /// 团队活动获取
        /// </summary>
        /// <param name="tid">团队ID</param>
        public abstract List<TeamActInfo> GetTeamActByTid(int tid);
        /// <summary>
        /// 团队活动获取（带缓存）
        /// </summary>
        public abstract List<TeamActInfo> GetTeamActByTidWithCache(int tid);
        /// <summary>
        /// 活动实体获得
        /// </summary>
        /// <param name="aid">活动ID</param>
        public abstract TeamActInfo GetTeamActInfo(int aid);

        /// <summary>
        /// 更新团队活动
        /// </summary>
        /// <param name="tinfo"></param>
        public abstract void UpdateTeamAct(TeamActInfo tinfo);
    }
}
