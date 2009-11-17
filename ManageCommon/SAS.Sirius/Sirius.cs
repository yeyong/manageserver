using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;
using SAS.Logic;
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
        public static int CreateTeam(TeamInfo teaminfo, out string members)
        {
            int teamID = 0;
            string relmembers = "";
            members = CheckMemberInfo(teaminfo.TeamMember, out relmembers);
            teaminfo.TeamMember = relmembers;
            teamID = Data.DbProvider.GetInstance().CreateTeams(teaminfo);
            SASCache.GetCacheService().RemoveObject("/Sirius/TeamInfoList");
            return teamID;
        }

        /// <summary>
        /// 核查团队人员信息
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        private static string CheckMemberInfo(string members,out string relmembers)
        {
            string novmemebers = "";
            members = members == null ? "" : members;
            relmembers = "";

            foreach (string mem in members.Split(','))
            {
                if (mem != "")
                {
                    UserInfo userInfo = Users.GetUserInfo(mem);

                    if (userInfo != null && userInfo.Ps_ug_id != 7)
                    {
                        if (userInfo.Ps_ug_id <= 3 && userInfo.Ps_ug_id > 0)
                        {
                            relmembers += mem + ",";
                            continue;
                        }
                    }
                    novmemebers += mem + ",";
                }
            }

            return novmemebers;
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
        public static bool UpdateTeamInfo(TeamInfo team, out string members)
        {
            string realmembers = "";
            members = CheckMemberInfo(team.TeamMember, out realmembers);
            team.TeamMember = realmembers;

            if (!Data.DbProvider.GetInstance().UpdateTeamInfo(team))
            {
                return false;
            }
            SASCache.GetCacheService().RemoveObject("/Sirius/TeamInfoList");
            return true;
        }
    }
}
