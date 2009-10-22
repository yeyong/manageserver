using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    public class Forums
    {
        /// <summary>
        /// 返回全部版块列表并缓存
        /// </summary>
        /// <returns>板块信息数组</returns>
        public static List<ForumInfo> GetForumList()
        {
            List<ForumInfo> forumlist = new List<ForumInfo>();
            DataTable dt = GetForumListForDataTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ForumInfo forum = new ForumInfo();
                    forum.Fid = TypeConverter.StrToInt(dr["fid"].ToString(), 0);
                    forum.Parentid = TypeConverter.StrToInt(dr["parentid"].ToString(), 0);
                    forum.Layer = TypeConverter.StrToInt(dr["layer"].ToString(), 0);
                    forum.Pathlist = dr["pathlist"].ToString();
                    forum.Parentidlist = dr["parentidlist"].ToString();
                    forum.Subforumcount = TypeConverter.StrToInt(dr["subforumcount"].ToString(), 0);
                    forum.Name = dr["name"].ToString();
                    forum.Status = TypeConverter.StrToInt(dr["status"].ToString(), 0);
                    forum.Colcount = TypeConverter.StrToInt(dr["colcount"].ToString(), 0);
                    forum.Displayorder = TypeConverter.StrToInt(dr["displayorder"].ToString(), 0);
                    forum.Templateid = TypeConverter.StrToInt(dr["templateid"].ToString(), 0);
                    forum.Topics = TypeConverter.StrToInt(dr["topics"].ToString(), 0);
                    forum.CurrentTopics = TypeConverter.StrToInt(dr["curtopics"].ToString(), 0);
                    forum.Posts = TypeConverter.StrToInt(dr["posts"].ToString(), 0);
                    //当前版块的最后发帖日期为空，则表示今日发帖数为0 
                    if (Utils.StrIsNullOrEmpty(dr["lastpost"].ToString()))
                        dr["todayposts"] = 0;
                    else
                    {
                        //当系统日期与最发发帖日期不同时，则表示今日发帖数为0 
                        if (Convert.ToDateTime(dr["lastpost"]).ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                            dr["todayposts"] = 0;
                    }
                    forum.Todayposts = TypeConverter.StrToInt(dr["todayposts"].ToString(), 0);
                    forum.Lasttid = TypeConverter.StrToInt(dr["lasttid"].ToString(), 0);
                    forum.Lasttitle = dr["lasttitle"].ToString();
                    forum.Lastpost = dr["lastpost"].ToString();
                    forum.Lastposterid = TypeConverter.StrToInt(dr["lastposterid"].ToString(), 0);
                    forum.Lastposter = dr["lastposter"].ToString();
                    forum.Allowsmilies = TypeConverter.StrToInt(dr["allowsmilies"].ToString(), 0);
                    forum.Allowrss = TypeConverter.StrToInt(dr["allowrss"].ToString(), 0);
                    forum.Allowhtml = TypeConverter.StrToInt(dr["allowhtml"].ToString(), 0);
                    forum.Allowbbcode = TypeConverter.StrToInt(dr["allowbbcode"].ToString(), 0);
                    forum.Allowimgcode = TypeConverter.StrToInt(dr["allowimgcode"].ToString(), 0);
                    ////forum.Allowblog = TypeConverter.StrToInt(dr["allowblog"].ToString(), 0);
                    ////forum.Istrade = TypeConverter.StrToInt(dr["istrade"].ToString(), 0);
                    ////forum.Allowpostspecial = TypeConverter.StrToInt(dr["allowpostspecial"].ToString(), 0);
                    ////forum.Allowspecialonly = TypeConverter.StrToInt(dr["allowspecialonly"].ToString(), 0);
                    forum.Alloweditrules = TypeConverter.StrToInt(dr["alloweditrules"].ToString(), 0);
                    forum.Allowthumbnail = TypeConverter.StrToInt(dr["allowthumbnail"].ToString(), 0);
                    forum.Allowtag = TypeConverter.StrToInt(dr["allowtag"].ToString(), 0);
                    forum.Recyclebin = TypeConverter.StrToInt(dr["recyclebin"].ToString(), 0);
                    forum.Modnewposts = TypeConverter.StrToInt(dr["modnewposts"].ToString(), 0);
                    forum.Jammer = TypeConverter.StrToInt(dr["jammer"].ToString(), 0);
                    forum.Disablewatermark = TypeConverter.StrToInt(dr["disablewatermark"].ToString(), 0);
                    forum.Inheritedmod = TypeConverter.StrToInt(dr["inheritedmod"].ToString(), 0);
                    forum.Autoclose = TypeConverter.StrToInt(dr["autoclose"].ToString(), 0);

                    forum.Password = dr["password"].ToString();
                    forum.Icon = dr["icon"].ToString();
                    ////forum.Postcredits = dr["postcredits"].ToString();
                    ////forum.Replycredits = dr["replycredits"].ToString();
                    forum.Redirect = dr["redirect"].ToString();
                    forum.Attachextensions = dr["attachextensions"].ToString();
                    forum.Rules = dr["rules"].ToString();
                    forum.Topictypes = dr["topictypes"].ToString();
                    forum.Viewperm = dr["viewperm"].ToString();
                    forum.Postperm = dr["postperm"].ToString();
                    forum.Replyperm = dr["replyperm"].ToString();
                    forum.Getattachperm = dr["getattachperm"].ToString();
                    forum.Postattachperm = dr["postattachperm"].ToString();
                    forum.Moderators = dr["moderators"].ToString();
                    forum.Description = dr["description"].ToString();
                    forum.Applytopictype = TypeConverter.StrToInt(dr["applytopictype"] == DBNull.Value ? "0" : dr["applytopictype"].ToString(), 0);
                    forum.Postbytopictype = TypeConverter.StrToInt(dr["postbytopictype"] == DBNull.Value ? "0" : dr["postbytopictype"].ToString(), 0);
                    forum.Viewbytopictype = TypeConverter.StrToInt(dr["viewbytopictype"] == DBNull.Value ? "0" : dr["viewbytopictype"].ToString(), 0);
                    forum.Topictypeprefix = TypeConverter.StrToInt(dr["topictypeprefix"] == DBNull.Value ? "0" : dr["topictypeprefix"].ToString(), 0);
                    forum.Permuserlist = dr["permuserlist"].ToString();
                    forum.Seokeywords = dr["seokeywords"].ToString();
                    forum.Seodescription = dr["seodescription"].ToString();
                    forum.Rewritename = dr["rewritename"].ToString();
                    forumlist.Add(forum);
                }
            }
            return forumlist;
        }

        /// <summary>
        /// 获取所有版块信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetForumListForDataTable()
        {
            return DatabaseProvider.GetInstance().GetForumsTable();
        }

        /// <summary>
        /// 更新版块和用户模板Id
        /// </summary>
        /// <param name="templateIdList">模板Id列表</param>
        public static void UpdateForumAndUserTemplateId(string templateIdList)
        {
            DatabaseProvider.GetInstance().UpdateForumAndUserTemplateId(templateIdList);
        }
    }
}
