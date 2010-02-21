using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;
using System.Xml.Serialization;
using System.Data;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// 行业类别操作
    /// </summary>
    public class Catalogs
    {
        /// <summary>
        /// 获取全部行业类别信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCatalog()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/SAS/CompanyCategories") as DataTable;
            if (dt == null)
            {
                dt = SAS.Data.DataProvider.Catalogies.GetAllCatalogList();
                cache.AddObject("/SAS/CompanyCategories", dt);
            }
            return dt;
        }
    }
}
