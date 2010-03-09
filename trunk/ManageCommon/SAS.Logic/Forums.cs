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
        /// 获得版块下的子版块列表
        /// </summary>
        /// <param name="fid">版块id(当为0时则获取'版块分类'信息)</param>
        /// <returns>子版块列表</returns>
        public static DataTable GetForumList(int fid)
        {
            return fid >= 0 ? GetSubForumListTable(fid) : new DataTable();
        }

        /// <summary>
        /// 获取指定FID的板块信息列表
        /// </summary>
        /// <param name="fidList">板块FID列表</param>
        /// <returns></returns>
        public static List<ForumInfo> GetForumList(string fidList)
        {
            List<ForumInfo> forumList = new List<ForumInfo>();
            foreach (ForumInfo info in GetForumList())
            {
                foreach (string fid in fidList.Split(','))
                {
                    if (fid == info.Fid.ToString())
                        forumList.Add(info);
                }
            }
            return forumList;
        }

        public static DataTable GetSubForumListTable(int fid)
        {
            DataTable dt = SAS.Data.DataProvider.Forums.GetSubForumTable(fid);

            ////if (dt != null)
            ////{
            ////    int status = 0; //是否显示
            ////    int colcount = 1; //设置该论坛的子论坛在列表时分几列显示                

            ////    foreach (DataRow dr in dt.Rows)
            ////    {
            ////        //如果板块可见
            ////        if (TypeConverter.ObjectToInt(dr["status"]) > 0)
            ////        {
            ////            if (colcount > 1)
            ////            {
            ////                dr["status"] = ++status;
            ////                dr["colcount"] = colcount;
            ////            }
            ////            //如果有子板块且按列显示
            ////            else if (TypeConverter.ObjectToInt(dr["subforumcount"]) > 0 && TypeConverter.ObjectToInt(dr["colcount"]) > 0)
            ////            {
            ////                colcount = Utils.StrToInt(dr["colcount"].ToString(), 0);
            ////                status = colcount;
            ////                dr["status"] = status + 1;
            ////            }
            ////        }
            ////    }
            ////}
            return dt;
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
        /// 获取所有版块信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetForumListForDataTable()
        {
            return Data.DataProvider.Forums.GetForumListForDataTable();
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

        /// <summary>
        /// 获取非默认模板数
        /// </summary>
        /// <returns></returns>
        public static int GetSpecifyForumTemplateCount()
        {
            int count = 0;
            foreach (ForumInfo forumInfo in GetForumList())
            {
                if (forumInfo.Templateid != 0 && forumInfo.Templateid != GeneralConfigs.GetDefaultTemplateID())
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 更新版块显示顺序，将大于当前显示顺序的版块显示顺序加1
        /// </summary>
        /// <param name="minDisplayOrder"></param>
        public static void UpdateFourmsDisplayOrder(int minDisplayOrder)
        {
            Data.DataProvider.Forums.UpdateFourmsDisplayOrder(minDisplayOrder);
        }

        /// <summary>
        /// 更新版本子版数
        /// </summary>
        /// <param name="fid">版块Id</param>
        public static void UpdateSubForumCount(int fid)
        {
            Data.DataProvider.Forums.UpdateSubForumCount(fid);
        }

        /// <summary>
        /// 检查rewritename是否存在或非法
        /// </summary>
        /// <param name="rewriteName"></param>
        /// <returns>如果存在或者非法的Rewritename则返回true,否则为false</returns>
        public static bool CheckRewriteNameInvalid(string rewriteName)
        {
            //先检查此name是否非法
            foreach (string illegalName in "install,upgrade,admin,aspx,tools,archive,space".Split(','))
            {
                if (rewriteName.IndexOf(illegalName) != -1)
                    return true;
            }

            if (!Regex.IsMatch(rewriteName, @"([\w|\-|_])+"))
                return true;

            //再检查是否存在
            return SAS.Data.DataProvider.Forums.CheckRewriteNameInvalid(rewriteName);
        }

        /// <summary>
        /// 返回用户是否有权在该版块上传附件
        /// </summary>
        /// <param name="Permuserlist">查看当前版块的相关权限</param>
        /// <param name="userid">查看权限的用户id</param>
        /// <returns>bool</returns>
        public static bool AllowPostAttachByUserID(string permUserList, int userId)
        {
            return ValidateSpecialUserPerm(permUserList, userId, ForumSpecialUserPower.PostAttachByUser);
        }

        /// <summary>
        /// 返回用户所在的用户组是否有权在该版块上传附件
        /// </summary>
        /// <param name="postattachperm"></param>
        /// <param name="usergroupid"></param>
        /// <returns></returns>
        public static bool AllowPostAttach(string postattachperm, int usergroupid)
        {
            return HasPerm(postattachperm, usergroupid);
        }

        /// <summary>
        /// 返回用户所在的用户组是否有权在该版块发主题或恢复
        /// </summary>
        /// <param name="perm">用户组</param>
        /// <param name="usergroupid">用户过在组别</param>
        /// <returns>bool</returns>
        private static bool HasPerm(string perm, int usergroupid)
        {
            if (Utils.StrIsNullOrEmpty(perm))
                return true;

            return Utils.InArray(usergroupid.ToString(), perm);
        }

        /// <summary>
        /// 检查特殊用户权限
        /// </summary>
        /// <param name="permUserList">特殊用户列表</param>
        /// <param name="userId">查看权限用户ID</param>
        /// <param name="forumSpecialUserPower">论坛特殊用户权限</param>
        /// <returns></returns>
        private static bool ValidateSpecialUserPerm(string permUserList, int userId, ForumSpecialUserPower forumSpecialUserPower)
        {
            if (!Utils.StrIsNullOrEmpty(permUserList))
            {
                ForumSpecialUserPower forumspecialuserpower = (ForumSpecialUserPower)GetForumSpecialUserPower(permUserList, userId);
                if (((int)(forumspecialuserpower & forumSpecialUserPower)) > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取指定版块特殊用户的权限
        /// </summary>
        /// <param name="Permuserlist"></param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        private static int GetForumSpecialUserPower(string Permuserlist, int userid)
        {
            foreach (string currentinf in Permuserlist.Split('|'))
            {
                if (!Utils.StrIsNullOrEmpty(currentinf) && currentinf.Split(',')[1] == userid.ToString())
                    return TypeConverter.StrToInt(currentinf.Split(',')[2]);
            }
            return 0;
        }

    }
}
