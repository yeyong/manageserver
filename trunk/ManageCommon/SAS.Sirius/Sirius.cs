using System;
using System.Collections;
using System.Data;
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
        /// 获取全部团队信息（非缓存类）
        /// </summary>
        public static DataTable GetAllTeam()
        {
            return Data.DbProvider.GetInstance().GetAllTeam();
        }

        /// <summary>
        /// 获取全部团队信息
        /// </summary>
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

        #region 团队活动操作
        /// <summary>
        /// 创建团队活动
        /// </summary>
        public static int CreateAct(TeamActInfo tacinfo)
        {
            int aid = 0;
            aid = Data.DbProvider.GetInstance().CreateAct(tacinfo);
            SASCache.GetCacheService().RemoveObject("/Sirius/ActList_" + tacinfo.Teamid);
            return aid;
        }

        /// <summary>
        /// 团队活动获取
        /// </summary>
        /// <param name="tid">团队ID</param>
        public static List<TeamActInfo> GetTeamActByTid(int tid)
        {
            return DTOProvider.GetTeamActInfoList(Data.DbProvider.GetInstance().GetTeamActListByTid(tid));
        }
        /// <summary>
        /// 团队活动获取（带缓存）
        /// </summary>
        public static List<TeamActInfo> GetTeamActByTidWithCache(int tid)
        {
            SASCache cache = SASCache.GetCacheService();
            string cachekey = "/Sirius/ActList_" + tid;
            SAS.Common.Generic.List<TeamActInfo> acic = cache.RetrieveObject(cachekey) as SAS.Common.Generic.List<TeamActInfo>;

            if (acic == null)
            {
                acic = new SAS.Common.Generic.List<TeamActInfo>();
                acic = GetTeamActByTid(tid);
                cache.AddObject(cachekey, (ICollection)acic);
            }
            return acic;
        }

        /// <summary>
        /// 活动实体获得
        /// </summary>
        /// <param name="aid">活动ID</param>
        public static TeamActInfo GetTeamActInfo(int aid)
        {
            return DTOProvider.GetTeamActEntity(Data.DbProvider.GetInstance().GetTeamActInfo(aid));
        }

        /// <summary>
        /// 更新团队活动
        /// </summary>
        /// <param name="tinfo"></param>
        public static void UpdateTeamAct(TeamActInfo tinfo)
        {
            Data.DbProvider.GetInstance().UpdateTeamAct(tinfo);
            SASCache.GetCacheService().RemoveObject("/Sirius/ActList_" + tinfo.Teamid);
        }
        #endregion
    }
}
