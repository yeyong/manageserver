using System;

namespace SAS.Config
{
    /// <summary>
    /// 数据缓存配置文件信息
    /// </summary>
    [Serializable]
    public class DataCacheConfigInfo : IConfigInfo
    {
        #region 私有字段
        private int m_enableCaching = 0;                                                //开启缓存
        private string m_cacheDependencyAssembly = "SAS.Cache.TableCacheDependency";    //缓存编译类
        private string m_cacheDatabaseName = "ntw";                                     //缓存数据库
        private string m_cacheTableList = "";                                           //缓存数据表集合
        private string m_companyTableDependency = "company";                            //缓存企业表依赖项
        private int m_companyCacheDuration = 12;                                        //企业信息缓存失效时间
        private int m_commonCacheDuration = 12;                                         //通用信息缓存失效时间
        private string m_provinceTableDependency = "province";                          //缓存省份表依赖项
        private string m_cityTableDependency = "city";                                  //缓存市级表依赖项
        private string m_districtTableDependency = "district";                          //缓存地区表依赖项
        private string m_catalogTableDependency = "catalog";                            //缓存行业类别依赖项
        private string m_navsTableDependency = "navs";                                  //缓存导航菜单依赖项
        private string m_categoryTableDependency = "category";                          //缓存商品类别依赖项
        #endregion

        #region 属性
        /// <summary>
        /// 开启缓存
        /// </summary>
        public int EnableCaching
        {
            set { m_enableCaching = value; }
            get { return m_enableCaching; }
        }
        /// <summary>
        /// 缓存编译类
        /// </summary>
        public string CacheDependencyAssembly
        {
            set { m_cacheDependencyAssembly = value; }
            get { return m_cacheDependencyAssembly; }
        }
        /// <summary>
        /// 缓存数据库
        /// </summary>
        public string CacheDatabaseName
        {
            set { m_cacheDatabaseName = value; }
            get { return m_cacheDatabaseName; }
        }
        /// <summary>
        /// 缓存数据表集合
        /// </summary>
        public string CacheTableList
        {
            set { m_cacheTableList = value; }
            get { return m_cacheTableList; }
        }
        /// <summary>
        /// 缓存企业表依赖项
        /// </summary>
        public string CompanyTableDependency
        {
            set { m_companyTableDependency = value; }
            get { return m_companyTableDependency; }
        }
        /// <summary>
        /// 企业信息缓存失效时间
        /// </summary>
        public int CompanyCacheDuration
        {
            set { m_companyCacheDuration = value; }
            get { return m_companyCacheDuration; }
        }
        /// <summary>
        /// 通用信息缓存失效时间
        /// </summary>
        public int CommonCacheDuration
        {
            set { m_commonCacheDuration = value; }
            get { return m_commonCacheDuration; }
        }
        /// <summary>
        /// 缓存地区表依赖项
        /// </summary>
        public string ProvinceTableDependency
        {
            set { m_provinceTableDependency = value; }
            get { return m_provinceTableDependency; }
        }
        /// <summary>
        /// 缓存地区表依赖项
        /// </summary>
        public string CityTableDependency
        {
            set { m_cityTableDependency = value; }
            get { return m_cityTableDependency; }
        }
        /// <summary>
        /// 缓存地区表依赖项
        /// </summary>
        public string DistrictTableDependency
        {
            set { m_districtTableDependency = value; }
            get { return m_districtTableDependency; }
        }
        /// <summary>
        /// 缓存行业类别依赖项
        /// </summary>
        public string CatalogTableDependency
        {
            set { m_catalogTableDependency = value; }
            get { return m_catalogTableDependency; }
        }
        /// <summary>
        /// 缓存导航菜单依赖项
        /// </summary>
        public string NavsTableDependency
        {
            set { m_navsTableDependency = value; }
            get { return m_navsTableDependency; }
        }
        /// <summary>
        /// 缓存商品类别依赖项
        /// </summary>
        public string CateGoryTableDependency
        {
            set { m_categoryTableDependency = value; }
            get { return m_categoryTableDependency; }
        }
        #endregion
    }
}
