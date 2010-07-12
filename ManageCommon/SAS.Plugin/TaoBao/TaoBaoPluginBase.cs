using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity.Domain;

namespace SAS.Plugin.TaoBao
{
    /// <summary>
    /// 淘宝控制插件类
    /// </summary>    
    public abstract class TaoBaoPluginBase : PluginBase
    {
        /// <summary>
        /// 获取类目列表
        /// </summary>
        public abstract System.Collections.Generic.List<ItemCat> GetItemCatCache();
    }
}
