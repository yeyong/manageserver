using System;
using System.Text;

namespace SAS.Sirius.Config
{
    /// <summary>
    /// Sirius studio 配置信息类
    /// </summary>
    public class SiriusConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static SiriusConfigInfo GetConfig()
        {
            return SiriusConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(SiriusConfigInfo siriusconfiginfo)
        {
            SiriusConfigFileManager acfm = new SiriusConfigFileManager();
            SiriusConfigFileManager.ConfigInfo = siriusconfiginfo;
            return acfm.SaveConfig();
        }
    }
}
