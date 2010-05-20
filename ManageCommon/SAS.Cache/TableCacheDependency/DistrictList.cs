using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 区级缓存依赖
    /// </summary>
    public class DistrictList : TableDependency
    {
        public DistrictList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().DistrictTableDependency) { }
    }
}
