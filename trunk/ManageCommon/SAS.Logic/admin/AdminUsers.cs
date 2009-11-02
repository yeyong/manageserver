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
        /// 更新用户全部信息
        /// </summary>
        /// <param name="__userinfo"></param>
        /// <returns></returns>
        public static bool UpdateUserAllInfo(UserInfo userInfo)
        {
            Users.UpdateUser(userInfo);

            //当用户不是版主(超级版主)或管理员
            ////if ((userInfo.Adminid == 0) || (userInfo.Adminid > 3))
            ////{
            ////    //删除用户在版主列表中相关数据
            ////    Data.Moderators.DeleteModerator(userInfo.Uid);
            ////    //同时更新版块相关的版主信息
            ////    UpdateForumsFieldModerators(userInfo.Username);
            ////}

            #region 以下为更新该用户的扩展信息

            string signature = Utils.HtmlEncode(LogicUtils.BanWordFilter(userInfo.Pd_sign));

            UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(userInfo.Ps_ug_id);
            GeneralConfigInfo config = GeneralConfigs.GetConfig();

            ////PostpramsInfo postPramsInfo = new PostpramsInfo();
            ////postPramsInfo.Usergroupid = usergroupinfo.Groupid;
            ////postPramsInfo.Attachimgpost = config.Attachimgpost;
            ////postPramsInfo.Showattachmentpath = config.Showattachmentpath;
            ////postPramsInfo.Hide = 0;
            ////postPramsInfo.Price = 0;
            ////postPramsInfo.Sdetail = userInfo.Signature;
            ////postPramsInfo.Smileyoff = 1;
            ////postPramsInfo.Bbcodeoff = 1 - usergroupinfo.Allowsigbbcode;
            ////postPramsInfo.Parseurloff = 1;
            ////postPramsInfo.Showimages = usergroupinfo.Allowsigimgcode;
            ////postPramsInfo.Allowhtml = 0;
            ////postPramsInfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
            ////postPramsInfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
            ////postPramsInfo.Smiliesmax = config.Smiliesmax;
            ////postPramsInfo.Signature = 1;
            ////postPramsInfo.Onlinetimeout = config.Onlinetimeout;

            userInfo.Pd_sign = signature;
            userInfo.Pd_authstr = LogicUtils.CreateAuthStr(20);
            ////userInfo.Sightml = UBB.UBBToHTML(postPramsInfo);
            Users.UpdateUser(userInfo);

            #endregion

            ////Users.UpdateUserForumSetting(userInfo);
            return true;
        }

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
