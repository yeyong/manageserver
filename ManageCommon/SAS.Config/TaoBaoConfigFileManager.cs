using System;
using System.Text;

using SAS.Common;
using SAS.Config;

namespace SAS.Config
{
    /// <summary>
    /// 淘宝配置文件管理类
    /// </summary>
    class TaoBaoConfigFileManager : SAS.Config.DefaultConfigFileManager
    {
        private static TaoBaoConfigInfo m_configinfo;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;


        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static TaoBaoConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (TaoBaoConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(TaoBaoConfigInfo));
        }

        /// <summary>
        /// 当前的配置类实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (TaoBaoConfigInfo)value; }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = Utils.GetMapPath(BaseConfigs.GetSitePath + "config/taobao.config");
                }

                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static TaoBaoConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo);
            return ConfigInfo as TaoBaoConfigInfo;
        }
    }
}
