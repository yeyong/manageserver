using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// AdminForumFactory 的摘要说明。
    /// 后台版块管理类
    /// </summary>
    public class AdminForums : Forums
    {
        /// <summary>
        /// 向版块列表中插入新的版块信息
        /// </summary>
        /// <param name="foruminfo"></param>
        /// <param name="moderatorsInfo">版主信息</param>
        /// <param name="adminUid">管理员Id</param>
        /// <param name="adminUserName">管理员用户名</param>
        /// <param name="adminUserGruopId">管理员用户组Id</param>
        /// <param name="adminUserGroupTitle">管理员用户组名称</param>
        /// <param name="adminIp">管理员IP</param>
        /// <returns></returns>
        public static int CreateForums(ForumInfo forumInfo, out string moderatorsInfo, int adminUid, string adminUserName, int adminUserGruopId, string adminUserGroupTitle, string adminIp)
        {
            int fid = Data.DataProvider.Forums.CreateForumInfo(forumInfo);
            SetForumsPathList();
            //moderatorsInfo = SetForumsModerators(fid.ToString(), forumInfo.Moderators, forumInfo.Inheritedmod).Replace("'", "’");
            moderatorsInfo = "";
            SASCache.GetCacheService().RemoveObject("/SAS/UI/ForumListBoxOptions");
            SASCache.GetCacheService().RemoveObject("/SAS/ForumList");
            SASCache.GetCacheService().RemoveObject("/Aggregation/HotForumList");
            SASCache.GetCacheService().RemoveObject("/Aggregation/ForumHotTopicList");
            SASCache.GetCacheService().RemoveObject("/Aggregation/ForumNewTopicList");
            SASCache.GetCacheService().RemoveObject("/SAS/DropdownOptions");
            SASCache.GetCacheService().RemoveObject("/SAS/ForumListMenuDiv");
            AdminVistLogs.InsertLog(adminUid, adminUserName, adminUserGruopId, adminUserGroupTitle, adminIp, "添加论坛版块", "添加论坛版块,名称为:" + forumInfo.Name);
            return fid;
        }

        /// <summary>
        /// 更新论坛版块(分类)的相关信息
        /// </summary>
        /// <param name="forumInfo"></param>
        /// <returns></returns>
        public static string UpdateForumInfo(ForumInfo forumInfo)
        {
            Data.DataProvider.Forums.UpdateForumInfo(forumInfo);
            SASCache.GetCacheService().RemoveObject("/SAS/ForumList");
            SetForumsPathList();
            //string result = SetForumsModerators(forumInfo.Fid.ToString(), forumInfo.Moderators, forumInfo.Inheritedmod);
            string result = "";
            SASCache.GetCacheService().RemoveObject("/SAS/UI/ForumListBoxOptions");
            SASCache.GetCacheService().RemoveObject("/SAS/ForumList");
            SASCache.GetCacheService().RemoveObject("/SAS/TopicTypesOption" + forumInfo.Fid);
            SASCache.GetCacheService().RemoveObject("/SAS/TopicTypesLink" + forumInfo.Fid);
            SASCache.GetCacheService().RemoveObject("/Aggregation/HotForumList");
            SASCache.GetCacheService().RemoveObject("/Aggregation/ForumHotTopicList");
            SASCache.GetCacheService().RemoveObject("/Aggregation/ForumNewTopicList");
            return result;
        }

        /////// <summary>
        /////// 设置指定论坛版块版主
        /////// </summary>
        /////// <param name="fid">指定的论坛版块id</param>
        /////// <param name="moderators">相关要设置的版主名称(注:用","号分割)</param>
        /////// <param name="inherited">是否使用继承选项 1为使用  0为不使用</param>
        /////// <returns></returns>
        ////public static string SetForumsModerators(string fid, string moderators, int inherited)
        ////{
        ////    //清除已有论坛的版主设置
        ////    Data.Moderators.DeleteModeratorByFid(int.Parse(fid));

        ////    //使用继承机制时
        ////    if (inherited == 1)
        ////    {
        ////        string parentid = fid;
        ////        string parendidlist = "-1";
        ////        while (true)
        ////        {
        ////            DataTable dt = SAS.Data.DataProvider.Forums.GetParentIdByFid(int.Parse(parentid));
        ////            if (dt.Rows.Count > 0) parentid = dt.Rows[0][0].ToString();
        ////            else
        ////                break;

        ////            if ((parentid == "0") || (parentid == ""))
        ////                break;

        ////            parendidlist = parendidlist + "," + parentid;
        ////        }

        ////        int count = 1;
        ////        foreach (DataRow dr in SAS.Data.DataProvide.Moderators.GetUidModeratorByFid(parendidlist).Rows)
        ////        {
        ////            SAS.Data.DataProvide.Moderators.AddModerator(int.Parse(dr[0].ToString()), int.Parse(fid), count, 1);

        ////            count++;
        ////        }
        ////    }

        ////    InsertForumsModerators(fid, moderators, 1, 0);

        ////    return UpdateUserInfoWithModerator(moderators);
        ////}

        /// <summary>
        /// 设置版块列表中层数(layer)和父列表(parentidlist)字段
        /// </summary>
        public static void SetForumslayer()
        {
            foreach (ForumInfo singleForumInfo in Forums.GetForumList())
            {
                int layer = 0;
                string parentidlist = "";
                int parentid = singleForumInfo.Parentid;

                //如果是(分类)顶层则直接更新数据库
                if (parentid == 0)
                {
                    ForumInfo forumInfo = Forums.GetForumInfo(singleForumInfo.Fid);
                    if (forumInfo.Layer != layer)
                    {
                        forumInfo.Layer = layer;
                        forumInfo.Parentidlist = "0";
                        UpdateForumInfo(forumInfo);
                    }
                    continue;
                }

                do
                { //更新子版块的层数(layer)和父列表(parentidlist)字段
                    int temp = parentid;

                    parentid = Forums.GetForumInfo(parentid).Parentid;
                    layer++;
                    if (parentid != 0)
                    {
                        parentidlist = temp + "," + parentidlist;
                    }
                    else
                    {
                        parentidlist = (temp + "," + parentidlist).TrimEnd(',');
                        ForumInfo forumInfo = Forums.GetForumInfo(singleForumInfo.Fid);
                        if (forumInfo.Layer != layer || forumInfo.Parentidlist != parentidlist)
                        {
                            forumInfo.Layer = layer;
                            forumInfo.Parentidlist = parentidlist;
                            UpdateForumInfo(forumInfo);
                        }
                        break;
                    }
                } while (true);
            }
        }

        /// <summary>
        /// 设置版块列表中论坛路径(pathlist)字段
        /// </summary>
        public static void SetForumsPathList()
        {
            GeneralConfigInfo config = GeneralConfigs.Deserialize(Utils.GetMapPath(BaseConfigs.GetSitePath + "config/general.config"));
            SetForumsPathList(config.Aspxrewrite == 1, config.Extname);
        }


        /// <summary>
        /// 按指定的文件扩展名称设置版块列表中论坛路径(pathlist)字段
        /// </summary>
        /// <param name="extname">扩展名称,如:aspx , html 等</param>
        public static void SetForumsPathList(bool isaspxrewrite, string extname)
        {
            DataTable dt = Forums.GetForumListForDataTable();
            string forumPath = BaseConfigs.GetSitePath;

            foreach (DataRow dr in dt.Rows)
            {
                string pathList = "";

                if (dr["parentidlist"].ToString().Trim() == "0")
                {
                    pathList = "<a href=\"" + (dr["rewritename"].ToString().Trim() == string.Empty ? string.Empty : forumPath) + Urls.ShowForumAspxRewrite(Utils.StrToInt(dr["fid"], 0), 0, dr["rewritename"].ToString()) + "\">" + dr["name"].ToString().Trim() + "</a>";
                }
                else
                {
                    foreach (string parentid in dr["parentidlist"].ToString().Trim().Split(','))
                    {
                        if (parentid.Trim() != "")
                        {
                            DataRow[] drs = dt.Select("[fid]=" + parentid);
                            if (drs.Length > 0)
                            {
                                pathList += "<a href=\"" + (drs[0]["rewritename"].ToString().Trim() == string.Empty ? string.Empty : forumPath) + Urls.ShowForumAspxRewrite(Utils.StrToInt(drs[0]["fid"], 0), 0, drs[0]["rewritename"].ToString()) + "\">" + drs[0]["name"].ToString().Trim() + "</a>";
                            }
                        }
                    }
                    string url = Urls.ShowForumAspxRewrite(Utils.StrToInt(dr["fid"], 0), 0, dr["rewritename"].ToString());
                    pathList += "<a href=\"" + (dr["rewritename"].ToString().Trim() == "" ? "" : forumPath) + Urls.ShowForumAspxRewrite(Utils.StrToInt(dr["fid"], 0), 0, dr["rewritename"].ToString()) + "\">" + dr["name"].ToString().Trim() + "</a>";
                }
                foreach (ForumInfo forumInfo in SAS.Data.DataProvider.Forums.GetForumList())
                {
                    if (forumInfo.Fid == int.Parse(dr["fid"].ToString()))
                    {
                        forumInfo.Pathlist = pathList;
                        Data.DataProvider.Forums.UpdateForumInfo(forumInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 设置论坛字版数和显示顺序
        /// </summary>
        public static void SetForumsSubForumCountAndDispalyorder()
        {
            DataTable dt = Forums.GetForumListForDataTable();

            foreach (DataRow dr in dt.Rows)
            {
                SAS.Data.DataProvider.Forums.UpdateSubForumCount(int.Parse(dt.Select("parentid=" + dr["fid"].ToString()).Length.ToString()), int.Parse(dr["fid"].ToString()));
            }

            if (dt.Rows.Count == 1) return;
            //因为不能拖动论坛分类，所以注释以下代码。sun 2009-1-7
            /*int displayorder = 1;
            string fidlist;
            foreach (DataRow dr in dt.Select("parentid=0"))
            {
                if (dr["parentid"].ToString() == "0")
                {
                    ChildNode = "0";
                    fidlist = ("," + FindChildNode(dr["fid"].ToString())).Replace(",0,", "");

                    foreach (string fidstr in fidlist.Split(','))
                    {
                        DatabaseProvider.GetInstance().UpdateDisplayorderInForumByFid(displayorder, int.Parse(fidstr));
                        displayorder++;
                    }

                }
            }*/
        }

        /// <summary>
        /// 移动论坛版块
        /// </summary>
        /// <param name="currentfid">当前论坛版块id</param>
        /// <param name="targetfid">目标论坛版块id</param>
        /// <param name="isaschildnode">是否作为子论坛移动</param>
        /// <returns></returns>
        public static bool MovingForumsPos(string currentfid, string targetfid, bool isaschildnode)
        {
            string extname = GeneralConfigs.Deserialize(Utils.GetMapPath(BaseConfigs.GetSitePath + "config/general.config")).Extname;

            SAS.Data.DataProvider.Forums.MovingForumsPos(currentfid, targetfid, isaschildnode, extname);
            AdminForums.SetForumslayer();
            AdminForums.SetForumsSubForumCountAndDispalyorder();
            AdminForums.SetForumsPathList();

            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/UI/ForumListBoxOptions");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/ForumList");

            return true;
        }
    }
}
