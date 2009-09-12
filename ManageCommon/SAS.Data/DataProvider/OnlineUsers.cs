using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Data.DataProvider
{
    public class OnlineUsers
    {
        /// <summary>
        /// 获得在线用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public static int GetOnlineAllUserCount()
        {
            int count = DatabaseProvider.GetInstance().GetOnlineAllUserCount();
            return count == 0 ? 1 : count;
        }

        /// <summary>
        /// 创建在线表记录(本方法在应用程序初始化时被调用)
        /// </summary>
        /// <returns></returns>
        public static int CreateOnlineTable()
        {
            return DatabaseProvider.GetInstance().CreateOnlineTable();
        }

        /// <summary>
        /// 获得在线注册用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public static int GetOnlineUserCount()
        {
            return DatabaseProvider.GetInstance().GetOnlineUserCount();
        }

    }
}
