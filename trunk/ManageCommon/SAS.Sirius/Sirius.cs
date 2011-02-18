using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;
using SAS.Cache;
using SAS.Sirius.Data;

namespace SAS.Sirius
{
    /// <summary>
    /// Sirius studio 实体操作类
    /// </summary>
    public class Sirius
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
        /// <returns>团队ID</returns>
        public static int CreateTeam(TeamInfo teaminfo, out string result)
        {
            int teamID = 0;
            string teammember = teaminfo.TeamMember;
            teaminfo.TeamMember = "";
            teamID = Data.DbProvider.GetInstance().CreateTeams(teaminfo);
            result = SetTeamMember(teamID, teammember);
            SASCache.GetCacheService().RemoveObject("/Sirius/TeamInfoList");
            return teamID;
        }

        /// <summary>
        /// 获取全部团队信息
        /// </summary>
        /// <returns></returns>
        public static SAS.Common.Generic.List<TeamInfo> GetAllTeamInfoList()
        {
            return DTOProvider.GetAllTeamInfoList();
        }

        /// <summary>
        /// 根据团队ID获取团队信息
        /// </summary>
        /// <param name="teamID">团队ID</param>
        /// <returns></returns>
        public static TeamInfo GetTeamInfoByTeamID(int teamID)
        {
            return DTOProvider.GetTeamInfoEntity(Data.DbProvider.GetInstance().GetTeamInfoByID(teamID));
        }

        /// <summary>
        /// 根据域名获取团队信息
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static TeamInfo GetTeamInfoByDomain(string domain)
        {
            return DTOProvider.GetTeamInfoEntity(Data.DbProvider.GetInstance().GetTeamInfoByDomain(domain));
        }

        /// <summary>
        /// 更新团队信息
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public static bool UpdateTeamInfo(TeamInfo team, out string result)
        {
            if (!Data.DbProvider.GetInstance().UpdateTeamInfo(team))
            {
                result = "";
                return false;
            }
            result = SetTeamMember(team.TeamID, team.TeamMember);
            SASCache.GetCacheService().RemoveObject("/Sirius/TeamInfoList");
            return true;
        }

        /// <summary>
        /// 设置团队成员
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public static string SetTeamMember(int tid, string members)
        {
            return Data.DbProvider.GetInstance().SetTeamMemberList(members, tid);
        }
    }
}
