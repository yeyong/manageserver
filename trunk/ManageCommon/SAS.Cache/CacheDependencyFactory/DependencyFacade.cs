using System.Configuration;
using System.Web.Caching;
using System.Collections.Generic;

using SAS.Config;

namespace SAS.Cache.CacheDependencyFactory
{
    public static class DependencyFacade
    {
        private static readonly string path = DataCacheConfigs.GetConfig().CacheDependencyAssembly;
    }
}
