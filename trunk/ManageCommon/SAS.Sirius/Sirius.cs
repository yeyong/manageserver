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
        /// 获取团队信息缓存
        /// </summary>
        public static TeamInfo GetTeamInfoCache(int tid)
        {
            List<TeamInfo> tlist = GetAllTeamInfoList();
            if (tlist.Count > 0)
            {
                TeamInfo tinfo = new TeamInfo();
                tinfo = tlist.Find(new Predicate<TeamInfo>(delegate(TeamInfo info) { return info.TeamID == tid; }));
                return tinfo;
            }
            return null;
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
            string teammember = team.TeamMember;
            team.TeamMember = "";
            if (!Data.DbProvider.GetInstance().UpdateTeamInfo(team))
            {
                result = "";
                return false;
            }
            result = SetTeamMember(team.TeamID, teammember);
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

        #region 成果操作
        /// <summary>
        /// 创建团队成果信息
        /// </summary>
        /// <param name="workinfo">成果实体</param>
        public static int CreateWork(TeamWorkInfo workinfo, out string result)
        {
            int workid = 0;
            string members = workinfo.Members;
            workinfo.Members = "";
            workid = Data.DbProvider.GetInstance().CreateWork(workinfo);
            result = Data.DbProvider.GetInstance().SetWorkMemberList(members, workid);
            SASCache.GetCacheService().RemoveObject("/Sirius/WorkList_" + workinfo.Teamid);
            return workid;
        }

        /// <summary>
        /// 成果信息获取
        /// </summary>
        /// <param name="tid">团队ID</param>
        public static List<TeamWorkInfo> GetTeamWorkByTid(int tid)
        {
            return DTOProvider.GetTeamWorkInfoList(Data.DbProvider.GetInstance().GetWorksByTid(tid));
        }
        /// <summary>
        /// 成果信息获取(cache)
        /// </summary>
        public static List<TeamWorkInfo> GetTeamWorksWithCache(int tid)
        {
            SASCache cache = SASCache.GetCacheService();
            string cachekey = "/Sirius/WorkList_" + tid;
            SAS.Common.Generic.List<TeamWorkInfo> acic = cache.RetrieveObject(cachekey) as SAS.Common.Generic.List<TeamWorkInfo>;

            if (acic == null)
            {
                acic = new SAS.Common.Generic.List<TeamWorkInfo>();
                acic = GetTeamWorkByTid(tid);
                cache.AddObject(cachekey, (ICollection)acic);
            }
            return acic;
        }

        /// <summary>
        /// 成果信息
        /// </summary>
        public static TeamWorkInfo GetWorkInfo(int wid)
        {
            return DTOProvider.GetWorkEntity(Data.DbProvider.GetInstance().GetWorkInfo(wid));
        }
        /// <summary>
        /// 成果信息更新
        /// </summary>
        public static void UpdateWorkInfo(TeamWorkInfo tinfo,out string result)
        {
            string members = tinfo.Members;
            tinfo.Members = "";
            Data.DbProvider.GetInstance().UpdateWorkInfo(tinfo);
            result = Data.DbProvider.GetInstance().SetWorkMemberList(members, tinfo.Id);
            SASCache.GetCacheService().RemoveObject("/Sirius/WorkList_" + tinfo.Teamid);
        }
        #endregion
    }
}
