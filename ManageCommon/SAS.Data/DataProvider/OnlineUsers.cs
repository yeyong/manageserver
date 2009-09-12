using System;
using System.Data;
using System.Text;

using SAS.Entity;

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

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOnlineUserListTable()
        {
            return DatabaseProvider.GetInstance().GetOnlineUserListTable();
        }

        /// <summary>
        /// 获取在线用户组图标
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOnlineGroupIconTable()
        {
            return DatabaseProvider.GetInstance().GetOnlineGroupIconTable();
        }

        public static OnlineUserInfo GetOnlineUser(int olid)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetOnlineUser(olid);
            OnlineUserInfo onlineuserinfo = null;

            if (reader.Read())
            {
                onlineuserinfo = LoadSingleOnlineUser(reader);
            }
            reader.Close();
            return onlineuserinfo;
        }
    }
}
