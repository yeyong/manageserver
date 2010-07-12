using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using SAS.Plugin.TaoBao;
using SAS.Common;
using SAS.Entity.Domain;
using SAS.Config;

namespace SAS.Taobao
{
    public class TaoBaoPlugin : TaoBaoPluginBase
    {
        /// <summary>
        /// 获取类目列表
        /// </summary>
        public override List<ItemCat> GetItemCatCache()
        {
            return TaoBaos.GetItemCatCache();
        }
        /// <summary>
        /// 获取商品类目信息
        /// </summary>
        public override SAS.Entity.CategoryInfo GetCategoryInfo(int cid)
        {
            return TaoBaos.GetCategoryInfo(cid);
        }
        /// <summary>
        /// 获取全部商品类别信息
        /// </summary>
        public override DataTable GetAllCategoryList()
        {
            return TaoBaos.GetAllCategoryList();
        }
    }
}
