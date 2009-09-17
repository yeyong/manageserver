using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;

namespace SAS.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private static string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }
        #region 统计信息statistics表操作

        /// <summary>
        /// 获得统计列
        /// </summary>
        /// <returns>统计列</returns>
        public DataTable GetStatisticsRow()
        {
            string commandText = string.Format("SELECT TOP 1 {0} FROM [{1}statistics]", DbFields.STATISTICS, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 更新指定名称的统计项
        /// </summary>
        /// <param name="param">项目名称</param>
        /// <param name="Value">指定项的值</param>
        /// <returns>更新数</returns>
        public int UpdateStatistics(string param, string strValue)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (!Utils.StrIsNullOrEmpty(param))
            {
                sb.AppendFormat("UPDATE [{0}statistics] SET {1}= '{2}'", BaseConfigs.GetTablePrefix, param, strValue);
                return DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 更新最后回复人用户名
        /// </summary>
        /// <param name="lastUserId">Uid</param>
        /// <param name="lastUserName">新用户名</param>
        /// <returns></returns>
        public int UpdateStatisticsLastUserName(int lastUserId, string lastUserName)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@lastuserid", (DbType)SqlDbType.Int, 4, lastUserId),
                                        DbHelper.MakeInParam("@lastusername", (DbType)SqlDbType.VarChar, 200, lastUserName)
                                    };
            string commandText = string.Format("UPDATE [{0}statistics] SET [st_lastuser]=@lastusername WHERE [st_lastuserid]=@lastuserid",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion
    }
}
