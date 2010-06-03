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
    /// 后台活动管理
    /// </summary>
    public class AdminActivities : Activities
    {
        /// <summary>
        /// 创建活动专题
        /// </summary>
        /// <param name="aif"></param>
        public static void CreateActivity(ActivityInfo aif)
        {
            SAS.Data.DataProvider.Activities.CreateActivityInfo(aif);
        }

        /// <summary>
        /// 更新活动
        /// </summary>
        /// <param name="aif"></param>
        /// <returns></returns>
        public static int UpdateActivityInfo(ActivityInfo aif)
        {
            return SAS.Data.DataProvider.Activities.UpdateActivityInfo(aif);
        }

        /// <summary>
        /// 批量删除活动
        /// </summary>
        /// <param name="idlist"></param>
        public static void DeleteActivities(string idlist)
        {
            SAS.Data.DataProvider.Activities.DeleteActivityInfo(idlist);
        }

        /// <summary>
        /// 批量设置活动启用
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static bool SetActivityEnabled(string idlist)
        {
            return SAS.Data.DataProvider.Activities.SetActivityStatus(idlist, 1);
        }

        /// <summary>
        /// 批量设置活动禁用
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public static bool SetActivityUnabled(string idlist)
        {
            return SAS.Data.DataProvider.Activities.SetActivityStatus(idlist, 0);
        }

        /// <summary>
        /// 批量设置活动类型
        /// </summary>
        /// <param name="idlist"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static bool SetActivityType(string idlist, int typeid)
        {
            return SAS.Data.DataProvider.Activities.SetActivityType(idlist, typeid);
        }
    }
}
