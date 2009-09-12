using System;
using System.Data;
using System.Data.Common;
using System.Text;

using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Data
{
    public interface IDataProvider
    {
        #region 在线用户OnlineUser表基本操作

        /// <summary>
        /// 获得在线用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        int GetOnlineAllUserCount();

        /// <summary>
        /// 创建在线表记录(本方法在应用程序初始化时被调用)
        /// </summary>
        /// <returns></returns>
        int CreateOnlineTable();

        /// <summary>
        /// 获取在线用户数
        /// </summary>
        /// <returns></returns>
        int GetOnlineUserCount();

        #endregion
    }
}
