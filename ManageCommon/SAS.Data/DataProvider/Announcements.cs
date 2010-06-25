using System;
using System.Data;
using System.Text;

using SAS.Entity;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 公告操作类
    /// </summary>
    public class Announcements
    {
        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="announcementInfo">公告对象</param>
        public static void CreateAnnouncement(AnnouncementInfo announcementInfo)
        {
            DatabaseProvider.GetInstance().CreateAnnouncement(announcementInfo);
        }

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <param name="aId">公告id</param>
        /// <returns></returns>
        public static AnnouncementInfo GetAnnouncement(int aid)
        {
            AnnouncementInfo announcementInfo = null;
            IDataReader reader = DatabaseProvider.GetInstance().GetAnnouncement(aid);
            if (reader.Read())
            {
                announcementInfo = LoadSingleAnnouncementInfo(reader);
            }
            reader.Close();
            return announcementInfo;
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <returns>公告列表</returns>
        public static DataTable GetAnnouncementList()
        {
            return DatabaseProvider.GetInstance().GetAnnouncements();
        }
        /// <summary>
        /// 获取公告列表
        /// </summary>
        public static List<AnnouncementInfo> GetAnnouncementList(int pageSize, int pageIndex)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetAnnouncements(pageSize, pageIndex);
            List<AnnouncementInfo> announcementlist = new List<AnnouncementInfo>();

            while (reader.Read())
            {
                announcementlist.Add(LoadSingleAnnouncementInfo(reader));
            }

            reader.Close();
            return announcementlist;
        }

        /// <summary>
        /// 首页公告
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static List<AnnouncementInfo> GetAnnouncementIndex(int nums)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetAnnouncementIndex(nums);
            List<AnnouncementInfo> announcementlist = new List<AnnouncementInfo>();

            while (reader.Read())
            {
                announcementlist.Add(LoadSingleAnnouncementInfo(reader));
            }

            reader.Close();
            return announcementlist;
        }
        /// <summary>
        /// 公告数量
        /// </summary>
        public static int GetAnnouncementCount()
        {
            return DatabaseProvider.GetInstance().GetAnnouncementCount();
        }

        /// <summary>
        /// 批量删除公告
        /// </summary>
        /// <param name="aidlist">逗号分隔的id列表字符串</param>
        public static void DeleteAnnouncements(string aidlist)
        {
            DatabaseProvider.GetInstance().DeleteAnnouncements(aidlist);
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="announcementInfo">公告对象</param>
        public static void UpdateAnnouncement(AnnouncementInfo announcementInfo)
        {
            DatabaseProvider.GetInstance().UpdateAnnouncement(announcementInfo);
        }

        /// <summary>
        /// 更新公告的创建者用户名
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="newUserName">新用户名</param>
        public static void UpdateAnnouncementPoster(int uid, string newUserName)
        {
            DatabaseProvider.GetInstance().UpdateAnnouncementPoster(uid, newUserName);
        }

        #region Private Methods
        /// <summary>
        /// 装载实体对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static AnnouncementInfo LoadSingleAnnouncementInfo(IDataReader reader)
        {
            AnnouncementInfo announcementInfo = new AnnouncementInfo();
            announcementInfo.Id = TypeConverter.ObjectToInt(reader["id"]);
            announcementInfo.Poster = reader["poster"].ToString();
            announcementInfo.Posterid = TypeConverter.ObjectToInt(reader["posterid"]);
            announcementInfo.Title = reader["title"].ToString();
            announcementInfo.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"]);
            announcementInfo.Starttime = Convert.ToDateTime(reader["starttime"].ToString());
            announcementInfo.Endtime = Convert.ToDateTime(reader["endtime"].ToString());
            announcementInfo.Message = reader["message"].ToString();
            announcementInfo.Relateactive = reader["relateactive"].ToString();
            return announcementInfo;
        }
        #endregion
    }
}
