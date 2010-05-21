using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 行业类别依赖项
    /// </summary>
    public class CatalogList : TableDependency
    {
        public CatalogList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().CatalogTableDependency) { }
    }
}
