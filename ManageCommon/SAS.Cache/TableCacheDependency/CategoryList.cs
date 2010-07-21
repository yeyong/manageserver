using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 商品类别缓存依赖
    /// </summary>
    public class CategoryList : TableDependency
    {
        public CategoryList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().CateGoryTableDependency) { }
    }
}
