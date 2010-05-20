using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 市级缓存依赖
    /// </summary>
    public class CityList : TableDependency
    {
        public CityList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().CityTableDependency) { }
    }
}
