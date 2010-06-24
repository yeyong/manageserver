using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 导航类别依赖项
    /// </summary>
    public class NavsList :TableDependency
    {
        public NavsList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().NavsTableDependency) { }
    }
}
