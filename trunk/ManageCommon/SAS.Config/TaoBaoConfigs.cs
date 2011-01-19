using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Config;

namespace SAS.Config
{
    /// <summary>
    /// 淘宝配置文件操作类
    /// </summary>
    public class TaoBaoConfigs
    {
        private static System.Timers.Timer taobaoConfigTimer = new System.Timers.Timer(15000);
        private static TaoBaoConfigInfo m_configinfo;

        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static TaoBaoConfigInfo GetConfig()
        {
            return TaoBaoConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static TaoBaoConfigs()
        {
            m_configinfo = TaoBaoConfigFileManager.LoadConfig();
            taobaoConfigTimer.AutoReset = true;
            taobaoConfigTimer.Enabled = true;
            taobaoConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            taobaoConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ResetConfig();
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = TaoBaoConfigFileManager.LoadConfig();
        }

        public static TaoBaoConfigInfo GetTaoBaoConfig()
        {
            return m_configinfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(TaoBaoConfigInfo TaoBaoconfiginfo)
        {
            TaoBaoConfigFileManager acfm = new TaoBaoConfigFileManager();
            TaoBaoConfigFileManager.ConfigInfo = TaoBaoconfiginfo;
            return acfm.SaveConfig();
        }
        /// <summary>
        /// 淘之购域名
        /// </summary>
        public static string GetTaoBaoUrl
        {
            get
            {
                return GetTaoBaoConfig().TaoDomain;
            }
        }
    }
}
