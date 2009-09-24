using System;
using System.Data;
using System.Text;

using SAS.Common;

namespace SAS.Logic
{
    public class Databases
    {
        /// <summary>
        /// 恢复备份数据库          
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="serverName">服务器名称</param>
        /// <param name="userName">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="fileName">备份文件名</param>
        /// <returns></returns>
        public static string RestoreDatabase(string backupPath, string serverName, string userName, string password, string dbName, string fileName)
        {
            return SAS.Data.DataProvider.Databases.RestoreDatabase(backupPath, serverName, userName, password, dbName, fileName.Replace(" ", "_"));
        }

        /// <summary>
        /// 备份数据库          
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="serverName">服务器名称</param>
        /// <param name="userName">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="fileName">备份文件名</param>
        /// <returns></returns>
        public static string BackUpDatabase(string backupPath, string serverName, string userName, string password, string dbName, string fileName)
        {
            return SAS.Data.DataProvider.Databases.BackUpDatabase(backupPath, serverName, userName, password, dbName, fileName.Replace(" ", "_"));
        }

        /// <summary>
        /// 是否允许备份数据库
        /// </summary>
        /// <returns></returns>
        public static bool IsBackupDatabase()
        {
            return SAS.Data.DataProvider.Databases.IsBackupDatabase();
        }

        /////// <summary>
        /////// 检测全文索引
        /////// </summary>
        /////// <param name="tableId">分表ID</param>
        ////public static int TestFullTextIndex(ref string msg)
        ////{
        ////    foreach (DataRow dr in SAS.Logic.Posts.GetAllPostTableName().Rows)
        ////    {
        ////        try
        ////        {
        ////            SAS.Data.DataProvider.Databases.TestFullTextIndex(SAS.Common.TypeConverter.ObjectToInt(dr["id"]));
        ////        }
        ////        catch
        ////        {
        ////            msg = "<script>alert('您的数据库帖子表[" + SAS.Config.BaseConfigs.GetTablePrefix + "posts" + dr["id"] + "]中暂未进行全文索引设置,因此使用数据库全文搜索无效');</script>";
        ////            return 0;
        ////        }
        ////    }
        ////    return 1;
        ////}

        /// <summary>
        /// 是否可用全文搜索
        /// </summary>
        /// <returns></returns>
        public static bool IsFullTextSearchEnabled()
        {
            return SAS.Data.DataProvider.Databases.IsFullTextSearchEnabled();
        }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        public static string GetDbName()
        {
            return SAS.Data.DataProvider.Databases.GetDbName();
        }

        #region 异步建立索引并进行填充的代理

        private delegate bool delegateCreateOrFillText(string DbName, string postidlist);

        //异步建立索引并进行填充的代理
        private delegateCreateOrFillText aysncallback;


        public void CallBack(IAsyncResult e)
        {
            aysncallback.EndInvoke(e);
        }


        ////public bool StarFillIndexWithPostid(string DbName, string postidlist)
        ////{
        ////    try
        ////    {
        ////        foreach (string postid in postidlist.Split(','))
        ////        {
        ////            Posts.CreateORFillIndex(DbName, postid);
        ////        }
        ////        return true;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        string message = FormatMessage(ex.Message);

        ////        return false;
        ////    }
        ////}

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string FormatMessage(string message)
        {
            return message.Replace("'", " ").Replace("\\", "/").Replace("\r\n", "\\r\\n").Replace("\r", "\\r").Replace("\n", "\\n");
        }

        /////// <summary>
        /////// 开始填充索引
        /////// </summary>
        /////// <param name="dbname"></param>
        ////public string StartFullIndex(string id, string dbname, string userName)
        ////{
        ////    string DbName = Databases.GetDbName();

        ////    if (id != "")
        ////    {
        ////        try
        ////        {
        ////            SAS.Data.DataProvider.Databases.StartFullIndex(DbName);
        ////            aysncallback = new delegateCreateOrFillText(StarFillIndexWithPostid);
        ////            AsyncCallback myCallBack = new AsyncCallback(CallBack);
        ////            aysncallback.BeginInvoke(DbName, id, myCallBack, userName); //
        ////            return "window.location.href='global_detachtable.aspx';";
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            return "<script>alert('" + FormatMessage(ex.Message) + "');</script>";
        ////        }
        ////    }
        ////    else
        ////    {
        ////        return "<script>alert('您未选中任何选项');window.location.href='global_detachtable.aspx';</script>";
        ////    }
        ////}
        #endregion


        /// <summary>
        /// 构建相应表及全文索引
        /// </summary>
        /// <param name="?"></param>
        public static void CreatePostTableAndIndex(string tablename)
        {
            SAS.Data.DataProvider.Databases.CreatePostTableAndIndex(tablename);
        }


        /// <summary>
        /// 是否允许创建存储过程
        /// </summary>
        /// <returns></returns>
        public static bool IsStoreProc()
        {
            return SAS.Data.DataProvider.Databases.IsStoreProc();
        }

        /// <summary>
        /// 是否支持收缩数据库
        /// </summary>
        /// <returns></returns>
        public static bool IsShrinkData()
        {
            return SAS.Data.DataProvider.Databases.IsShrinkData();
        }

        /// <summary>
        /// 收缩数据库
        /// </summary>
        /// <param name="shrinksize">收缩大小</param>
        /// <param name="dbname">数据库名</param>
        public static string ShrinkDataBase(string strDbName, string size)
        {
            try
            {
                string shrinksize = !Utils.StrIsNullOrEmpty(size) ? size : "0";
                SAS.Data.DataProvider.Databases.ShrinkDataBase(shrinksize, strDbName);
                return "window.location.href='global_logandshrinkdb.aspx';";
            }
            catch (Exception ex)
            {
                return "<script language=\"javascript\">alert('" + FormatMessage(ex.Message) + "!');window.location.href='global_logandshrinkdb.aspx';</script>";
            }
        }

        /// <summary>
        /// 清空数据库日志
        /// </summary>
        /// <param name="dbname"></param>
        public static string ClearDBLog(string dbname)
        {
            try
            {
                SAS.Data.DataProvider.Databases.ClearDBLog(dbname);
                return "window.location.href='global_logandshrinkdb.aspx';";
            }
            catch (Exception ex)
            {
                return "<script language=\"javascript\">alert('" + FormatMessage(ex.Message) + "!');window.location.href='global_logandshrinkdb.aspx';</script>";
            }
        }

        /// <summary>
        /// 运行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public static string RunSql(string sql)
        {
            return SAS.Data.DataProvider.Databases.RunSql(sql);
        }


        #region 异步建立索引并进行填充的代理

        private delegate void delegateCreateFillIndex(string DbName);

        //异步建立索引并进行填充的代理
        private delegateCreateFillIndex aysncallbackFillIndex;

        public void CallBackFillIndex(IAsyncResult e)
        {
            aysncallbackFillIndex.EndInvoke(e);
        }

        ////public void CreateFullText(string DbName)
        ////{
        ////    foreach (DataRow dr in SAS.Logic.Posts.GetAllPostTableName().Rows)
        ////    {
        ////        SAS.Data.DataProvider.PostTables.CreateORFillIndex(DbName, dr["id"].ToString());
        ////    }
        ////}

        /////// <summary>
        /////// 建立全文索引
        /////// </summary>
        /////// <param name="tableName">表名</param>
        ////public string CreateFullTextIndex(string tableName, string userName)
        ////{
        ////    try
        ////    {
        ////        SAS.Data.DataProvider.Databases.CreateFullTextIndex(tableName);

        ////        aysncallbackFillIndex = new delegateCreateFillIndex(CreateFullText);
        ////        AsyncCallback myCallBack = new AsyncCallback(CallBackFillIndex);
        ////        aysncallbackFillIndex.BeginInvoke(tableName, myCallBack, userName); //
        ////        return "window.location.href='forum_updateforumstatic.aspx';";
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return "<script>alert('" + FormatMessage(ex.Message) + "');</script>";
        ////    }
        ////}

        #endregion


        /////// <summary>
        /////// 更新分表存储过程
        /////// </summary>
        ////public static void UpdatePostSP()
        ////{
        ////    if (Databases.IsStoreProc())
        ////        SAS.Data.DataProvider.Databases.UpdatePostSP();
        ////}

        /// <summary>
        /// 获取数据库版本
        /// </summary>
        /// <returns></returns>
        public static string GetDataBaseVersion()
        {
            return SAS.Data.DataProvider.Databases.GetDataBaseVersion();
        }
    }
}
