using System;
using System.Text;

using SAS.Entity;
using SAS.Common.Generic;
using SAS.NETCMS.Data;

namespace SAS.NETCMS
{
    /// <summary>
    /// 文章实体操作
    /// </summary>
    public class NETCMS
    {
        /// <summary>
        /// 活得新闻信息列表
        /// </summary>
        /// <param name="newscount">新闻数量</param>
        /// <param name="ordercol">排序字段</param>
        /// <param name="ordertype">排序类型</param>
        public static List<NewsContent> GetNewsList(int newscount, string ordercol, string ordertype)
        {
            return DTOProvider.GetNewsEntity(Data.DbProvider.GetInstance().GetNewsList(newscount, ordercol, ordertype));
        }
    }
}
