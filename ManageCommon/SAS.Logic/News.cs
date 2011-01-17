using System;
using System.Text;
using System.Data;
using System.Web.Caching;

using SAS.Entity;
using SAS.Plugin.NETCMS;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;

namespace SAS.Logic
{
    public class News
    {
        /// <summary>
        /// 获取最新每日资讯
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<NewsContent> GetHourNews(int count)
        {
            return NETCMSPluginProvider.GetInstance().GetNewsList(count, "id", "desc");
        }
    }
}
