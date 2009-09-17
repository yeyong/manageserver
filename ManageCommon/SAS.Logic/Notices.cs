using System;
using System.Text;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 通知信息操作类
    /// </summary>
    public class Notices
    {
        /// <summary>
        /// 添加指定的通知信息
        /// </summary>
        /// <param name="noticeinfo">要添加的通知信息</param>
        /// <returns></returns>
        public static int CreateNoticeInfo(NoticeInfo noticeinfo)
        {
#if !DEBUG
            if (noticeinfo.Posterid == noticeinfo.Uid)
                return 0;
#endif
            int olid = OnlineUsers.GetOlidByUid(noticeinfo.Uid);
            if (olid > 0)
            {
                OnlineUsers.UpdateNewNotices(olid, 1);
            }
            return SAS.Data.DataProvider.Notices.CreateNoticeInfo(noticeinfo);
        }

        /// <summary>
        /// 删除通知(计划任务调用)
        /// </summary>
        /// <returns></returns>
        public static void DeleteNotice()
        {
            SAS.Data.DataProvider.Notices.DeleteNotice();//删除指定天数内的通知
        }
        /// <summary>
        /// 获取指定用户id及通知类型的通知数
        /// </summary>
        /// <param name="uid">指定用户id</param>
        /// <param name="noticetype">通知类型</param>
        /// <returns></returns>
        public static int GetNoticeCountByUid(Guid uid, Noticetype noticetype)
        {
            return uid != new Guid("00000000-0000-0000-0000-000000000000") ? SAS.Data.DataProvider.Notices.GetNoticeCountByUid(uid, noticetype) : 0;
        }


        /// <summary>
        /// 获取指定用户和分页下的通知
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>通知集合</returns>
        public static NoticeinfoCollection GetNoticeinfoCollectionByUid(Guid uid, Noticetype noticetype, int pageid, int pagesize)
        {
            return (uid != new Guid("00000000-0000-0000-0000-000000000000") && pageid > 0) ? SAS.Data.DataProvider.Notices.GetNoticeinfoCollectionByUid(uid, noticetype, pageid, pagesize) : null;
        }

        ///// <summary>
        ///// 发送回复通知
        ///// </summary>
        ///// <param name="postinfo">回复信息</param>
        ///// <param name="topicinfo">所属主题信息</param>
        ///// <param name="replyuserid">回复的某一楼的作者</param>
        //public static void SendPostReplyNotice(PostInfo postinfo, TopicInfo topicinfo, int replyuserid)
        //{
        //    NoticeInfo noticeinfo = new NoticeInfo();

        //    noticeinfo.Note = Utils.HtmlEncode(string.Format("<a href=\"userinfo.aspx?userid={0}\">{1}</a> 给您回帖, <a href =\"showtopic.aspx?page=end&topicid={2}#{3}\">{4}</a>.", postinfo.Posterid, postinfo.Poster, topicinfo.Tid, postinfo.Pid, topicinfo.Title));
        //    noticeinfo.Type = Noticetype.PostReplyNotice;
        //    noticeinfo.New = 1;
        //    noticeinfo.Posterid = postinfo.Posterid;
        //    noticeinfo.Poster = postinfo.Poster;
        //    noticeinfo.Postdatetime = Utils.GetDateTime();

        //    //当回复人与帖子作者不是同一人时，则向帖子作者发送通知
        //    if (postinfo.Posterid != replyuserid && replyuserid > 0)
        //    {
        //        noticeinfo.Uid = replyuserid;
        //        Notices.CreateNoticeInfo(noticeinfo);
        //    }

        //    //当上面通知的用户与该主题作者不同，则还要向主题作者发通知
        //    if (noticeinfo.Uid != topicinfo.Posterid && topicinfo.Posterid > 0)
        //    {
        //        noticeinfo.Uid = topicinfo.Posterid;
        //        Notices.CreateNoticeInfo(noticeinfo);
        //    }
        //}

        /// <summary>
        /// 获取指定字滤字符串所对应的通知类型
        /// </summary>
        /// <param name="filter">字滤字符串</param>
        /// <returns></returns>
        public static Noticetype GetNoticetype(string filter)
        {
            switch (filter)
            {
                case "spacecomment": //日志评论
                    return Noticetype.SpaceCommentNotice;

                case "albumcomment": //相册图片评论
                    return Noticetype.AlbumCommentNotice;

                case "postreply": //主题回复
                    return Noticetype.PostReplyNotice;

                case "goodstrade": //商品交易
                    return Noticetype.GoodsTradeNotice;

                case "goodsleaveword": //商品留言通知
                    return Noticetype.GoodsLeaveWordNotice;

                default:
                    return Noticetype.All;
            }
        }

        /// <summary>
        /// 获取指定用户和分页下的通知
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>通知集合</returns>
        public static int GetNewNoticeCountByUid(Guid uid)
        {
            return uid != new Guid("00000000-0000-0000-0000-000000000000") ? SAS.Data.DataProvider.Notices.GetNewNoticeCountByUid(uid) : 0;
        }

        /// <summary>
        /// 更新指定用户的通知新旧状态
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="newtype">通知新旧状态(1:新通知 0:旧通知)</param>
        public static void UpdateNoticeNewByUid(Guid uid, int newtype)
        {
            if (uid != new Guid("00000000-0000-0000-0000-000000000000"))
                SAS.Data.DataProvider.Notices.UpdateNoticeNewByUid(uid, newtype);
        }

        /// <summary>
        /// 得到通知数
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="state">通知状态(0为已读，1为未读)</param>
        /// <returns></returns>
        public static int GetNoticeCount(Guid userid, int state)
        {
            return userid != new Guid("00000000-0000-0000-0000-000000000000") ? SAS.Data.DataProvider.Notices.GetNoticeCount(userid, state) : 0;
        }

        /// <summary>
        /// 获得最新的通知ID
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public static int GetLatestNoticeID(Guid userid)
        {
            return userid != new Guid("00000000-0000-0000-0000-000000000000") ? SAS.Data.DataProvider.Notices.GetLatestNoticeID(userid) : 0;
        }

        /// <summary>
        /// 获得指定用户的新通知
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static NoticeInfo[] GetNewNotices(Guid userid)
        {
            return userid != new Guid("00000000-0000-0000-0000-000000000000") ? SAS.Data.DataProvider.Notices.GetNewNotices(userid) : null;
        }
    }
}
