using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 省级信息缓存依赖
    /// </summary>
    public class ProvinceList : TableDependency
    {
        public ProvinceList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().ProvinceTableDependency) { }
    }
}
