using System;
using System.Data;
using System.Text;

using SAS.Entity;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 地区操作类
    /// </summary>
    public class Areas
    {
        /// <summary>
        /// 获取省份信息集合
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProvince()
        {
            return DatabaseProvider.GetInstance().GetProvince();
        }

        /// <summary>
        /// 获取城市信息集合
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCity()
        {
            return DatabaseProvider.GetInstance().GetCity();
        }

        /// <summary>
        /// 获取地区信息集合
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistrict()
        {
            return DatabaseProvider.GetInstance().GetDistrict();
        }
    }
}
