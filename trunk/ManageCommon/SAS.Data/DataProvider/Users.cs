using System;
using System.Text;
using System.Data;

using SAS.Common;
using SAS.Entity;

namespace SAS.Data.DataProvider
{
    public class Users
    {
        /// <summary>
        /// 返回指定用户的完整信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static UserInfo GetUserInfo(int uid)
        {
            return LoadSingleUserInfo(DatabaseProvider.GetInstance().GetUserInfoToReader(uid));
        }

        public static UserInfo LoadSingleUserInfo(IDataReader reader)
        {
            UserInfo userinfo = null;
            if (reader.Read())
            {
                userinfo = new UserInfo();
                userinfo.Ps_id = TypeConverter.StrToInt(reader["Ps_id"].ToString(), 0);
                userinfo.Ps_en_id = TypeConverter.StrToInt(reader["Ps_en_id"].ToString(), 0);
                userinfo.Ps_name = reader["Ps_name"].ToString();
                userinfo.Ps_nickName = reader["Ps_nickName"].ToString();
                userinfo.Ps_password = reader["Ps_password"].ToString();
                userinfo.Ps_pay_pass = reader["Ps_pay_pass"].ToString();
                userinfo.Ps_init = reader["Ps_init"].ToString();
                userinfo.ps_secques = reader["ps_secques"].ToString();
                userinfo.Ps_isLock = TypeConverter.StrToBool(reader["Ps_isLock"].ToString().Trim(), false);
                userinfo.Ps_gender = TypeConverter.StrToInt(reader["Ps_gender"].ToString(), 0);
                userinfo.Ps_pg_id = TypeConverter.StrToInt(reader["Ps_pg_id"].ToString(), 0);
                userinfo.Ps_ug_id = TypeConverter.StrToInt(reader["Ps_ug_id"].ToString(), 0);
                userinfo.Ps_company = reader["Ps_company"].ToString();
                userinfo.Ps_regIP = reader["Ps_regIP"].ToString();
                userinfo.Ps_createDate = Utils.GetStandardDateTime(reader["Ps_createDate"].ToString());
                userinfo.Ps_lastChangePass = Utils.GetStandardDateTime(reader["Ps_lastChangePass"].ToString());
                userinfo.Ps_lockDate = Utils.GetStandardDateTime(reader["Ps_lockDate"].ToString());
                userinfo.Ps_loginIP = reader["Ps_loginIP"].ToString();
                userinfo.Ps_lastactivity = Utils.GetStandardDateTime(reader["Ps_lastactivity"].ToString());
                userinfo.Ps_lastLogin = Utils.GetStandardDateTime(reader["Ps_lastLogin"].ToString());
                userinfo.Ps_onlinetime = TypeConverter.StrToInt(reader["Ps_onlinetime"].ToString(), 0);
                userinfo.ps_pageviews = TypeConverter.StrToInt(reader["ps_pageviews"].ToString(), 0);
                userinfo.Ps_credits = TypeConverter.StrToInt(reader["Ps_credits"].ToString(), 0);
                userinfo.Ps_star = TypeConverter.StrToInt(reader["Ps_star"].ToString(), 0);
                userinfo.Ps_scores = TypeConverter.StrToInt(reader["Ps_scores"].ToString(), 0);
                userinfo.Ps_email = reader["Ps_email"].ToString();
                userinfo.Ps_prev_email = reader["Ps_prev_email"].ToString();
                userinfo.Pd_birthday = reader["Pd_birthday"].ToString();
                userinfo.ps_issign = TypeConverter.StrToInt(reader["ps_issign"].ToString(), 0);
                userinfo.Ps_tempID = TypeConverter.StrToInt(reader["Ps_tempID"].ToString(), 0);
                userinfo.Ps_bdSound = TypeConverter.StrToInt(reader["Ps_bdSound"].ToString(), 0);
                userinfo.Ps_isEmail = TypeConverter.StrToInt(reader["Ps_isEmail"].ToString(), 0);
                userinfo.ps_newsletter = (ReceivePMSettingType)TypeConverter.StrToInt(reader["ps_newsletter"].ToString(), 0);
                userinfo.ps_invisible = TypeConverter.StrToInt(reader["ps_invisible"].ToString(), 0);
                userinfo.ps_newpm = TypeConverter.StrToInt(reader["ps_newpm"].ToString(), 0);
                userinfo.Ps_newMess = TypeConverter.StrToInt(reader["Ps_newMess"].ToString(), 0);
                userinfo.Ps_status = TypeConverter.StrToInt(reader["Ps_status"].ToString(), 0);
                userinfo.Ps_isDetail = TypeConverter.StrToBool(reader["Ps_isDetail"].ToString().Trim(), true);
                userinfo.Ps_isCreater = TypeConverter.StrToBool(reader["Ps_isCreater"].ToString().Trim(), false);
                userinfo.Ps_creater = TypeConverter.StrToInt(reader["Ps_creater"].ToString(), 0);

                userinfo.Pd_website = reader["Pd_website"].ToString();
                userinfo.Pd_QQ = reader["Pd_QQ"].ToString();
                userinfo.Pd_MSN = reader["Pd_MSN"].ToString();
                userinfo.Pd_Yahoo = reader["Pd_Yahoo"].ToString();
                userinfo.Pd_Skype = reader["Pd_Skype"].ToString();
                userinfo.pd_bio = reader["pd_bio"].ToString();
                //userinfo.Avatar = reader["avatar"].ToString();
                //userinfo.Avatarwidth = TypeConverter.StrToInt(reader["avatarwidth"].ToString(), 0);
                //userinfo.Avatarheight = TypeConverter.StrToInt(reader["avatarheight"].ToString(), 0);
                userinfo.Pd_sign = reader["Pd_sign"].ToString();
                userinfo.pd_authstr = reader["pd_authstr"].ToString();
                userinfo.pd_authtime = reader["pd_authtime"].ToString();
                userinfo.pd_authflag = Byte.Parse(reader["pd_authflag"].ToString());
                userinfo.Pd_name = reader["Pd_name"].ToString();
                userinfo.pd_idcard = reader["pd_idcard"].ToString();
                userinfo.Pd_mobile = reader["Pd_mobile"].ToString();
                userinfo.Pd_phone = reader["Pd_phone"].ToString();
                //userinfo.Ignorepm = reader["ignorepm"].ToString();
                userinfo.ps_salt = reader["ps_salt"].ToString().Trim();
                userinfo.Pd_logo = TypeConverter.StrToInt(reader["pd_logo"].ToString().Trim(), 0);
                userinfo.Pd_address_1 = reader["Pd_address_1"].ToString();
                userinfo.Pd_address_2 = reader["Pd_address_2"].ToString();
                userinfo.Pd_address_3 = reader["Pd_address_3"].ToString();
                userinfo.Pd_address_temp = reader["Pd_address_temp"].ToString();
                userinfo.Pd_ai_id_1 = TypeConverter.StrToInt(reader["Pd_ai_id_1"].ToString().Trim(), 0);
                userinfo.Pd_ai_id_2 = TypeConverter.StrToInt(reader["Pd_ai_id_2"].ToString().Trim(), 0);
                userinfo.Pd_ai_id_3 = TypeConverter.StrToInt(reader["Pd_ai_id_3"].ToString().Trim(), 0);
                userinfo.Pd_ai_id_temp = TypeConverter.StrToInt(reader["Pd_ai_id_temp"].ToString().Trim(), 0);
            }
            reader.Close();
            return userinfo;
        }

        public static ShortUserInfo LoadSingleShortUserInfo(IDataReader reader)
        {
            ShortUserInfo userInfo = null;
            if (reader.Read())
            {
                userInfo = new ShortUserInfo();
                userInfo.Ps_id = TypeConverter.StrToInt(reader["Ps_id"].ToString(), 0);
                userInfo.Ps_en_id = TypeConverter.StrToInt(reader["Ps_en_id"].ToString(), 0);
                userInfo.Ps_name = reader["Ps_name"].ToString();
                userInfo.Ps_nickName = reader["Ps_nickName"].ToString();
                userInfo.Ps_password = reader["Ps_password"].ToString();
                userInfo.Ps_pay_pass = reader["Ps_pay_pass"].ToString();
                userInfo.Ps_init = reader["Ps_init"].ToString();
                userInfo.ps_secques = reader["ps_secques"].ToString();
                userInfo.Ps_isLock = TypeConverter.StrToBool(reader["Ps_isLock"].ToString().Trim(), false);
                userInfo.Ps_gender = TypeConverter.StrToInt(reader["Ps_gender"].ToString(), 0);
                userInfo.Ps_pg_id = TypeConverter.StrToInt(reader["Ps_pg_id"].ToString(), 0);
                userInfo.Ps_ug_id = TypeConverter.StrToInt(reader["Ps_ug_id"].ToString(), 0);
                userInfo.Ps_company = reader["Ps_company"].ToString();
                userInfo.Ps_regIP = reader["Ps_regIP"].ToString();
                userInfo.Ps_createDate = Utils.GetStandardDateTime(reader["Ps_createDate"].ToString());
                userInfo.Ps_lastChangePass = Utils.GetStandardDateTime(reader["Ps_lastChangePass"].ToString());
                userInfo.Ps_lockDate = Utils.GetStandardDateTime(reader["Ps_lockDate"].ToString());
                userInfo.Ps_loginIP = reader["Ps_loginIP"].ToString();
                userInfo.Ps_lastactivity = Utils.GetStandardDateTime(reader["Ps_lastactivity"].ToString());
                userInfo.Ps_lastLogin = Utils.GetStandardDateTime(reader["Ps_lastLogin"].ToString());
                userInfo.Ps_onlinetime = TypeConverter.StrToInt(reader["Ps_onlinetime"].ToString(), 0);
                userInfo.ps_pageviews = TypeConverter.StrToInt(reader["ps_pageviews"].ToString(), 0);
                userInfo.Ps_credits = TypeConverter.StrToInt(reader["Ps_credits"].ToString(), 0);
                userInfo.Ps_star = TypeConverter.StrToInt(reader["Ps_star"].ToString(), 0);
                userInfo.Ps_scores = TypeConverter.StrToInt(reader["Ps_scores"].ToString(), 0);
                userInfo.Ps_email = reader["Ps_email"].ToString();
                userInfo.Ps_prev_email = reader["Ps_prev_email"].ToString();
                //userInfo.Pd_birthday = reader["Pd_birthday"].ToString();
                userInfo.ps_issign = TypeConverter.StrToInt(reader["ps_issign"].ToString(), 0);
                userInfo.Ps_tempID = TypeConverter.StrToInt(reader["Ps_tempID"].ToString(), 0);
                userInfo.Ps_bdSound = TypeConverter.StrToInt(reader["Ps_bdSound"].ToString(), 0);
                userInfo.Ps_isEmail = TypeConverter.StrToInt(reader["Ps_isEmail"].ToString(), 0);
                userInfo.ps_newsletter = (ReceivePMSettingType)TypeConverter.StrToInt(reader["ps_newsletter"].ToString(), 0);
                userInfo.ps_invisible = TypeConverter.StrToInt(reader["ps_invisible"].ToString(), 0);
                userInfo.ps_newpm = TypeConverter.StrToInt(reader["ps_newpm"].ToString(), 0);
                userInfo.Ps_newMess = TypeConverter.StrToInt(reader["Ps_newMess"].ToString(), 0);
                userInfo.Ps_status = TypeConverter.StrToInt(reader["Ps_status"].ToString(), 0);
                userInfo.Ps_isDetail = TypeConverter.StrToBool(reader["Ps_isDetail"].ToString().Trim(), true);
                userInfo.Ps_isCreater = TypeConverter.StrToBool(reader["Ps_isCreater"].ToString().Trim(), false);
                userInfo.Ps_creater = TypeConverter.StrToInt(reader["Ps_creater"].ToString().Trim(),0);
                userInfo.ps_salt = reader["ps_salt"].ToString().Trim();
            }
            reader.Close();
            return userInfo;
        }

        /// <summary>
        /// 返回指定用户的简短信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        public static ShortUserInfo GetShortUserInfo(int uid)
        {
            return LoadSingleShortUserInfo(DatabaseProvider.GetInstance().GetShortUserInfoToReader(uid));
        }

        /// <summary>
        /// 更新用户的用户组信息
        /// </summary>
        /// <param name="uidList">用户ID列表</param>
        /// <param name="groupId">用户组ID</param>
        public static void UpdateUserGroup(string uidList, int groupId)
        {
            //DatabaseProvider.GetInstance().UpdateUserGroup(uid, userGroupId);
            DatabaseProvider.GetInstance().ChangeUserGroupByUid(groupId, uidList);
        }


        /// <summary>
        /// 根据IP查找用户
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>用户信息</returns>
        public static UserInfo GetShortUserInfoByIP(string ip)
        {
            return LoadSingleUserInfo(DatabaseProvider.GetInstance().GetUserInfoByIP(ip));
        }

        /// <summary>
        /// 根据用户名返回用户id
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户id</returns>
        public static ShortUserInfo GetShortUserInfoByName(string username)
        {
            return LoadSingleShortUserInfo(DatabaseProvider.GetInstance().GetShortUserInfoByName(username));
        }

        /// <summary>
        /// 获得用户列表DataTable
        /// </summary>
        /// <param name="pagesize">每页记录数</param>
        /// <param name="pageindex">当前页数</param>
        /// <returns>用户列表DataTable</returns>
        public static DataTable GetUserList(int pagesize, int pageindex, string column, string ordertype)
        {
            return DatabaseProvider.GetInstance().GetUserList(pagesize, pageindex, column, ordertype);
        }


        /// <summary>
        /// 检测Email和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="email">email</param>
        /// <param name="userSecques">用户安全问题答案的存储数据</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public static int CheckEmailAndSecques(string username, string email, string userSecques)
        {
            IDataReader reader = DatabaseProvider.GetInstance().CheckEmailAndSecques(username, email, userSecques);
            int userid = -1;
            if (reader.Read())
            {
                userid = Int32.Parse(reader[0].ToString());
            }
            reader.Close();
            return userid;
        }

        /// <summary>
        /// 检测密码和安全项
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <param name="userSecques">用户安全问题答案的存储数据</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        public static int CheckPasswordAndSecques(string username, string password, bool originalpassword, string userSecques)
        {
            IDataReader reader = DatabaseProvider.GetInstance().CheckPasswordAndSecques(username, password, originalpassword, userSecques);
            int userid = -1;
            if (reader.Read())
            {
                userid = Int32.Parse(reader[0].ToString());
            }
            reader.Close();
            return userid;
        }


        /// <summary>
        /// 判断用户密码是否正确
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否为未MD5密码</param>
        /// <returns>如果正确则返回uid</returns>
        public static ShortUserInfo CheckPassword(string username, string password, bool originalpassword)
        {
            IDataReader reader = DatabaseProvider.GetInstance().CheckPassword(username, password, originalpassword);
            ShortUserInfo userInfo = null;

            if (reader.Read())
            {
                userInfo = new ShortUserInfo();
                userInfo.Ps_id = Utils.StrToInt(reader[0].ToString(), -1);
                userInfo.Ps_ug_id = Utils.StrToInt(reader[1].ToString(), -1);
                userInfo.Ps_pg_id = Utils.StrToInt(reader[2].ToString(), -1);
            }
            reader.Close();
            return userInfo;
        }

        /// <summary>
        /// 检测密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="originalpassword">是否非MD5密码</param>
        /// <returns>如果用户密码正确则返回uid, 否则返回-1</returns>
        public static ShortUserInfo CheckPassword(int uid, string password, bool originalpassword)
        {
            IDataReader reader = DatabaseProvider.GetInstance().CheckPassword(uid, password, originalpassword);
            ShortUserInfo userInfo = null;

            if (reader.Read())
            {
                userInfo = new ShortUserInfo();
                userInfo.Ps_id = Utils.StrToInt(reader[0].ToString(), -1);
                userInfo.Ps_ug_id = Utils.StrToInt(reader[1].ToString(), -1);
                userInfo.Ps_pg_id = Utils.StrToInt(reader[2].ToString(), -1);
            }
            reader.Close();
            return userInfo;
        }

        /// <summary>
        /// 根据指定的email查找用户并返回用户uid
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns>用户uid</returns>
        public static int FindUserEmail(string email)
        {
            IDataReader reader = DatabaseProvider.GetInstance().FindUserEmail(email);

            int uid = -1;
            if (reader.Read())
            {
                uid = Utils.StrToInt(reader[0].ToString(), -1);
            }
            reader.Close();
            return uid;
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public static int GetUserCount()
        {
            return DatabaseProvider.GetInstance().GetUserCount();
        }

        /// <summary>
        /// 得到论坛中用户总数
        /// </summary>
        /// <returns>用户总数</returns>
        public static int GetUserCountByAdmin()
        {
            return DatabaseProvider.GetInstance().GetUserCountByAdmin();
        }

        /// <summary>
        /// 创建新用户.
        /// </summary>
        /// <param name="__userinfo">用户信息</param>
        /// <returns>返回用户ID, 如果已存在该用户名则返回-1</returns>
        public static int CreateUser(UserInfo userinfo)
        {
            return DatabaseProvider.GetInstance().CreateUser(userinfo);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateUser(UserInfo userinfo)
        {
            return DatabaseProvider.GetInstance().UpdateUser(userinfo);
        }

        /// <summary>
        /// 更新权限验证字符串
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="authstr">验证串</param>
        /// <param name="authflag">验证标志</param>
        public static void UpdateAuthStr(int uid, string authstr, int authflag)
        {
            DatabaseProvider.GetInstance().UpdateAuthStr(uid, authstr, authflag);
        }


        /////// <summary>
        /////// 更新指定用户的个人资料
        /////// </summary>
        /////// <param name="__userinfo">用户信息</param>
        /////// <returns>如果用户不存在则为false, 否则为true</returns>
        ////public static void UpdateUserProfile(UserInfo userinfo)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserProfile(userinfo);
        ////}

        /////// <summary>
        /////// 更新用户论坛设置
        /////// </summary>
        /////// <param name="__userinfo">用户信息</param>
        /////// <returns>如果用户不存在则返回false, 否则返回true</returns>
        ////public static void UpdateUserForumSetting(UserInfo userinfo)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserForumSetting(userinfo);
        ////}

        ///// <summary>
        ///// 修改用户自定义积分字段的值
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="extid">扩展字段序号(1-8)</param>
        ///// <param name="pos">增加的数值(可以是负数)</param>
        ///// <returns>执行是否成功</returns>
        //public static void UpdateUserExtCredits(int uid, int extid, float pos)
        //{
        //    DatabaseProvider.GetInstance().UpdateUserExtCredits(uid, extid, pos);
        //}

        ///// <summary>
        ///// 获得指定用户的指定积分扩展字段的值
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="extid">扩展字段序号(1-8)</param>
        ///// <returns>值</returns>
        //public static float GetUserExtCredits(int uid, int extid)
        //{
        //    return DatabaseProvider.GetInstance().GetUserExtCredits(uid, extid);
        //}

        /////// <summary>
        /////// 更新用户头像
        /////// </summary>
        /////// <param name="uid">用户id</param>
        /////// <param name="avatar">头像</param>
        /////// <param name="avatarwidth">头像宽度</param>
        /////// <param name="avatarheight">头像高度</param>
        /////// <param name="templateid">模板Id</param>
        /////// <returns>如果用户不存在则返回false, 否则返回true</returns>
        ////public static void UpdateUserPreference(int uid, string avatar, int avatarwidth, int avatarheight, int templateid)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserPreference(uid, avatar, avatarwidth, avatarheight, templateid);
        ////}

        /////// <summary>
        /////// 更新用户密码
        /////// </summary>
        /////// <param name="uid">用户id</param>
        /////// <param name="password">密码</param>
        /////// <param name="originalpassword">是否非MD5密码</param>
        /////// <returns>成功返回true否则false</returns>
        ////public static void UpdateUserPassword(int uid, string password, bool originalpassword)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserPassword(uid, password, originalpassword);
        ////}

        /////// <summary>
        /////// 更新用户安全问题
        /////// </summary>
        /////// <param name="uid">用户id</param>
        /////// <param name="userSecques">用户安全问题答案的存储数据</param>
        /////// <returns>成功返回true否则false</returns>
        ////public static void UpdateUserSecques(int uid, string userSecques)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserSecques(uid, userSecques);
        ////}


        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="uid">用户id</param>
        public static void UpdateUserLastvisit(int uid, string ip)
        {
            DatabaseProvider.GetInstance().UpdateUserLastvisit(uid, ip);
        }

        /////// <summary>
        /////// 更新用户当前的在线状态
        /////// </summary>
        /////// <param name="uidlist">用户uid列表</param>
        /////// <param name="state">当前在线状态(0:离线,1:在线)</param>
        ////public static void UpdateUserOnlineState(string uidlist, int state, string activitytime)
        ////{
        ////    switch (state)
        ////    {
        ////        case 0:		//正常退出
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uidlist, 0, activitytime);
        ////            break;
        ////        case 1:		//正常登录
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uidlist, 1, activitytime);
        ////            break;
        ////        case 2:		//超时退出
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uidlist, 0, activitytime);
        ////            break;
        ////        case 3:		//隐身登录
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uidlist, 0, activitytime);
        ////            break;
        ////    }
        ////}

        /////// <summary>
        /////// 更新用户当前的在线状态
        /////// </summary>
        /////// <param name="uid">用户uid列表</param>
        /////// <param name="state">当前在线状态(0:离线,1:在线)</param>
        ////public static void UpdateUserOnlineState(int uid, int state, string activitytime)
        ////{
        ////    switch (state)
        ////    {
        ////        case 0:		//正常退出
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uid, 0, activitytime);
        ////            break;
        ////        case 1:		//正常登录
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uid, 1, activitytime);
        ////            break;
        ////        case 2:		//超时退出
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastActivity(uid, 0, activitytime);
        ////            break;
        ////        case 3:		//隐身登录
        ////            DatabaseProvider.GetInstance().UpdateUserOnlineStateAndLastVisit(uid, 0, activitytime);
        ////            break;
        ////    }
        ////}

        /////// <summary>
        /////// 更新用户当前的在线时间和最后活动时间
        /////// </summary>
        /////// <param name="uid">用户uid</param>
        ////public static void UpdateUserOnlineTime(int uid, string activitytime)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserLastActivity(uid, activitytime);
        ////}

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmnum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        public static int SetUserNewPMCount(int uid, int pmnum)
        {
            return DatabaseProvider.GetInstance().SetUserNewPMCount(uid, pmnum);
        }

        /////// <summary>
        /////// 将用户的未读短信息数量减小一个指定的值
        /////// </summary>
        /////// <param name="uid">用户ID</param>
        /////// <param name="subval">短消息将要减小的值,负数为加</param>
        /////// <returns>更新记录个数</returns>
        ////public static int DecreaseNewPMCount(int uid, int subval)
        ////{
        ////    return DatabaseProvider.GetInstance().DecreaseNewPMCount(uid, subval);
        ////}

        ///// <summary>
        ///// 更新用户精华数
        ///// </summary>
        ///// <param name="useridlist">uid列表</param>
        ///// <returns></returns>
        //public static int UpdateUserDigest(string useridlist)
        //{
        //    return DatabaseProvider.GetInstance().UpdateUserDigest(useridlist);
        //}

        ///// <summary>
        ///// 更新用户SpaceID
        ///// </summary>
        ///// <param name="spaceid">要更新的SpaceId</param>
        ///// <param name="userid">要更新的UserId</param>
        ///// <returns>是否更新成功</returns>
        //public static void UpdateUserSpaceId(int spaceid, int userid)
        //{
        //    DatabaseProvider.GetInstance().UpdateUserSpaceId(spaceid, userid);
        //}

        /////// <summary>
        /////// 根据验证字串获取用户Id
        /////// </summary>
        /////// <param name="authStr"></param>
        /////// <returns></returns>
        ////public static DataTable GetUserIdByAuthStr(string authstr)
        ////{
        ////    return DatabaseProvider.GetInstance().GetUserIdByAuthStr(authstr);
        ////}

        /////// <summary>
        /////// 获取指定组的用户列表
        /////// </summary>
        /////// <param name="groupIdList"></param>
        /////// <returns></returns>
        ////public static DataTable GetUsers(string groupIdList)
        ////{
        ////    return DatabaseProvider.GetInstance().GetUsers(groupIdList);
        ////}

        ///// <summary>
        ///// 通过RewriteName获取用户ID
        ///// </summary>
        ///// <param name="rewritename"></param>
        ///// <returns></returns>
        //public static int GetUserIdByRewriteName(string rewritename)
        //{
        //    return DatabaseProvider.GetInstance().GetUserIdByRewriteName(rewritename);
        //}

        /////// <summary>
        /////// 更新用户短消息设置
        /////// </summary>
        /////// <param name="user">用户信息</param>
        ////public static void UpdateUserPMSetting(UserInfo user)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserPMSetting(user);
        ////}

        /////// <summary>
        /////// 更新被禁止的用户
        /////// </summary>
        /////// <param name="groupid">用户组id</param>
        /////// <param name="groupexpiry">过期时间</param>
        /////// <param name="uid">用户id</param>
        ////public static void UpdateBanUser(int groupid, string groupexpiry, int uid)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateBanUser(groupid, groupexpiry, uid);
        ////}

        /////// <summary>
        /////// 搜索特定板块特殊用户
        /////// </summary>
        /////// <param name="fid">板块id</param>
        /////// <returns></returns>
        ////public static DataTable SearchSpecialUser(int fid)
        ////{
        ////    return DatabaseProvider.GetInstance().SearchSpecialUser(fid);
        ////}

        /////// <summary>
        /////// 更新特定板块特殊用户
        /////// </summary>
        /////// <param name="permuserlist">特殊用户列表</param>
        /////// <param name="fid">板块id</param>
        ////public static void UpdateSpecialUser(string permuserlist, int fid)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateSpecialUser(permuserlist, fid);
        ////}

        ///// <summary>
        ///// 得到指定用户的指定积分扩展字段的积分值
        ///// </summary>
        ///// <param name="uid">指定用户id</param>
        ///// <param name="extnumber">指定扩展字段</param>
        ///// <returns>扩展字展积分值</returns>
        //public static int GetUserExtCreditsByUserid(int uid, int extnumber)
        //{
        //    return DatabaseProvider.GetInstance().GetUserExtCreditsByUserid(uid, extnumber);
        //}

        ///// <summary>
        ///// 更新用户勋章信息
        ///// </summary>
        ///// <param name="uid">用户Id</param>
        ///// <param name="medals">勋章信息</param>
        //public static void UpdateMedals(int uid, string medals)
        //{
        //    DatabaseProvider.GetInstance().UpdateMedals(uid, medals);
        //}

        /// <summary>
        /// 更改用户组用户的管理权限
        /// </summary>
        /// <param name="adminId">管理组Id</param>
        /// <param name="groupId">用户组Id</param>
        public static void UpdateUserAdminIdByGroupId(int adminId, int groupId)
        {
            DatabaseProvider.GetInstance().UpdateUserAdminIdByGroupId(adminId, groupId);
        }

        /// <summary>
        /// 更新用户到禁言组
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        public static void UpdateUserToStopTalkGroup(string uidList)
        {
            DatabaseProvider.GetInstance().SetStopTalkUser(uidList);
        }

        ///// <summary>
        ///// 清除用户所发帖数以及精华数
        ///// </summary>
        ///// <param name="uid">用户Id</param>
        //public static void ClearPosts(int uid)
        //{
        //    DatabaseProvider.GetInstance().ClearPosts(uid);
        //}

        /// <summary>
        /// 更新Email验证信息
        /// </summary>
        /// <param name="authstr">验证字符串</param>
        /// <param name="authtime">验证时间</param>
        /// <param name="uid">用户Id</param>
        public static void UpdateEmailValidateInfo(string authstr, DateTime authTime, int uid)
        {
            DatabaseProvider.GetInstance().UpdateEmailValidateInfo(authstr, authTime, uid);
        }

        /////// <summary>
        /////// 更新用户积分
        /////// </summary>
        /////// <param name="credits">积分</param>
        ////public static void UpdateUserCredits(string credits)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserCredits(credits);
        ////}

        /////// <summary>
        /////// 获取用户组列表中的所有用户
        /////// </summary>
        /////// <param name="groupIdList">用户组列表</param>
        /////// <returns></returns>
        ////public static DataTable GetUserListByGroupid(string groupIdList)
        ////{
        ////    return DatabaseProvider.GetInstance().GetUserListByGroupid(groupIdList);
        ////}

        /// <summary>
        /// 获取当前页用户列表
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentPage">当前页数</param>
        /// <returns></returns>
        public static DataTable GetUserListByCurrentPage(int pageSize, int currentPage)
        {
            return DatabaseProvider.GetInstance().GetUserList(pageSize, currentPage);
        }

        /////// <summary>
        /////// 获取用户名列表指定的Email列表
        /////// </summary>
        /////// <param name="userNameList">用户名列表</param>
        /////// <returns></returns>
        ////public static DataTable GetEmailListByUserNameList(string userNameList)
        ////{
        ////    return DatabaseProvider.GetInstance().MailListTable(userNameList);
        ////}

        /////// <summary>
        /////// 获取用户组Id列表指定的Email列表
        /////// </summary>
        /////// <param name="userNameList">用户名列表</param>
        /////// <returns></returns>
        ////public static DataTable GetEmailListByGroupidList(string groupidList)
        ////{
        ////    return DatabaseProvider.GetInstance().GetUserEmailByGroupid(groupidList);
        ////}

        /////// <summary>
        /////// 将Uid列表中的用户更新到目标组中
        /////// </summary>
        /////// <param name="groupid">目标组</param>
        /////// <param name="uidList">用户列表</param>
        ////public static void UpdateUserGroupByUidList(int groupid, string uidList)
        ////{
        ////    DatabaseProvider.GetInstance().ChangeUserGroupByUid(groupid, uidList);
        ////}

        /////// <summary>
        /////// 按用户Id列表删除用户
        /////// </summary>
        /////// <param name="uidList">用户Id列表</param>
        ////public static void DeleteUsers(string uidList)
        ////{
        ////    //TODO:是否应该调用DeleteUser方法？
        ////    DatabaseProvider.GetInstance().DeleteUserByUidlist(uidList);
        ////}

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="delPosts">是否删除帖子</param>
        /// <param name="delPms">是否删除短信</param>
        /// <returns></returns>
        public static bool DeleteUser(int uid, bool delPosts, bool delPms)
        {
            return DatabaseProvider.GetInstance().DelUserAllInf(uid, delPosts, delPms);
        }

        /////// <summary>
        /////// 清空用户Id列表中的验证码
        /////// </summary>
        /////// <param name="uidList">用户Id列表</param>
        ////public static void ClearUsersAuthstr(string uidList)
        ////{
        ////    DatabaseProvider.GetInstance().ClearAuthstrByUidlist(uidList);
        ////}

        /////// <summary>
        /////// 搜索未审核用户              
        /////// </summary>
        /////// <param name="searchUserName">用户名</param>
        /////// <param name="regBefore">注册时间</param>
        /////// <param name="regIp">注册IP</param>
        /////// <returns></returns>
        ////public static DataTable AuditNewUserClear(string searchUserName, string regBefore, string regIp)
        ////{
        ////    return DatabaseProvider.GetInstance().AuditNewUserClear(searchUserName, regBefore, regIp);
        ////}

        /////// <summary>
        /////// 获取用户Id列表中的用户
        /////// </summary>
        /////// <param name="uidList">用户Id列表</param>
        /////// <returns></returns>
        ////public static DataTable GetUsersByUidlLst(string uidList)
        ////{
        ////    return DatabaseProvider.GetInstance().GetUsersByUidlLst(uidList);
        ////}

        ///// <summary>
        ///// 获取版块版主
        ///// </summary>
        ///// <param name="fid">版块Id</param>
        ///// <returns></returns>
        //public static DataTable GetModerators(int fid)
        //{
        //    return DatabaseProvider.GetInstance().GetModerators(fid);
        //}

        /// <summary>
        /// 获取模糊匹配用户名的用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static IDataReader GetUserListByUserName(string userName)
        {
            return DatabaseProvider.GetInstance().GetUserInfoByName(userName);
        }

        ///// <summary>
        ///// 更新用户签名，来自，简介三个字段
        ///// </summary>
        ///// <param name="__userinfo"></param>
        ///// <returns></returns>
        //public static void UpdateUserShortInfo(string location, string bio, string signature, int uid)
        //{
        //    DatabaseProvider.GetInstance().UpdateUserShortInfo(location, bio, signature, uid);
        //}

        public static UserInfo GetUserInfo(string userName)
        {
            IDataReader info = DatabaseProvider.GetInstance().GetUserInfoToReader(userName);
            return LoadSingleUserInfo(info);
        }

        ///// <summary>
        ///// 设置用户为版主
        ///// </summary>
        ///// <param name="userName">用户名</param>
        //public static void SetUserToModerator(string userName)
        //{
        //    DatabaseProvider.GetInstance().SetModerator(userName);
        //}

        ///// <summary>
        ///// 合并用户
        ///// </summary>
        ///// <param name="postTableName">分表名称</param>
        ///// <param name="targetUserInfo">目标用户</param>
        ///// <param name="srcUserInfo">要合并用户</param>
        //public static void CombinationUser(string postTableName, UserInfo targetUserInfo, UserInfo srcUserInfo)
        //{
        //    DatabaseProvider.GetInstance().CombinationUser(postTableName, targetUserInfo, srcUserInfo);
        //}

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="start_uid">起始UId</param>
        /// <param name="end_uid">终止UID</param>
        /// <returns></returns>
        public static IDataReader GetUsers(int start_uid, int end_uid)
        {
            return DatabaseProvider.GetInstance().GetUsers(start_uid, end_uid);
        }

        ///// <summary>
        ///// 更新用户帖数
        ///// </summary>
        ///// <param name="postcount">帖子数</param>
        ///// <param name="userid">用户ID</param>
        //public static void UpdateUserPostCount(int postcount, int userid)
        //{
        //    DatabaseProvider.GetInstance().UpdateUserPostCount(postcount, userid);
        //}

        ///// <summary>
        ///// 重建用户精华帖数
        ///// </summary>
        ///// <param name="userid">用户ID</param>
        //public static void ResetUserDigestPosts(int userid)
        //{
        //    DatabaseProvider.GetInstance().ResetUserDigestPosts(userid);
        //}

        /////// <summary>
        /////// 获取指定数量的用户
        /////// </summary>
        /////// <param name="statcount">获取数量</param>
        /////// <param name="lastuid">最小用户ID</param>
        /////// <returns></returns>
        ////public static IDataReader GetTopUsers(int statcount, int lastuid)
        ////{
        ////    return DatabaseProvider.GetInstance().GetTopUsers(statcount, lastuid);
        ////}

        /////// <summary>
        /////// 得到最后注册的用户ID和用户名
        /////// </summary>
        /////// <param name="lastuserid">输出参数：最后注册的用户ID</param>
        /////// <param name="lastusername">输出参数：最后注册的用户名</param>
        /////// <returns>存在返回true,不存在返回false</returns>
        ////public static bool GetLastUserInfo(out string lastuserid, out string lastusername)
        ////{
        ////    return DatabaseProvider.GetInstance().GetLastUserInfo(out lastuserid, out lastusername);
        ////}

        /////// <summary>
        /////// 更新普通用户用户组
        /////// </summary>
        /////// <param name="groupid">用户组id</param>
        /////// <param name="userid">用户ID</param>
        ////public static void UpdateUserOtherInfo(int groupid, int userid)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserOtherInfo(groupid, userid);
        ////}

        /////// <summary>
        /////// 更新在线表用户信息
        /////// </summary>
        /////// <param name="groupid">用户组id</param>
        /////// <param name="userid">用户ID</param>
        ////public static void UpdateUserOnlineInfo(int groupid, int userid)
        ////{
        ////    DatabaseProvider.GetInstance().UpdateUserOnlineInfo(groupid, userid);
        ////}

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
        public static string GetUsersSearchCondition(bool isLike, bool isPostDateTime, string userName, string nickName,
            string userGroup, string email, string credits_Start, string credits_End, string lastIp, string posts, string digestPosts,
            string uid, string joindateStart, string joindateEnd)
        {
            return DatabaseProvider.GetInstance().Global_UserGrid_SearchCondition(isLike, isPostDateTime, userName, nickName,
                userGroup, email, credits_Start, credits_End, lastIp, posts, digestPosts, uid, joindateStart, joindateEnd);
        }

        /// <summary>
        /// 获取按条件搜索得到的用户列表
        /// </summary>
        /// <param name="searchCondition">搜索条件</param>
        /// <returns></returns>
        public static DataTable GetUsersByCondition(string searchCondition)
        {
            return DatabaseProvider.GetInstance().Global_UserGrid(searchCondition);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="currentpage">当前页</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetUserList(int pagesize, int currentpage, string condition)
        {
            return DatabaseProvider.GetInstance().UserList(pagesize, currentpage, condition);
        }

        /// <summary>
        /// 获取符合条件的用户数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int GetUserCount(string condition)
        {
            return DatabaseProvider.GetInstance().Global_UserGrid_RecordCount(condition);
        }

        /// <summary>
        /// 获取用户查询条件
        /// </summary>
        /// <param name="getstring"></param>
        /// <returns></returns>
        public static string GetUserListCondition(string getstring)
        {
            return DatabaseProvider.GetInstance().Global_UserGrid_GetCondition(getstring);
        }
    }
}
