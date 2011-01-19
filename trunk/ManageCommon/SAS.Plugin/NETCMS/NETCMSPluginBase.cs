using System;
using System.Text;
using System.IO;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Plugin.NETCMS
{
    /// <summary>
    /// 文章管理控制插件类
    /// </summary>
    public abstract class NETCMSPluginBase : PluginBase
    {
        /// <summary>
        /// 活得新闻信息列表
        /// </summary>
        /// <param name="classid">所在类别ID</param>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        public abstract List<NewsContent> GetNewsList(string classid, int newscount, string ordercol, string ordertype);
        /// <summary>
        /// 获得新闻栏目
        /// </summary>
        public abstract PubClassInfo GetClassUrl(string classid);
    }
}
