using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 登录日志操作类
    /// </summary>
    public class LoginLogs
    {
        /// <summary>
        /// 增加错误次数并返回错误次数, 如不存在登录错误日志则建立
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>int</returns>
        public static int UpdateLoginLog(string ip, bool update)
        {
            DataTable dt = SAS.Data.DataProvider.LoginLogs.GetErrLoginRecordByIP(ip);
            if (dt.Rows.Count > 0)
            {
                int errcount = Utils.StrToInt(dt.Rows[0][0].ToString(), 0);
                if (Utils.StrDateDiffMinutes(dt.Rows[0][1].ToString(), 0) < 15)
                {
                    if ((errcount >= 5) || (!update))
                    {
                        return errcount;
                    }
                    else
                    {
                        SAS.Data.DataProvider.LoginLogs.AddErrLoginCount(ip);
                        return errcount + 1;
                    }
                }
                SAS.Data.DataProvider.LoginLogs.ResetErrLoginCount(ip);
                return 1;
            }
            else
            {
                if (update)
                {
                    SAS.Data.DataProvider.LoginLogs.AddErrLoginRecord(ip);
                }
                return 1;
            }
        }

        /// <summary>
        /// 删除指定ip地址的登录错误日志
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>int</returns>
        public static int DeleteLoginLog(string ip)
        {
            return Utils.IsIP(ip) ? SAS.Data.DataProvider.LoginLogs.DeleteErrLoginRecord(ip) : 0;
        }
    }
}
