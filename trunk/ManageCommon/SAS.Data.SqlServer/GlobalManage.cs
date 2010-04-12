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
using SQLDMO;

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

        private string GetSqlstringByPostDatetime(string commandText, DateTime postdatetimeStart, DateTime postdatetimeEnd)
        {
            //日期需要改成参数，以后需要重构！需要先修改后台传递参数方式
            if (!Utils.StrIsNullOrEmpty(postdatetimeStart.ToString()))
                commandText += string.Format(" AND [av_postdatetime]>='{0}'", postdatetimeStart.ToString("yyyy-MM-dd HH:mm:ss"));

            if (!Utils.StrIsNullOrEmpty(postdatetimeEnd.ToString()))
                commandText += string.Format(" AND [av_postdatetime]<='{0}'", postdatetimeEnd.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));

            return commandText;
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

        #region 名片模板cardtemplate基本操作

        /// <summary>
        /// 添加名片模板
        /// </summary>
        /// <param name="name">模版名称</param>
        /// <param name="directory">模版目录</param>
        /// <param name="copyright">版权信息</param>
        /// <param name="author">作者</param>
        /// <param name="createdate">创建日期</param>
        /// <param name="ver">版本</param>
        /// <param name="currentfile">当前参数</param>
        public void AddCardTemplate(string name, string directory, string copyRight, string author, string createDate, string ver, string currentfile)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 50, name),
                                        DbHelper.MakeInParam("@directory", (DbType)SqlDbType.NVarChar, 100, directory),
                                        DbHelper.MakeInParam("@copyright", (DbType)SqlDbType.NVarChar, 100, copyRight),
                                        DbHelper.MakeInParam("@author", (DbType)SqlDbType.NVarChar, 100, author),
                                        DbHelper.MakeInParam("@createdate", (DbType)SqlDbType.NVarChar, 50, createDate),
                                        DbHelper.MakeInParam("@ver", (DbType)SqlDbType.NVarChar, 100, ver),
                                        DbHelper.MakeInParam("@currentfile", (DbType)SqlDbType.NVarChar, 1000, currentfile)
                                    };
            string commandText = string.Format("INSERT INTO [{0}cardtemplate] ([name],[directory],[copyright],[author],[createdate],[ver],[currentfile]) VALUES(@name,@directory,@copyright,@author,@createdate,@ver,@currentfile)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得前台有效的名片模板列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetValidCardTemplateList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}cardtemplate] ORDER BY [id]",
                                                DbFields.CARDTEMPLATES,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得前台有效的名片模板ID列表
        /// </summary>
        /// <returns>模板ID列表</returns>
        public DataTable GetValidCardTemplateIDList()
        {
            string commandText = string.Format("SELECT [id] FROM [{0}cardtemplate] ORDER BY [id]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得所有在名片模板目录下的模板列表(即:子目录名称)
        /// </summary>
        /// <param name="templatePath">模板所在路径</param>
        /// <example>GetAllTemplateList(Utils.GetMapPath(@"..\..\cardtemplate\"))</example>
        /// <returns>模板列表</returns>
        public DataTable GetAllCardTemplateList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}cardtemplate] ORDER BY [id]",
                                                DbFields.CARDTEMPLATES,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 删除指定的名片模板项列表,
        /// </summary>
        /// <param name="templateidlist">格式为： 1,2,3</param>
        public void DeleteCardTemplateItem(string templateIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}cardtemplate] WHERE [id] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        #endregion

        #region 名片模板配置文件 cardconfig 表操作
        /// <summary>
        /// 删除名片配置信息
        /// </summary>
        /// <param name="cardconfigid"></param>
        public void DeleteCardConfig(int cardconfigid)
        {
            string commandText = String.Format("DELETE FROM [{0}cardconfig] WHERE [id] = {1}", BaseConfigs.GetTablePrefix, cardconfigid);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }
        /// <summary>
        /// 增加企业名片配置信息
        /// </summary>
        /// <param name="cci"></param>
        public void InsertCardConfig(CardConfigInfo cci)
        {
            DbParameter[] param = {
                                         DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,cci.tid),
                                         DbHelper.MakeInParam("@hasflash",(DbType)SqlDbType.TinyInt, 1, cci.hasflash),
                                         DbHelper.MakeInParam("@hasimage",(DbType)SqlDbType.TinyInt, 1, cci.hasimage),
                                         DbHelper.MakeInParam("@hasjs",(DbType)SqlDbType.TinyInt, 1, cci.hasjs),
                                         DbHelper.MakeInParam("@hassilverlight",(DbType)SqlDbType.TinyInt, 1, cci.hassilverlight),
                                         DbHelper.MakeInParam("@showparams",(DbType)SqlDbType.NText,1,cci.showparams),
                                         DbHelper.MakeInParam("@vailddate",(DbType)SqlDbType.DateTime,8,cci.vailddate)
                                       };
            string commandText = String.Format("INSERT INTO [{0}cardconfig] ([tid],[hasflash],[hasimage],[hasjs],[hassilverlight],[showparams],[vailddate]) VALUES(@tid,@hasflash,@hasimage,@hasjs,@hassilverlight,@showparams,@vailddate)",
                                    BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 名片配置文件模板ID更新操作
        /// </summary>
        public IDataReader GetCardConfigData()
        {
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getcardconfigs", BaseConfigs.GetTablePrefix));
        }
        /// <summary>
        /// 名片配置文件模板ID更新操作
        /// </summary>
        public void UpdateCardConfigTemplateID(string templateIdList)
        {
            string commandText = string.Format("UPDATE [{0}cardconfig] SET [tid]=0 WHERE [tid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);            
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
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, noticeInfo.Uid),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, noticeInfo.Type),
                                        DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, noticeInfo.New),
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, noticeInfo.Posterid),
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
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, noticeInfo.Uid),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, noticeInfo.Type),
                                        DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, noticeInfo.New),
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, noticeInfo.Posterid),
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
        public bool DeleteNoticeByUid(int uid)
        {
            string commandText = string.Format("DELETE FROM [{0}notices] WHERE [uid] = @uid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText,
                                            DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)) > 0;
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
        public IDataReader GetNoticeByUid(int uid, Noticetype noticeType, int pageId, int pageSize)
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
        public int ReNewNotice(int type, int uid)
        {
            DbParameter[] parms = {
						   DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int, 4, type),
                           DbHelper.MakeInParam("@date",(DbType)SqlDbType.DateTime, 4, DateTime.Now),
						   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int, 4, uid)
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
        public IDataReader GetNewNotices(int userId)
        {
            string commandText = string.Format("SELECT Top 5 [nid], [uid], [nn_type], [new], [posterid], [poster], [note], [postdatetime]  FROM [{0}notices] WHERE [uid] = @uid AND [new] = 1 ORDER BY [postdatetime] DESC",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, userId));
        }

        /// <summary>
        /// 获取指定用户和通知类型的通知信息
        /// </summary>
        /// <param name="uid">指定的用户id</param>
        /// <param name="noticeType">通知类型</param>
        /// <returns></returns>
        public IDataReader GetNoticeByUid(int uid, Noticetype noticeType)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
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
        public int GetNoticeCountByUid(int uid, Noticetype noticeType)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
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
        public int GetNewNoticeCountByUid(int uid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)                                        
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
        public void UpdateNoticeNewByUid(int uid, int newType)
        {
            DbParameter[] parms = {                                    
                                    DbHelper.MakeInParam("@new", (DbType)SqlDbType.Int, 4, newType),
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
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
        public int GetNoticeCount(int userId, int state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4, userId),
									   DbHelper.MakeInParam("@type",(DbType)SqlDbType.Int,4, -1),
									   DbHelper.MakeInParam("@state",(DbType)SqlDbType.Int,4,state)
								   };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                    string.Format("{0}getnoticecount", BaseConfigs.GetTablePrefix),
                                                                    parms));
        }

        #endregion

        #region 公告处理announcements表基本操作
        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="announcementInfo">公告对象</param>
        public int CreateAnnouncement(AnnouncementInfo announcementInfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@poster", (DbType)SqlDbType.NVarChar, 20, announcementInfo.Poster),
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, announcementInfo.Posterid),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 250, announcementInfo.Title),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, announcementInfo.Displayorder),
                                        DbHelper.MakeInParam("@starttime", (DbType)SqlDbType.DateTime, 8, announcementInfo.Starttime),
                                        DbHelper.MakeInParam("@endtime", (DbType)SqlDbType.DateTime, 8, announcementInfo.Endtime),
                                        DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText, 0, announcementInfo.Message)
                                    };
            string commandText = string.Format("INSERT INTO [{0}announcements] ([poster],[posterid],[title],[displayorder],[starttime],[endtime],[message]) VALUES(@poster, @posterid, @title, @displayorder, @starttime, @endtime, @message)", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 获取通告
        /// </summary>
        public IDataReader GetAnnouncement(int id)
        {
            DbParameter param = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id);
            string commandText = string.Format("SELECT {0} FROM [{1}announcements] WHERE [id]=@id",
                                                DbFields.ANNOUNCEMENTS,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 获取通告
        /// </summary>
        /// <returns></returns>
        public DataTable GetAnnouncements()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}announcements] ORDER BY [displayorder] DESC,[id] DESC",
                                                DbFields.ANNOUNCEMENTS,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDatasetInMasterDB(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 删除通告
        /// </summary>
        /// <param name="idList">逗号分隔的id列表字符串</param>
        public int DeleteAnnouncements(string idList)
        {
            if (!Utils.IsNumericList(idList))
                return 0;

            string commandText = string.Format("DELETE FROM [{0}announcements] WHERE [id] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                idList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }
        /// <summary>
        /// 更新通告
        /// </summary>
        public int UpdateAnnouncement(AnnouncementInfo announcementInfo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, announcementInfo.Id),
                                        DbHelper.MakeInParam("@poster", (DbType)SqlDbType.NVarChar, 20, announcementInfo.Poster),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 250, announcementInfo.Title),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, announcementInfo.Displayorder),
                                        DbHelper.MakeInParam("@starttime", (DbType)SqlDbType.DateTime, 8, announcementInfo.Starttime),
                                        DbHelper.MakeInParam("@endtime", (DbType)SqlDbType.DateTime, 8, announcementInfo.Endtime),
                                        DbHelper.MakeInParam("@message", (DbType)SqlDbType.NText, 0, announcementInfo.Message)
                                    };
            string commandText = string.Format("UPDATE [{0}announcements] SET [displayorder]=@displayorder,[title]=@title, [poster]=@poster,[starttime]=@starttime,[endtime]=@endtime,[message]=@message WHERE [id]=@id",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 更新公告的创建者用户名
        /// </summary>
        /// <param name="posterId">posterId</param>
        /// <param name="poster">新用户名</param>
        public void UpdateAnnouncementPoster(int posterId, string poster)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@posterid", (DbType)SqlDbType.Int, 4, posterId),
                                        DbHelper.MakeInParam("@poster", (DbType)SqlDbType.VarChar, 20, poster)
                                    };
            string commandText = string.Format("UPDATE [{0}announcements] SET [poster]=@poster WHERE [posterid]=@posterid",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
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

        #region 登录日志loginlog操作

        /// <summary>
        /// 根据IP获取错误登录记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataTable GetErrLoginRecordByIP(string ip)
        {
            DbParameter[] parms = {
										 DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15, ip),
			                        };
            string commandText = string.Format("SELECT TOP 1 [errcount], [lastupdate] FROM [{0}failedlogins] WHERE [ip]=@ip", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 增加指定IP的错误记录数
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int AddErrLoginCount(string ip)
        {
            DbParameter[] parms = {
										 DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15, ip),
			                        };
            string commandText = string.Format("UPDATE [{0}failedlogins] SET [errcount]=[errcount]+1, [lastupdate]=GETDATE() WHERE [ip]=@ip",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 增加指定IP的错误记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int AddErrLoginRecord(string ip)
        {
            DbParameter[] parms = {
										 DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15, ip),
			                        };
            string commandText = string.Format("INSERT INTO [{0}failedlogins] ([ip], [errcount], [lastupdate]) VALUES(@ip, 1, GETDATE())",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 将指定IP的错误登录次数重置为1
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int ResetErrLoginCount(string ip)
        {
            DbParameter[] parms = {
										 DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15, ip),
			                        };
            string commandText = string.Format("UPDATE [{0}failedlogins] SET [errcount]=1, [lastupdate]=GETDATE() WHERE [ip]=@ip",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除指定IP或者超过15分钟的记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int DeleteErrLoginRecord(string ip)
        {
            DbParameter[] parms = {
										 DbHelper.MakeInParam("@ip",(DbType)SqlDbType.Char,15, ip),
			                      };
            string commandText = string.Format("DELETE FROM [{0}failedlogins] WHERE [ip]=@ip OR DATEDIFF(n,[lastupdate], GETDATE()) > 15",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion

        #region 管理员访问日志操作

        /// <summary>
        /// 添加访问日志
        /// </summary>
        /// <param name="uid">用户UID</param>
        /// <param name="userName">用户名</param>
        /// <param name="groupId">所属组ID</param>
        /// <param name="groupTitle">所属组名称</param>
        /// <param name="ip">IP地址</param>
        /// <param name="actions">动作</param>
        /// <param name="others"></param>
        public void AddVisitLog(int uid, string userName, int groupId, string groupTitle, string ip, string actions, string others)
        {
            DbParameter[] parms = {
					DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
					DbHelper.MakeInParam("@username", (DbType)SqlDbType.VarChar, 50, userName),
					DbHelper.MakeInParam("@groupid", (DbType)SqlDbType.Int, 4, groupId),
					DbHelper.MakeInParam("@grouptitle", (DbType)SqlDbType.VarChar, 50, groupTitle),
					DbHelper.MakeInParam("@ip", (DbType)SqlDbType.VarChar, 15, ip),
					DbHelper.MakeInParam("@actions", (DbType)SqlDbType.VarChar, 500, actions),
					DbHelper.MakeInParam("@others", (DbType)SqlDbType.VarChar, 1000, others)
				};
            string commandText = string.Format("INSERT INTO [{0}adminvisitlog] ([av_ps_id],[av_ps_name],[av_ug_id],[av_ug_name],[av_ip],[av_actions],[av_others]) VALUES (@uid,@username,@groupid,@grouptitle,@ip,@actions,@others)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 删除访问日志
        /// </summary>
        public void DeleteVisitLogs()
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}adminvisitlog]", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 删除访问日志
        /// </summary>
        /// <param name="condition">查询条件</param>
        public void DeleteVisitLogs(string condition)
        {
            string commandText = string.Format("DELETE FROM [{0}adminvisitlog] WHERE {1}", BaseConfigs.GetTablePrefix, condition);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 得到当前指定页数的后台访问日志记录(表)
        /// </summary>
        /// <param name="pagesize">当前分页的尺寸大小</param>
        /// <param name="currentpage">当前页码</param>
        /// <returns></returns>
        public DataTable GetVisitLogList(int pageSize, int currentPage)
        {
            int pagetop = (currentPage - 1) * pageSize;
            string commandText;

            if (currentPage == 1)
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}adminvisitlog] ORDER BY [av_id] DESC",
                                             pageSize,
                                             DbFields.ADMIN_VISIT_LOG,
                                             BaseConfigs.GetTablePrefix);
            else
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}adminvisitlog]  WHERE [av_id] < (SELECT MIN([av_id]) FROM (SELECT TOP {3} [av_id] FROM [{2}adminvisitlog] ORDER BY [av_id] DESC) AS tblTmp )  ORDER BY [av_id] DESC",
                                             pageSize,
                                             DbFields.ADMIN_VISIT_LOG,
                                             BaseConfigs.GetTablePrefix,
                                             pagetop);

            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 得到当前指定条件和页数的后台访问日志记录(表)
        /// </summary>
        /// <param name="pagesize">当前分页的尺寸大小</param>
        /// <param name="currentpage">当前页码</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataTable GetVisitLogList(int pageSize, int currentPage, string condition)
        {
            int pagetop = (currentPage - 1) * pageSize;
            string commandText;

            if (currentPage == 1)
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}adminvisitlog] WHERE {3} ORDER BY [av_id] DESC",
                                             pageSize,
                                             DbFields.ADMIN_VISIT_LOG,
                                             BaseConfigs.GetTablePrefix,
                                             condition);
            else
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}adminvisitlog]  WHERE [av_id] < (SELECT MIN([av_id])  FROM (SELECT TOP {3} [av_id] FROM [{2}adminvisitlog] WHERE {4} ORDER BY [av_id] DESC) AS tblTmp ) AND {4} ORDER BY [av_id] DESC",
                                             pageSize,
                                             DbFields.ADMIN_VISIT_LOG,
                                             BaseConfigs.GetTablePrefix,
                                             pagetop,
                                             condition);

            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获取访问日志数
        /// </summary>
        /// <returns></returns>
        public int GetVisitLogCount()
        {
            string commandText = string.Format("SELECT COUNT(av_id) FROM [{0}adminvisitlog]", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 获取访问日志数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int GetVisitLogCount(string condition)
        {
            string commandText = string.Format("SELECT COUNT(av_id) FROM [{0}adminvisitlog] WHERE {1}", BaseConfigs.GetTablePrefix, condition);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 删除指定条件的访问日志
        /// </summary>
        /// <param name="deleteMod">删除方式</param>
        /// <param name="visitId">管理日志Id</param>
        /// <param name="deleteNum">删除条数</param>
        /// <param name="deleteFrom">删除从何时起</param>
        /// <returns></returns>
        public string DelVisitLogCondition(string deleteMod, string visitId, string deleteNum, string deleteFrom)
        {
            string condition = null;
            switch (deleteMod)
            {
                case "chkall":
                    if (visitId != "")
                        condition = string.Format(" [av_id] IN ({0})", visitId);
                    break;
                case "deleteNum":
                    if (deleteNum != "" && Utils.IsNumeric(deleteNum))
                        condition = string.Format(" [av_id] NOT IN (SELECT TOP {0} [av_id] FROM [{1}adminvisitlog] ORDER BY [av_id] DESC)",
                                                   deleteNum,
                                                   BaseConfigs.GetTablePrefix);
                    break;
                case "deleteFrom":
                    if (deleteFrom != "")
                        condition = " [av_postdatetime]<'" + deleteFrom + "'";
                    break;
            }
            return condition;
        }


        /// <summary>
        /// 获取管理日志条件
        /// </summary>
        /// <param name="postDateTimeStart">访问起始日期</param>
        /// <param name="postDateTimeEnd">访问结束日期</param>
        /// <param name="userName">用户名</param>
        /// <param name="others">其它</param>
        /// <returns></returns>
        public string SearchVisitLog(DateTime postDateTimeStart, DateTime postDateTimeEnd, string userName, string others)
        {
            string commandText = GetSqlstringByPostDatetime(" [av_id]>0", postDateTimeStart, postDateTimeEnd);

            if (!Utils.StrIsNullOrEmpty(others))
                commandText += string.Format(" AND [av_others] LIKE '%{0}%'", RegEsc(others));

            if (!Utils.StrIsNullOrEmpty(userName))
            {
                commandText += " AND (";
                foreach (string word in userName.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(word))
                        commandText += string.Format(" [av_ps_name] LIKE '%{0}%' OR ", RegEsc(word));
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }
            return commandText;
        }

        #endregion

        #region 菜单表navs操作

        /// <summary>
        /// 得到自定义菜单
        /// </summary>
        /// <returns></returns>
        public IDataReader GetNavigationData(bool getAllNavigation)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}navs] {2} ORDER BY [parentid],[displayorder],[id]",
                                                DbFields.NAVS,
                                                BaseConfigs.GetTablePrefix,
                                                getAllNavigation ? "" : " WHERE [available]=1 ");
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 得到拥有子菜单的主菜单ID
        /// </summary>
        /// <returns></returns>
        public IDataReader GetNavigationHasSub()
        {
            string commandText = string.Format("SELECT DISTINCT [parentid] FROM [{0}navs] ORDER BY [parentid]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        public void DeleteNavigation(int id)
        {
            string commandText = String.Format("DELETE FROM [{0}navs] WHERE [id] = {1}", BaseConfigs.GetTablePrefix, id);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="nav">导航菜单</param>
        public void InsertNavigation(NavInfo nav)
        {
            DbParameter[] param = {
                                         DbHelper.MakeInParam("@parentid",(DbType)SqlDbType.Int,4,nav.Parentid),
                                         DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar, 50, nav.Name),
                                         DbHelper.MakeInParam("@title",(DbType)SqlDbType.NChar, 255, nav.Title),
                                         DbHelper.MakeInParam("@url",(DbType)SqlDbType.Char, 255, nav.Url),
                                         DbHelper.MakeInParam("@target",(DbType)SqlDbType.TinyInt, 1, nav.Target),
                                         DbHelper.MakeInParam("@available",(DbType)SqlDbType.TinyInt,1,nav.Available),
                                         DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.SmallInt,2,nav.Displayorder),
                                         DbHelper.MakeInParam("@level",(DbType)SqlDbType.TinyInt,1,nav.Level)
                                       };
            string commandText = String.Format("INSERT INTO [{0}navs] ([parentid],[name],[title],[url],[target],[navstype],[available],[displayorder],[level]) VALUES(@parentid,@name,@title,@url,@target,1,@available,@displayorder,@level)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        /// <summary>
        /// 更校菜单
        /// </summary>
        /// <param name="nav">导航菜单</param>
        public void UpdateNavigation(NavInfo nav)
        {
            DbParameter[] param = {
                                         DbHelper.MakeInParam("@name",(DbType)SqlDbType.NChar, 50, nav.Name),
                                         DbHelper.MakeInParam("@title",(DbType)SqlDbType.NChar, 255, nav.Title),
                                         DbHelper.MakeInParam("@url",(DbType)SqlDbType.Char, 255, nav.Url),
                                         DbHelper.MakeInParam("@target",(DbType)SqlDbType.TinyInt, 1, nav.Target),
                                         DbHelper.MakeInParam("@available",(DbType)SqlDbType.TinyInt,1,nav.Available),
                                         DbHelper.MakeInParam("@displayorder",(DbType)SqlDbType.SmallInt,2,nav.Displayorder),
                                         DbHelper.MakeInParam("@level",(DbType)SqlDbType.TinyInt,1,nav.Level),
                                         DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,nav.Id)
                                       };
            string commandText = String.Format("UPDATE [{0}navs] set [name]=@name,[title]=@title,[url]=@url,[target]=@target,[available]=@available,[displayorder]=@displayorder,[level]=@level WHERE id=@id",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, param);
        }

        #endregion

        #region 广告表advertisements操作

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <returns>广告列表</returns>
        public DataTable GetAdsTable()
        {
            string commandText = string.Format("SELECT [advid], [adtype], [addisplayorder], [targets], [parameters], [code] FROM [{0}advertisements] WHERE [adavailable]=1 AND [starttime] <='{1}' AND [endtime] >='{1}' ORDER BY [addisplayorder], [advid] DESC",
                                                BaseConfigs.GetTablePrefix,
                                                DateTime.Now.ToShortDateString());
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 添加广告信息
        /// </summary>
        /// <param name="available">广告是否有效</param>
        /// <param name="type">广告类型</param>
        /// <param name="displayOrder">显示顺序</param>
        /// <param name="title">广告标题</param>
        /// <param name="targets">投放位置</param>
        /// <param name="parameters">相关参数</param>
        /// <param name="code">广告代码</param>
        /// <param name="startDateTime">起始日期</param>
        /// <param name="endDateTime">结束日期</param>
        public void AddAdInfo(int available, string type, int displayOrder, string title, string targets, string parameters, string code, string startTime, string endTime)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@available", (DbType)SqlDbType.Int, 4, available),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.NVarChar, 50, type),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 50, title),
                                        DbHelper.MakeInParam("@targets", (DbType)SqlDbType.NVarChar, 255, targets),
                                        DbHelper.MakeInParam("@parameters", (DbType)SqlDbType.NText, 0, parameters),
                                        DbHelper.MakeInParam("@code", (DbType)SqlDbType.NText, 0, code),
                                        DbHelper.MakeInParam("@starttime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(startTime)),
                                        DbHelper.MakeInParam("@endtime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(endTime))                                        
                                    };
            string commandText = string.Format("INSERT INTO  [{0}advertisements] ([adavailable],[adtype],[addisplayorder],[title],[targets],[parameters],[code],[starttime],[endtime]) VALUES(@available,@type,@displayorder,@title,@targets,@parameters,@code,@starttime,@endtime)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdvertisements()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}advertisements] ORDER BY [advid] ASC",
                                                DbFields.ADVERTISEMENTS,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 删除广告列表            
        /// </summary>
        /// <param name="aidList">广告列表Id</param>
        public void DeleteAdvertisement(string aidList)
        {
            string commandText = string.Format("DELETE FROM [{0}advertisements] WHERE [advid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                aidList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新广告可用状态
        /// </summary>
        /// <param name="aidList">广告Id</param>
        /// <param name="available"></param>
        /// <returns></returns>
        public int UpdateAdvertisementAvailable(string aidList, int available)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@available", (DbType)SqlDbType.Int, 4, available)
                                    };
            string commandText = string.Format("UPDATE [{0}advertisements] SET [adavailable]=@available  WHERE [advid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                aidList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="adId">广告Id</param>
        /// <param name="available">是否生效</param>
        /// <param name="type">广告类型</param>
        /// <param name="displayorder">显示顺序</param>
        /// <param name="title">广告标题</param>
        /// <param name="targets">广告投放范围</param>
        /// <param name="parameters">展现方式</param>
        /// <param name="code">广告内容</param>
        /// <param name="startTime">生效时间</param>
        /// <param name="endTime">结束时间</param>
        public int UpdateAdvertisement(int aid, int available, string type, int displayOrder, string title, string targets, string parameters, string code, string startTime, string endTime)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@aid", (DbType)SqlDbType.Int, 4, aid),
                                        DbHelper.MakeInParam("@available", (DbType)SqlDbType.Int, 4, available),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.NVarChar, 50, type),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NVarChar, 50, title),
                                        DbHelper.MakeInParam("@targets", (DbType)SqlDbType.NVarChar, 255, targets),
                                        DbHelper.MakeInParam("@parameters", (DbType)SqlDbType.NText, 0, parameters),
                                        DbHelper.MakeInParam("@code", (DbType)SqlDbType.NText, 0, code),
                                        DbHelper.MakeInParam("@starttime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(startTime)),
                                        DbHelper.MakeInParam("@endtime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(endTime))
                                    };
            string commandText = string.Format("UPDATE [{0}advertisements] SET [adavailable]=@available,[adtype]=@type, [addisplayorder]=@displayorder,[title]=@title,[targets]=@targets,[parameters]=@parameters,[code]=@code,[starttime]=@starttime,[endtime]=@endtime WHERE [advid]=@aid",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <param name="aId">广告Id</param>
        /// <returns></returns>
        public DataTable GetAdvertisement(int aid)
        {
            //此函数放在Advs.cs文件中较好
            string commandText = string.Format("SELECT {0} FROM [{1}advertisements] WHERE [advid]={2}",
                                                DbFields.ADVERTISEMENTS,
                                                BaseConfigs.GetTablePrefix, aid);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion

        #region 数据库databases操作

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
        public string RestoreDatabase(string backUpPath, string serverName, string userName, string password, string strDbName, string strFileName)
        {
            #region 数据库的恢复的代码

            SQLServer svr = new SQLServerClass();
            try
            {
                svr.Connect(serverName, userName, password);
                QueryResults qr = svr.EnumProcesses(-1);
                int iColPIDNum = -1;
                int iColDbName = -1;
                for (int i = 1; i <= qr.Columns; i++)
                {
                    string strName = qr.get_ColumnName(i);
                    if (strName.ToUpper().Trim() == "SPID")
                        iColPIDNum = i;
                    else if (strName.ToUpper().Trim() == "DBNAME")
                        iColDbName = i;

                    if (iColPIDNum != -1 && iColDbName != -1)
                        break;
                }

                for (int i = 1; i <= qr.Rows; i++)
                {
                    string strDBName = qr.GetColumnString(i, iColDbName);
                    if (strDBName.ToUpper() == strDbName.ToUpper())
                        svr.KillProcess(qr.GetColumnLong(i, iColPIDNum));
                }

                Restore res = new RestoreClass();
                res.Action = 0;
                res.Files = backUpPath + strFileName + ".config";
                res.Database = strDbName;
                res.ReplaceDatabase = true;
                res.SQLRestore(svr);
                return "";
            }
            catch (Exception err)
            {
                return err.Message.Replace("'", " ").Replace("\n", " ").Replace("\\", "/");
            }
            finally
            {
                svr.DisConnect();
            }
            #endregion
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="backUpPath">备份文件路径</param>
        /// <param name="serverName">服务器名称</param>
        /// <param name="userName">数据库用户名</param>
        /// <param name="passWord">数据库密码</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="strFileName">备份文件名</param>
        /// <returns></returns>
        public string BackUpDatabase(string backUpPath, string serverName, string userName, string password, string strDbName, string strFileName)
        {
            SQLServer svr = new SQLServerClass();
            try
            {
                svr.Connect(serverName, userName, password);
                Backup bak = new BackupClass();
                bak.Action = 0;
                bak.Initialize = true;
                bak.Files = backUpPath + strFileName + ".config";
                bak.Database = strDbName;
                bak.SQLBackup(svr);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message.Replace("'", " ").Replace("\n", " ").Replace("\\", "/");
            }
            finally
            {
                svr.DisConnect();
            }
        }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        public string GetDbName()
        {
            foreach (string info in BaseConfigs.GetDBConnectString.Split(';'))
            {
                if (info.ToLower().IndexOf("initial catalog") >= 0 || info.ToLower().IndexOf("database") >= 0)
                    return info.Split('=')[1].Trim();
            }
            return "sas";
        }

        /// <summary>
        /// 开始填充全文索引
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public int StartFullIndex(string dbName)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("USE {0};EXECUTE sp_fulltext_database 'enable';", dbName));
        }

        /// <summary>
        /// 构建相应表及全文索引
        /// </summary>
        /// <param name="tableName"></param>
        public void CreatePostTableAndIndex(string tableName)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, GetSpecialTableFullIndexSQL(tableName, GetDbName()));
        }

        /// <summary>
        /// 得到指定帖子分表的全文索引建立(填充)语句
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public string GetSpecialTableFullIndexSQL(string tableName, string dbName)
        {
            #region 建表

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[{0}]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)  DROP TABLE [{0}];");
            sqlBuilder.Append("CREATE TABLE [{0}] ([pid] [int] NOT NULL ,[fid] [int] NOT NULL ,");
            sqlBuilder.Append("[tid] [int] NOT NULL ,[parentid] [int] NOT NULL ,[layer] [int] NOT NULL ,[poster] [nvarchar] (20) NOT NULL ,");
            sqlBuilder.Append("[posterid] [int] NOT NULL ,[title] [nvarchar] (80) NOT NULL ,[postdatetime] [smalldatetime] NOT NULL ,");
            sqlBuilder.Append("[message] [ntext] NOT NULL ,[ip] [nvarchar] (15) NOT NULL ,");
            sqlBuilder.Append("[lastedit] [nvarchar] (50) NOT NULL ,[invisible] [int] NOT NULL ,[usesig] [int] NOT NULL ,[htmlon] [int] NOT NULL ,");
            sqlBuilder.Append("[smileyoff] [int] NOT NULL ,[parseurloff] [int] NOT NULL ,[bbcodeoff] [int] NOT NULL ,[attachment] [int] NOT NULL ,[rate] [int] NOT NULL ,");
            sqlBuilder.Append("[ratetimes] [int] NOT NULL ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];");
            sqlBuilder.Append("ALTER TABLE [{0}] WITH NOCHECK ADD CONSTRAINT [PK_{0}] PRIMARY KEY  CLUSTERED ([pid])  ON [PRIMARY];");

            sqlBuilder.Append("ALTER TABLE [{0}] ADD ");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_pid] DEFAULT (0) FOR [pid],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_parentid] DEFAULT (0) FOR [parentid],CONSTRAINT [DF_{0}_layer] DEFAULT (0) FOR [layer],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_poster] DEFAULT ('') FOR [poster],CONSTRAINT [DF_{0}_posterid] DEFAULT (0) FOR [posterid],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_postdatetime] DEFAULT (getdate()) FOR [postdatetime],CONSTRAINT [DF_{0}_message] DEFAULT ('') FOR [message],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_ip] DEFAULT ('') FOR [ip],CONSTRAINT [DF_{0}_lastedit] DEFAULT ('') FOR [lastedit],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_invisible] DEFAULT (0) FOR [invisible],CONSTRAINT [DF_{0}_usesig] DEFAULT (0) FOR [usesig],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_htmlon] DEFAULT (0) FOR [htmlon],CONSTRAINT [DF_{0}_smileyoff] DEFAULT (0) FOR [smileyoff],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_parseurloff] DEFAULT (0) FOR [parseurloff],CONSTRAINT [DF_{0}_bbcodeoff] DEFAULT (0) FOR [bbcodeoff],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_attachment] DEFAULT (0) FOR [attachment],CONSTRAINT [DF_{0}_rate] DEFAULT (0) FOR [rate],");
            sqlBuilder.Append("CONSTRAINT [DF_{0}_ratetimes] DEFAULT (0) FOR [ratetimes];");

            sqlBuilder.Append("CREATE  INDEX [parentid] ON [{0}]([parentid]) ON [PRIMARY];");
            sqlBuilder.Append("CREATE  UNIQUE  INDEX [showtopic] ON [{0}]([tid], [invisible], [pid]) ON [PRIMARY];");
            sqlBuilder.Append("CREATE  INDEX [treelist] ON [{0}]([tid], [invisible], [parentid]) ON [PRIMARY];");

            #endregion

            #region 建全文索引

            sqlBuilder.Append("USE {1} \r\n");
            sqlBuilder.Append("EXECUTE sp_fulltext_database 'enable'; \r\n");
            sqlBuilder.Append("IF(SELECT DATABASEPROPERTY('[{1}]','isfulltextenabled'))=0  EXECUTE sp_fulltext_database 'enable';");
            sqlBuilder.Append("IF EXISTS (SELECT * FROM sysfulltextcatalogs WHERE name ='pk_{0}_msg')  EXECUTE sp_fulltext_catalog 'pk_{0}_msg','drop';");
            sqlBuilder.Append("IF EXISTS (SELECT * FROM sysfulltextcatalogs WHERE name ='pk_{0}_msg')  EXECUTE sp_fulltext_table '[{0}]', 'drop' ;");
            sqlBuilder.Append("EXECUTE sp_fulltext_catalog 'pk_{0}_msg','create';");
            sqlBuilder.Append("EXECUTE sp_fulltext_table '[{0}]','create','pk_{0}_msg','pk_{0}';");
            sqlBuilder.Append("EXECUTE sp_fulltext_column '[{0}]','message','add';");
            sqlBuilder.Append("EXECUTE sp_fulltext_table '[{0}]','activate';");
            sqlBuilder.Append("EXECUTE sp_fulltext_catalog 'pk_{0}_msg','start_full';");

            #endregion

            return string.Format(sqlBuilder.ToString(), tableName, dbName);
        }

        /// <summary>
        /// 收缩数据库
        /// </summary>
        /// <param name="shrinkSize">收缩大小</param>
        /// <param name="dbName">数据库名</param>
        public void ShrinkDataBase(string shrinkSize, string dbName)
        {
            StringBuilder sqlBuilder = new StringBuilder("SET NOCOUNT ON ");

            sqlBuilder.Append("DECLARE @LogicalFileName sysname, @MaxMinutes INT, @NewSize INT ");
            sqlBuilder.AppendFormat("USE [{0}] -- 要操作的数据库名 ", dbName);
            sqlBuilder.AppendFormat("SELECT @LogicalFileName = '{0}_log', -- 日志文件名 ", dbName);
            sqlBuilder.Append("@MaxMinutes = 10, -- Limit on time allowed to wrap log. ");
            sqlBuilder.Append("@NewSize = 1 -- 你想设定的日志文件的大小(M) ");
            sqlBuilder.Append("-- Setup / initialize ");
            sqlBuilder.Append("DECLARE @OriginalSize int ");
            sqlBuilder.AppendFormat("SELECT @OriginalSize = {0}", shrinkSize);
            sqlBuilder.Append("FROM sysfiles ");
            sqlBuilder.Append("WHERE name = @LogicalFileName ");
            sqlBuilder.Append("SELECT 'Original Size of ' + db_name() + ' LOG is ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),@OriginalSize) + ' 8K pages or ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),(@OriginalSize*8/1024)) + 'MB' ");
            sqlBuilder.Append("FROM sysfiles ");
            sqlBuilder.Append("WHERE name = @LogicalFileName ");
            sqlBuilder.Append("CREATE TABLE DummyTrans ");
            sqlBuilder.Append("(DummyColumn char (8000) not null) ");
            sqlBuilder.Append("DECLARE @Counter INT, ");
            sqlBuilder.Append("@StartTime DATETIME, ");
            sqlBuilder.Append("@TruncLog VARCHAR(255) ");
            sqlBuilder.Append("SELECT @StartTime = GETDATE(), ");
            sqlBuilder.Append("@TruncLog = 'BACKUP LOG ' + db_name() + ' WITH TRUNCATE_ONLY' ");
            sqlBuilder.Append("DBCC SHRINKFILE (@LogicalFileName, @NewSize) ");
            sqlBuilder.Append("EXEC (@TruncLog) ");
            sqlBuilder.Append("-- Wrap the log if necessary. ");
            sqlBuilder.Append("WHILE @MaxMinutes > DATEDIFF (mi, @StartTime, GETDATE()) -- time has not expired ");
            sqlBuilder.Append("AND @OriginalSize = (SELECT size FROM sysfiles WHERE name = @LogicalFileName) ");
            sqlBuilder.Append("AND (@OriginalSize * 8 /1024) > @NewSize ");
            sqlBuilder.Append("BEGIN -- Outer loop. ");
            sqlBuilder.Append("SELECT @Counter = 0 ");
            sqlBuilder.Append("WHILE ((@Counter < @OriginalSize / 16) AND (@Counter < 50000)) ");
            sqlBuilder.Append("BEGIN -- update ");
            sqlBuilder.Append("INSERT DummyTrans VALUES ('Fill Log') ");
            sqlBuilder.Append("DELETE DummyTrans ");
            sqlBuilder.Append("SELECT @Counter = @Counter + 1 ");
            sqlBuilder.Append("END ");
            sqlBuilder.Append("EXEC (@TruncLog) ");
            sqlBuilder.Append("END ");
            sqlBuilder.Append("SELECT 'Final Size of ' + db_name() + ' LOG is ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),size) + ' 8K pages or ' + ");
            sqlBuilder.Append("CONVERT(VARCHAR(30),(size*8/1024)) + 'MB' ");
            sqlBuilder.Append("FROM sysfiles ");
            sqlBuilder.Append("WHERE name = @LogicalFileName ");
            sqlBuilder.Append("DROP TABLE DummyTrans ");
            sqlBuilder.Append("SET NOCOUNT OFF ");

            DbHelper.ExecuteDataset(CommandType.Text, sqlBuilder.ToString());
        }

        /// <summary>
        /// 清空数据库日志
        /// </summary>
        /// <param name="dbName"></param>
        public void ClearDBLog(string dbName)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@DBName", (DbType)SqlDbType.VarChar, 50, dbName),
			};
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}shrinklog", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 运行SQL语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public string RunSql(string sql)
        {
            string errorInfo = "";
            if (sql != "")
            {
                SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
                conn.Open();
                string[] sqlArray = Utils.SplitString(sql, "--/* Discuz!NT SQL Separator */--");
                foreach (string sqlStr in sqlArray)
                {
                    if (Utils.StrIsNullOrEmpty(sqlStr))   //当读到空的Sql语句则继续
                    {
                        continue;
                    }
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            DbHelper.ExecuteNonQuery(CommandType.Text, sqlStr);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            string message = ex.Message.Replace("'", " ");
                            message = message.Replace("\\", "/");
                            message = message.Replace("\r\n", "\\r\\n");
                            message = message.Replace("\r", "\\r");
                            message = message.Replace("\n", "\\n");
                            errorInfo += message + "<br />";
                        }
                    }
                }
                conn.Close();
            }
            return errorInfo;
        }

        /////// <summary>
        /////// 更新分表存储过程
        /////// </summary>
        ////public void UpdatePostSP()
        ////{
        ////    #region 更新分表的存储过程
        ////    foreach (DataRow dr in DatabaseProvider.GetInstance().GetDatechTableIds())
        ////    {
        ////        CreateStoreProc(TypeConverter.ObjectToInt(dr["id"]));
        ////    }
        ////    #endregion
        ////}

        ////public void CreateStoreProc(int tableListMaxId)
        ////{
        ////    #region 创建分表存储过程
        ////    StringBuilder sb = new StringBuilder();

        ////    string detachTablePath = "";
        ////    if (DbHelper.ExecuteScalar(CommandType.Text, "SELECT @@VERSION").ToString().Substring(20, 24).Trim().IndexOf("2000") >= 0)
        ////        detachTablePath = Utils.GetMapPath(BaseConfigs.GetForumPath.ToLower() + "config/detachtable_2000.config");
        ////    else
        ////        detachTablePath = Utils.GetMapPath(BaseConfigs.GetForumPath.ToLower() + "config/detachtable_2005.config");

        ////    if (!File.Exists(detachTablePath))
        ////    {
        ////        throw new FileNotFoundException("分表存储过程文件不存在!");
        ////    }
        ////    using (StreamReader objReader = new StreamReader(detachTablePath, Encoding.UTF8))
        ////    {
        ////        sb.Append(objReader.ReadToEnd());
        ////        objReader.Close();
        ////    }

        ////    sb.Replace("\"", "'").Replace("dnt_posts1", BaseConfigs.GetTablePrefix + "posts" + tableListMaxId);
        ////    sb.Replace("maxtablelistid", tableListMaxId.ToString());
        ////    sb.Replace("dnt_createpost", BaseConfigs.GetTablePrefix + "createpost" + tableListMaxId);
        ////    sb.Replace("dnt_getfirstpostid", BaseConfigs.GetTablePrefix + "getfirstpost" + tableListMaxId + "id");
        ////    //sb.Replace("dnt_getpostcount", BaseConfigs.GetTablePrefix + "getpost" + tableListMaxId + "count");
        ////    sb.Replace("dnt_deletepostbypid", BaseConfigs.GetTablePrefix + "deletepost" + tableListMaxId + "bypid");
        ////    sb.Replace("dnt_getposttree", BaseConfigs.GetTablePrefix + "getpost" + tableListMaxId + "tree");
        ////    sb.Replace("dnt_getsinglepost", BaseConfigs.GetTablePrefix + "getsinglepost" + tableListMaxId);
        ////    sb.Replace("dnt_updatepost", BaseConfigs.GetTablePrefix + "updatepost" + tableListMaxId);
        ////    sb.Replace("dnt_getnewtopics", BaseConfigs.GetTablePrefix + "getnewtopics");
        ////    sb.Replace("dnt_getpostlist1", BaseConfigs.GetTablePrefix + "getpostlist" + tableListMaxId);
        ////    sb.Replace("dnt_deletetopicbytidlist1", BaseConfigs.GetTablePrefix + "deletetopicbytidlist" + tableListMaxId);
        ////    sb.Replace("dnt_getreplypid1", BaseConfigs.GetTablePrefix + "getreplypid" + tableListMaxId);
        ////    sb.Replace("dnt_getnewtopics1", BaseConfigs.GetTablePrefix + "getnewtopics" + tableListMaxId);
        ////    sb.Replace("dnt_getlastpostlist1", BaseConfigs.GetTablePrefix + "getlastpostlist" + tableListMaxId);
        ////    sb.Replace("dnt_getdebatepostlist1", BaseConfigs.GetTablePrefix + "getdebatepostlist" + tableListMaxId);
        ////    sb.Replace("dnt_getpostcountbycondition1", BaseConfigs.GetTablePrefix + "getpostcountbycondition" + tableListMaxId);
        ////    sb.Replace("dnt_getpostlistbycondition1", BaseConfigs.GetTablePrefix + "getpostlistbycondition" + tableListMaxId);
        ////    sb.Replace("dnt_", BaseConfigs.GetTablePrefix);

        ////    DatabaseProvider.GetInstance().CreatePostProcedure(sb.ToString());

        ////    #endregion
        ////}


        /// <summary>
        /// 获取数据库版本
        /// </summary>
        /// <returns></returns>
        public string GetDataBaseVersion()
        {
            return DbHelper.ExecuteScalar(CommandType.Text, "SELECT @@version").ToString();
        }

        #endregion

        #region 表情操作simlies

        /// <summary>
        /// 得到表情符数据
        /// </summary>
        /// <returns>表情符数据</returns>
        public IDataReader GetSmiliesList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}smilies] WHERE [smtype]=0 ORDER BY [displayorder] DESC,[id] ASC",
                                                DbFields.SMILIES,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(System.Data.CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取表情
        /// </summary>
        /// <returns></returns>
        public DataTable GetSmilies()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}smilies] WHERE [smtype]=0", DbFields.SMILIES, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 得到表情符数据
        /// </summary>
        /// <returns>表情符表</returns>
        public DataTable GetSmiliesListDataTable()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}smilies] ORDER BY [smtype],[displayorder],[id]",
                                                DbFields.SMILIES,
                                                BaseConfigs.GetTablePrefix);
            DataSet ds = DbHelper.ExecuteDataset(System.Data.CommandType.Text, commandText);
            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 得到不带分类的表情符数据
        /// </summary>
        /// <returns>表情符表</returns>
        public DataTable GetSmiliesListWithoutType()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}smilies] WHERE [smtype]<>0 ORDER BY [smtype],[displayorder],[id]",
                                                DbFields.SMILIES,
                                                BaseConfigs.GetTablePrefix);
            DataSet ds = DbHelper.ExecuteDataset(System.Data.CommandType.Text, commandText);
            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 获得表情分类列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSmilieTypes()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}smilies] WHERE [smtype]=0 ORDER BY [displayorder],[id]",
                                                DbFields.SMILIES,
                                                BaseConfigs.GetTablePrefix);
            DataSet ds = DbHelper.ExecuteDataset(System.Data.CommandType.Text, commandText);
            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 获取指定type的smilies信息
        /// </summary>
        /// <param name="typeId">分类Id</param>
        /// <returns></returns>
        public DataTable GetSmiliesInfoByType(int type)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}smilies] WHERE [smtype]={2}",
                                                DbFields.SMILIES,
                                                BaseConfigs.GetTablePrefix,
                                                type);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 删除表情
        /// </summary>
        /// <param name="idList">表情Id</param>
        /// <returns></returns>
        public int DeleteSmilies(string idList)
        {
            string commandText = string.Format("DELETE FROM [{0}smilies]  WHERE [ID] IN({1})", BaseConfigs.GetTablePrefix, idList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 添加表情
        /// </summary>
        /// <param name="id">表情Id</param>
        /// <param name="displayOrder">显示顺序</param>
        /// <param name="type">分类</param>
        /// <param name="code">快捷编码</param>
        /// <param name="url">图片地址</param>
        public void AddSmiles(int id, int displayOrder, int type, string code, string url)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, type),
                                        DbHelper.MakeInParam("@code", (DbType)SqlDbType.NVarChar, 30, code),
                                        DbHelper.MakeInParam("@url", (DbType)SqlDbType.VarChar, 60, url)
                                    };

            string commandText = string.Format("INSERT INTO [{0}smilies] ([id],[displayorder],[smtype],[code],[url]) Values (@id,@displayorder,@type,@code,@url)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取最大表情Id
        /// </summary>
        /// <returns></returns>
        public int GetMaxSmiliesId()
        {
            string commandText = string.Format("SELECT ISNULL(MAX(id), 0) FROM [{0}smilies]", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText)) + 1;
        }

        /// <summary>
        /// 更新表情
        /// </summary>
        /// <param name="id">表情ID</param>
        /// <param name="displayOrder">排序</param>
        /// <param name="type">类型</param>
        /// <param name="code">代码</param>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public int UpdateSmilies(int id, int displayOrder, int type, string code)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 4, type),
                                        DbHelper.MakeInParam("@code", (DbType)SqlDbType.NVarChar, 30, code)
                                    };
            string commandText = string.Format("UPDATE [{0}smilies] SET [displayorder]=@displayorder,[smtype]=@type,[code]=@code Where [id]=@id",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新表情
        /// </summary>
        /// <param name="id">表情ID</param>
        /// <param name="displayOrder">排序</param>
        /// <param name="code">代码</param>
        /// <returns></returns>
        public int UpdateSmiliesPart(string code, int displayOrder, int id)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@code", (DbType)SqlDbType.NVarChar, 30, code)
                                    };
            string commandText = string.Format("UPDATE [{0}smilies] SET [code]=@code,[displayorder]=@displayorder WHERE [id]=@id",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 按类型删除表情
        /// </summary>
        /// <param name="type">类型</param>
        public void DeleteSmilyByType(int type)
        {
            string commandText = string.Format("DELETE FROM [{0}smilies] WHERE [smtype]={1}", BaseConfigs.GetTablePrefix, type);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        #endregion

        #region 标签操作Tags

        /// <summary>
        /// 获取Tag信息
        /// </summary>
        /// <param name="tagId">标签id</param>
        /// <returns></returns>
        public IDataReader GetTagInfo(int tagId)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@tagid",(DbType)SqlDbType.Int,4,tagId)
                                  };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettaginfo", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 更新TAG
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <param name="orderId">排序</param>
        /// <param name="color">颜色</param>
        public void UpdateForumTags(int tagId, int orderId, string color)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@orderid", (DbType)SqlDbType.Int,4, orderId),
                DbHelper.MakeInParam("@color", (DbType)SqlDbType.Char,6, color),
				DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int,4,tagId)
			};
            string commandText = string.Format("UPDATE [{0}tags] SET [orderid]=@orderid,[color]=@color WHERE [tagid]=@tagid",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 返回论坛Tag列表
        /// </summary>
        /// <param name="tagname">查询关键字</param>
        /// <param name="type">全部0 锁定1 开放2</param>
        /// <returns></returns>
        public DataTable GetForumTags(string tagName, int type)
        {
            //type 全部0 锁定1 开放2
            string commandText = string.Format("SELECT {0} FROM [{1}tags]  {2} ",
                                        DbFields.TAGS,
                                        BaseConfigs.GetTablePrefix,
                                        !Utils.StrIsNullOrEmpty(tagName) ? " WHERE [tagname] LIKE '%" + RegEsc(tagName) + "%'" : "");

            if (type == 1)
                commandText += !Utils.StrIsNullOrEmpty(tagName) ? " AND [orderid] < 0 " : " WHERE [orderid] < 0 ";
            else if (type == 2)
                commandText += !Utils.StrIsNullOrEmpty(tagName) ? " AND [orderid] >= 0" : " WHERE [orderid] >= 0 ";

            commandText += " ORDER BY [fcount] DESC";

            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        #endregion

        #region 计划任务处理

        /// <summary>
        /// 设置上次任务计划的执行时间
        /// </summary>
        /// <param name="key">任务的标识</param>
        /// <param name="serverName">主机名</param>
        /// <param name="lastExecuted">最后执行时间</param>
        public void SetLastExecuteScheduledEventDateTime(string key, string serverName, DateTime lastExecuted)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@key", (DbType)SqlDbType.VarChar, 100, key),
                DbHelper.MakeInParam("@servername", (DbType)SqlDbType.VarChar, 100, serverName),
                DbHelper.MakeInParam("@lastexecuted", (DbType)SqlDbType.DateTime, 8, lastExecuted)
            };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                     string.Format("{0}setlastexecutescheduledeventdatetime", BaseConfigs.GetTablePrefix),
                                     parms);
        }
        /// <summary>
        /// 获取上次任务计划的执行时间
        /// </summary>
        /// <param name="key">任务的标识</param>
        /// <param name="serverName">主机名</param>
        /// <returns></returns>
        public DateTime GetLastExecuteScheduledEventDateTime(string key, string serverName)
        {
            DbParameter[] parms = {
                DbHelper.MakeInParam("@key", (DbType)SqlDbType.VarChar, 100, key),
                DbHelper.MakeInParam("@servername", (DbType)SqlDbType.VarChar, 100, serverName),
                DbHelper.MakeOutParam("@lastexecuted", (DbType)SqlDbType.DateTime, 8)
            };

            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                     string.Format("{0}getlastexecutescheduledeventdatetime", BaseConfigs.GetTablePrefix),
                                     parms);

            return Convert.IsDBNull(parms[2].Value) ? DateTime.MinValue : Convert.ToDateTime(parms[2].Value);
        }

        #endregion

        public DataTable GetMailTable(string uids)
        {
            if (!Utils.IsSafeSqlString(uids))
                return new DataTable();

            string commandText = string.Format("SELECT [ps_id],[ps_name],[ps_email] FROM [{0}personInfo]  WHERE [ps_email] IS NOT NULL AND [ps_id] IN ({1})",
                                               BaseConfigs.GetTablePrefix,
                                               uids);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 获得脏字过滤列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetBanWordList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}badword]", DbFields.WORDS, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 以DataReader返回自定义编辑器按钮列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetCustomEditButtonList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}bbcodes] WHERE [available] = 1", DbFields.BBCODES, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取省份信息集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetProvince()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}Province]", DbFields.PROVINCE, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获取城市信息集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetCity()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}City]", DbFields.CITY, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获取地区信息集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetDistrict()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}District]", DbFields.DISTRICT, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

    }
}
