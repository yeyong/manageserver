using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 活动专题数据操作
    /// </summary>
    public class Activities
    {
        /// <summary>
        /// 创建活动
        /// </summary>
        /// <param name="aif"></param>
        public static void CreateActivityInfo(ActivityInfo aif)
        {
            DatabaseProvider.GetInstance().CreateActivityInfo(aif);
        }

        /// <summary>
        /// 更新活动专题
        /// </summary>
        /// <param name="aif"></param>
        /// <returns></returns>
        public static int UpdateActivityInfo(ActivityInfo aif)
        {
            return DatabaseProvider.GetInstance().UpdateActivityInfo(aif);
        }

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="idlist"></param>
        public static void DeleteActivityInfo(string idlist)
        {
            DatabaseProvider.GetInstance().DeleteActivities(idlist);
        }

        /// <summary>
        /// 获得活动专题查询语句
        /// </summary>
        /// <param name="atype">专题类型</param>
        /// <param name="title">标题</param>
        /// <param name="keyword">关键字</param>
        /// <param name="startdate">活动开始时间</param>
        /// <param name="endtdate">活动结束时间</param>
        /// <param name="status">状态</param>
        public static string GetActivitiesSearchConditions(int atype, string title, string keyword, DateTime startdate, DateTime endtdate, int status)
        {
            return DatabaseProvider.GetInstance().GetActivitiesSearchConditions(atype, title, keyword, startdate, endtdate, status);
        }

        /// <summary>
        /// 根据条件获取活动信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static DataTable GetActivitiesByConditions(string conditions)
        {
            return DatabaseProvider.GetInstance().GetActivitiesByConditions(conditions);
        }

        /// <summary>
        /// 获取活动信息实体
        /// </summary>
        public static ActivityInfo GetActivityInfo(int id)
        {
            ActivityInfo activityInfo = null;
            IDataReader reader = DatabaseProvider.GetInstance().GetActivityInfoReader(id);
            if (reader.Read())
            {
                activityInfo = LoadSingleActivityInfo(reader);
            }
            reader.Close();
            return activityInfo;
        }

        /// <summary>
        /// 设置活动状态
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="statusid"></param>
        /// <returns></returns>
        public static bool SetActivityStatus(string idlist, int statusid)
        {
            return DatabaseProvider.GetInstance().SetActivityStatus(idlist, statusid);
        }

        /// <summary>
        /// 设置活动专题类型
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static bool SetActivityType(string idlist, int typeid)
        {
            return DatabaseProvider.GetInstance().SetActivityType(idlist, typeid);
        }

        #region Private Methods
        /// <summary>
        /// 装载实体对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static ActivityInfo LoadSingleActivityInfo(IDataReader reader)
        {
            ActivityInfo activityInfo = new ActivityInfo();
            activityInfo.Id = TypeConverter.ObjectToInt(reader["id"]);
            activityInfo.Atitle = reader["atitle"].ToString();
            activityInfo.Stylecode = reader["stylecode"].ToString();
            activityInfo.Desccode = reader["desccode"].ToString();
            activityInfo.Scriptcode = reader["scriptcode"].ToString();
            activityInfo.Begintime = reader["begintime"].ToString();
            activityInfo.Endtime = reader["endtime"].ToString();
            activityInfo.Atype = TypeConverter.ObjectToInt(reader["atype"]);
            activityInfo.Enabled = TypeConverter.ObjectToInt(reader["enabled"]);
            activityInfo.Seotitle = reader["seotitle"].ToString();
            activityInfo.Seodesc = reader["seodesc"].ToString();
            activityInfo.Seokeyword = reader["seokeyword"].ToString();
            activityInfo.Createdate = reader["createdate"].ToString();
            return activityInfo;
        }
        #endregion
    }
}
