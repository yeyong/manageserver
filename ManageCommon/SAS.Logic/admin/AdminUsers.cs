using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// UserFactoryAdmin 的摘要说明。
    /// 后台用户信息操作管理类
    /// </summary>
    public class AdminUsers : Users
    {
        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="oldusername">以前用户的名称</param>
        /// <returns></returns>
        public static bool UserNameChange(UserInfo userInfo, string oldusername)
        {
            //将新主题表
            ////Data.Topics.UpdateTopicLastPoster(userInfo.Uid, userInfo.Username);
            ////Data.Topics.UpdateTopicPoster(userInfo.Uid, userInfo.Username);

            //更新帖子表
            //foreach (DataRow dr in DatabaseProvider.GetInstance().GetTableListIds())            
            ////foreach (DataRow dr in Data.PostTables.GetAllPostTableName().Rows)
            ////{
            ////    Data.Posts.UpdatePostPoster(userInfo.Uid, userInfo.Username, dr["id"].ToString());
            ////}

            //更新短消息
            Data.DataProvider.PrivateMessages.UpdatePMSenderAndReceiver(userInfo.Ps_id, userInfo.Ps_name);
            //更新公告
            Data.DataProvider.Announcements.UpdateAnnouncementPoster(userInfo.Ps_id, userInfo.Ps_name);
            //更新统计表中的信息
            if (Data.DataProvider.Statistics.UpdateStatisticsLastUserName(userInfo.Ps_id, userInfo.Ps_name) != 0)
            {
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/Statistics");
            }

            //更新论坛版主相关信息
            ////foreach (DataRow dr in Data.Forums.GetModerators(oldusername).Rows)
            ////{
            ////    string moderators = "," + dr["moderators"].ToString().Trim() + ",";
            ////    if (moderators.IndexOf("," + oldusername + ",") >= 0)
            ////        Forums.UpdateForumField(Utils.StrToInt(dr["fid"], 0), "moderators", dr["moderators"].ToString().Trim().Replace(oldusername, userInfo.Username));
            ////}
            return true;
        }

        /// <summary>
        /// 删除指定用户的所有信息
        /// </summary>
        /// <param name="uid">指定的用户uid</param>
        /// <param name="delposts">是否删除帖子</param>
        /// <param name="delpms">是否删除短消息</param>
        /// <returns></returns>
        public static bool DelUserAllInf(int uid, bool delposts, bool delpms)
        {
            bool val = Data.DataProvider.Users.DeleteUser(uid, delposts, delpms);
            if (val)
                SASCache.GetCacheService().RemoveObject("/SAS/Statistics");

            return val;
        }
    }
}
