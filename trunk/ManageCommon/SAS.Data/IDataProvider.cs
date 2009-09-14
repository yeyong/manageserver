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
        #region 用户user操作

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDataReader GetUserInfoToReader(Guid uid);

        /// <summary>
        /// 返回指定用户的简短信息
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>用户信息</returns>
        IDataReader GetShortUserInfoToReader(Guid uid);

        /// <summary>
        /// 更改用户组
        /// </summary>
        /// <param name="groupId">目标组</param>
        /// <param name="uidList">用户列表</param>
        void ChangeUserGroupByUid(int groupId, string uidList);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        IDataReader GetUserInfoByIP(string ip);

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IDataReader GetShortUserInfoByName(string userName);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="column"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        DataTable GetUserList(int pageSize, int pageIndex, string column, string orderType);

        /// <summary>
        /// 检查Email和安全问题
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">email</param>
        /// <param name="userSecques">用户安全问题答案的存储数据</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        IDataReader CheckEmailAndSecques(string userName, string email, string secques);

        /// <summary>
        /// 检查密码和安全问题
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="originalPassword">是否非MD5密码</param>
        /// <param name="secques">用户安全问题答案的存储数据</param>
        /// <returns>如果正确则返回用户id, 否则返回-1</returns>
        IDataReader CheckPasswordAndSecques(string userName, string passWord, bool originalPassword, string secques);

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="originalPassWord">是否为未MD5密码</param>
        /// <returns>如果正确则返回uid</returns>
        IDataReader CheckPassword(string userName, string passWord, bool originalPassWord);

        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="passWord">密码</param>
        /// <param name="originalPassword">是否非MD5密码</param>
        /// <returns>如果用户密码正确则返回uid, 否则返回-1</returns>
        IDataReader CheckPassword(Guid uid, string passWord, bool originalPassword);

        /// <summary>
        /// 查找用户Email
        /// </summary>
        /// <param name="email">email地址</param>
        /// <returns>用户uid</returns>
        IDataReader FindUserEmail(string email);

        /// <summary>
        /// 获取用户数
        /// </summary>
        /// <returns></returns>
        int GetUserCount();

        /// <summary>
        /// 获取有管理权限的用户数
        /// </summary>
        /// <returns></returns>
        int GetUserCountByAdmin();

        /// <summary>
        /// 创建新用户.
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <returns>返回用户ID, 如果已存在该用户名则返回-1</returns>
        Guid CreateUser(UserInfo userinfo);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <returns>是否更新成功</returns>
        bool UpdateUser(UserInfo userInfo);

        /// <summary>
        /// 更新权限验证字符串
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="authStr">验证串</param>
        /// <param name="authFlag">验证标志</param>
        void UpdateAuthStr(Guid uid, string authStr, int authFlag);

        /// <summary>
        /// 更新指定用户的个人资料
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>如果用户不存在则为false, 否则为true</returns>
        void UpdateUserProfile(UserInfo userInfo);

        /// <summary>
        /// 更新用户论坛设置
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>如果用户不存在则返回false, 否则返回true</returns>
        void UpdateUserForumSetting(UserInfo userInfo);

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="avatar">头像</param>
        /// <param name="avatarWidth">头像宽度</param>
        /// <param name="avatarHeight">头像高度</param>
        /// <param name="templateId">模板Id</param>
        /// <returns>如果用户不存在则返回false, 否则返回true</returns>
        void UpdateUserPreference(Guid uid, string avatar, int avatarWidth, int avatarHeight, int templateId);

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="passWord">密码</param>
        /// <param name="originalPassWord">是否非MD5密码</param>
        /// <returns>成功返回true否则false</returns>
        void UpdateUserPassword(Guid uid, string passWord, bool originalPassWord);

        /// <summary>
        /// 更新用户安全问题
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="secques">用户安全问题答案的存储数据</param>
        /// <returns>成功返回true否则false</returns>
        void UpdateUserSecques(Guid uid, string secques);

        /// <summary>
        /// 更新用户最后访问时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="ip"></param>
        void UpdateUserLastvisit(Guid uid, string ip);

        /// <summary>
        /// 更新用户在线信息
        /// </summary>
        /// <param name="uidList">用户uid列表</param>
        /// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        /// <param name="activityTime"></param>
        void UpdateUserOnlineStateAndLastActivity(string uidList, int onlineState, string activityTime);

        /// <summary>
        /// 更新用户在线信息
        /// </summary>
        /// <param name="uidList">用户uid列表</param>
        /// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        /// <param name="activityTime"></param>
        void UpdateUserOnlineStateAndLastVisit(string uidList, int onlineState, string activityTime);

        /// <summary>
        /// 更新用户在线信息
        /// </summary>
        /// <param name="uid">用户uid列表</param>
        /// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        /// <param name="activityTime"></param>
        void UpdateUserOnlineStateAndLastActivity(Guid uid, int onlineState, string activityTime);

        /// <summary>
        /// 更新用户在线信息
        /// </summary>
        /// <param name="uid">用户uid列表</param>
        /// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        /// <param name="activityTime"></param>
        void UpdateUserOnlineStateAndLastVisit(Guid uid, int onlineState, string activityTime);

        /// <summary>
        /// 更新用户在线时间
        /// </summary>
        /// <param name="uid">用户uid</param>
        /// <param name="activityTime"></param>
        void UpdateUserLastActivity(Guid uid, string activityTime);

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmNum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        int SetUserNewPMCount(Guid uid, int pmNum);

        /// <summary>
        /// 将用户的未读短信息数量减小一个指定的值
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="subval">短消息将要减小的值,负数为加</param>
        /// <returns>更新记录个数</returns>
        int DecreaseNewPMCount(Guid uid, int subVal);

        /// <summary>
        /// 根据验证字串获取用户Id
        /// </summary>
        /// <param name="authStr"></param>
        /// <returns></returns>
        DataTable GetUserIdByAuthStr(string authStr);

        /// <summary>
        /// 获取指定组的用户列表
        /// </summary>
        /// <param name="groupIdList"></param>
        /// <returns></returns>
        DataTable GetUsers(string groupIdList);

        /// <summary>
        /// 更新用户短消息设置
        /// </summary>
        /// <param name="user">用户信息</param>
        void UpdateUserPMSetting(UserInfo user);

        /// <summary>
        /// 更新被禁止的用户
        /// </summary>
        /// <param name="groupId">用户组id</param>
        /// <param name="groupExpiry">过期时间</param>
        /// <param name="uid">用户id</param>
        void UpdateBanUser(int groupId, string groupExpiry, Guid uid);

        /// <summary>
        /// 获取指定论坛的特殊用户
        /// </summary>
        /// <param name="fid">板块id</param>
        /// <returns></returns>
        DataTable SearchSpecialUser(int fid);

        /// <summary>
        /// 更新特定板块特殊用户
        /// </summary>
        /// <param name="permUserList">特殊用户列表</param>
        /// <param name="fid">板块id</param>
        void UpdateSpecialUser(string permUserList, int fid);

        /// <summary>
        /// 更改用户管理权限Id
        /// </summary>
        /// <param name="adminId">管理组Id</param>
        /// <param name="groupId">用户组Id</param>
        void UpdateUserAdminIdByGroupId(int adminId, int groupId);

        /// <summary>
        /// 禁言用户
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        void SetStopTalkUser(string uidList);

        /// <summary>
        /// 更新Email验证信息
        /// </summary>
        /// <param name="authStr"></param>
        /// <param name="authTime"></param>
        /// <param name="uid"></param>
        void UpdateEmailValidateInfo(string authStr, DateTime authTime, Guid uid);

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="credits">积分</param>
        void UpdateUserCredits(string credits);

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="groupIdList"></param>
        /// <returns></returns>
        DataTable GetUserListByGroupid(string groupIdList);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        DataTable GetUserList(int pageSize, int currentPage);

        /// <summary>
        /// 获取用户名列表指定的Email列表
        /// </summary>
        /// <param name="userNameList">用户名列表</param>
        /// <returns></returns>
        DataTable MailListTable(string userNameList);

        /// <summary>
        /// 获取指定组的用户Email地址
        /// </summary>
        /// <param name="groupIdList"></param>
        /// <returns></returns>
        DataTable GetUserEmailByGroupid(string groupIdList);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        void DeleteUserByUidlist(string uidList);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="delPosts"></param>
        /// <param name="delPms"></param>
        /// <returns></returns>
        bool DelUserAllInf(Guid uid, bool delPosts, bool delPms);

        /// <summary>
        /// 清空指定用户的认证串
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        void ClearAuthstrByUidlist(string uidList);

        /// <summary>
        /// 搜索未审核用户              
        /// </summary>
        /// <param name="searchUser">用户名</param>
        /// <param name="regBefore">注册时间</param>
        /// <param name="regIp">注册IP</param>
        /// <returns></returns>
        DataTable AuditNewUserClear(string searchUser, string regBefore, string regIp);

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="uidList"></param>
        /// <returns></returns>
        DataTable GetUsersByUidlLst(string uidList);

        /// <summary>
        /// 获取指定用户名的用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        IDataReader GetUserInfoByName(string userName);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IDataReader GetUserInfoToReader(string userName);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="startUid"></param>
        /// <param name="endUid"></param>
        /// <returns></returns>
        IDataReader GetUsers(Guid startUid, Guid endUid);

        /// <summary>
        /// 获取指定数量的用户
        /// </summary>
        /// <param name="statCount"></param>
        /// <param name="lastUid"></param>
        /// <returns></returns>
        IDataReader GetTopUsers(int statCount, Guid lastUid);

        /// <summary>
        /// 得到论坛中最后注册的用户ID和用户名
        /// </summary>
        /// <param name="lastuserid">输出参数：最后注册的用户ID</param>
        /// <param name="lastusername">输出参数：最后注册的用户名</param>
        /// <returns>存在返回true,不存在返回false</returns>
        bool GetLastUserInfo(out string lastUserId, out string lastUserName);

        /// <summary>
        /// 更新用户其他信息
        /// </summary>
        /// <param name="groupId">用户组id</param>
        /// <param name="userId">用户ID</param>
        void UpdateUserOtherInfo(int groupId, Guid userId);

        /// <summary>
        /// 更新在线表用户信息
        /// </summary>
        /// <param name="groupId">用户组id</param>
        /// <param name="userId">用户ID</param>
        void UpdateUserOnlineInfo(int groupId, Guid userId);

        /// <summary>
        /// 获取指定条件和分页下的用户列表信息
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable UserList(int pageSize, int currentPage, string condition);

        /// <summary>
        /// 获取按条件搜索得到的用户列表
        /// </summary>
        /// <param name="searchCondition">搜索条件</param>
        /// <returns></returns>
        DataTable Global_UserGrid(string searchCondition);

        /// <summary>
        /// 获取符合条件的用户数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Global_UserGrid_RecordCount(string condition);

        /// <summary>
        /// 获取用户查询条件
        /// </summary>
        /// <param name="getstring"></param>
        /// <returns></returns>
        string Global_UserGrid_GetCondition(string getString);

        #endregion

        #region 用户组usergroup基本操作

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        DataTable GetUserGroups();

        #endregion

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

        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        DataTable GetOnlineUser(Guid userId, string passWord);

        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ip"></param>
        /// <returns>用户的详细信息</returns>
        DataTable GetOnlineUserByIP(Guid userId, string ip);

        /// <summary>
        /// 检查用户验证码
        /// </summary>
        /// <param name="olId">在组用户ID</param>
        /// <param name="verifyCode">验证码</param>
        /// <param name="newverifyCode">新验证码</param>
        /// <returns></returns>
        bool CheckUserVerifyCode(int olId, string verifyCode, string newverifyCode);

        /// <summary>
        /// 添加在线用户
        /// </summary>
        /// <param name="onlineuserinfo">在组用户信息内容</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        int AddOnlineUser(OnlineUserInfo onlineUserInfo, int timeOut, int deletingFrequency);

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        /// <param name="uid">在线用户id</param>
        /// <param name="onlineState">在线用户状态</param>
        /// <returns></returns>
        int SetUserOnlineState(Guid uid, int onlineState);

        /// <summary>
        /// 更新用户的当前动作及相关信息
        /// </summary>
        /// <param name="olid">在线列表id</param>
        /// <param name="action">动作</param>
        /// <param name="inid">所在位置代码</param>
        void UpdateAction(int olId, int action, int inid);

        /// <summary>
        /// 更新用户动作
        /// </summary>
        /// <param name="olid">在线用户id</param>
        /// <param name="action">用户操作</param>
        /// <param name="fid">版块id</param>
        /// <param name="forumName">版块名称</param>
        /// <param name="tid">主题id</param>
        /// <param name="topicTitle">主题标题</param>
        void UpdateAction(int olId, int action, int fid, string forumName, int tid, string topicTitle);

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="olId">在线id</param>
        void UpdateLastTime(int olId);

        /// <summary>
        /// 更新用户最后发帖时间
        /// </summary>
        /// <param name="olId">在线id</param>
        void UpdatePostTime(int olId);

        /// <summary>
        /// 更新用户最后发短消息时间
        /// </summary>
        /// <param name="olId">在线id</param>
        void UpdatePostPMTime(int olId);

        /// <summary>
        /// 更新在线表中指定用户是否隐身
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="invisible">是否隐身</param>
        void UpdateInvisible(int olId, int invisible);

        /// <summary>
        /// 更新在线表中指定用户的用户密码
        /// </summary>
        /// <param name="olId">在线id</param>
        /// <param name="passWord">用户密码</param>
        void UpdatePassword(int olId, string passWord);

        /// <summary>
        /// 更新用户IP地址
        /// </summary>
        /// <param name="olId">在线id</param>
        /// <param name="ip">ip地址</param>
        void UpdateIP(int olId, string ip);

        /// <summary>
        /// 更新最后搜索时间
        /// </summary>
        /// <param name="olId">在线id</param>
        void UpdateSearchTime(int olId);

        /// <summary>
        /// 更新用户的用户组
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="groupId">组名</param>
        void UpdateGroupid(Guid userId, int groupId);

        /// <summary>
        /// 删除在线表中的一行
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>删除行数</returns>
        int DeleteRowsByIP(string ip);

        /// <summary>
        /// 删除在线表中的一行
        /// </summary>
        /// <param name="olId">在线id</param>
        /// <returns></returns>
        int DeleteRows(int olId);

        /// <summary>
        /// 获取板块在线用户列表
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        IDataReader GetForumOnlineUserList(int forumId);

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetOnlineUserList();

        /// <summary>
        /// 更新在线时间
        /// </summary>
        /// <param name="olTimeSpan">在线时间间隔</param>
        /// <param name="uid">当前用户id</param>
        void UpdateOnlineTime(int olTimeSpan, Guid uid);

        /// <summary>
        /// 同步在线时间
        /// </summary>
        /// <param name="uid">用户id</param>
        void SynchronizeOnlineTime(Guid uid);

        /// <summary>
        /// 根据Uid获得Olid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int GetOlidByUid(Guid uid);

        /// <summary>
        /// 更新用户新短消息数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="pluscount">增加量</param>
        /// <returns></returns>
        int UpdateNewPms(int olId, int plusCount);

        /// <summary>
        /// 更新用户新通知数
        /// </summary>
        /// <param name="olid">在线id</param>
        /// <param name="plusCount">增加量</param>
        /// <returns></returns>
        int UpdateNewNotices(int olId, int plusCount);

        /// <summary>
        /// 删除在线图例
        /// </summary>
        /// <param name="groupId">用户组Id</param>
        void DeleteOnlineList(int groupId);

        /// <summary>
        /// 添加在线用户组图例
        /// </summary>
        /// <param name="grouptitle"></param>
        void AddOnlineList(string grouptitle);

        #endregion

        #region 模板Templates表基本操作

        /// <summary>
        /// 获取可用模板列表
        /// </summary>
        /// <returns></returns>
        DataTable GetValidTemplateList();

        /// <summary>
        /// 获取可用模板Id列表
        /// </summary>
        /// <returns></returns>
        DataTable GetValidTemplateIDList();

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
        void AddTemplate(string name, string directory, string copyRight, string author, string createDate, string ver, string forDntVer);

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <param name="directory">模板文件所在目录</param>
        /// <param name="copyright">模板版权文字</param>
        /// <returns></returns>
        int AddTemplate(string templateName, string directory, string copyRight);

        /// <summary>
        /// 删除模板项
        /// </summary>
        /// <param name="templateId">模板id</param>
        void DeleteTemplateItem(int templateId);

        /// <summary>
        /// 删除模板项
        /// </summary>
        /// <param name="templateIdList">格式为： 1,2,3</param>
        void DeleteTemplateItem(string templateIdList);

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        DataTable GetAllTemplateList();

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
