using System;
using System.Collections.Generic;
using System.Text;
using SAS.Cache;

namespace SAS.Logic
{
    public class ForumOperator
    {
        /// <summary>
        /// 刷新论坛缓存信息
        /// </summary>
        public static void RefreshForumCache()
        {
            SASCache.GetCacheService().RemoveObject("/SAS/DropdownOptions");
            SASCache.GetCacheService().RemoveObject("/SAS/ForumList");
            SASCache.GetCacheService().RemoveObject("/SAS/ForumListMenuDiv");
        }
    }
}
