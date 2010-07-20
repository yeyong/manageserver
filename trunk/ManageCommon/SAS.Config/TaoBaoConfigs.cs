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
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static TaoBaoConfigInfo GetConfig()
        {
            return TaoBaoConfigFileManager.LoadConfig();
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
    }
}
