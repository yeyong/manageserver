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
        public static DataTable GetProvince()
        {
            return DatabaseProvider.GetInstance().GetProvince();
        }
        /// <summary>
        /// 获取省份信息集合（生成json）
        /// </summary>
        public static DataTable GetProvinceJosnList()
        {
            return DatabaseProvider.GetInstance().GetProvinceJosnList();
        }

        /// <summary>
        /// 获取城市信息集合
        /// </summary>
        public static DataTable GetCity()
        {
            return DatabaseProvider.GetInstance().GetCity();
        }
        /// <summary>
        /// 获取城市信息集合（生成json）
        /// </summary>
        public static DataTable GetCityJosnList()
        {
            return DatabaseProvider.GetInstance().GetCityJosnList();
        }

        /// <summary>
        /// 获取地区信息集合
        /// </summary>
        public static DataTable GetDistrict()
        {
            return DatabaseProvider.GetInstance().GetDistrict();
        }
        /// <summary>
        /// 获取地区信息集合（生成json）
        /// </summary>
        public static DataTable GetDistrictJosnList()
        {
            return DatabaseProvider.GetInstance().GetDistrictJosnList();
        }
    }
}
