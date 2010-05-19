using System.Web.Caching;
using System.Configuration;

using SAS.Config;

namespace SAS.Cache.TableCacheDependency
{
    public abstract class TableDependency : SAS.Cache.IWMTVCacheDependency.ICacheDependency
    {
        // This is the separator that's used in web.config
        protected char[] configurationSeparator = new char[] { ',' };
        protected AggregateCacheDependency dependency = new AggregateCacheDependency();
        protected DataCacheConfigInfo dataconfig = DataCacheConfigs.GetConfig();
        protected TableDependency(string configKey)
        {
            string dbName = dataconfig.CacheDatabaseName;
            string[] tables = configKey.Split(configurationSeparator);

            foreach (string tableName in tables)
                dependency.Add(new SqlCacheDependency(dbName, tableName));
        }

        public AggregateCacheDependency GetDependency()
        {
            return dependency;
        }
    }
}
