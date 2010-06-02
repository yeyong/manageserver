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
    }
}
