using System;

using SAS.Common.Generic;

namespace SAS.Config
{
    /// <summary>
    /// 网站快照设置类
    /// </summary>
    public class DbSnapConfigs
    {
        private static object lockHelper = new object();

        private static System.Timers.Timer dbSnapConfigTimer = new System.Timers.Timer(60000);

        private static DbSnapAppConfig m_dbSnapAppConfig;

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static DbSnapConfigs()
        {
            m_dbSnapAppConfig = DbSnapConfigFileManager.LoadConfig();

            dbSnapConfigTimer.AutoReset = true;
            dbSnapConfigTimer.Enabled = true;
            dbSnapConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            dbSnapConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }

        /// <summary>
        /// 有效的数据快照列表
        /// </summary>
        private static List<DbSnapInfo> enableSnapList = null;

        /// <summary>
        /// 获取有效的数据快照列表
        /// </summary>
        /// <returns></returns>
        public static List<DbSnapInfo> GetEnableSnapList()
        {
            if (enableSnapList == null)
            {
                if (m_dbSnapAppConfig != null)
                {
                    enableSnapList = new List<DbSnapInfo>();
                    foreach (DbSnapInfo dbSnapInfo in m_dbSnapAppConfig.DbSnapInfoList)
                    {
                        if (dbSnapInfo.Enable && !string.IsNullOrEmpty(dbSnapInfo.DbconnectString))
                            enableSnapList.Add(dbSnapInfo);
                    }
                }
            }
            return enableSnapList;
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_dbSnapAppConfig = DbSnapConfigFileManager.LoadConfig();
            enableSnapList = null;
        }

        public static DbSnapAppConfig GetConfig()
        {
            return m_dbSnapAppConfig;
        }

        /// <summary>
        /// 保存配置实例
        /// </summary>
        /// <param name="baseconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(DbSnapAppConfig baseconfiginfo)
        {
            DbSnapConfigFileManager dbscfm = new DbSnapConfigFileManager();
            DbSnapConfigFileManager.ConfigInfo = baseconfiginfo;
            return dbscfm.SaveConfig();
        }
    }
}
