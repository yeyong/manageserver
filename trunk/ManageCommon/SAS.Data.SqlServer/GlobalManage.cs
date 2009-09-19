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

        #region 模板类template操作

        /// <summary>
        /// 获得前台有效的模板列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetValidTemplateList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}templates] ORDER BY [tp_id]",
                                                DbFields.TEMPLATES,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得前台有效的模板ID列表
        /// </summary>
        /// <returns>模板ID列表</returns>
        public DataTable GetValidTemplateIDList()
        {
            string commandText = string.Format("SELECT [tp_id] FROM [{0}templates] ORDER BY [tp_id]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="name">模版名称</param>
        /// <param name="directory">模版目录</param>
        /// <param name="copyRight">版权信息</param>
        /// <param name="author">作者</param>
        /// <param name="createDate">创建日期</param>
        /// <param name="ver">版本</param>
        /// <param name="forDntVer">适用论坛版本</param>
        public void AddTemplate(string name, string directory, string copyRight, string author, string createDate, string ver, string forDntVer)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@tp_name", (DbType)SqlDbType.NVarChar, 50, name),
                                        DbHelper.MakeInParam("@tp_directory", (DbType)SqlDbType.NVarChar, 100, directory),
                                        DbHelper.MakeInParam("@tp_copyright", (DbType)SqlDbType.NVarChar, 100, copyRight),
                                        DbHelper.MakeInParam("@tp_author", (DbType)SqlDbType.NVarChar, 100, author),
                                        DbHelper.MakeInParam("@tp_createdate", (DbType)SqlDbType.NVarChar, 50, createDate),
                                        DbHelper.MakeInParam("@tp_ver", (DbType)SqlDbType.NVarChar, 100, ver),
                                        DbHelper.MakeInParam("@tp_fordntver", (DbType)SqlDbType.NVarChar, 100, forDntVer)
                                    };
            string commandText = string.Format("INSERT INTO [{0}templates] ([tp_name],[tp_directory],[tp_copyright],[tp_author],[tp_createdate],[tp_ver],[tp_fordntver]) VALUES(@tp_name,@tp_directory,@tp_copyright,@tp_author,@tp_createdate,@tp_ver,@tp_fordntver)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 添加新的模板项
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <param name="directory">模板文件所在目录</param>
        /// <param name="copyright">模板版权文字</param>
        /// <returns>模板id</returns>
        public int AddTemplate(string templateName, string directory, string copyRight)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@templatename", (DbType)SqlDbType.VarChar, 0, templateName),
				DbHelper.MakeInParam("@directory", (DbType)SqlDbType.VarChar, 0, directory),
				DbHelper.MakeInParam("@copyright", (DbType)SqlDbType.VarChar, 0, copyRight)
			};
            string commandText = string.Format("INSERT INTO [{0}templates]([tp_name],[tp_directory],[tp_copyright]) VALUES(@templatename, @directory, @copyright);SELECT SCOPE_IDENTITY()",
                                                BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }

        /// <summary>
        /// 删除指定的模板项
        /// </summary>
        /// <param name="templateid">模板id</param>
        public void DeleteTemplateItem(int templateId)
        {
            string commandText = string.Format("DELETE FROM [{0}templates] WHERE [tp_id]={1}",
                                                BaseConfigs.GetTablePrefix,
                                                templateId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 删除指定的模板项列表,
        /// </summary>
        /// <param name="templateidlist">格式为： 1,2,3</param>
        public void DeleteTemplateItem(string templateIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}templates] WHERE [tp_id] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得所有在模板目录下的模板列表(即:子目录名称)
        /// </summary>
        /// <param name="templatePath">模板所在路径</param>
        /// <example>GetAllTemplateList(Utils.GetMapPath(@"..\..\templates\"))</example>
        /// <returns>模板列表</returns>
        public DataTable GetAllTemplateList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}templates] ORDER BY [tp_id]",
                                                DbFields.TEMPLATES,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion

        

        #region 通知notice基本操作

        /// <summary>
        /// 添加指定的通知信息
        /// </summary>
        /// <param name="noticeInfo">要添加的通知信息</param>
        /// <returns></returns>
        public int CreateNoticeInfo(NoticeInfo noticeInfo)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, noticeInfo.Uid),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, noticeInfo.Type),
                                        DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, noticeInfo.New),
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.UniqueIdentifier, 16, noticeInfo.Posterid),
                                        DbHelper.MakeInParam("@poster", (DbType)SqlDbType.NChar, 20, noticeInfo.Poster),
                                        DbHelper.MakeInParam("@note", (DbType)SqlDbType.NText, 0, noticeInfo.Note),
                                        DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, noticeInfo.Postdatetime)
                                    };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}createnotice", BaseConfigs.GetTablePrefix),
                                                                    parms), -1);
        }

        /// <summary>
        /// 更新指定的通知信息
        /// </summary>
        /// <param name="noticeInfo">要更新的通知信息</param>
        /// <returns></returns>
        public bool UpdateNoticeInfo(NoticeInfo noticeInfo)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@nid", (DbType)SqlDbType.Int, 4, noticeInfo.Nid),
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, noticeInfo.Uid),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, noticeInfo.Type),
                                        DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, noticeInfo.New),
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.UniqueIdentifier, 16, noticeInfo.Posterid),
                                        DbHelper.MakeInParam("@poster", (DbType)SqlDbType.NChar, 20, noticeInfo.Poster),
                                        DbHelper.MakeInParam("@note", (DbType)SqlDbType.NText, 0, noticeInfo.Note),
                                        DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, noticeInfo.Postdatetime)
                                    };
            string commandText = string.Format("UPDATE [{0}notices] SET  [uid] = @uid, [nn_type] = @type, [new] = @new, [posterid] = @posterid, [poster] = @poster, [note] = @note, [postdatetime] = @postdatetime  WHERE [nid] = @nid",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) > 0;
        }

        /// <summary>
        /// 删除指定通知id的信息
        /// </summary>
        /// <param name="nid">指定的通知id</param>
        /// <returns></returns>
        public bool DeleteNoticeByNid(int nid)
        {
            string commandText = string.Format("DELETE FROM [{0}notices] WHERE [nid] = @nid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText,
                                            DbHelper.MakeInParam("@nid", (DbType)SqlDbType.Int, 4, nid)) > 0;
        }

        /// <summary>
        /// 删除指定用户id的通知信息
        /// </summary>
        /// <param name="uid">指定的通知id</param>
        /// <returns></returns>
        public bool DeleteNoticeByUid(Guid uid)
        {
            string commandText = string.Format("DELETE FROM [{0}notices] WHERE [uid] = @uid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText,
                                            DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, uid)) > 0;
        }

        /// <summary>
        /// 删除指定通知类型和天数内的通知
        /// </summary>
        /// <param name="noticeType">删除的通知类型</param>
        /// <param name="days">指定天数</param>
        public void DeleteNotice(Noticetype noticeType, int days)
        {
            string commandText;
            if (noticeType == Noticetype.All)
            {
                commandText = string.Format("DELETE FROM [{0}notices] WHERE DATEDIFF(d,[postdatetime], GETDATE()) > {1}",
                                             BaseConfigs.GetTablePrefix,
                                             days);
                DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
            else
            {
                commandText = string.Format("DELETE FROM [{0}notices] WHERE [type] = {1}  AND DATEDIFF(d,[postdatetime], GETDATE()) > {2}",
                                             BaseConfigs.GetTablePrefix,
                                             (int)noticeType, days);
                DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
            }
        }

        /// <summary>
        /// 获取指定通知id的信息
        /// </summary>
        /// <param name="nid">通知id</param>
        /// <returns>通知信息</returns>
        public IDataReader GetNoticeByNid(int noticeId)
        {
            string commandText = string.Format("SELECT [nid], [uid], [type], [new], [posterid], [poster], [note], [postdatetime]  FROM [{0}notices] WHERE [nid] = @nid ORDER BY [postdatetime] DESC",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText,
                                          DbHelper.MakeInParam("@nid", (DbType)SqlDbType.Int, 4, noticeId));
        }

        /// <summary>
        /// 获取指定通知id和类型的通知
        /// </summary>
        /// <param name="uid">指定通知id</param>
        /// <param name="noticeType"><see cref="Noticetype"/>通知类型</param>
        /// <param name="pageId">分页id</param>
        /// <param name="pageSize">页面尽寸</param>
        /// <returns></returns>
        public IDataReader GetNoticeByUid(Guid uid, Noticetype noticeType, int pageId, int pageSize)
        {
            string commandText = "";
            if (pageId == 1)
            {
                commandText = string.Format("SELECT TOP {0} [nid], [uid], [nn_type], [new], [posterid], [poster], [note], [postdatetime]  FROM [{1}notices] WHERE [uid]={2} {3} ORDER BY [nid] DESC",
                                            pageSize,
                                            BaseConfigs.GetTablePrefix,
                                            uid,
                                            noticeType == Noticetype.All ? "" : " AND [nn_type]=" + (int)noticeType);
            }
            else
            {
                commandText = string.Format("SELECT TOP {0} [nid], [uid], [nn_type], [new], [posterid], [poster], [note], [postdatetime]  FROM [{1}notices] WHERE [nid] < (SELECT MIN([nid])  FROM (SELECT TOP {2} [nid] FROM [{1}notices] WHERE [uid]={3} {4} ORDER BY [nid] DESC) AS tblTmp ) AND [uid]={3} {4} ORDER BY [nid] DESC",
                                            pageSize,
                                            BaseConfigs.GetTablePrefix,
                                            (pageId - 1) * pageSize,
                                            uid,
                                            noticeType == Noticetype.All ? "" : " AND [nn_type]=" + (int)noticeType);
            }
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 将某一类通知更改为未读状态
        /// </summary>
        /// <param name="type">通知类型</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public int ReNewNotice(int type, Guid uid)
        {
            DbParameter[] parms = {
						   DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                           DbHelper.MakeInParam("@date",(DbType)SqlDbType.DateTime, 4, DateTime.Now),
						   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.UniqueIdentifier, 16, uid)
					   };
            string commandText = string.Format("UPDATE [{0}notices] SET [new]=1,[postdatetime]=@date WHERE [nn_type]=@type AND [uid]=@uid",
                                                BaseConfigs.GetTablePrefix);
            int noticecount = DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            if (noticecount > 1)
            {
                commandText = string.Format("DELETE FROM [{0}notices] WHERE [nn_type]=@type AND [uid]=@uid", BaseConfigs.GetTablePrefix);
                DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
                return 0;
            }
            else
                return noticecount;
        }

        /// <summary>
        /// 获得指定用户的新通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IDataReader GetNewNotices(Guid userId)
        {
            string commandText = string.Format("SELECT Top 5 [nid], [uid], [nn_type], [new], [posterid], [poster], [note], [postdatetime]  FROM [{0}notices] WHERE [uid] = @uid AND [new] = 1 ORDER BY [postdatetime] DESC",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, userId));
        }

        /// <summary>
        /// 获取指定用户和通知类型的通知信息
        /// </summary>
        /// <param name="uid">指定的用户id</param>
        /// <param name="noticeType">通知类型</param>
        /// <returns></returns>
        public IDataReader GetNoticeByUid(Guid uid, Noticetype noticeType)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, uid),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, noticeType),
                                  };
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("{0}getnoticebyuid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获取指定用户id及通知类型的通知数
        /// </summary>
        /// <param name="uid">指定用户id</param>
        /// <param name="noticeType">通知类型</param>
        /// <returns></returns>
        public int GetNoticeCountByUid(Guid uid, Noticetype noticeType)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, uid),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, (int)noticeType)
                                  };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}getnoticecountbyuid", BaseConfigs.GetTablePrefix),
                                                                    parms));
        }

        /// <summary>
        /// 获取指定用户和分页下的通知
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>通知集合</returns>
        public int GetNewNoticeCountByUid(Guid uid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, uid)                                        
                                  };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}getnewnoticecountbyuid", BaseConfigs.GetTablePrefix),
                                                                    parms));
        }

        /// <summary>
        /// 更新指定用户的通知新旧状态
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="newType">通知新旧状态(1:新通知 0:旧通知)</param>
        public void UpdateNoticeNewByUid(Guid uid, int newType)
        {
            DbParameter[] parms = {                                    
                                    DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, newType),
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, uid)
                                };
            string commandText = string.Format("Update [{0}notices] SET [new] = @new WHERE [uid] = @uid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 得到通知数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="state">通知状态(0为已读，1为未读)</param>
        /// <returns></returns>
        public int GetNoticeCount(Guid userId, int state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.UniqueIdentifier,16, userId),
									   DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int,4, -1),
									   DbHelper.MakeInParam("@state",(DbType)SqlDbType.Int,4,state)
								   };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}getnoticecount", BaseConfigs.GetTablePrefix),
                                                                    parms));
        }

        #endregion

        #region IP禁止操作

        /// <summary>
        /// 添加被禁止的ip
        /// </summary>
        /// <param name="info"></param>
        public void AddBannedIp(IpInfo info)
        {
            DbParameter[] parameters = {
                                         DbHelper.MakeInParam("@ip1",(DbType)SqlDbType.Int, 4, info.Ip1),
                                         DbHelper.MakeInParam("@ip2",(DbType)SqlDbType.Int, 4, info.Ip2),
                                         DbHelper.MakeInParam("@ip3",(DbType)SqlDbType.Int, 4, info.Ip3),
                                         DbHelper.MakeInParam("@ip4",(DbType)SqlDbType.Int, 4, info.Ip4),
                                         DbHelper.MakeInParam("@admin",(DbType)SqlDbType.NVarChar,50,info.Username),
                                         DbHelper.MakeInParam("@dateline",(DbType)SqlDbType.NVarChar,50,info.Dateline),
                                         DbHelper.MakeInParam("@expiration",(DbType)SqlDbType.NVarChar,50,info.Expiration)
                                       };

            string sql = string.Format("INSERT INTO [{0}banned](ip1,ip2,ip3,ip4,admin,dateline,expiration) VALUES(@ip1,@ip2,@ip3,@ip4,@admin,@dateline,@expiration)",
                                        BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 获得被禁止ip列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetBannedIpList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}banned] ORDER BY [bdid] DESC",
                                                DbFields.BANNED,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取指定分页的禁止IP列表
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public IDataReader GetBannedIpList(int num, int pageId)
        {
            string commandText = string.Format("SELECT TOP {0} {1} FROM [{2}banned]  WHERE [bdid] NOT IN (SELECT TOP {3} [bdid] FROM [{2}banned] ORDER BY [bdid] DESC) ORDER BY [bdid] DESC",
                                                num,
                                                DbFields.BANNED,
                                                BaseConfigs.GetTablePrefix,
                                                (pageId - 1) * num);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 显示被禁止的ip数量
        /// </summary>
        /// <returns></returns>
        public int GetBannedIpCount()
        {
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(bdid) FROM [{0}banned]", BaseConfigs.GetTablePrefix)));
        }

        /// <summary>
        /// 删除选中的ip地址段
        /// </summary>
        /// <param name="bannedIdList"></param>
        public int DeleteBanIp(string bannedIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}banned] WHERE [bdid] IN({1})", BaseConfigs.GetTablePrefix, bannedIdList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 编辑banip结束时间
        /// </summary>
        /// <param name="iplist"></param>
        /// <param name="endTime"></param>
        public int UpdateBanIpExpiration(int id, string endTime)
        {
            DbParameter[] parameters = {
                                         DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int, 4, id),
                                         DbHelper.MakeInParam("@expiration",(DbType)SqlDbType.DateTime, 8, DateTime.Parse(endTime)),
                                       };
            string commandText = string.Format("UPDATE [{0}banned] SET [expiration] = @expiration WHERE [idbdidid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parameters);
        }

        #endregion

        #region 统计,统计信息stats,statvars表操作

        /// <summary>
        /// 更新统计变量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public void UpdateStatVars(string type, string variable, string value)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@type", (DbType)SqlDbType.Char, 20, type),
                DbHelper.MakeInParam("@variable", (DbType)SqlDbType.Char, 20, variable),
                DbHelper.MakeInParam("@value", (DbType)SqlDbType.Text, 0, value)
            };

            if (DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}statvars] SET [value]=@value WHERE [type]=@type AND [variable]=@variable", BaseConfigs.GetTablePrefix), parms) == 0)
                DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("INSERT INTO [{0}statvars] ([type],[variable],[value]) VALUES(@type, @variable, @value)", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获得所有统计信息
        /// </summary>
        /// <returns></returns>
        public IDataReader GetAllStats()
        {
            string commandText = string.Format("SELECT [type], [variable], [count] FROM [{0}stats] ORDER BY [type],[variable]",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得所有统计
        /// </summary>
        /// <returns></returns>
        public IDataReader GetAllStatVars()
        {
            string commandText = string.Format("SELECT [type], [variable], [value] FROM [{0}statvars] ORDER BY [type],[variable]",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 统计板块数量
        /// </summary>
        /// <returns></returns>
        public int GetForumCount()
        {
            string commandText = string.Format("SELECT COUNT(1) FROM [{0}pageModule] WHERE [pm_sort]>0 AND [pm_status]>0", BaseConfigs.GetTablePrefix);
            return (int)DbHelper.ExecuteScalar(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得今日新用户数
        /// </summary>
        /// <returns></returns>
        public int GetTodayNewMemberCount()
        {
            string commandText = string.Format("SELECT COUNT(1) FROM [{0}personInfo] WHERE [ps_createDate]>='{1}'",
                                                BaseConfigs.GetTablePrefix,
                                                DateTime.Now.ToString("yyyy-MM-dd"));
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得管理员数量
        /// </summary>
        /// <returns></returns>
        public int GetAdminCount()
        {
            string commandText = string.Format("SELECT COUNT(1) FROM [{0}personInfo] WHERE [ps_pg_id]>0",
                                                BaseConfigs.GetTablePrefix,
                                                DateTime.Now.ToString("yyyy-MM-dd"));
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得用户排行
        /// </summary>
        /// <param name="count"></param>
        /// <param name="postTableId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IDataReader GetUsersRank(int count, string postTableId, string type)
        {
            if (!Utils.IsNumeric(postTableId))
                postTableId = "1";

            string commandText = "";
            switch (type)
            {
                //case "posts":
                //    commandText = string.Format("SELECT TOP {0} [ps_name], [ps_id], [posts] FROM [{1}personInfo] ORDER BY [posts] DESC, [ps_id]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "digestposts":
                //    commandText = string.Format("SELECT TOP {0} [ps_name], [ps_id], [digestposts] FROM [{1}personInfo] ORDER BY [digestposts] DESC, [ps_id]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "thismonth":
                //    commandText = string.Format("SELECT DISTINCT TOP {0} [poster] AS [username], [posterid] AS [uid], COUNT(pid) AS [posts] FROM [{1}posts{2}] WHERE [postdatetime]>='{3}' AND [invisible]=0 AND [posterid]>0 GROUP BY [poster], [posterid] ORDER BY [posts] DESC", count, BaseConfigs.GetTablePrefix, postTableId, DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
                //    break;
                //case "today":
                //    commandText = string.Format("SELECT DISTINCT TOP {0} [poster] AS [username], [posterid] AS [uid], COUNT(pid) AS [posts] FROM [{1}posts{2}] WHERE [postdatetime]>='{3}' AND [invisible]=0 AND [posterid]>0 GROUP BY [poster], [posterid] ORDER BY [posts] DESC", count, BaseConfigs.GetTablePrefix, postTableId, DateTime.Now.ToString("yyyy-MM-dd"));
                //    break;
                case "credits":
                    commandText = string.Format("SELECT TOP {0} [ps_name], [ps_id], [ps_credits] FROM [{1}personInfo] ORDER BY [ps_credits] DESC, [ps_id]", count, BaseConfigs.GetTablePrefix);
                    break;
                //case "extcredits1":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits1] FROM [{1}users] ORDER BY [extcredits1] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits2":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits2] FROM [{1}users] ORDER BY [extcredits2] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits3":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits3] FROM [{1}users] ORDER BY [extcredits3] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits4":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits4] FROM [{1}users] ORDER BY [extcredits4] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits5":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits5] FROM [{1}users] ORDER BY [extcredits5] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits6":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits6] FROM [{1}users] ORDER BY [extcredits6] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits7":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits7] FROM [{1}users] ORDER BY [extcredits7] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                //case "extcredits8":
                //    commandText = string.Format("SELECT TOP {0} [username], [uid], [extcredits8] FROM [{1}users] ORDER BY [extcredits8] DESC, [uid]", count, BaseConfigs.GetTablePrefix);
                //    break;
                case "oltime":
                    commandText = string.Format("SELECT TOP {0} [ps_name], [ps_id], [ps_onlinetime] FROM [{1}personInfo] ORDER BY [ps_onlinetime] DESC, [ps_id]", count, BaseConfigs.GetTablePrefix);
                    break;
                default:
                    return null;

            }
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得用户排行
        /// </summary>
        /// <param name="filed">当月还是总在线时间</param>
        /// <returns></returns>
        public IDataReader GetUserByOnlineTime(string field)
        {
            string commandText = string.Format("SELECT TOP 20 [o].[uid], [u].[ps_name], [o].[{0}] FROM [{1}onlinetime] [o] LEFT JOIN [{1}personInfo] [u] ON [o].[uid]=[u].[ps_id] ORDER BY [o].[{0}] DESC",
                                                field,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新统计数据
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="os"></param>
        /// <param name="visitorsAdd"></param>
        public void UpdateStatCount(string browser, string os, string visitorsAdd)
        {
            string month = DateTime.Now.Year + DateTime.Now.Month.ToString("00");
            string dayofweek = ((int)DateTime.Now.DayOfWeek).ToString();

            string commandText = string.Format("UPDATE [{0}stats] SET [count]=[count]+1 WHERE ([type]='total' AND [variable]='hits') {1} OR ([type]='month' AND [variable]='{2}') OR ([type]='week' AND [variable]='{3}') OR ([type]='hour' AND [variable]='{4}')",
                                                BaseConfigs.GetTablePrefix,
                                                visitorsAdd,
                                                month,
                                                dayofweek,
                                                DateTime.Now.Hour.ToString("00"));
            int affectedrows = DbHelper.ExecuteNonQuery(CommandType.Text, commandText);

            int updaterows = Utils.StrIsNullOrEmpty(visitorsAdd) ? 4 : 7;
            if (updaterows > affectedrows)
            {
                UpdateStats("browser", browser, 0);
                UpdateStats("os", os, 0);
                UpdateStats("total", "members", 0);
                UpdateStats("total", "guests", 0);
                UpdateStats("total", "hits", 0);
                UpdateStats("month", month, 0);
                UpdateStats("week", dayofweek, 0);
                UpdateStats("hour", DateTime.Now.Hour.ToString("00"), 0);
            }
        }

        public void UpdateStats(string type, string variable, int count)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@type", (DbType)SqlDbType.Char, 10, type),
                DbHelper.MakeInParam("@variable", (DbType)SqlDbType.Char, 20, variable),
                DbHelper.MakeInParam("@count", (DbType)SqlDbType.Int, 4, count)
            };
            string commandText = string.Format("UPDATE [{0}stats] SET [count]=[count]+@count WHERE [type]=@type AND [variable]=@variable",
                                                BaseConfigs.GetTablePrefix);
            if (DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) == 0)
            {
                if (count == 0)
                    parms[2].Value = 1;

                commandText = string.Format("INSERT INTO [{0}stats] ([type],[variable],[count]) VALUES(@type, @variable, @count)",
                                             BaseConfigs.GetTablePrefix);
                DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            }
        }

        #endregion

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
