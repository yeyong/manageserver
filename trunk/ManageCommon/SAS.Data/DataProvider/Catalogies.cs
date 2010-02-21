using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 行业类别数据操作
    /// </summary>
    public class Catalogies
    {
        /// <summary>
        /// 获取全部行业类别信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCatalogList()
        {
            return DatabaseProvider.GetInstance().GetAllCatalog();
        }
    }
}
