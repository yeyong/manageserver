using System;

using SAS.Data;
using SAS.Entity;
using SAS.Common;
using SAS.Plugin;
using SAS.Logic;

namespace SAS.Plugin.PasswordMode
{
    /// <summary>
    /// 第三方认识模式
    /// </summary>
    public class ThirdPartMode : IPasswordMode
    {
        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="userInfo">通过username获取的用户信息，用于进行密码验证</param>
        /// <param name="postpassword">用户提交的密码</param>
        /// <returns>返回当前用户信息与提交密码的验证结果</returns>
        public bool CheckPassword(UserInfo userInfo, string postpassword)
        {
            string doubleMd5 = Utils.MD5(Utils.MD5(postpassword) + userInfo.ps_salt); //两遍MD5

            return doubleMd5 == userInfo.Ps_password;//比较
        }

        /// <summary>
        /// 创建用户信息(用于用户注册等行为)
        /// </summary>
        /// <param name="userInfo">要创建的用户信息(密码为明文)</param>
        /// <returns></returns>
        public int CreateUserInfo(UserInfo userInfo)
        {
            userInfo.ps_salt = Logic.LogicUtils.CreateAuthStr(6, false);
            userInfo.Ps_password = Utils.MD5(Utils.MD5(userInfo.Ps_password) + userInfo.ps_salt);
            return SAS.Data.DataProvider.Users.CreateUser(userInfo);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userInfo">要保存的用户信息(密码为明文)</param>
        /// <returns>返回经过处理之后的实际用户信息</returns>
        public UserInfo SaveUserInfo(UserInfo userInfo)
        {
            if (Utils.StrIsNullOrEmpty(userInfo.ps_salt))
                userInfo.ps_salt = Logic.LogicUtils.CreateAuthStr(6, false);

            userInfo.Ps_password = Utils.MD5(Utils.MD5(userInfo.Ps_password) + userInfo.ps_salt); //两遍MD5

            return SAS.Data.DataProvider.Users.UpdateUser(userInfo) ? userInfo : null;
        }
    }
}
