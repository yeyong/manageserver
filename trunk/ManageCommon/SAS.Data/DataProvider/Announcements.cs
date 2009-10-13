using System;
using System.Data;
using System.Text;

using SAS.Entity;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 公告操作类
    /// </summary>
    public class Announcements
    {
        /// <summary>
        /// 更新公告的创建者用户名
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="newUserName">新用户名</param>
        public static void UpdateAnnouncementPoster(int uid, string newUserName)
        {
            DatabaseProvider.GetInstance().UpdateAnnouncementPoster(uid, newUserName);
        }
    }
}
