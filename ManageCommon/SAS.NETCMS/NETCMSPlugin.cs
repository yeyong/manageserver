using System;
using System.Text;

using SAS.Plugin.NETCMS;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.NETCMS
{
    /// <summary>
    /// 文章插件类
    /// </summary>
    public class NETCMSPlugin : NETCMSPluginBase
    {
        /// <summary>
        /// 活得新闻信息列表
        /// </summary>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        public override List<NewsContent> GetNewsList(int newscount, string ordercol, string ordertype)
        {
            return NETCMS.GetNewsList(newscount, ordercol, ordertype);
        }
    }
}
