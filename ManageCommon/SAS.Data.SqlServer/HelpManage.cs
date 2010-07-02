using System;
using System.Text;
using System.Data;
using System.Data.Common;

using SAS.Config;
using SAS.Common;

namespace SAS.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// 添加帮助信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="pid"></param>
        /// <param name="orderBy"></param>
        public void AddHelp(string title, string message, int pid, int orderBy)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.Char, 100, title),
                                        DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText,0,message),
                                        DbHelper.MakeInParam("@pid", (DbType)SqlDbType.Int,4, pid),
                                        DbHelper.MakeInParam("@orderby", (DbType)SqlDbType.Int, 4, orderBy)                                        
                                    };
            string commandText = string.Format("INSERT INTO [{0}help]([title],[message],[pid],[orderby]) VALUES(@title,@message,@pid,@orderby)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 删除帮助信息
        /// </summary>
        /// <param name="idList"></param>
        public void DelHelp(string idList)
        {
            string commandText = string.Format("DELETE FROM [{0}help] WHERE [id] IN ({1}) OR [pid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                idList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }
        /// <summary>
        /// 获取帮助信息条数
        /// </summary>
        /// <returns></returns>
        public int HelpCount()
        {
            return TypeConverter.ObjectToInt(
                                DbHelper.ExecuteScalar(CommandType.Text,
                                                       string.Format("SELECT COUNT(id) FROM [{0}help]", BaseConfigs.GetTablePrefix)));
        }
        /// <summary>
        /// 获取帮助列表
        /// </summary>
        public IDataReader GetHelpList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}help]", DbFields.HELP, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 获取首页帮助列表
        /// </summary>
        public IDataReader GetIndexHelpList(int num)
        {
            string commandText = string.Format("SELECT TOP {2} {0} FROM [{1}help] WHERE [pid] > 1 ORDER BY [orderby]", DbFields.HELP, BaseConfigs.GetTablePrefix, num);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 获取帮助信息类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpTypes()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}help] WHERE [pid]=0 ORDER BY [orderby] ASC",
                                                DbFields.HELP,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 获取指定ID的帮助信息
        /// </summary>
        public IDataReader ShowHelp(int id)
        {
            string commandText = string.Format("SELECT [title],[message],[pid],[orderby] FROM [{0}help] WHERE [id]={1}",
                                                BaseConfigs.GetTablePrefix,
                                                id);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 更新帮助信息
        /// </summary>
        /// <param name="id">帮助ID</param>
        /// <param name="title">帮助标题</param>
        /// <param name="message">帮助内容</param>
        /// <param name="pid">帮助</param>
        /// <param name="ordeorderByrby">排序方式</param>
        public void UpdateHelp(int id, string title, string message, int pid, int orderBy)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.Char, 100, title),
                                        DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText,0,message),
                                        DbHelper.MakeInParam("@pid", (DbType)SqlDbType.Int,4, pid),
                                        DbHelper.MakeInParam("@orderby", (DbType)SqlDbType.Int, 4, orderBy),
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id)                                        
                                    };

            string commandText = string.Format("UPDATE [{0}help] SET [title]=@title,[message]=@message,[pid]=@pid,[orderby]=@orderby WHERE [id]=@id",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 更新帮助信息
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="id"></param>
        public void UpdateOrder(string orderBy, string id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@orderby", (DbType)SqlDbType.Char, 100, orderBy),
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.VarChar, 100,id)
                                    };
            string commandText = string.Format("UPDATE [{0}help] SET [ORDERBY]=@orderby  Where id=@id", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
    }
}
