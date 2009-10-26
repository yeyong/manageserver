using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Entity;
using SAS.Config;

namespace SAS.Logic
{
    /// <summary>
    /// AdminUserGroupFactory 的摘要说明。
    /// 后台用户组管理操作类
    /// </summary>
    public class AdminUserGroups : UserGroups
    {
        public static string opresult = ""; //存储操作结果或返回给用户的信息

        /// <summary>
        /// 通过指定的用户组id得到相关的用户组信息
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static UserGroupInfo AdminGetUserGroupInfo(int groupid)
        {
            return UserGroups.GetUserGroupInfo(groupid);
        }

        /// <summary>
        /// 更新用户组信息
        /// </summary>
        /// <param name="userGroupInfo">用户组信息</param>
        /// <returns></returns>
        public static bool UpdateUserGroupInfo(UserGroupInfo userGroupInfo)
        {
            int Creditshigher = userGroupInfo.ug_scorehight;
            int Creditslower = userGroupInfo.ug_scorelow;

            if ((userGroupInfo.ug_id >= 9) && (userGroupInfo.ug_pg_id == 0))
            {
                //当已存在的用户组积分上下限不是当前组的时候,则不允许编辑
                DataTable dt = SAS.Data.DataProvider.UserGroups.GetUserGroupByCreditsHigherAndLower(Creditshigher, Creditslower);
                if (dt.Rows.Count > 0 && userGroupInfo.ug_id.ToString() != dt.Rows[0][0].ToString())
                    return false;

                if (!SystemCheckCredits("update", ref Creditshigher, ref Creditslower, userGroupInfo.ug_id))
                    return false;
            }

            UserGroups.UpdateUserGroup(userGroupInfo);
            SAS.Data.DataProvider.UserGroups.UpdateOnlineList(userGroupInfo);

            Caches.ReSetAdminGroupList();
            Caches.ReSetUserGroupList();
            return true;
        }

        /// <summary>
        /// 校验指定积分用户组的上下限
        /// </summary>
        /// <param name="opname">操作名称</param>
        /// <param name="Creditshigher">积分下限</param>
        /// <param name="Creditslower">积分上限</param>
        /// <param name="groupid"></param>
        public static bool SystemCheckCredits(string opname, ref int creditsHigher, ref int creditsLower, int groupid)
        {
            opresult = "";

            switch (opname.ToLower())
            {
                case "add":
                    {
                        #region

                        DataTable dt = Data.DataProvider.UserGroups.GetMinCreditHigher();

                        if (dt.Rows.Count > 0)
                        {
                            int systemMiniCredits = Convert.ToInt32(dt.Rows[0][0].ToString());
                            if (creditsLower <= systemMiniCredits) //当系统最小积分下限组还大于等于当前添加的积分组的上限时
                            {
                                creditsLower = systemMiniCredits;
                                opresult = "由您所输入的积分下限小于或等于系统最小值,因此系统已将其调整为" + systemMiniCredits;
                                break;
                            }
                        }

                        dt = Data.DataProvider.UserGroups.GetMaxCreditLower();
                        if (dt.Rows.Count > 0)
                        {
                            int systemMaxCredits = Convert.ToInt32(dt.Rows[0][0].ToString());
                            if (creditsHigher >= systemMaxCredits) //当系统最大积分上限组还小于等于当前添加的积分组的下限时
                            {
                                creditsHigher = systemMaxCredits;
                                opresult = "由您所输入的积分上限大于或等于系统最大值,因此系统已将其调整为" + systemMaxCredits;
                                break;
                            }
                        }

                        dt = SAS.Data.DataProvider.UserGroups.GetUserGroupByCreditshigher(creditsHigher);
                        if (dt.Rows.Count > 0)
                        {
                            int currentGroupID = Convert.ToInt32(dt.Rows[0][0].ToString());
                            int currentCreditsHigher = Convert.ToInt32(dt.Rows[0][1].ToString());
                            int currentCreditsLower = Convert.ToInt32(dt.Rows[0][2].ToString());

                            if (creditsLower > currentCreditsLower)
                                return false;
                            else
                            {
                                if (creditsHigher == currentCreditsHigher)
                                {
                                    if (creditsLower < currentCreditsLower)
                                    {
                                        UserGroupInfo userGroupInfo = UserGroups.GetUserGroupInfo(currentGroupID);
                                        userGroupInfo.ug_scorelow = creditsLower;
                                        UserGroups.UpdateUserGroup(userGroupInfo);
                                        break;
                                    }
                                    else
                                    {
                                        opresult = "由您所输入的积分上限大于或等于所属有效积分上限的最大值" + currentCreditsLower + ",因此系统无效提交您的数据!";
                                        return false;
                                    }
                                }
                                else
                                {
                                    creditsLower = currentCreditsLower;
                                    //更新当前查询积分组,也就是要添加的组的积分上限位于当前查询组的积份上下限之间
                                    SAS.Data.DataProvider.UserGroups.UpdateUserGroupCreidtsLower(currentCreditsHigher, creditsHigher);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            opresult = "系统未提到合适的位置保存您提交的信息!";
                            return false;
                        }

                        #endregion
                    }
                case "delete":
                    {
                        if (SAS.Data.DataProvider.UserGroups.GetGroupCountByCreditsLower(creditsHigher) > 0)
                            SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsLowerByCreditsLower(creditsLower, creditsHigher);
                        else
                            SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsHigherByCreditsHigher(creditsHigher, creditsLower);
                        break;
                    }
                case "update":
                    {
                        #region

                        UserGroupInfo oldInfo = UserGroups.GetUserGroupInfo(groupid);
                        int currentGroupOldCreatesHigher = oldInfo.ug_scorehight; //要更新的当前用户组的老的下限积分
                        int currentGroupOldCreatesLower = oldInfo.ug_scorelow; //要更新的当前用户组的老的上限积分

                        DataTable dt = SAS.Data.DataProvider.UserGroups.GetMinCreditHigher();
                        if (dt.Rows.Count > 0)
                        {
                            int systemMiniCredits = Convert.ToInt32(dt.Rows[0][0].ToString());
                            if (creditsLower <= systemMiniCredits) //当系统最小积分下限组还大于等于当前添加的积分组的上限时
                            {
                                creditsLower = systemMiniCredits;
                                opresult = "由您所输入的积分下限小于或等于系统最大值,因此系统已将其调整为" + systemMiniCredits;
                                SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsHigherByCreditsHigher(currentGroupOldCreatesHigher, currentGroupOldCreatesLower);
                                break;
                            }
                        }

                        dt = Data.DataProvider.UserGroups.GetMaxCreditLower();
                        if (dt.Rows.Count > 0)
                        {
                            int systemMaxCredits = Convert.ToInt32(dt.Rows[0][0].ToString());
                            if (creditsHigher >= systemMaxCredits) //当系统最大积分上限组还小于等于当前添加的积分组的下限时
                            {
                                creditsHigher = systemMaxCredits;
                                opresult = "由您所输入的积分上限大于或等于系统最大值,因此系统已将其调整为" + systemMaxCredits;
                                SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsLowerByCreditsLower(currentGroupOldCreatesLower, currentGroupOldCreatesHigher);
                                break;
                            }
                        }

                        dt = SAS.Data.DataProvider.UserGroups.GetUserGroupByCreditshigher(creditsHigher);
                        if (dt.Rows.Count > 0)
                        {
                            int currentGroupID = Convert.ToInt32(dt.Rows[0][0].ToString());
                            int currentCreditsHigher = Convert.ToInt32(dt.Rows[0][1].ToString());
                            int currentCreditsLower = Convert.ToInt32(dt.Rows[0][2].ToString());

                            if (creditsLower > currentCreditsLower)
                            {
                                opresult = "由您所输入的积分上限大于或等于所属有效积分上限的最大值" + currentCreditsLower + ",因此系统无效提交您的数据!";
                                return false;
                            }
                            else
                            {
                                if (creditsHigher == currentCreditsHigher)
                                {
                                    if (creditsLower < currentCreditsLower)
                                    {
                                        //提升以当前老的积分下限为上限的用户组的上限值为老的积分上限
                                        SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsHigherByCreditsHigher(creditsLower, currentCreditsLower);
                                        break;
                                    }
                                }
                                else
                                {
                                    opresult = "系统已自动将您提交的积分上限调整为" + currentCreditsLower;

                                    //提升以当前老的积分下限为上限的用户组的上限值为老的积分上限
                                    SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsHigherByCreditsHigher(creditsLower, currentCreditsLower);
                                    SAS.Data.DataProvider.UserGroups.UpdateUserGroupsCreditsLowerByCreditsLower(creditsHigher, currentCreditsHigher);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            opresult = "系统未提到合适的位置保存您提交的信息!";
                            return false;
                        }

                        #endregion

                        break;
                    }
            }
            return true;
        }
    }
}
