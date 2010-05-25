﻿using System.Configuration;
using System.Web.Caching;
using System.Collections.Generic;

using SAS.Config;

namespace SAS.Cache.CacheDependencyFactory
{
    public static class DependencyFacade
    {
        private static readonly string path = DataCacheConfigs.GetConfig().CacheDependencyAssembly;

        /// <summary>
        /// 获取省级信息缓存依赖
        /// </summary>
        /// <returns></returns>
        public static AggregateCacheDependency GetProvinceDependency()
        {
            if (!string.IsNullOrEmpty(path))
            {
                return DependencyAccess.CreateProvinceDependency().GetDependency();
            }
            return null;
        }
        /// <summary>
        /// 获取市级信息缓存依赖
        /// </summary>
        /// <returns></returns>
        public static AggregateCacheDependency GetCityDependency()
        {
            if (!string.IsNullOrEmpty(path))
            {
                return DependencyAccess.CreateCityDependency().GetDependency();
            }
            return null;
        }
        /// <summary>
        /// 获取区级信息缓存依赖
        /// </summary>
        /// <returns></returns>
        public static AggregateCacheDependency GetDistrictDependency()
        {
            if (!string.IsNullOrEmpty(path))
            {
                return DependencyAccess.CreateDistrictDependency().GetDependency();
            }
            return null;
        }
        /// <summary>
        /// 获取行业类别信息依赖项
        /// </summary>
        /// <returns></returns>
        public static AggregateCacheDependency GetCatalogDependency()
        {
            if (!string.IsNullOrEmpty(path))
            {
                return DependencyAccess.CreateCatalogDependency().GetDependency();
            }
            return null;
        }
    }
}