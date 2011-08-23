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
        /// <param name="classid">所在类别ID</param>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        public override List<NewsContent> GetNewsList(string classid, int newscount, string ordercol, string ordertype)
        {
            return NETCMS.GetNewsList(classid, newscount, ordercol, ordertype);
        }
        /// <summary>
        /// 获得图片类新闻信息列表
        /// </summary>
        public override List<NewsContent> GetPicNewList(string classid, int newscount, string ordercol, string ordertype)
        {
            return NETCMS.GetPicNewList(classid, newscount, ordercol, ordertype);
        }

        /// <summary>
        /// 获得新闻栏目
        /// </summary>
        public override PubClassInfo GetClassUrl(string classid)
        {
            return NETCMS.GetNewsClassInfo(classid);
        }
    }
}
