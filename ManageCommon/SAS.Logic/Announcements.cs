using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;
using SAS.Data;

namespace SAS.Logic
{
    /// <summary>
    /// 网站公告操作类
    /// </summary>
    public class Announcements
    {
        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="userId">用户id</param>
        /// <param name="subject">公告主题</param>
        /// <param name="displayOrder">排序序号</param>
        /// <param name="startDateTime">起始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <param name="message">公告内容</param>
        /// <param name="relateactive">相关活动</param>
        public static void CreateAnnouncement(string username, int userid, string subject, int displayorder, string starttime, string endtime, string message, string relateactive)
        {
            AnnouncementInfo announcementInfo = new AnnouncementInfo();
            announcementInfo.Title = subject;
            announcementInfo.Poster = username;
            announcementInfo.Posterid = userid;
            announcementInfo.Displayorder = displayorder;
            DateTime dt;
            DateTime.TryParse(starttime, out dt);
            announcementInfo.Starttime = dt;
            DateTime.TryParse(endtime, out dt);
            announcementInfo.Endtime = dt;
            announcementInfo.Message = message;
            announcementInfo.Relateactive = relateactive;

            Data.DataProvider.Announcements.CreateAnnouncement(announcementInfo);
        }

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="aId">公告id</param>
        /// <returns></returns>
        public static AnnouncementInfo GetAnnouncement(int aid)
        {
            return aid > 0 ? Data.DataProvider.Announcements.GetAnnouncement(aid) : null;
        }

        /// <summary>
        /// 获得全部指定时间段内的公告列表
        /// </summary>
        /// <param name="startDateTime">开始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <returns>公告列表</returns>
        public static DataTable GetAnnouncementList(string starttime, string endtime)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/SAS/AnnouncementList") as DataTable;

            if (dt == null)
            {
                dt = Data.DataProvider.Announcements.GetAnnouncementList();
                cache.AddObject("/SAS/AnnouncementList", dt);
            }
            return dt;
        }

        /// <summary>
        /// 首页公告
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static List<AnnouncementInfo> GetAnnouncementIndex()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<AnnouncementInfo> dt = cache.RetrieveObject("/SAS/AnnouncementIndex") as List<AnnouncementInfo>;

            if (dt == null)
            {
                dt = Data.DataProvider.Announcements.GetAnnouncementIndex(9);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 30;
                cache.LoadCacheStrategy(ica);
                cache.AddObject("/SAS/AnnouncementIndex", dt);
                cache.LoadDefaultCacheStrategy();
            }
            return dt;
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        public static DataTable GetAnnouncementList()
        {
            return Data.DataProvider.Announcements.GetAnnouncementList();
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        public static List<AnnouncementInfo> GetAnnouncementList(int pageSize, int pageIndex)
        {
            return SAS.Data.DataProvider.Announcements.GetAnnouncementList(pageSize, pageIndex);
        }

        /// <summary>
        /// 公告总数
        /// </summary>
        public static int GetAnnouncementCount()
        {
            return Data.DataProvider.Announcements.GetAnnouncementCount();
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="aidlist">逗号分隔的id字符串</param>
        public static void DeleteAnnouncements(string aidlist)
        {
            Data.DataProvider.Announcements.DeleteAnnouncements(aidlist);
            //移除公告缓存
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/AnnouncementList");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SimplifiedAnnouncementList");
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="aId">公告id</param>
        /// <param name="username">用户名</param>
        /// <param name="subject">公告主题</param>
        /// <param name="displayOrder">排序序号</param>
        /// <param name="startDateTime">起始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <param name="message">公告内容</param>
        public static void UpdateAnnouncement(AnnouncementInfo announcementInfo)
        {
            if (announcementInfo.Id > 0)
                Data.DataProvider.Announcements.UpdateAnnouncement(announcementInfo);
        }
    }
}
