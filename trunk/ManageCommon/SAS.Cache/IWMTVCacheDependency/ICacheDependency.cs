using System.Web.Caching;

namespace SAS.Cache.IWMTVCacheDependency
{
    public interface ICacheDependency
    {
        AggregateCacheDependency GetDependency();
    }
}
