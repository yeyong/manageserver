using System.Reflection;
using System.Configuration;

using SAS.Config;
using SAS.Cache.IWMTVCacheDependency;

namespace SAS.Cache.CacheDependencyFactory
{
    public static class DependencyAccess
    {
        /// <summary>
        /// 创建企业信息缓存依赖
        /// </summary>
        /// <returns></returns>
        public static ICacheDependency CreateCompanyDependency()
        {
            return LoadInstance("CompanyList");
        }

        /// <summary>
        /// Common method to load dependency class from information provided from configuration file 
        /// </summary>
        /// <param name="className">Type of dependency</param>
        /// <returns>Concrete Dependency Implementation instance</returns>
        private static ICacheDependency LoadInstance(string className)
        {

            string path = DataCacheConfigs.GetConfig().CacheDependencyAssembly;
            string fullyQualifiedClass = path + "." + className;

            // Using the evidence given in the config file load the appropriate assembly and class
            return (ICacheDependency)Assembly.Load(path).CreateInstance(fullyQualifiedClass);
        }
    }
}
