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

        ///// <summary>
        ///// 更新指定用户的个人资料
        ///// </summary>
        ///// <param name="userInfo">用户信息</param>
        ///// <returns>如果用户不存在则为false, 否则为true</returns>
        //void UpdateUserProfile(UserInfo userInfo);

        ///// <summary>
        ///// 更新用户论坛设置
        ///// </summary>
        ///// <param name="userInfo">用户信息</param>
        ///// <returns>如果用户不存在则返回false, 否则返回true</returns>
        //void UpdateUserForumSetting(UserInfo userInfo);

        ///// <summary>
        ///// 更新用户头像
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="avatar">头像</param>
        ///// <param name="avatarWidth">头像宽度</param>
        ///// <param name="avatarHeight">头像高度</param>
        ///// <param name="templateId">模板Id</param>
        ///// <returns>如果用户不存在则返回false, 否则返回true</returns>
        //void UpdateUserPreference(Guid uid, string avatar, int avatarWidth, int avatarHeight, int templateId);

        ///// <summary>
        ///// 更新用户密码
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="passWord">密码</param>
        ///// <param name="originalPassWord">是否非MD5密码</param>
        ///// <returns>成功返回true否则false</returns>
        //void UpdateUserPassword(Guid uid, string passWord, bool originalPassWord);

        ///// <summary>
        ///// 更新用户安全问题
        ///// </summary>
        ///// <param name="uid">用户id</param>
        ///// <param name="secques">用户安全问题答案的存储数据</param>
        ///// <returns>成功返回true否则false</returns>
        //void UpdateUserSecques(Guid uid, string secques);

        /// <summary>
        /// 更新用户最后访问时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="ip"></param>
        void UpdateUserLastvisit(Guid uid, string ip);

        ///// <summary>
        ///// 更新用户在线信息
        ///// </summary>
        ///// <param name="uidList">用户uid列表</param>
        ///// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        ///// <param name="activityTime"></param>
        //void UpdateUserOnlineStateAndLastActivity(string uidList, int onlineState, string activityTime);

        ///// <summary>
        ///// 更新用户在线信息
        ///// </summary>
        ///// <param name="uidList">用户uid列表</param>
        ///// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        ///// <param name="activityTime"></param>
        //void UpdateUserOnlineStateAndLastVisit(string uidList, int onlineState, string activityTime);

        ///// <summary>
        ///// 更新用户在线信息
        ///// </summary>
        ///// <param name="uid">用户uid列表</param>
        ///// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        ///// <param name="activityTime"></param>
        //void UpdateUserOnlineStateAndLastActivity(Guid uid, int onlineState, string activityTime);

        ///// <summary>
        ///// 更新用户在线信息
        ///// </summary>
        ///// <param name="uid">用户uid列表</param>
        ///// <param name="onlineState">当前在线状态(0:离线,1:在线)</param>
        ///// <param name="activityTime"></param>
        //void UpdateUserOnlineStateAndLastVisit(Guid uid, int onlineState, string activityTime);

        ///// <summary>
        ///// 更新用户在线时间
        ///// </summary>
        ///// <param name="uid">用户uid</param>
        ///// <param name="activityTime"></param>
        //void UpdateUserLastActivity(Guid uid, string activityTime);

        /// <summary>
        /// 设置用户信息表中未读短消息的数量
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="pmNum">短消息数量</param>
        /// <returns>更新记录个数</returns>
        int SetUserNewPMCount(Guid uid, int pmNum);

        ///// <summary>
        ///// 将用户的未读短信息数量减小一个指定的值
        ///// </summary>
        ///// <param name="uid">用户ID</param>
        ///// <param name="subval">短消息将要减小的值,负数为加</param>
        ///// <returns>更新记录个数</returns>
        //int DecreaseNewPMCount(Guid uid, int subVal);

        ///// <summary>
        ///// 根据验证字串获取用户Id
        ///// </summary>
        ///// <param name="authStr"></param>
        ///// <returns></returns>
        //DataTable GetUserIdByAuthStr(string authStr);

        ///// <summary>
        ///// 获取指定组的用户列表
        ///// </summary>
        ///// <param name="groupIdList"></param>
        ///// <returns></returns>
        //DataTable GetUsers(string groupIdList);

        ///// <summary>
        ///// 更新用户短消息设置
        ///// </summary>
        ///// <param name="user">用户信息</param>
        //void UpdateUserPMSetting(UserInfo user);

        ///// <summary>
        ///// 更新被禁止的用户
        ///// </summary>
        ///// <param name="groupId">用户组id</param>
        ///// <param name="groupExpiry">过期时间</param>
        ///// <param name="uid">用户id</param>
        //void UpdateBanUser(int groupId, string groupExpiry, Guid uid);

        ///// <summary>
        ///// 获取指定论坛的特殊用户
        ///// </summary>
        ///// <param name="fid">板块id</param>
        ///// <returns></returns>
        //DataTable SearchSpecialUser(int fid);

        ///// <summary>
        ///// 更新特定板块特殊用户
        ///// </summary>
        ///// <param name="permUserList">特殊用户列表</param>
        ///// <param name="fid">板块id</param>
        //void UpdateSpecialUser(string permUserList, int fid);

        ///// <summary>
        ///// 更改用户管理权限Id
        ///// </summary>
        ///// <param name="adminId">管理组Id</param>
        ///// <param name="groupId">用户组Id</param>
        //void UpdateUserAdminIdByGroupId(int adminId, int groupId);

        /// <summary>
        /// 禁言用户
        /// </summary>
        /// <param name="uidList">用户Id列表</param>
        void SetStopTalkUser(string uidList);

        ///// <summary>
        ///// 更新Email验证信息
        ///// </summary>
        ///// <param name="authStr"></param>
        ///// <param name="authTime"></param>
        ///// <param name="uid"></param>
        //void UpdateEmailValidateInfo(string authStr, DateTime authTime, Guid uid);

        ///// <summary>
        ///// 更新用户积分
        ///// </summary>
        ///// <param name="credits">积分</param>
        //void UpdateUserCredits(string credits);

        ///// <summary>
        ///// 获取用户名
        ///// </summary>
        ///// <param name="groupIdList"></param>
        ///// <returns></returns>
        //DataTable GetUserListByGroupid(string groupIdList);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        DataTable GetUserList(int pageSize, int currentPage);

        ///// <summary>
        ///// 获取用户名列表指定的Email列表
        ///// </summary>
        ///// <param name="userNameList">用户名列表</param>
        ///// <returns></returns>
        //DataTable MailListTable(string userNameList);

        ///// <summary>
        ///// 获取指定组的用户Email地址
        ///// </summary>
        ///// <param name="groupIdList"></param>
        ///// <returns></returns>
        //DataTable GetUserEmailByGroupid(string groupIdList);

        ///// <summary>
        ///// 删除用户
        ///// </summary>
        ///// <param name="uidList">用户Id列表</param>
        //void DeleteUserByUidlist(string uidList);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="delPosts"></param>
        /// <param name="delPms"></param>
        /// <returns></returns>
        bool DelUserAllInf(Guid uid, bool delPosts, bool delPms);

        ///// <summary>
        ///// 清空指定用户的认证串
        ///// </summary>
        ///// <param name="uidList">用户Id列表</param>
        //void ClearAuthstrByUidlist(string uidList);

        ///// <summary>
        ///// 搜索未审核用户              
        ///// </summary>
        ///// <param name="searchUser">用户名</param>
        ///// <param name="regBefore">注册时间</param>
        ///// <param name="regIp">注册IP</param>
        ///// <returns></returns>
        //DataTable AuditNewUserClear(string searchUser, string regBefore, string regIp);

        ///// <summary>
        ///// 获取用户
        ///// </summary>
        ///// <param name="uidList"></param>
        ///// <returns></returns>
        //DataTable GetUsersByUidlLst(string uidList);

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

        ///// <summary>
        ///// 获取指定数量的用户
        ///// </summary>
        ///// <param name="statCount"></param>
        ///// <param name="lastUid"></param>
        ///// <returns></returns>
        //IDataReader GetTopUsers(int statCount, Guid lastUid);

        ///// <summary>
        ///// 得到论坛中最后注册的用户ID和用户名
        ///// </summary>
        ///// <param name="lastuserid">输出参数：最后注册的用户ID</param>
        ///// <param name="lastusername">输出参数：最后注册的用户名</param>
        ///// <returns>存在返回true,不存在返回false</returns>
        //bool GetLastUserInfo(out string lastUserId, out string lastUserName);

        ///// <summary>
        ///// 更新用户其他信息
        ///// </summary>
        ///// <param name="groupId">用户组id</param>
        ///// <param name="userId">用户ID</param>
        //void UpdateUserOtherInfo(int groupId, Guid userId);

        ///// <summary>
        ///// 更新在线表用户信息
        ///// </summary>
        ///// <param name="groupId">用户组id</param>
        ///// <param name="userId">用户ID</param>
        //void UpdateUserOnlineInfo(int groupId, Guid userId);

        /// <summary>
        /// 获取指定条件和分页下的用户列表信息
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable UserList(int pageSize, int currentPage, string condition);

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
        string Global_UserGrid_SearchCondition(bool isLike, bool isPostDateTime, string userName, string nickName, string userGroup, string email, string creditsStart, string creditsEnd, string lastIp, string posts, string digestPosts, string uid, string joindateStart, string joindateEnd);

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

        #region 短消息pms处理操作

        /// <summary>
        /// 获得指定ID的短消息的内容
        /// </summary>
        /// <param name="pmid">短消息pmid</param>
        /// <returns>短消息内容</returns>
        IDataReader GetPrivateMessageInfo(int pmId);

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、2:最近消息（7天内）、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        int GetPrivateMessageCount(Guid userId, int folder, int state);

        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        int GetAnnouncePrivateMessageCount();

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="privateMessageInfo">短消息内容</param>
        /// <param name="saveToSentBox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        int CreatePrivateMessage(PrivateMessageInfo privateMessageInfo, int saveToSentBox);

        /// <summary>
        /// 删除短消息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemidList">要删除的短信息列表</param>
        /// <returns>删除记录数</returns>
        int DeletePrivateMessages(Guid userId, string pmIdList);

        /// <summary>
        /// 获取新短消息数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        int GetNewPMCount(Guid userId);

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmId">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        int SetPrivateMessageState(int pmId, byte state);

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pageSize">每页显示短信息数</param>
        /// <param name="pageIndex">当前要显示的页数</param>
        /// <param name="intType">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        IDataReader GetPrivateMessageList(Guid userId, int folder, int pageSize, int pageIndex, int intType);

        /// <summary>
        /// 获得公共消息列表
        /// </summary>
        /// <param name="pageSize">每页显示短信息数</param>
        /// <param name="pageIndex">当前要显示的页数</param>
        /// <returns>公共消息列表</returns>
        IDataReader GetAnnouncePrivateMessageList(int pageSize, int pageIndex);

        /// <summary>
        /// 更新短信发送和接收者的用户名
        /// </summary>
        /// <param name="uid">Uid</param>
        /// <param name="newUserName">新用户名</param>
        void UpdatePMSenderAndReceiver(Guid uid, string newUserName);

        /// <summary>
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
        string DeletePrivateMessages(bool isnew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm);

        #endregion

        #region 通知notices处理操作

        /// <summary>
        /// 添加指定的通知信息
        /// </summary>
        /// <param name="noticeInfo">要添加的通知信息</param>
        /// <returns></returns>
        int CreateNoticeInfo(NoticeInfo noticeInfo);

        /// <summary>
        /// 更新指定的通知信息
        /// </summary>
        /// <param name="noticeInfo">要更新的通知信息</param>
        /// <returns></returns>
        bool UpdateNoticeInfo(NoticeInfo noticeInfo);

        /// <summary>
        /// 删除指定通知id的信息
        /// </summary>
        /// <param name="nid">指定的通知id</param>
        /// <returns></returns>
        bool DeleteNoticeByNid(int nid);

        /// <summary>
        /// 删除指定用户id的通知信息
        /// </summary>
        /// <param name="uid">指定的通知id</param>
        /// <returns></returns>
        bool DeleteNoticeByUid(Guid uid);

        /// <summary>
        /// 删除指定通知类型和天数内的通知
        /// </summary>
        /// <param name="noticeType">删除的通知类型</param>
        /// <param name="days">指定天数</param>
        void DeleteNotice(Noticetype noticeType, int days);

        /// <summary>
        /// 获取指定通知id的信息
        /// </summary>
        /// <param name="nid">通知id</param>
        /// <returns>通知信息</returns>
        IDataReader GetNoticeByNid(int nid);

        /// <summary>
        /// 获取指定通知id和类型的通知
        /// </summary>
        /// <param name="uid">指定通知id</param>
        /// <param name="noticeType"><see cref="Noticetype"/>通知类型</param>
        /// <param name="pageId">分页id</param>
        /// <param name="pageSize">页面尽寸</param>
        /// <returns></returns>
        IDataReader GetNoticeByUid(Guid uid, Noticetype noticeType, int pageId, int pageSize);

        /// <summary>
        /// 将某一类通知更改为未读状态
        /// </summary>
        /// <param name="type">通知类型</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        int ReNewNotice(int type, Guid uid);

        /// <summary>
        /// 获得指定用户的新通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDataReader GetNewNotices(Guid userId);

        /// <summary>
        /// 获取指定用户和通知类型的通知信息
        /// </summary>
        /// <param name="uid">指定的用户id</param>
        /// <param name="noticeType">通知类型</param>
        /// <returns></returns>
        IDataReader GetNoticeByUid(Guid uid, Noticetype noticeType);

        /// <summary>
        /// 获取指定用户id及通知类型的通知数
        /// </summary>
        /// <param name="uid">指定用户id</param>
        /// <param name="noticeType">通知类型</param>
        /// <returns></returns>
        int GetNoticeCountByUid(Guid uid, Noticetype noticeType);

        /// <summary>
        /// 获取指定用户和分页下的通知
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>通知集合</returns>
        int GetNewNoticeCountByUid(Guid uid);

        /// <summary>
        /// 更新指定用户的通知新旧状态
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="newType">通知新旧状态(1:新通知 0:旧通知)</param>
        void UpdateNoticeNewByUid(Guid uid, int newType);

        /// <summary>
        /// 得到通知数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="state">通知状态(0为已读，1为未读)</param>
        /// <returns></returns>
        int GetNoticeCount(Guid userId, int state);

        #endregion

        #region IP禁止操作

        /// <summary>
        /// 添加被禁止的ip
        /// </summary>
        /// <param name="info"></param>
        void AddBannedIp(IpInfo info);

        /// <summary>
        /// 获得被禁止ip列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetBannedIpList();

        /// <summary>
        /// 获取指定分页的禁止IP列表
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        IDataReader GetBannedIpList(int num, int pageId);

        /// <summary>
        /// 显示被禁止的ip数量
        /// </summary>
        /// <returns></returns>
        int GetBannedIpCount();

        /// <summary>
        /// 删除选中的ip地址段
        /// </summary>
        /// <param name="bannedIdList"></param>
        int DeleteBanIp(string bannedIdList);

        /// <summary>
        /// 编辑banip结束时间
        /// </summary>
        /// <param name="iplist"></param>
        /// <param name="endTime"></param>
        int UpdateBanIpExpiration(int id, string endTime);

        #endregion

        #region 统计,统计信息stats,statvars表操作

        /// <summary>
        /// 更新统计变量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        void UpdateStatVars(string type, string variable, string value);

        /// <summary>
        /// 获得所有统计信息
        /// </summary>
        /// <returns></returns>
        IDataReader GetAllStats();

        /// <summary>
        /// 获得所有统计
        /// </summary>
        /// <returns></returns>
        IDataReader GetAllStatVars();

        /// <summary>
        /// 统计板块数量
        /// </summary>
        /// <returns></returns>
        int GetForumCount();

        /// <summary>
        /// 获得今日新用户数
        /// </summary>
        /// <returns></returns>
        int GetTodayNewMemberCount();

        /// <summary>
        /// 获得管理员数量
        /// </summary>
        /// <returns></returns>
        int GetAdminCount();

        /// <summary>
        /// 获得用户排行
        /// </summary>
        /// <param name="count"></param>
        /// <param name="postTableId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IDataReader GetUsersRank(int count, string postTableId, string type);

        /// <summary>
        /// 获得用户排行
        /// </summary>
        /// <param name="filed">当月还是总在线时间</param>
        /// <returns></returns>
        IDataReader GetUserByOnlineTime(string filed);

        /// <summary>
        /// 更新统计数据
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="os"></param>
        /// <param name="visitorsAdd"></param>
        void UpdateStatCount(string browser, string os, string visitorsAdd);   

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

        #region 登录日志loginlog操作

        /// <summary>
        /// 返加登录错误日志列表
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        DataTable GetErrLoginRecordByIP(string ip);

        /// <summary>
        /// 添加错误登录次数
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        int AddErrLoginCount(string ip);

        /// <summary>
        /// 重置登录错误次数
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        int ResetErrLoginCount(string ip);

        /// <summary>
        /// 添加错误登录记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        int AddErrLoginRecord(string ip);

        /// <summary>
        /// 删除指定ip地址的登录错误日志
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>int</returns>
        int DeleteErrLoginRecord(string ip);

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
        void AddVisitLog(Guid uid, string userName, int groupId, string groupTitle, string ip, string actions, string others);

        /// <summary>
        /// 删除访问日志
        /// </summary>
        void DeleteVisitLogs();

        /// <summary>
        /// 删除访问日志
        /// </summary>
        /// <param name="condition">查询条件</param>
        void DeleteVisitLogs(string condition);

        /// <summary>
        /// 获取访问日志列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetVisitLogList(int pageSize, int currentPage, string condition);

        /// <summary>
        /// 获取访问日志列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        DataTable GetVisitLogList(int pageSize, int currentPage);

        /// <summary>
        /// 获取访问日志数
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int GetVisitLogCount(string condition);
        /// <summary>
        /// 获取访问日志数
        /// </summary>
        /// <returns></returns>
        int GetVisitLogCount();

        /// <summary>
        /// 删除指定条件的访问日志
        /// </summary>
        /// <param name="deleteMod">删除方式</param>
        /// <param name="visitId">管理日志Id</param>
        /// <param name="deleteNum">删除条数</param>
        /// <param name="deleteFrom">删除从何时起</param>
        /// <returns></returns>
        string DelVisitLogCondition(string deleteMod, string visitId, string deleteNum, string deleteFrom);

        /// <summary>
        /// 获取管理日志条件
        /// </summary>
        /// <param name="postDateTimeStart">访问起始日期</param>
        /// <param name="postDateTimeEnd">访问结束日期</param>
        /// <param name="userName">用户名</param>
        /// <param name="others">其它</param>
        /// <returns></returns>
        string SearchVisitLog(DateTime postDateTimeStart, DateTime postDateTimeEnd, string userName, string others);

        #endregion

        #region 菜单表navs操作

        /// <summary>
        /// 得到自定义菜单
        /// </summary>
        /// <returns></returns>
        IDataReader GetNavigation(bool getAllNavigation);

        /// <summary>
        /// 得到拥有子菜单的主菜单ID
        /// </summary>
        /// <returns></returns>
        IDataReader GetNavigationHasSub();

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        void DeleteNavigation(int id);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="nav">导航菜单</param>
        void InsertNavigation(NavInfo nav);

        /// <summary>
        /// 更校菜单
        /// </summary>
        /// <param name="nav">导航菜单</param>
        void UpdateNavigation(NavInfo nav);

        #endregion

        #region 广告表advertisements操作

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <returns>广告列表</returns>
        DataTable GetAdsTable();

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
        void AddAdInfo(int available, string type, int displayOrder, string title, string targets, string parameters, string code, string startTime, string endTime);

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <returns></returns>
        DataTable GetAdvertisements();

        /// <summary>
        /// 删除广告列表            
        /// </summary>
        /// <param name="aidList">广告列表Id</param>
        void DeleteAdvertisement(string aidList);

        /// <summary>
        /// 更新广告可用状态
        /// </summary>
        /// <param name="aidList">广告Id</param>
        /// <param name="available"></param>
        /// <returns></returns>
        int UpdateAdvertisementAvailable(string aidList, int available);

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
        int UpdateAdvertisement(int aid, int available, string type, int displayOrder, string title, string targets, string parameters, string code, string startTime, string endTime);

        /// <summary>
        /// 获取广告
        /// </summary>
        /// <param name="aId">广告Id</param>
        /// <returns></returns>
        DataTable GetAdvertisement(int aid);

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
        string RestoreDatabase(string backUpPath, string serverName, string userName, string passWord, string strDbName, string strFileName);

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
        string BackUpDatabase(string backUpPath, string serverName, string userName, string passWord, string strDbName, string strFileName);

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        string GetDbName();

        /// <summary>
        /// 开始填充全文索引
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        int StartFullIndex(string dbName);

        /// <summary>
        /// 构建相应表及全文索引
        /// </summary>
        /// <param name="tableName"></param>
        void CreatePostTableAndIndex(string tableName);

        /// <summary>
        /// 收缩数据库
        /// </summary>
        /// <param name="shrinkSize">收缩大小</param>
        /// <param name="dbName">数据库名</param>
        void ShrinkDataBase(string shrinkSize, string dbName);

        /// <summary>
        /// 清空数据库日志
        /// </summary>
        /// <param name="dbName"></param>
        void ClearDBLog(string dbName);

        /// <summary>
        /// 运行SQL语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        string RunSql(string sql);

        /////// <summary>
        /////// 更新分表存储过程
        /////// </summary>
        ////void UpdatePostSP();

        /// <summary>
        /// 获取数据库版本
        /// </summary>
        /// <returns></returns>
        string GetDataBaseVersion();

        #endregion

        #region 专题处理Topic

        /// <summary>
        /// 更新主题浏览量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewCount">浏览量</param>
        /// <returns>成功返回1，否则返回0</returns>
        int UpdateTopicViewCount(int tid, int viewCount);

        #endregion

        #region 板块处理forum操作

        /// <summary>
        /// 更新版块和用户模板Id
        /// </summary>
        /// <param name="templateIdList">模板Id列表</param>
        void UpdateForumAndUserTemplateId(string templateIdList);

        #endregion

        /// <summary>
        /// 获取指定用户ID列表的邮件信息
        /// </summary>
        /// <param name="uids">用户id列表</param>
        /// <returns></returns>
        DataTable GetMailTable(string uids);
    }
}
