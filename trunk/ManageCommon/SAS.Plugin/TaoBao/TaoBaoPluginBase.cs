using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity.Domain;
using SAS.Entity;

namespace SAS.Plugin.TaoBao
{
    /// <summary>
    /// 淘宝控制插件类
    /// </summary>    
    public abstract class TaoBaoPluginBase : PluginBase
    {
        /// <summary>
        /// 获取Taobao类目列表
        /// </summary>
        public abstract System.Collections.Generic.List<ItemCat> GetItemCatCache();
        /// <summary>
        /// 获取商品类目信息
        /// </summary>
        public abstract CategoryInfo GetCategoryInfo(int cid);
        /// <summary>
        /// 获取商品类别全部信息
        /// </summary>
        public abstract DataTable GetAllCategoryList();
    }
}
