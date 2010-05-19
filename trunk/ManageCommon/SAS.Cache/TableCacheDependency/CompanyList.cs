using System.Web.Caching;

using SAS.Config;

namespace SAS.Cache
{
    /// <summary>
    /// 企业信息缓存
    /// </summary>
    public class CompanyList : TableDependency
    {
        public CompanyList() : base(BaseConfigs.GetTablePrefix + DataCacheConfigs.GetConfig().CompanyTableDependency) { }
    }
}
