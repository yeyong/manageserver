using System;
using System.Text;

namespace SAS.Config
{
    /// <summary>
    /// 数据缓存配置操作
    /// </summary>
    public class DataCacheConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static DataCacheConfigInfo GetConfig()
        {
            return DataCacheConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="emailconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(DataCacheConfigInfo emailconfiginfo)
        {
            DataCacheConfigFileManager ecfm = new DataCacheConfigFileManager();
            DataCacheConfigFileManager.ConfigInfo = emailconfiginfo;
            return ecfm.SaveConfig();
        }
    }
}
