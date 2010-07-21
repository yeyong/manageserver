using System.Reflection;
using System.Configuration;

using SAS.Config;
using SAS.Cache.IWMTVCacheDependency;

namespace SAS.Cache.CacheDependencyFactory
{
    public static class DependencyAccess
    {        
        /// <summary>
        /// 创建省级信息缓存依赖
        /// </summary>
        public static ICacheDependency CreateProvinceDependency()
        {
            return LoadInstance("ProvinceList");
        }

        /// <summary>
        /// 创建市级信息缓存依赖
        /// </summary>
        public static ICacheDependency CreateCityDependency()
        {
            return LoadInstance("CityList");
        }

        /// <summary>
        /// 创建区级信息缓存依赖
        /// </summary>
        public static ICacheDependency CreateDistrictDependency()
        {
            return LoadInstance("DistrictList");
        }

        /// <summary>
        /// 创建行业类别信息缓存依赖项
        /// </summary>
        public static ICacheDependency CreateCatalogDependency()
        {
            return LoadInstance("CatalogList");
        }

        /// <summary>
        /// 创建行业类别信息缓存依赖项
        /// </summary>
        public static ICacheDependency CreateNavsDependency()
        {
            return LoadInstance("NavsList");
        }

        /// <summary>
        /// 创建商品类别信息缓存依赖项
        /// </summary>
        public static ICacheDependency CreateCategoryDependency()
        {
            return LoadInstance("CategoryList");
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
