using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using SAS.Data;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        #region 在线用户OnlineUser表基本操作

        /// <summary>
        /// 获得全部在线用户数
        /// </summary>
        /// <returns></returns>
        public int GetOnlineAllUserCount()
        {
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}getonlineuercount", BaseConfigs.GetTablePrefix)), 1);
        }

        /// <summary>
        /// 创建在线表
        /// </summary>
        /// <returns></returns>
        public int CreateOnlineTable()
        {
            try
            {
                return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("TRUNCATE TABLE [{0}online]", BaseConfigs.GetTablePrefix));
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获得在线注册用户总数量
        /// </summary>
        /// <returns>用户数量</returns>
        public int GetOnlineUserCount()
        {
            return TypeConverter.ObjectToInt(
                                 DbHelper.ExecuteDataset(CommandType.Text,
                                                         string.Format("SELECT COUNT(ol_id) FROM [{0}online] WHERE [ol_ps_id]<>'00000000-0000-0000-0000-000000000000'", BaseConfigs.GetTablePrefix)).Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 获得全部在线用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetOnlineUserListTable()
        {
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}getonlineuserlist", BaseConfigs.GetTablePrefix)).Tables[0];
        }

        /// <summary>
        /// 返回在线用户图例
        /// </summary>
        /// <returns></returns>
        public DataTable GetOnlineGroupIconTable()
        {
            string commandText = string.Format("SELECT [ui_id], [ui_displayOrder], [ui_ug_name], [ui_img] FROM [{0}userGroupIcon] WHERE [ui_img] <> '' ORDER BY [ui_displayOrder]",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion
    }
}
