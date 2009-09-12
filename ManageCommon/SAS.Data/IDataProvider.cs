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

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetOnlineUserListTable();

        /// <summary>
        /// 获取在线图例
        /// </summary>
        /// <returns></returns>
        DataTable GetOnlineGroupIconTable();

        /// <summary>
        /// 获取指定olId的在线用户信息
        /// </summary>
        /// <param name="olId"></param>
        /// <returns></returns>
        IDataReader GetOnlineUser(int olId);

        #endregion

        #region 统计信息Statistics表操作

        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        DataTable GetStatisticsRow();

        /// <summary>
        /// 更新指定名称的统计项
        /// </summary>
        /// <param name="param">项目名称</param>
        /// <param name="Value">指定项的值</param>
        /// <returns>更新数</returns>
        int UpdateStatistics(string param, string strValue);

        /// <summary>
        /// 更新最后回复人用户名
        /// </summary>
        /// <param name="lastUserId">Uid</param>
        /// <param name="lastUserName">新用户名</param>
        /// <returns></returns>
        int UpdateStatisticsLastUserName(int lastUserId, string lastUserName);

        #endregion
    }
}
