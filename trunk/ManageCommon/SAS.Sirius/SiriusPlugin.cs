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
        public override int CreateTeamInfo(TeamInfo teaminfo,out string result)
        {
            return Sirius.CreateTeam(teaminfo, out result);
        }

        /// <summary>
        /// 创建团队成果信息
        /// </summary>
        /// <param name="workinfo">成果实体</param>
        public override int CreateWork(TeamWorkInfo workinfo, out string result)
        {
            return Sirius.CreateWork(workinfo, out result);
        }

        /// <summary>
        /// 创建团队活动
        /// </summary>
        public override int CreateAct(TeamActInfo tacinfo)
        {
            return Sirius.CreateAct(tacinfo);
        }

        /// <summary>
        /// 获得所有团队信息
        /// </summary>
        public override DataTable GetAllTeam()
        {
            return Sirius.GetAllTeam();
        }

        /// <summary>
        /// 获得所有团队信息
        /// </summary>
        public override SAS.Common.Generic.List<TeamInfo> GetAllTeamList()
        {
            return Sirius.GetAllTeamInfoList();
        }

        /// <summary>
        /// 根据ID获取团队信息
        /// </summary>
        /// <param name="teamID"></param>
        public override TeamInfo GetTeamByTeamID(int teamID)
        {
            return Sirius.GetTeamInfoByTeamID(teamID);
        }

        public override bool UpdateTeamInfo(TeamInfo teaminfo, out string result)
        {
            return Sirius.UpdateTeamInfo(teaminfo, out result);
        }

        public override SAS.Common.Generic.List<TeamActInfo> GetTeamActByTid(int tid)
        {
            return Sirius.GetTeamActByTid(tid);
        }

        public override SAS.Common.Generic.List<TeamActInfo> GetTeamActByTidWithCache(int tid)
        {
            return Sirius.GetTeamActByTidWithCache(tid);
        }

        public override TeamActInfo GetTeamActInfo(int aid)
        {
            return Sirius.GetTeamActInfo(aid);
        }

        public override void UpdateTeamAct(TeamActInfo tinfo)
        {
            Sirius.UpdateTeamAct(tinfo);
        }

        public override SAS.Common.Generic.List<TeamWorkInfo> GetTeamWorkByTid(int tid)
        {
            return Sirius.GetTeamWorkByTid(tid);
        }

        /// <summary>
        /// 成果信息
        /// </summary>
        public override TeamWorkInfo GetWorkInfo(int wid)
        {
            return Sirius.GetWorkInfo(wid);
        }

         /// <summary>
        /// 成果信息更新
        /// </summary>
        public override void UpdateWorkInfo(TeamWorkInfo tinfo, out string result)
        {
            Sirius.UpdateWorkInfo(tinfo, out result);
        }

        /// <summary>
        /// 成果信息获取(cache)
        /// </summary>
        public override List<TeamWorkInfo> GetTeamWorksWithCache(int tid)
        {
            return Sirius.GetTeamWorksWithCache(tid);
        }
    }
}
