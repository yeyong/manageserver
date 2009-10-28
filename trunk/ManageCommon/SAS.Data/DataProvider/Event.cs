using System;

namespace SAS.Data.DataProvider
{
    public class Event
    {
        /// <summary>
        /// 设置上次任务计划的执行时间
        /// </summary>
        /// <param name="key">任务的标识</param>
        /// <param name="serverName">主机名</param>
        /// <param name="lastExecuted">最后执行时间</param>
        public static void SetLastExecuteScheduledEventDateTime(string key, string servername, DateTime lastexecuted)
        {
            DatabaseProvider.GetInstance().SetLastExecuteScheduledEventDateTime(key, servername, lastexecuted);
        }

        /// <summary>
        /// 获取上次任务计划的执行时间
        /// </summary>
        /// <param name="key">任务的标识</param>
        /// <param name="serverName">主机名</param>
        /// <returns></returns>
        public static DateTime GetLastExecuteScheduledEventDateTime(string key, string servername)
        {
            return DatabaseProvider.GetInstance().GetLastExecuteScheduledEventDateTime(key, servername);
        }
    }
}
