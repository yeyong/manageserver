using System;
using System.Collections;
using System.Data;
using System.Text;

using SAS.Cache;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Sirius.Data
{
    public class DTOProvider
    {
        /// <summary>
        /// 获取全部团队信息列表
        /// </summary>
        /// <returns></returns>
        public static SAS.Common.Generic.List<TeamInfo> GetAllTeamInfoList()
        {
            SASCache cache = SASCache.GetCacheService();
            SAS.Common.Generic.List<TeamInfo> acic = cache.RetrieveObject("/Sirius/TeamInfoList") as SAS.Common.Generic.List<TeamInfo>;

            if (acic == null)
            {
                acic = new SAS.Common.Generic.List<TeamInfo>();
                acic = Data.DbProvider.GetInstance().GetAllTeamList();
                cache.AddObject("/Sirius/TeamInfoList", (ICollection)acic);
            }
            return acic;
        }
        /// <summary>
        /// 团队活动信息实体列表
        /// </summary>
        public static List<TeamActInfo> GetTeamActInfoList(IDataReader reader)
        {
            List<TeamActInfo> tlist = new List<TeamActInfo>();
            while (reader.Read())
            {
                TeamActInfo tinfo = new TeamActInfo();
                tinfo.Id = TypeConverter.StrToInt(reader["id"].ToString());
                tinfo.Name = reader["name"].ToString();
                tinfo.Start = reader["start"].ToString();
                tinfo.End = reader["end"].ToString();
                tinfo.Shortdesc = reader["shortdesc"].ToString();
                tinfo.Img = reader["img"].ToString();
                tinfo.Imgbak = reader["imgbak"].ToString();
                tinfo.Teamid = TypeConverter.StrToInt(reader["teamid"].ToString());
                tinfo.Atype = TypeConverter.StrToInt(reader["atype"].ToString());
                tinfo.Piccollect = reader["piccollect"].ToString();
                tlist.Add(tinfo);
            }
            reader.Close();
            return tlist;
        }
        /// <summary>
        /// 团队活动信息实体列表
        /// </summary>
        public static TeamActInfo GetTeamActEntity(IDataReader reader)
        {
            if (reader.Read())
            {
                TeamActInfo tinfo = new TeamActInfo();
                tinfo.Id = TypeConverter.StrToInt(reader["id"].ToString());
                tinfo.Name = reader["name"].ToString();
                tinfo.Start = reader["start"].ToString();
                tinfo.End = reader["end"].ToString();
                tinfo.Shortdesc = reader["shortdesc"].ToString();
                tinfo.Img = reader["img"].ToString();
                tinfo.Imgbak = reader["imgbak"].ToString();
                tinfo.Teamid = TypeConverter.StrToInt(reader["teamid"].ToString());
                tinfo.Atype = TypeConverter.StrToInt(reader["atype"].ToString());
                tinfo.Piccollect = reader["piccollect"].ToString();
                reader.Close();
                return tinfo;
            }
            return null;
        }

        /// <summary>
        /// 获取团队信息实体
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static TeamInfo GetTeamInfoEntity(IDataReader reader)
        {
            if (reader.Read())
            {
                TeamInfo team = new TeamInfo();
                team.TeamID = TypeConverter.ObjectToInt(reader["teamID"].ToString(), 0);
                team.Name = reader["name"].ToString();
                team.Teamdomain = reader["teamdomain"].ToString();
                team.Templateid = TypeConverter.ObjectToInt(reader["templateid"].ToString(), 0);
                team.BuildDate = Utils.GetStandardDate(reader["builddate"].ToString());
                team.CreateDate = Utils.GetStandardDate(reader["createdate"].ToString());
                team.UpdateDate = Utils.GetStandardDate(reader["updatedate"].ToString());
                team.Imgs = reader["imgs"].ToString();
                team.Bio = reader["bio"].ToString();
                team.Content1 = reader["content1"].ToString();
                team.Content2 = reader["content2"].ToString();
                team.Content3 = reader["content3"].ToString();
                team.Content4 = reader["content4"].ToString();
                team.Stutas = TypeConverter.ObjectToInt(reader["stutas"], 0);
                team.Pageviews = TypeConverter.ObjectToInt(reader["pageviews"], 0);
                team.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"], 0);
                team.TeamMember = reader["teammember"].ToString();
                team.Seokeywords = reader["seokeywords"].ToString();
                team.Seodescription = reader["seodescription"].ToString();
                team.Creater = reader["creater"].ToString();

                reader.Close();
                return team;
            }
            return null;
        }
    }
}
