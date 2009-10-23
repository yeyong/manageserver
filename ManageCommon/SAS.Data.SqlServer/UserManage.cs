﻿using System;
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
        private static int _lastRemoveTimeout;

        #region 用户personinfo操作

        /// <summary>
        /// 返回指定用户的信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public IDataReader GetUserInfoToReader(int uid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int,4, uid)
			                      };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getuserinfo", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获取简短用户信息
        /// </summary>
        /// <param name="uid">用id</param>
        /// <returns>用户简短信息</returns>
        public IDataReader GetShortUserInfoToReader(int uid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int,4, uid),
			                      };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getshortuserinfo", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 更改用户组
        /// </summary>
        /// <param name="groupId">目标组</param>
        /// <param name="uidList">用户列表</param>
        public void ChangeUserGroupByUid(int groupId, string uidList)
        {
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_ug_id]={1}  WHERE [ps_id] IN ({2})",
                                                BaseConfigs.GetTablePrefix,
                                                groupId,
                                                uidList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        ///<summary>
        /// 根据IP查找用户
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>用户信息</returns>
        public IDataReader GetUserInfoByIP(string ip)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@regip", (DbType)SqlDbType.VarChar,50, ip),
			                      };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getuserinfobyip", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IDataReader GetShortUserInfoByName(string userName)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@username",(DbType)SqlDbType.VarChar,20,userName),
			                      };
            string commandText = string.Format("SELECT TOP 1 {0} FROM [{1}personInfo] WHERE [{1}personInfo].[ps_name]=@username",
                                                DbFields.USERS,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得用户列表DataTable
        /// </summary>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageindex">当前页数</param>
        /// <returns>用户列表DataTable</returns>
        public DataTable GetUserList(int pageSize, int pageIndex, string column, string orderType)
        {
            string[] arrayorderby = new string[] { "ps_name", "ps_credits", "posts", "admin", "ps_lastactivity", "ps_createDate", "ps_onlinetime" };
            int i = Array.IndexOf(arrayorderby, column);
            column = (i > 6 || i < 0) ? "ps_id" : arrayorderby[i];

            DbParameter[] parms = {
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
									   DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
									   DbHelper.MakeInParam("@column",(DbType)SqlDbType.VarChar,1000,column),
                                       DbHelper.MakeInParam("@ordertype",(DbType)SqlDbType.VarChar,5,orderType)
								   };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}getuserlist", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public DataTable GetUserList(int pageSize, int currentPage)
        {
            int pagetop = (currentPage - 1) * pageSize;
            string commandText;
            if (currentPage == 1)
                commandText = string.Format("SELECT TOP {0} [{1}personInfo].[ps_id], [{1}personInfo].[ps_name],[{1}personInfo].[ps_nickName], [{1}personInfo].[ps_createDate], [{1}personInfo].[ps_credits], [{1}personInfo].[ps_lastactivity], [{1}personInfo].[ps_email],[{1}personInfo].[ps_lastLogin],[{1}personInfo].[ps_lastLogin],[{1}personDetail].[pd_website],[{1}userGroup].[ug_name] FROM [{1}personInfo] LEFT JOIN [{1}personDetail] ON [{1}personDetail].[pd_id] = [{1}personInfo].[ps_id] LEFT JOIN [{1}userGroup] ON [{1}userGroup].[ug_id]=[{1}personInfo].[ps_ug_id] ORDER BY [{1}personInfo].[ps_id] DESC",
                                             pageSize,
                                             BaseConfigs.GetTablePrefix);
            else
                commandText = string.Format("SELECT TOP {0} [{1}personInfo].[ps_id], [{1}personInfo].[ps_name],[{1}personInfo].[ps_nickName], [{1}personInfo].[ps_createDate], [{1}personInfo].[ps_credits], [{1}personInfo].[ps_lastactivity], [{1}personInfo].[ps_email],[{1}personInfo].[ps_lastLogin],[{1}personInfo].[ps_lastLogin],[{1}personDetail].[pd_website],[{1}userGroup].[ug_name] FROM [{1}personInfo],[{1}personDetail],[{1}userGroup] WHERE [{1}personDetail].[pd_id] = [{1}personInfo].[ps_id] AND  [{1}userGroup].[ug_id]=[{1}personInfo].[ps_ug_id] AND [{1}personInfo].[ps_id] < (SELECT min([ps_id])  FROM (SELECT TOP {2} [ps_id] FROM [{1}personInfo] ORDER BY [ps_id] DESC) AS tblTmp )  ORDER BY [{1}personInfo].[ps_id] DESC",
                                             pageSize,
                                             BaseConfigs.GetTablePrefix,
                                             pagetop);

            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 获取指定条件和分页下的用户列表信息
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public DataTable UserList(int pageSize, int currentPage, string condition)
        {
            int pagetop = (currentPage - 1) * pageSize;
            string commandText;
            if (currentPage == 1)
                  commandText = string.Format("SELECT TOP {0} [{1}personInfo].[ps_id], [{1}personInfo].[ps_name],[{1}personInfo].[ps_nickName], [{1}personInfo].[ps_createDate], [{1}personInfo].[ps_credits], [{1}personInfo].[ps_lastactivity], [{1}personInfo].[ps_email],[{1}personInfo].[ps_lastLogin],[{1}personInfo].[ps_lastLogin],[{1}personDetail].[pd_website],[{1}userGroup].[ug_name] FROM [{1}personInfo] LEFT JOIN [{1}personDetail] ON [{1}personDetail].[pd_id] = [{1}personInfo].[ps_id] LEFT JOIN [{1}userGroup] ON [{1}userGroup].[ug_id]=[{1}personInfo].[ps_ug_id] WHERE {2} ORDER BY [{1}personInfo].[ps_id] DESC",
                                             pageSize,
                                             BaseConfigs.GetTablePrefix,
                                             condition);
            else
                commandText = string.Format("SELECT TOP {0} [{1}personInfo].[ps_id], [{1}personInfo].[ps_name],[{1}personInfo].[ps_nickName], [{1}personInfo].[ps_createDate], [{1}personInfo].[ps_credits], [{1}personInfo].[ps_lastactivity], [{1}personInfo].[ps_email],[{1}personInfo].[ps_lastLogin],[{1}personInfo].[ps_lastLogin],[{1}personDetail].[pd_website],[{1}userGroup].[ug_name] FROM [{1}personInfo],[{1}personDetail],[{1}userGroup] WHERE [{1}personDetail].[pd_id] = [{1}personInfo].[ps_id] AND  [{1}userGroup].[ug_id]=[{1}personInfo].[ps_ug_id] AND [{1}personInfo].[ps_id] < (SELECT min([ps_id])  FROM (SELECT TOP {2} [ps_id] FROM [{1}personInfo] WHERE {4} ORDER BY [ps_id] DESC) AS tblTmp ) AND {4} ORDER BY [{1}personInfo].[ps_id] DESC",
                                             pageSize,
                                             BaseConfigs.GetTablePrefix,
                                             condition,
                                             pagetop,
                                             condition);

            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 删除指定用户的所有信息
        /// </summary>
        /// <param name="uid">指定的用户uid</param>
        /// <param name="delposts">是否删除帖子</param>
        /// <param name="delpms">是否删除短消息</param>
        /// <returns></returns>
        public bool DelUserAllInf(int uid, bool delPosts, bool delPms)
        {
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}personInfo] WHERE [ps_id]={1}",
                                                                              BaseConfigs.GetTablePrefix, uid));
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}personDetail] WHERE [pd_id]={1}",
                                                                              BaseConfigs.GetTablePrefix, uid));
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}onlinetime] WHERE [uid]='{1}'",
                                                                              BaseConfigs.GetTablePrefix, uid));
                    //DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}polls] WHERE [uid]={1}",
                    //                                                          BaseConfigs.GetTablePrefix, uid));
                    //DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}favorites] WHERE [uid]={1}",
                    //                                                          BaseConfigs.GetTablePrefix, uid));

                    if (delPosts)
                    {
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("Delete From [{0}topics] Where [posterid]={1}",
                                                                                  BaseConfigs.GetTablePrefix, uid));
                        //清除用户所发的帖子
                        foreach (DataRow dr in DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT {0} FROM [{1}tablelist]", DbFields.TABLE_LIST, BaseConfigs.GetTablePrefix)).Tables[0].Rows)
                        {
                            if (dr["id"].ToString() != "")
                                DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM  [{0}posts{1}] WHERE [posterid]={2}",
                                                                                          BaseConfigs.GetTablePrefix, dr["id"].ToString(), uid));
                        }
                    }
                    else
                    {
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}topics] SET [poster]='该用户已被删除' Where [posterid]={1}",
                                                                                  BaseConfigs.GetTablePrefix, uid));
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}topics] SET [lastposter]='该用户已被删除' Where [lastpostid]={1}",
                                                                                  BaseConfigs.GetTablePrefix, uid));

                        //清除用户所发的帖子
                        foreach (DataRow dr in DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT {0} FROM [{1}tablelist]", DbFields.TABLE_LIST, BaseConfigs.GetTablePrefix)).Tables[0].Rows)
                        {
                            if (dr["id"].ToString() != "")
                                DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}posts{1}] SET [poster]='该用户已被删除' WHERE [posterid]={2}",
                                                                                          BaseConfigs.GetTablePrefix, dr["id"].ToString(), uid));
                        }
                    }

                    if (delPms)
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}pms] Where [msgfromid]={1}",
                                                                                  BaseConfigs.GetTablePrefix, uid));
                    else
                    {
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}pms] SET [msgfrom]='该用户已被删除' WHERE [msgfromid]={1}",
                                                                                  BaseConfigs.GetTablePrefix, uid));
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}pms] SET [msgto]='该用户已被删除' WHERE [msgtoid]={1}",
                                                                                  BaseConfigs.GetTablePrefix, uid));
                    }

                    //删除版主表的相关用户信息
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}moderators] WHERE [uid]={1}",
                                                                              BaseConfigs.GetTablePrefix, uid));
                    //更新当前论坛总人数
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}Statistics] SET [totalusers]=[totalusers]-1",
                                                                              BaseConfigs.GetTablePrefix));

                    DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT TOP 1 [uid],[username] FROM [{0}users] ORDER BY [uid] DESC",
                                                                              BaseConfigs.GetTablePrefix)).Tables[0];
                    //更新当前论坛最新注册会员信息
                    if (dt.Rows.Count > 0)
                        DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("UPDATE [{0}Statistics] SET [lastuserid]={1}, [lastusername]='{2}'",
                                                                                  BaseConfigs.GetTablePrefix, dt.Rows[0][0], dt.Rows[0][1]));

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            conn.Close();
            return true;
        }

        /// <summary>
        /// 检测Email和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="email">email</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckEmailAndSecques(string userName, string email, string secques)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@username",(DbType)SqlDbType.VarChar,50,userName),
									   DbHelper.MakeInParam("@email",(DbType)SqlDbType.VarChar,100, email),
									   DbHelper.MakeInParam("@secques",(DbType)SqlDbType.Char,8, secques)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}checkemailandsecques", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 检测密码和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckPasswordAndSecques(string userName, string passWord, bool originalPassWord, string secques)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@username",(DbType)SqlDbType.VarChar,50,userName),
									   DbHelper.MakeInParam("@password",(DbType)SqlDbType.VarChar,200, originalPassWord ? Utils.MD5(passWord) : passWord),
									   DbHelper.MakeInParam("@secques",(DbType)SqlDbType.Char,8, secques)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}checkpasswordandsecques", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public IDataReader CheckPassword(string userName, string passWord, bool originalPassWord)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@username",(DbType)SqlDbType.VarChar,50, userName),
									   DbHelper.MakeInParam("@password",(DbType)SqlDbType.VarChar,200, originalPassWord ? Utils.MD5(passWord) : passWord)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}checkpasswordbyusername", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 检测密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="groupid">用户组id</param>
        /// <param name="adminid">管理id</param>
        /// <returns>如果用户密码正确则返回uid, 否则返回-1</returns>
        public IDataReader CheckPassword(int uid, string passWord, bool originalPassWord)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,uid),
									   DbHelper.MakeInParam("@password",(DbType)SqlDbType.Char,32, originalPassWord ? Utils.MD5(passWord) : passWord)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}checkpasswordbyuid", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 根据指定的email查找用户并返回用户uid
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns>用户uid</returns>
        public IDataReader FindUserEmail(string email)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@email",(DbType)SqlDbType.VarChar,100, email),
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getuseridbyemail", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public int GetUserCount()
        {
            string commandText = string.Format("SELECT COUNT(ps_id) FROM [{0}personInfo]", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public int GetUserCountByAdmin()
        {
            string commandText = string.Format("SELECT COUNT(ps_id) FROM [{0}personInfo] WHERE [{0}personInfo].[ps_pg_id] > 0", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 创建新用户.
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>返回用户ID, 如果已存在该用户名则返回-1</returns>
        public int CreateUser(UserInfo userInfo)
        {
            DbParameter[] parms = {
					DbHelper.MakeInParam("@ps_en_id", (DbType)SqlDbType.Int,4,userInfo.Ps_en_id),
					DbHelper.MakeInParam("@ps_name", (DbType)SqlDbType.VarChar,50,userInfo.Ps_name),
					DbHelper.MakeInParam("@ps_nickName", (DbType)SqlDbType.VarChar,50,userInfo.Ps_nickName),
					DbHelper.MakeInParam("@ps_gender", (DbType)SqlDbType.SmallInt,2,userInfo.Ps_gender),
					DbHelper.MakeInParam("@ps_password", (DbType)SqlDbType.VarChar,200,userInfo.Ps_password),
					DbHelper.MakeInParam("@ps_pay_pass", (DbType)SqlDbType.VarChar,200,userInfo.Ps_pay_pass),
					DbHelper.MakeInParam("@ps_init", (DbType)SqlDbType.VarChar,200,userInfo.Ps_init),
					DbHelper.MakeInParam("@ps_company", (DbType)SqlDbType.VarChar,200,userInfo.Ps_company),
					DbHelper.MakeInParam("@ps_question", (DbType)SqlDbType.VarChar,1000,userInfo.Ps_question),
					DbHelper.MakeInParam("@ps_answer", (DbType)SqlDbType.VarChar,1000,userInfo.Ps_answer),
					DbHelper.MakeInParam("@ps_isLock", (DbType)SqlDbType.Bit,1,userInfo.Ps_isLock),
					DbHelper.MakeInParam("@ps_lastLogin", (DbType)SqlDbType.Char,19,userInfo.Ps_lastLogin),
					DbHelper.MakeInParam("@ps_lastChangePass", (DbType)SqlDbType.Char,19,userInfo.Ps_lastChangePass),
					DbHelper.MakeInParam("@ps_lockDate", (DbType)SqlDbType.Char,19,userInfo.Ps_lockDate),
					DbHelper.MakeInParam("@ps_email", (DbType)SqlDbType.VarChar,100,userInfo.Ps_email),
					DbHelper.MakeInParam("@ps_prev_email", (DbType)SqlDbType.VarChar,100,userInfo.Ps_prev_email),
					DbHelper.MakeInParam("@ps_regIP", (DbType)SqlDbType.VarChar,50,userInfo.Ps_regIP),
					DbHelper.MakeInParam("@ps_loginIP", (DbType)SqlDbType.VarChar,50,userInfo.Ps_loginIP),
					DbHelper.MakeInParam("@ps_star", (DbType)SqlDbType.Int,4,userInfo.Ps_star),
					DbHelper.MakeInParam("@ps_credits", (DbType)SqlDbType.Int,4,userInfo.Ps_credits),
					DbHelper.MakeInParam("@ps_scores", (DbType)SqlDbType.Int,4,userInfo.Ps_scores),
					DbHelper.MakeInParam("@ps_pg_id", (DbType)SqlDbType.Int,4,userInfo.Ps_pg_id),
					DbHelper.MakeInParam("@ps_ug_id", (DbType)SqlDbType.Int,4,userInfo.Ps_ug_id),
					DbHelper.MakeInParam("@ps_tempID", (DbType)SqlDbType.Int,4,userInfo.Ps_tempID),
					DbHelper.MakeInParam("@ps_isEmail", (DbType)SqlDbType.Int,4,userInfo.Ps_isEmail),
					DbHelper.MakeInParam("@ps_bdSound", (DbType)SqlDbType.Int,4,userInfo.Ps_bdSound),
					DbHelper.MakeInParam("@ps_onlinetime", (DbType)SqlDbType.Int,4,userInfo.Ps_onlinetime),
					DbHelper.MakeInParam("@ps_isDetail", (DbType)SqlDbType.Bit,1,userInfo.Ps_isDetail),
					DbHelper.MakeInParam("@ps_isCreater", (DbType)SqlDbType.Bit,1,userInfo.Ps_isCreater),
					DbHelper.MakeInParam("@ps_creater", (DbType)SqlDbType.Int,4,userInfo.Ps_creater),
					DbHelper.MakeInParam("@ps_lastactivity", (DbType)SqlDbType.Char,19,userInfo.Ps_lastactivity),
					DbHelper.MakeInParam("@ps_secques", (DbType)SqlDbType.Char,8,userInfo.ps_secques),
					DbHelper.MakeInParam("@ps_pageviews", (DbType)SqlDbType.Int,4,userInfo.ps_pageviews),
					DbHelper.MakeInParam("@ps_issign", (DbType)SqlDbType.SmallInt,2,userInfo.ps_issign),
					DbHelper.MakeInParam("@ps_newsletter", (DbType)SqlDbType.Int,4,userInfo.ps_newsletter),
					DbHelper.MakeInParam("@ps_invisible", (DbType)SqlDbType.Int,4,userInfo.ps_invisible),
					DbHelper.MakeInParam("@ps_newMess", (DbType)SqlDbType.Int,4,userInfo.Ps_newMess),
					DbHelper.MakeInParam("@ps_newpm", (DbType)SqlDbType.Int,4,userInfo.ps_newpm),
					DbHelper.MakeInParam("@ps_salt", (DbType)SqlDbType.NChar,6,userInfo.ps_salt),

					DbHelper.MakeInParam("@pd_name", (DbType)SqlDbType.VarChar,50,userInfo.Pd_name),
					DbHelper.MakeInParam("@pd_birthday", (DbType)SqlDbType.VarChar,200,userInfo.Pd_birthday),
					DbHelper.MakeInParam("@pd_MSN", (DbType)SqlDbType.VarChar,100,userInfo.Pd_MSN),
					DbHelper.MakeInParam("@pd_QQ", (DbType)SqlDbType.VarChar,100,userInfo.Pd_QQ),
					DbHelper.MakeInParam("@pd_Skype", (DbType)SqlDbType.VarChar,100,userInfo.Pd_Skype),
					DbHelper.MakeInParam("@pd_Yahoo", (DbType)SqlDbType.VarChar,100,userInfo.Pd_Yahoo),
					DbHelper.MakeInParam("@pd_sign", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_sign),
					DbHelper.MakeInParam("@pd_logo", (DbType)SqlDbType.Int,4,userInfo.Pd_logo),
					DbHelper.MakeInParam("@pd_phone", (DbType)SqlDbType.VarChar,50,userInfo.Pd_phone),
					DbHelper.MakeInParam("@pd_mobile", (DbType)SqlDbType.VarChar,50,userInfo.Pd_mobile),
					DbHelper.MakeInParam("@pd_website", (DbType)SqlDbType.VarChar,200,userInfo.Pd_website),
					DbHelper.MakeInParam("@pd_ai_id_1", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_1),
					DbHelper.MakeInParam("@pd_address_1", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_1),
					DbHelper.MakeInParam("@pd_ai_id_2", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_2),
					DbHelper.MakeInParam("@pd_address_2", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_2),
					DbHelper.MakeInParam("@pd_ai_id_3", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_3),
					DbHelper.MakeInParam("@pd_address_3", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_3),
					DbHelper.MakeInParam("@pd_ai_id_temp", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_temp),
					DbHelper.MakeInParam("@pd_address_temp", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_temp),
					DbHelper.MakeInParam("@pd_authstr", (DbType)SqlDbType.VarChar,20,userInfo.pd_authstr),
					DbHelper.MakeInParam("@pd_idcard", (DbType)SqlDbType.VarChar,50,userInfo.pd_idcard),
					DbHelper.MakeInParam("@pd_bio", (DbType)SqlDbType.Text,16,userInfo.pd_bio)
		    };

            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}createuser", BaseConfigs.GetTablePrefix), parms), -1);
        }

        /// <summary>
        /// 更新用户信息.
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <returns>返回是否成功</returns>
        public bool UpdateUser(UserInfo userInfo)
        {
            DbParameter[] parms = {
					DbHelper.MakeInParam("@ps_en_id", (DbType)SqlDbType.Int,4,userInfo.Ps_en_id),
					DbHelper.MakeInParam("@ps_name", (DbType)SqlDbType.VarChar,50,userInfo.Ps_name),
					DbHelper.MakeInParam("@ps_nickName", (DbType)SqlDbType.VarChar,50,userInfo.Ps_nickName),
					DbHelper.MakeInParam("@ps_gender", (DbType)SqlDbType.SmallInt,2,userInfo.Ps_gender),
					DbHelper.MakeInParam("@ps_password", (DbType)SqlDbType.VarChar,200,userInfo.Ps_password),
					DbHelper.MakeInParam("@ps_pay_pass", (DbType)SqlDbType.VarChar,200,userInfo.Ps_pay_pass),
					DbHelper.MakeInParam("@ps_init", (DbType)SqlDbType.VarChar,200,userInfo.Ps_init),
					DbHelper.MakeInParam("@ps_company", (DbType)SqlDbType.VarChar,200,userInfo.Ps_company),
					DbHelper.MakeInParam("@ps_question", (DbType)SqlDbType.VarChar,1000,userInfo.Ps_question),
					DbHelper.MakeInParam("@ps_answer", (DbType)SqlDbType.VarChar,1000,userInfo.Ps_answer),
					DbHelper.MakeInParam("@ps_isLock", (DbType)SqlDbType.Bit,1,userInfo.Ps_isLock),
					DbHelper.MakeInParam("@ps_lastLogin", (DbType)SqlDbType.Char,19,userInfo.Ps_lastLogin),
					DbHelper.MakeInParam("@ps_lastChangePass", (DbType)SqlDbType.Char,19,userInfo.Ps_lastChangePass),
					DbHelper.MakeInParam("@ps_lockDate", (DbType)SqlDbType.Char,19,userInfo.Ps_lockDate),
					DbHelper.MakeInParam("@ps_email", (DbType)SqlDbType.VarChar,100,userInfo.Ps_email),
					DbHelper.MakeInParam("@ps_prev_email", (DbType)SqlDbType.VarChar,100,userInfo.Ps_prev_email),
					DbHelper.MakeInParam("@ps_regIP", (DbType)SqlDbType.VarChar,50,userInfo.Ps_regIP),
					DbHelper.MakeInParam("@ps_loginIP", (DbType)SqlDbType.VarChar,50,userInfo.Ps_loginIP),
					DbHelper.MakeInParam("@ps_star", (DbType)SqlDbType.Int,4,userInfo.Ps_star),
					DbHelper.MakeInParam("@ps_credits", (DbType)SqlDbType.Int,4,userInfo.Ps_credits),
					DbHelper.MakeInParam("@ps_scores", (DbType)SqlDbType.Int,4,userInfo.Ps_scores),
					DbHelper.MakeInParam("@ps_pg_id", (DbType)SqlDbType.Int,4,userInfo.Ps_pg_id),
					DbHelper.MakeInParam("@ps_ug_id", (DbType)SqlDbType.Int,4,userInfo.Ps_ug_id),
					DbHelper.MakeInParam("@ps_tempID", (DbType)SqlDbType.Int,4,userInfo.Ps_tempID),
					DbHelper.MakeInParam("@ps_isEmail", (DbType)SqlDbType.Int,4,userInfo.Ps_isEmail),
					DbHelper.MakeInParam("@ps_bdSound", (DbType)SqlDbType.Int,4,userInfo.Ps_bdSound),
					DbHelper.MakeInParam("@ps_onlinetime", (DbType)SqlDbType.Int,4,userInfo.Ps_onlinetime),
					DbHelper.MakeInParam("@ps_isDetail", (DbType)SqlDbType.Bit,1,userInfo.Ps_isDetail),
					DbHelper.MakeInParam("@ps_isCreater", (DbType)SqlDbType.Bit,1,userInfo.Ps_isCreater),
					DbHelper.MakeInParam("@ps_creater", (DbType)SqlDbType.Int,4,userInfo.Ps_creater),
					DbHelper.MakeInParam("@ps_lastactivity", (DbType)SqlDbType.Char,19,userInfo.Ps_lastactivity),
					DbHelper.MakeInParam("@ps_secques", (DbType)SqlDbType.Char,8,userInfo.ps_secques),
					DbHelper.MakeInParam("@ps_pageviews", (DbType)SqlDbType.Int,4,userInfo.ps_pageviews),
					DbHelper.MakeInParam("@ps_issign", (DbType)SqlDbType.SmallInt,2,userInfo.ps_issign),
					DbHelper.MakeInParam("@ps_newsletter", (DbType)SqlDbType.Int,4,userInfo.ps_newsletter),
					DbHelper.MakeInParam("@ps_invisible", (DbType)SqlDbType.Int,4,userInfo.ps_invisible),
					DbHelper.MakeInParam("@ps_newMess", (DbType)SqlDbType.Int,4,userInfo.Ps_newMess),
					DbHelper.MakeInParam("@ps_newpm", (DbType)SqlDbType.Int,4,userInfo.ps_newpm),
					DbHelper.MakeInParam("@ps_salt", (DbType)SqlDbType.NChar,6,userInfo.ps_salt),
                    DbHelper.MakeInParam("@ps_status", (DbType)SqlDbType.Int,4,userInfo.Ps_status),

					DbHelper.MakeInParam("@pd_name", (DbType)SqlDbType.VarChar,50,userInfo.Pd_name),
					DbHelper.MakeInParam("@pd_birthday", (DbType)SqlDbType.VarChar,200,userInfo.Pd_birthday),
					DbHelper.MakeInParam("@pd_MSN", (DbType)SqlDbType.VarChar,100,userInfo.Pd_MSN),
					DbHelper.MakeInParam("@pd_QQ", (DbType)SqlDbType.VarChar,100,userInfo.Pd_QQ),
					DbHelper.MakeInParam("@pd_Skype", (DbType)SqlDbType.VarChar,100,userInfo.Pd_Skype),
					DbHelper.MakeInParam("@pd_Yahoo", (DbType)SqlDbType.VarChar,100,userInfo.Pd_Yahoo),
					DbHelper.MakeInParam("@pd_sign", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_sign),
					DbHelper.MakeInParam("@pd_logo", (DbType)SqlDbType.Int,4,userInfo.Pd_logo),
					DbHelper.MakeInParam("@pd_phone", (DbType)SqlDbType.VarChar,50,userInfo.Pd_phone),
					DbHelper.MakeInParam("@pd_mobile", (DbType)SqlDbType.VarChar,50,userInfo.Pd_mobile),
					DbHelper.MakeInParam("@pd_website", (DbType)SqlDbType.VarChar,200,userInfo.Pd_website),
					DbHelper.MakeInParam("@pd_ai_id_1", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_1),
					DbHelper.MakeInParam("@pd_address_1", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_1),
					DbHelper.MakeInParam("@pd_ai_id_2", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_2),
					DbHelper.MakeInParam("@pd_address_2", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_2),
					DbHelper.MakeInParam("@pd_ai_id_3", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_3),
					DbHelper.MakeInParam("@pd_address_3", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_3),
					DbHelper.MakeInParam("@pd_ai_id_temp", (DbType)SqlDbType.Int,4,userInfo.Pd_ai_id_temp),
					DbHelper.MakeInParam("@pd_address_temp", (DbType)SqlDbType.VarChar,2000,userInfo.Pd_address_temp),
					DbHelper.MakeInParam("@pd_authstr", (DbType)SqlDbType.VarChar,20,userInfo.pd_authstr),
                    DbHelper.MakeInParam("@pd_authtime", (DbType)SqlDbType.SmallDateTime,4,userInfo.pd_authtime),
					DbHelper.MakeInParam("@pd_authflag", (DbType)SqlDbType.TinyInt,1,userInfo.pd_authflag),
					DbHelper.MakeInParam("@pd_idcard", (DbType)SqlDbType.VarChar,50,userInfo.pd_idcard),
					DbHelper.MakeInParam("@pd_bio", (DbType)SqlDbType.Text,16,userInfo.pd_bio),
                    DbHelper.MakeInParam("@ps_id", (DbType)SqlDbType.Int,4,userInfo.Ps_id)
                };

            return TypeConverter.ObjectToInt(DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                                                      string.Format("{0}updateuser", BaseConfigs.GetTablePrefix),
                                                                      parms), -1) == 2;
        }

        /// <summary>
        /// 更新权限验证字符串
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="authstr">验证串</param>
        /// <param name="authflag">验证标志</param>
        public void UpdateAuthStr(int uid, string authStr, int authFlag)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid), 
									   DbHelper.MakeInParam("@authstr", (DbType)SqlDbType.VarChar, 20, authStr),
									   DbHelper.MakeInParam("@authflag", (DbType)SqlDbType.TinyInt, 2, authFlag) 
								   };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateuserauthstr", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="uid">用户id</param>
        public void UpdateUserLastvisit(int uid, string ip)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
									   DbHelper.MakeInParam("@ip", (DbType)SqlDbType.Char,15, ip)
								   };
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_lastactivity]=GETDATE(), [ps_loginIP]=@ip WHERE [ps_id] =@uid",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新Email验证信息
        /// </summary>
        /// <param name="authStr"></param>
        /// <param name="authTime"></param>
        /// <param name="uid"></param>
        public void UpdateEmailValidateInfo(string authStr, DateTime authTime, int uid)
        {
            DbParameter[] parms = 
			{
				DbHelper.MakeInParam("@authstr",(DbType)SqlDbType.VarChar,20,authStr),
                DbHelper.MakeInParam("@authtime",(DbType)SqlDbType.DateTime,8,authTime),
                DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int,4, uid)
			};
            string commandText = string.Format("UPDATE [{0}personDetail] SET [pd_authstr]=@authstr,[pd_authtime]=@authtime ,[pd_authflag]=1  WHERE [pd_id]=@uid",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 禁言用户
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        public void SetStopTalkUser(string uidList)
        {
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_ug_id]=4, [ps_pg_id]=0  WHERE [ps_id] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                uidList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmnum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        public int SetUserNewPMCount(int uid, int pmNum)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid), 
									   DbHelper.MakeInParam("@value", (DbType)SqlDbType.Int, 4, pmNum)
			                      };
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_newMess]=@value WHERE [ps_id]=@uid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取指定用户名的用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public IDataReader GetUserInfoByName(string userName)
        {
            string commandText = string.Format("SELECT [ps_id], [ps_name] FROM [{0}personInfo] WHERE [ps_name] LIKE '%{1}%'",
                                                BaseConfigs.GetTablePrefix,
                                                RegEsc(userName));
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDataReader GetUserInfoToReader(string userName)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@username", (DbType)SqlDbType.VarChar,50, userName)
			                      };
            string commandText = string.Format("SELECT TOP 1 {1} FROM [{0}personInfo] AS [u] LEFT JOIN [{0}personDetail] AS [uf] ON [u].[ps_id]=[uf].[pd_id] WHERE [u].[ps_name]=@username",
                                                BaseConfigs.GetTablePrefix,
                                                DbFields.USERS_JOIN_FIELDS);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="startUid"></param>
        /// <param name="endUid"></param>
        /// <returns></returns>
        public IDataReader GetUsers(int startUid, int endUid)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@start_uid", (DbType)SqlDbType.Int, 4, startUid),
				DbHelper.MakeInParam("@end_uid", (DbType)SqlDbType.Int, 4, endUid)
			};
            string commandText = string.Format("SELECT [ps_id] FROM [{0}personInfo] WHERE [ps_id] >= @start_uid AND [ps_id]<=@end_uid",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获取用户查询条件
        /// </summary>
        /// <param name="isLike">模糊查询</param>
        /// <param name="isPostDateTime">发帖日期</param>
        /// <param name="userName">用户名</param>
        /// <param name="nickName">昵称</param>
        /// <param name="userGroup">用户组</param>
        /// <param name="email">Email</param>
        /// <param name="credits_Start">积分起始值</param>
        /// <param name="credits_End">积分结束值 </param>
        /// <param name="lastIp">最全登录IP</param>
        /// <param name="posts">帖数</param>
        /// <param name="digestPosts">精华帖数</param>
        /// <param name="uid">Uid</param>
        /// <param name="joindateStart">注册起始日期</param>
        /// <param name="joindateEnd">注册结束日期</param>
        /// <returns></returns>
        public string Global_UserGrid_SearchCondition(bool isLike, bool isPostDateTime, string userName, string nickName, string userGroup, string email, string creditsStart, string creditsEnd, string lastIp, string posts, string digestPosts, string uid, string joinDateStart, string joinDateEnd)
        {
            string tableName = string.Format("[{0}personInfo]", BaseConfigs.GetTablePrefix);
            StringBuilder sqlBuilder = new StringBuilder(" " + tableName + ".[ps_id]>0 ");

            if (isLike)
            {
                if (!Utils.StrIsNullOrEmpty(userName))
                    sqlBuilder.AppendFormat(" AND {1}.[ps_name] like'%{0}%'", RegEsc(userName), tableName);
                if (!Utils.StrIsNullOrEmpty(nickName))
                    sqlBuilder.AppendFormat(" AND {1}.[ps_nickName] like'%{0}%'", RegEsc(nickName), tableName);
            }
            else
            {
                if (!Utils.StrIsNullOrEmpty(userName))
                    sqlBuilder.AppendFormat(" AND {1}.[ps_name] ='{0}'", userName, tableName);
                if (!Utils.StrIsNullOrEmpty(nickName))
                    sqlBuilder.AppendFormat(" AND {1}.[ps_nickName] ='{0}'", nickName, tableName);
            }

            if (TypeConverter.StrToInt(userGroup) > 0)
                sqlBuilder.AppendFormat(" AND {1}.[ps_ug_id]={0}", userGroup, tableName);

            if (!Utils.StrIsNullOrEmpty(email))
                sqlBuilder.AppendFormat(" AND {1}.[ps_email] LIKE '%{0}%'", RegEsc(email), tableName);

            if (!Utils.StrIsNullOrEmpty(creditsStart))
                sqlBuilder.AppendFormat(" AND {1}.[ps_credits] >={0}", creditsStart, tableName);

            if (!Utils.StrIsNullOrEmpty(creditsEnd))
                sqlBuilder.AppendFormat(" AND {1}.[ps_credits] <={0}", creditsEnd, tableName);

            if (!Utils.StrIsNullOrEmpty(lastIp))
                sqlBuilder.AppendFormat(" AND {1}.[ps_loginIP] LIKE '%{0}%'", RegEsc(lastIp), tableName);

            ////if (!Utils.StrIsNullOrEmpty(posts))
            ////    sqlBuilder.AppendFormat(" AND {1}.[posts] >={0}", posts, tableName);

            ////if (!Utils.StrIsNullOrEmpty(digestPosts))
            ////    sqlBuilder.AppendFormat(" AND {1}.[digestposts] >={0}", digestPosts, tableName);

            if (uid != "")
            {
                uid = uid.Replace(", ", ",");

                if (uid.IndexOf(",") == 0)
                    uid = uid.Substring(1, uid.Length - 1);

                if (uid.LastIndexOf(",") == (uid.Length - 1))
                    uid = uid.Substring(0, uid.Length - 1);

                if (uid != "")
                    sqlBuilder.AppendFormat(" AND {1}.[ps_id] IN({0})", uid, tableName);
            }

            if (isPostDateTime)
            {
                sqlBuilder.AppendFormat(" AND {1}.[ps_createDate] >='{0}'", DateTime.Parse(joinDateStart).ToString("yyyy-MM-dd HH:mm:ss"), tableName);
                sqlBuilder.AppendFormat(" AND {1}.[ps_createDate] <='{0}'", DateTime.Parse(joinDateEnd).ToString("yyyy-MM-dd HH:mm:ss"), tableName);
            }
            return sqlBuilder.ToString();
        }

        /// <summary>
        /// 获取按条件搜索得到的用户列表
        /// </summary>
        /// <param name="searchCondition">搜索条件</param>
        /// <returns></returns>
        public DataTable Global_UserGrid(string searchCondition)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}personInfo] WHERE {2}",
                                                DbFields.USERS,
                                                BaseConfigs.GetTablePrefix,
                                                searchCondition);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 获取用户查询条件
        /// </summary>
        /// <param name="getstring"></param>
        /// <returns></returns>
        public string Global_UserGrid_GetCondition(string getString)
        {
            return string.Format("[{0}personInfo].[ps_name]='{1}'", BaseConfigs.GetTablePrefix, getString);
        }

        /// <summary>
        /// 获取符合条件的用户数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Global_UserGrid_RecordCount(string condition)
        {
            string commandText = string.Format("SELECT COUNT(ps_id) FROM [{0}personInfo]  WHERE {1}", BaseConfigs.GetTablePrefix, condition);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteDataset(commandText).Tables[0].Rows[0][0]);
        }

        #endregion

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
                                                         string.Format("SELECT COUNT(ol_id) FROM [{0}online] WHERE [ol_ps_id]>0", BaseConfigs.GetTablePrefix)).Tables[0].Rows[0][0]);
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

        /// <summary>
        /// 获得指定在线用户
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns>在线用户的详细信息</returns>
        public IDataReader GetOnlineUser(int olId)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}online] WHERE [ol_id]={2}",
                                                DbFields.ONLINE,
                                                BaseConfigs.GetTablePrefix,
                                                olId);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <param name="userId">在线用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns>用户的详细信息</returns>
        public DataTable GetOnlineUser(int userId, string passWord)
        {
            DbParameter[] parms =  { 
                                        DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userId),
                                        DbHelper.MakeInParam("@password", (DbType)SqlDbType.Char, 32, passWord)
                                    };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}getonlineuser", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }

        /// <summary>
        /// 获得指定用户的详细信息
        /// </summary>
        /// <param name="userId">在线用户ID</param>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public DataTable GetOnlineUserByIP(int userId, string ip)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userId),
                                        DbHelper.MakeInParam("@ip", (DbType)SqlDbType.VarChar, 15, ip)
                                    };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}getonlineuserbyip", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }


        /// <summary>
        /// 检查在线用户验证码是否有效
        /// </summary>
        /// <param name="olid">在组用户ID</param>
        /// <param name="verifycode">验证码</param>
        /// <returns>在组用户ID</returns>
        public bool CheckUserVerifyCode(int olId, string verifyCode, string newVerifyCode)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@olid", (DbType)SqlDbType.Int, 4, olId),
                                        DbHelper.MakeInParam("@verifycode", (DbType)SqlDbType.VarChar, 10, verifyCode)
                                    };
            string commandText = string.Format("SELECT TOP 1 [ol_id] FROM [{0}online] WHERE [ol_id]=@olid and [ol_verifycode]=@verifycode",
                                                BaseConfigs.GetTablePrefix);
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, commandText, parms).Tables[0];
            parms[1].Value = newVerifyCode;

            commandText = string.Format("UPDATE [{0}online] SET [ol_verifycode]=@verifycode WHERE [ol_id]=@olid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 执行在线用户向表及缓存中添加的操作。
        /// </summary>
        /// <param name="onlineuserinfo">在组用户信息内容</param>
        /// <param name="timeout">系统设置用户多少时间即算做离线</param>
        /// <param name="deletingfrequency">删除过期用户频率(单位:分钟)</param>
        /// <returns>添加成功则返回刚刚添加的olid,失败则返回0</returns>
        public int AddOnlineUser(OnlineUserInfo onlineUserInfo, int timeOut, int deletingFrequency)
        {
            //标识需要更新用户在线状态，0表示需要更新
            int onlinestate = 1;

            // 如果timeout为负数则代表不需要精确更新用户是否在线的状态
            if (timeOut > 0)
            {
                if (onlineUserInfo.ol_ps_id > 0)
                    onlinestate = 0;
            }
            else
                timeOut = timeOut * -1;

            if (timeOut > 9999)
                timeOut = 9999;

            DbParameter[] parms = {
									   DbHelper.MakeInParam("@onlinestate",(DbType)SqlDbType.Int,4,onlinestate),
									   DbHelper.MakeInParam("@ol_ps_id",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_ps_id),
									   DbHelper.MakeInParam("@ol_ip",(DbType)SqlDbType.VarChar,50,onlineUserInfo.ol_ip),
									   DbHelper.MakeInParam("@ol_name",(DbType)SqlDbType.VarChar,50,onlineUserInfo.ol_name),
									   DbHelper.MakeInParam("@ol_nickName",(DbType)SqlDbType.VarChar,50,onlineUserInfo.ol_nickName),
									   DbHelper.MakeInParam("@ol_password",(DbType)SqlDbType.VarChar,200,onlineUserInfo.ol_password),
									   DbHelper.MakeInParam("@ol_ug_id",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_ug_id),
									   DbHelper.MakeInParam("@ol_img",(DbType)SqlDbType.VarChar,200,onlineUserInfo.ol_img),
									   DbHelper.MakeInParam("@ol_pg_id",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_pg_id),
									   DbHelper.MakeInParam("@ol_invisible",(DbType)SqlDbType.SmallInt,2,onlineUserInfo.ol_invisible),
									   DbHelper.MakeInParam("@ol_action",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_action),
									   DbHelper.MakeInParam("@ol_lastactivity",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_lastactivity),
									   DbHelper.MakeInParam("@ol_lastpostpmtime",(DbType)SqlDbType.DateTime,8,DateTime.Parse(onlineUserInfo.ol_lastpostpmtime)),
									   DbHelper.MakeInParam("@ol_lastsearchtime",(DbType)SqlDbType.DateTime,8,DateTime.Parse(onlineUserInfo.ol_lastsearchtime)),
									   DbHelper.MakeInParam("@ol_lastupdatetime",(DbType)SqlDbType.DateTime,8,DateTime.Parse(onlineUserInfo.ol_lastupdatetime)),
									   DbHelper.MakeInParam("@ol_pm_id",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_pm_id),
									   DbHelper.MakeInParam("@ol_pm_name",(DbType)SqlDbType.VarChar,200,onlineUserInfo.ol_pm_name),
									   DbHelper.MakeInParam("@ol_verifycode",(DbType)SqlDbType.VarChar,50,onlineUserInfo.ol_verifycode),
									   DbHelper.MakeInParam("@ol_newpms",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_newpms),
									   DbHelper.MakeInParam("@ol_newnotices",(DbType)SqlDbType.Int,4,onlineUserInfo.ol_newnotices)
								   };
            int olid = TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                        string.Format("{0}createonlineuser", BaseConfigs.GetTablePrefix),
                                                                        parms));

            //按照系统设置频率(默认5分钟)清除过期用户
            if (_lastRemoveTimeout == 0 || (System.Environment.TickCount - _lastRemoveTimeout) > 60000 * deletingFrequency)
            {
                DeleteExpiredOnlineUsers(timeOut);
                _lastRemoveTimeout = System.Environment.TickCount;
            }
            // 如果id值太大则重建在线表
            if (olid > 2147483000)
            {
                CreateOnlineTable();
                DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}createonlineuser", BaseConfigs.GetTablePrefix), parms);
                return 1;
            }
            return 0;
        }

        private void DeleteExpiredOnlineUsers(int timeOut)
        {
            System.Text.StringBuilder timeoutStrBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder memberStrBuilder = new System.Text.StringBuilder();

            DbParameter param = DbHelper.MakeInParam("@expires", (DbType)SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(timeOut * -1));
            IDataReader dr = DbHelper.ExecuteReader(CommandType.StoredProcedure,
                                                    string.Format("{0}getexpiredonlineuserlist", BaseConfigs.GetTablePrefix),
                                                    param);
            while (dr.Read())
            {
                timeoutStrBuilder.Append(",");
                timeoutStrBuilder.Append(dr["ol_id"].ToString());
                if (dr["ol_ps_id"].ToString() != "-1")
                {
                    memberStrBuilder.Append(",");
                    memberStrBuilder.Append(dr["ol_ps_id"].ToString());
                }
            }
            dr.Close();

            if (timeoutStrBuilder.Length > 0)
            {
                timeoutStrBuilder.Remove(0, 1);
                DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                         string.Format("{0}deleteonlineusers", BaseConfigs.GetTablePrefix),
                                         DbHelper.MakeInParam("@olidlist", (DbType)SqlDbType.VarChar, 5000, timeoutStrBuilder.Length <= 5000 ? timeoutStrBuilder.ToString() : timeoutStrBuilder.ToString().Substring(0, 5000).TrimEnd(',')));
            }
            if (memberStrBuilder.Length > 0)
            {
                memberStrBuilder.Remove(0, 1);
                DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                         string.Format("{0}updateuseronlinestates", BaseConfigs.GetTablePrefix),
                                         DbHelper.MakeInParam("@uidlist", (DbType)SqlDbType.VarChar, 5000, memberStrBuilder.Length <= 5000 ? memberStrBuilder.ToString() : memberStrBuilder.ToString().Substring(0, 5000).TrimEnd(',')));
            }
        }

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="onlinestate">在线状态，1在线</param>
        /// <returns></returns>
        public int SetUserOnlineState(int uid, int onlineState)
        {
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_status]={1},[ps_lastactivity]=GETDATE(),[ps_lastLogin]=GETDATE() WHERE [ps_id]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                onlineState,
                                                uid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        public void UpdateAction(int olId, int action, int inid)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@ol_action",(DbType)SqlDbType.Int,4,action),
                                           DbHelper.MakeInParam("@ol_lastupdatetime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))),
										   DbHelper.MakeInParam("@ol_pm_id",(DbType)SqlDbType.Int,4,inid),
										   DbHelper.MakeInParam("@ol_pm_name",(DbType)SqlDbType.VarChar,200,""),
										   DbHelper.MakeInParam("@ol_id",(DbType)SqlDbType.Int,4,olId)
								  };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateonlineaction", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作id</param>
        /// <param name="fid">版块id</param>
        /// <param name="forumname">版块名</param>
        /// <param name="tid">主题id</param>
        /// <param name="topictitle">主题名</param>
        public void UpdateAction(int olId, int action, int fid, string forumName, int tid, string topicTitle)
        {
            DbParameter[] parms = {
										   DbHelper.MakeInParam("@ol_action",(DbType)SqlDbType.Int,4,action),
                                           DbHelper.MakeInParam("@ol_lastupdatetime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))),
										   DbHelper.MakeInParam("@ol_pm_id",(DbType)SqlDbType.Int,4,fid),
										   DbHelper.MakeInParam("@ol_pm_name",(DbType)SqlDbType.VarChar,200,forumName),
										   DbHelper.MakeInParam("@titleid",(DbType)SqlDbType.Int,4,tid),
										   DbHelper.MakeInParam("@title",(DbType)SqlDbType.NVarChar,160,topicTitle),
										   DbHelper.MakeInParam("@ol_id",(DbType)SqlDbType.Int,4,olId)
								  };
            DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updateonlineaction", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdateLastTime(int olId)
        {
            DbParameter[] parms = {
                                           DbHelper.MakeInParam("@lastupdatetime", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))),
										   DbHelper.MakeInParam("@olid",(DbType)SqlDbType.Int,4,olId)
									   };
            string commandText = string.Format("UPDATE [{0}online] SET [ol_lastupdatetime]=@lastupdatetime WHERE [ol_id]=@olid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新用户最后发帖时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdatePostTime(int olId)
        {
            string commandText = string.Format("UPDATE [{0}online] SET [lastposttime]='{1}' WHERE [ol_id]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), olId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新用户最后发短消息时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdatePostPMTime(int olId)
        {
            string commandText = string.Format("UPDATE [{0}online] SET [ol_lastpostpmtime]='{1}' WHERE [ol_id]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), olId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新在线表中指定用户是否隐身
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="invisible">是否隐身</param>
        public void UpdateInvisible(int olId, int invisible)
        {
            string commandText = string.Format("UPDATE [{0}online] SET [ol_invisible]={1} WHERE [ol_id]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                invisible,
                                                olId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新在线表中指定用户的用户密码
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="password">用户密码</param>
        public void UpdatePassword(int olId, string passWord)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@password",(DbType)SqlDbType.Char,32,passWord),
									   DbHelper.MakeInParam("@olid",(DbType)SqlDbType.Int,4,olId)
								   };
            string commandText = string.Format("UPDATE [{0}online] SET [ol_password]=@password WHERE [ol_id]=@olid",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新用户IP地址
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="ip">ip地址</param>
        public void UpdateIP(int olId, string ip)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@ip",(DbType)SqlDbType.VarChar,15,ip),
									   DbHelper.MakeInParam("@olid",(DbType)SqlDbType.Int,4,olId)
								   };
            string commandText = string.Format("UPDATE [{0}online] SET [ol_ip]=@ip WHERE [ol_id]=@olid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新用户最后搜索时间
        /// </summary>
        /// <param name="olid">在线id</param>
        public void UpdateSearchTime(int olId)
        {
            string commandText = string.Format("UPDATE [{0}online] SET [ol_lastsearchtime]={1} WHERE [ol_id]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                olId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新用户的用户组
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="groupid">组名</param>
        public void UpdateGroupid(int userId, int groupId)
        {
            string commandText = string.Format("UPDATE [{0}online] SET [ol_ug_id]={1} WHERE [ol_ps_id]={2}",
                                                BaseConfigs.GetTablePrefix,
                                                groupId,
                                                userId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 删除符合条件的一个或多个用户信息
        /// </summary>
        /// <returns>删除行数</returns>
        public int DeleteRowsByIP(string ip)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@ip",(DbType)SqlDbType.VarChar,50,ip)
								   };
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_status]=0,[ps_lastactivity]=GETDATE() WHERE [ps_id] IN (SELECT [ol_ps_id] FROM [{0}online] WHERE [ol_ps_id]>0 AND [ol_ip]=@ip)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            if (ip != "0.0.0.0")
                return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}online] WHERE [ol_ps_id]=-1 AND [ol_ip]=@ip", BaseConfigs.GetTablePrefix), parms);
            return 0;
        }

        /// <summary>
        /// 删除在线表中指定在线id的行
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <returns></returns>
        public int DeleteRows(int olId)
        {
            string commandText = string.Format("DELETE FROM [{0}online] WHERE [ol_id]={1}", BaseConfigs.GetTablePrefix, olId);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        // <summary>
        /// 获得版块在线用户列表
        /// </summary>
        /// <param name="forumid">版块Id</param>
        /// <returns></returns>
        public IDataReader GetForumOnlineUserList(int forumId)
        {
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getonlineuserlistbyfid", BaseConfigs.GetTablePrefix),
                                          DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, forumId));
        }

        /// <summary>
        /// 获得全部在线用户列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetOnlineUserList()
        {
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getonlineuserlist", BaseConfigs.GetTablePrefix));
        }

        /// <summary>
        /// 更新在线时间
        /// </summary>
        /// <param name="olTimeSpan">在线时间间隔</param>
        /// <param name="uid">当前用户id</param>
        public void UpdateOnlineTime(int oltimeSpan, int uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
                                    DbHelper.MakeInParam("@oltimespan", (DbType)SqlDbType.SmallInt, 2, oltimeSpan),
                                    DbHelper.MakeInParam("@lastupdate", (DbType)SqlDbType.DateTime, 8, DateTime.Now),
                                    DbHelper.MakeInParam("@expectedlastupdate", (DbType)SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(0 - oltimeSpan))
                                };
            string commandText = string.Format("UPDATE [{0}onlinetime] SET [thismonth]=[thismonth]+@oltimespan, [total]=[total]+@oltimespan, [lastupdate]=@lastupdate WHERE [uid]=@uid AND [lastupdate]<=@expectedlastupdate",
                                                BaseConfigs.GetTablePrefix);
            if (DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) < 1)
            {
                try
                {
                    commandText = string.Format("INSERT INTO [{0}onlinetime]([uid], [thismonth], [total], [lastupdate]) VALUES(@uid, @oltimespan, @oltimespan, @lastupdate)",
                                                 BaseConfigs.GetTablePrefix);
                    DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
                }
                catch { }
            }
        }

        /// <summary>
        /// 同步在线时间
        /// </summary>
        /// <param name="uid">用户id</param>
        public void SynchronizeOnlineTime(int uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
                                  };
            string commandText = string.Format("SELECT [total] FROM [{0}onlinetime] WHERE [uid]=@uid", BaseConfigs.GetTablePrefix);
            int total = TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));

            commandText = string.Format("UPDATE [{0}personInfo] SET [ps_onlinetime]={1} WHERE [ps_onlinetime]<{1} AND [ps_id]=@uid", BaseConfigs.GetTablePrefix, total);
            if (DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms) < 1)
            {
                try
                {
                    commandText = string.Format("UPDATE [{0}onlinetime] SET [total]=(SELECT [ps_onlinetime] FROM [{0}personInfo] WHERE [ps_id]=@uid) WHERE [uid]=@uid",
                                                 BaseConfigs.GetTablePrefix);
                    DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
                }
                catch { }
            }
        }

        /// <summary>
        /// 根据uid获得olid
        /// </summary>
        /// <param name="uid">uid</param>
        /// <returns>olid</returns>
        public int GetOlidByUid(int uid)
        {
            string commandText = string.Format("SELECT [ol_id] FROM [{0}online] WHERE [ol_ps_id]={1}", BaseConfigs.GetTablePrefix, uid);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalarToStr(CommandType.Text, commandText), -1);
        }

        /// <summary>
        /// 更新用户新短消息数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="pluscount">增加量</param>
        /// <returns></returns>
        public int UpdateNewPms(int olid, int count)
        {
            DbParameter[] parms = { 
                                     DbHelper.MakeInParam("@count",(DbType)SqlDbType.Int, 4, short.Parse(count.ToString())),
                                     DbHelper.MakeInParam("@olid",(DbType)SqlDbType.Int, 4, olid)
                                  };
            string commandText = string.Format("UPDATE [{0}online] SET [ol_newpms]=@count WHERE [ol_id]=@olid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 更新用户新通知数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="plusCount">增加量</param>
        /// <returns></returns>
        public int UpdateNewNotices(int olId, int plusCount)
        {
            DbParameter[] parms = { 
                                     DbHelper.MakeInParam("@pluscount",(DbType)SqlDbType.SmallInt, 2, short.Parse(plusCount.ToString())),
                                     DbHelper.MakeInParam("@olid",(DbType)SqlDbType.Int, 4, olId)
                                  };
            string commandText = string.Format("UPDATE [{0}online] SET [ol_newnotices]=[ol_newnotices]+@pluscount WHERE [ol_id]=@olid",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }


        /// <summary>
        /// 删除在线图例
        /// </summary>
        /// <param name="groupId">用户组Id</param>
        public void DeleteOnlineList(int groupId)
        {
            string commandText = string.Format("DELETE FROM [{0}userGroupIcon] WHERE [ui_id]={1}",
                                                BaseConfigs.GetTablePrefix,
                                                groupId);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 添加在线用户组图例
        /// </summary>
        /// <param name="grouptitle"></param>
        public void AddOnlineList(string groupTitle)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@groupid", (DbType)SqlDbType.Int, 4, GetMaxUserGroupId()),
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.VarChar, 200, groupTitle)
                                    };
            string commandText = string.Format("INSERT INTO [{0}userGroupIcon] ([ui_id], [ui_ug_name], [ui_img]) VALUES(@groupid,@title, '')",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #endregion

        #region 用户组usergroup usergroup基本操作

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserGroups()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}userGroup] ORDER BY [ug_id]", DbFields.USER_GROUPS, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 用户组最大ID值
        /// </summary>
        /// <returns></returns>
        public int GetMaxUserGroupId()
        {
            string commandText = string.Format("SELECT ISNULL(MAX(ug_id), 0) FROM [{0}userGroup]", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        #endregion

        #region 短消息pms基本操作

        // <summary>
        /// 获得指定ID的短消息的内容
        /// </summary>
        /// <param name="pmid">短消息pmid</param>
        /// <returns>短消息内容</returns>
        public IDataReader GetPrivateMessageInfo(int pmId)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@pmid", (DbType)SqlDbType.Int,4, pmId),
			                        };
            string commandText = string.Format("SELECT TOP 1 {0} FROM [{1}pms] WHERE [pmid]=@pmid",
                                                DbFields.PMS,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }


        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public int GetPrivateMessageCount(int userId, int folder, int state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,userId),
									   DbHelper.MakeInParam("@folder",(DbType)SqlDbType.Int,4,folder),								   
									   DbHelper.MakeInParam("@state",(DbType)SqlDbType.Int,4,state)
								   };
            return TypeConverter.ObjectToInt(
                                 DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                        string.Format("{0}getpmcount", BaseConfigs.GetTablePrefix),
                                                        parms));
        }

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        public int GetAnnouncePrivateMessageCount()
        {
            return TypeConverter.ObjectToInt(
                             DbHelper.ExecuteScalar(CommandType.Text,
                                                    string.Format("SELECT COUNT(pmid) FROM [{0}pms] WHERE [msgtoid] = 0", BaseConfigs.GetTablePrefix)));
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="__privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public int CreatePrivateMessage(PrivateMessageInfo privateMessageInfo, int saveToSentBox)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@pmid",(DbType)SqlDbType.Int,4,privateMessageInfo.Pmid),
									   DbHelper.MakeInParam("@msgfrom",(DbType)SqlDbType.NVarChar,20,privateMessageInfo.Msgfrom),
									   DbHelper.MakeInParam("@msgfromid",(DbType)SqlDbType.Int,4,privateMessageInfo.Msgfromid),
									   DbHelper.MakeInParam("@msgto",(DbType)SqlDbType.NVarChar,20,privateMessageInfo.Msgto),
									   DbHelper.MakeInParam("@msgtoid",(DbType)SqlDbType.Int,4,privateMessageInfo.Msgtoid),
									   DbHelper.MakeInParam("@folder",(DbType)SqlDbType.SmallInt,2,privateMessageInfo.Folder),
									   DbHelper.MakeInParam("@new",(DbType)SqlDbType.Int,4,privateMessageInfo.New),
									   DbHelper.MakeInParam("@subject",(DbType)SqlDbType.NVarChar,80,privateMessageInfo.Subject),
									   DbHelper.MakeInParam("@postdatetime",(DbType)SqlDbType.DateTime,8,DateTime.Parse(privateMessageInfo.Postdatetime)),
									   DbHelper.MakeInParam("@message",(DbType)SqlDbType.NText,0,privateMessageInfo.Message),
									   DbHelper.MakeInParam("@savetosentbox",(DbType)SqlDbType.Int,4,saveToSentBox)
								   };
            return TypeConverter.ObjectToInt(
                                 DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                        string.Format("{0}createpm", BaseConfigs.GetTablePrefix), parms), -1);
        }

        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public int DeletePrivateMessages(int userId, string pmIdList)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int,4, userId)
			                      };
            string commandText = string.Format("DELETE FROM [{0}pms] WHERE [pmid] IN ({1}) AND ([msgtoid] = @userid OR [msgfromid] = @userid)",
                                                BaseConfigs.GetTablePrefix,
                                                pmIdList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得新短消息数
        /// </summary>
        /// <returns></returns>
        public int GetNewPMCount(int userId)
        {
            string commandText = string.Format("SELECT COUNT([pmid]) AS [pmcount] FROM [{0}pms] WHERE [new] = 1 AND [folder] = 0 AND [msgtoid] = {1}",
                                                BaseConfigs.GetTablePrefix,
                                                userId);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public int SetPrivateMessageState(int pmId, byte state)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@pmid", (DbType)SqlDbType.Int,1,pmId),
									   DbHelper.MakeInParam("@state",(DbType)SqlDbType.TinyInt,1,state)
								   };
            string commandText = string.Format("UPDATE [{0}pms] SET [new]=@state WHERE [pmid]=@pmid", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        public IDataReader GetPrivateMessageList(int userId, int folder, int pageSize, int pageIndex, int intType)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@userid",(DbType)SqlDbType.Int,4,userId),
									   DbHelper.MakeInParam("@folder",(DbType)SqlDbType.Int,4,folder),
									   DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int,4,pageSize),
									   DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageIndex),
								       DbHelper.MakeInParam("@inttype",(DbType)SqlDbType.VarChar,500,intType)
								   };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}getpmlist", BaseConfigs.GetTablePrefix), parms);
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数,为-1时返回全部</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>短信息列表</returns>
        public IDataReader GetAnnouncePrivateMessageList(int pageSize, int pageIndex)
        {
            string commandText = "";
            if (pageSize == -1)
                commandText = string.Format("SELECT {0} FROM [{1}pms] WHERE [msgtoid] = 0 ORDER BY [pmid] DESC",
                                             DbFields.PMS, BaseConfigs.GetTablePrefix);
            else if (pageIndex <= 1)
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}pms] WHERE [msgtoid] = 0 ORDER BY [pmid] DESC",
                                             pageSize, DbFields.PMS, BaseConfigs.GetTablePrefix);
            else
                commandText = string.Format("SELECT TOP {0} {1} FROM [{2}pms] WHERE [msgtoid] = 0 AND [pmid] < (SELECT MIN([pmid]) FROM (SELECT TOP {3} [pmid] FROM [{2}pms] WHERE [msgtoid] = 0  ORDER BY [pmid] DESC) AS tblTmp)  ORDER BY [pmid] DESC",
                                             pageSize, DbFields.PMS, BaseConfigs.GetTablePrefix, (pageIndex - 1) * pageSize);

            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新短信发送和接收者的用户名
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="newUserName">新用户名</param>
        public void UpdatePMSenderAndReceiver(int uid, string newUserName)
        {
            DbParameter[] parms =  { 
                                        DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid),
                                        DbHelper.MakeInParam("@username", (DbType)SqlDbType.VarChar, 20, newUserName)
                                    };
            string commandText = string.Format("UPDATE [{0}pms] SET [msgfrom]=@username WHERE [msgfromid]=@uid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);

            commandText = string.Format("UPDATE [{0}pms] SET [msgto]=@username  WHERE [msgtoid]=@uid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        // <summary>
        /// 删除短消息
        /// </summary>
        /// <param name="isNew">是否删除新短消息</param>
        /// <param name="postDateTime">发送日期</param>
        /// <param name="msgFromList">发送者列表</param>
        /// <param name="lowerUpper">是否区分大小写</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        /// <param name="isUpdateUserNewPm">是否更新用户短消息数</param>
        /// <returns></returns>
        public string DeletePrivateMessages(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm)
        {
            string commandText = "WHERE [pmid]>0";

            if (isNew)
                commandText += " AND [new]=0";

            if (!Utils.StrIsNullOrEmpty(postDateTime))
                commandText += string.Format(" AND DATEDIFF(day,postdatetime,getdate())>={0}", postDateTime);

            if (msgFromList != "")
            {
                commandText += " AND (";
                foreach (string msgfrom in msgFromList.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(msgfrom))
                    {
                        if (lowerUpper)
                            commandText += string.Format(" [msgfrom]='{0}' OR", msgfrom);
                        else
                            commandText += string.Format(" [msgfrom] COLLATE Chinese_PRC_CS_AS_WS ='{0}' OR", msgfrom);
                    }
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }

            if (subject != "")
            {
                commandText += " AND (";
                foreach (string sub in subject.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(sub))
                        commandText += string.Format(" [subject] LIKE '%{0}%' OR ", RegEsc(sub));
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }

            if (message != "")
            {
                commandText += " AND (";
                foreach (string mess in message.Split(','))
                {
                    if (!Utils.StrIsNullOrEmpty(mess))
                        commandText += string.Format(" [message] LIKE '%{0}%' OR ", RegEsc(mess));
                }
                commandText = commandText.Substring(0, commandText.Length - 3) + ")";
            }

            if (isUpdateUserNewPm)
            {
                DbHelper.ExecuteNonQuery(string.Format("UPDATE [{0}users] SET [newpm]=0 WHERE [uid] IN (SELECT [msgtoid] FROM [{0}pms] {1})", BaseConfigs.GetTablePrefix, commandText));
            }
            DbHelper.ExecuteNonQuery(string.Format("DELETE FROM [{0}pms] {1}", BaseConfigs.GetTablePrefix, commandText));
            return commandText;
        }

        #endregion

        


    }
}