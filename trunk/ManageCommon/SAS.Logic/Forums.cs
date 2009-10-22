using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 版块操作类
    /// </summary>
    public class Forums
    {
        /// <summary>
        /// 返回全部版块列表并缓存
        /// </summary>
        /// <returns>板块信息数组</returns>
        public static List<ForumInfo> GetForumList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<ForumInfo> forumList = cache.RetrieveObject("/SAS/ForumList") as List<ForumInfo>;

            if (forumList == null)
            {
                forumList = SAS.Data.DataProvider.Forums.GetForumList();
                //声明新的缓存策略接口
                SAS.Cache.ICacheStrategy ics = new SASCacheStrategy();
                ics.TimeOut = 5;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/SAS/ForumList", forumList);
                cache.LoadDefaultCacheStrategy();
            }
            return forumList;
        }

        /// <summary>
        /// 获得指定的分类或版块信息
        /// </summary>
        /// <param name="fid">分类或版块ID</param>
        /// <returns>返回分类或版块的信息</returns>
        public static ForumInfo GetForumInfo(int fid)
        {
            if (fid < 1)
                return null;

            List<ForumInfo> forumList = GetForumList();
            if (forumList == null)
                return null;

            foreach (ForumInfo foruminfo in forumList)
            {
                if (foruminfo.Fid == fid)
                {
                    foruminfo.Pathlist = foruminfo.Pathlist.Replace("a><a", "a> &raquo; <a");
                    return foruminfo.Clone();
                }
            }
            return null;
        }

        /// <summary>
        /// 得到当前版块的主题类型选项
        /// </summary>
        /// <param name="fid">板块ID</param>
        /// <returns>主题类型字符串</returns>
        public static string GetCurrentTopicTypesOption(int fid, string topictypes)
        {
            //判断当前版块没有相应主题分类时
            if (Utils.StrIsNullOrEmpty(topictypes) || topictypes == "|")
                return "";

            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string topictypeoptions = cache.RetrieveObject("/SAS/TopicTypesOption" + fid) as string;
            if (topictypeoptions == null)
            {
                StringBuilder builder = new StringBuilder("<option value=\"0\">分类</option>");

                foreach (string topictype in topictypes.Split('|'))
                {
                    if (!Utils.StrIsNullOrEmpty(topictype.Trim()))
                        builder.AppendFormat("<option value=\"{0}\">{1}</option>", topictype.Split(',')[0], topictype.Split(',')[1]);
                }
                topictypeoptions = builder.ToString();
                cache.AddObject("/SAS/TopicTypesOption" + fid, topictypeoptions);
            }
            return topictypeoptions;
        }

    }
}
