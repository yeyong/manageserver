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
        private string m_companyTableDependency = "company";                            //缓存企业表
        private int m_companyCacheDuration = 12;                                        //企业信息缓存失效时间
        private int m_commonCacheDuration = 12;                                         //通用信息缓存失效时间
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
        /// 缓存企业表
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
        #endregion
    }
}
