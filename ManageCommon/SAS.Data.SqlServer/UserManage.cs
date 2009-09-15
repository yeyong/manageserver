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
        private static int _lastRemoveTimeout;
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
        public DataTable GetOnlineUser(Guid userId, string passWord)
        {
            DbParameter[] parms =  { 
                                        DbHelper.MakeInParam("@userid", (DbType)SqlDbType.UniqueIdentifier, 16, userId),
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
        public DataTable GetOnlineUserByIP(Guid userId, string ip)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@userid", (DbType)SqlDbType.UniqueIdentifier, 16, userId),
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
                if (onlineUserInfo.ol_ps_id != new Guid("00000000-0000-0000-0000-000000000000"))
                    onlinestate = 0;
            }
            else
                timeOut = timeOut * -1;

            if (timeOut > 9999)
                timeOut = 9999;

            DbParameter[] parms = {
									   DbHelper.MakeInParam("@onlinestate",(DbType)SqlDbType.Int,4,onlinestate),
									   DbHelper.MakeInParam("@ol_ps_id",(DbType)SqlDbType.UniqueIdentifier,16,onlineUserInfo.ol_ps_id),
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
                if (dr["ol_ps_id"].ToString() != "00000000-0000-0000-0000-000000000000")
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
        public int SetUserOnlineState(Guid uid, int onlineState)
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
        public void UpdateGroupid(Guid userId, int groupId)
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
            string commandText = string.Format("UPDATE [{0}personInfo] SET [ps_status]=0,[ps_lastactivity]=GETDATE() WHERE [ps_id] IN (SELECT [ol_ps_id] FROM [{0}online] WHERE [ol_ps_id]<>'00000000-0000-0000-0000-000000000000' AND [ol_ip]=@ip)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
            if (ip != "0.0.0.0")
                return DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}online] WHERE [ol_ps_id]='00000000-0000-0000-0000-000000000000' AND [ol_ip]=@ip", BaseConfigs.GetTablePrefix), parms);
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
        public void UpdateOnlineTime(int oltimeSpan, Guid uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.UniqueIdentifier, 16, uid),
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

        #endregion
    }
}
