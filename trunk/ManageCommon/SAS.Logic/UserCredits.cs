using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 用户积分操作
    /// </summary>
    public class UserCredits
    {

        /// <summary>
        /// 根据积分获得积分用户组所应该匹配的用户组描述 (如果没有匹配项或用户非积分用户组则返回null)
        /// </summary>
        /// <param name="Credits">积分</param>
        /// <returns>用户组描述</returns>
        public static UserGroupInfo GetCreditsUserGroupId(float Credits)
        {
            List<UserGroupInfo> usergroupinfo = UserGroups.GetUserGroupList();
            UserGroupInfo tmpitem = null;

            UserGroupInfo maxCreditGroup = null;
            foreach (UserGroupInfo infoitem in usergroupinfo)
            {
                // 积分用户组的特征是radminid等于0
                if (infoitem.ug_pg_id == 0 && infoitem.ug_isSystem == 0 && (Credits >= infoitem.ug_scorehight && Credits <= infoitem.ug_scorelow))
                {
                    if (tmpitem == null || infoitem.ug_scorehight > tmpitem.ug_scorehight)
                        tmpitem = infoitem;
                }
                //更新积分上线最高的用户组
                if (maxCreditGroup == null || maxCreditGroup.ug_scorehight < infoitem.ug_scorehight)
                    maxCreditGroup = infoitem;
            }

            if (maxCreditGroup != null && maxCreditGroup.ug_scorehight < Credits)
                tmpitem = maxCreditGroup;

            return tmpitem == null ? new UserGroupInfo() : tmpitem;
        }
    }
}
