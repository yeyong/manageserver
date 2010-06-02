using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Caching;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 活动专题操作类
    /// </summary>
    public class Activities
    {
        /// <summary>
        /// 获得活动专题查询语句
        /// </summary>
        /// <param name="atype">专题类型</param>
        /// <param name="title">标题</param>
        /// <param name="keyword">关键字</param>
        /// <param name="startdate">活动开始时间</param>
        /// <param name="endtdate">活动结束时间</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static string GetActivitiesSearchConditions(int atype, string title, string keyword, DateTime startdate, DateTime endtdate, int status)
        {
            return SAS.Data.DataProvider.Activities.GetActivitiesSearchConditions(atype, title, keyword, startdate, endtdate, status);
        }

        /// <summary>
        /// 根据条件获取活动信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static DataTable GetActivitiesByConditions(string conditions)
        {
            return SAS.Data.DataProvider.Activities.GetActivitiesByConditions(conditions);
        }
    }
}
