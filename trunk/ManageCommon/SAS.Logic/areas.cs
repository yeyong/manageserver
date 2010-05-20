using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Xml.Serialization;
using System.Data;

using SAS.Common;
using SAS.Data;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Cache.CacheDependencyFactory;

namespace SAS.Logic
{
    /// <summary>
    /// 地区操作
    /// </summary>
    public class areas : WriteFile
    {
        #region 私有变量
        private static volatile areas instance = null;
        private static object lockHelper = new object();
        private static string jsonPath = "";
        private static DataCacheConfigInfo dataconfig = DataCacheConfigs.GetConfig();
        #endregion

        #region 返回唯一实例
        private areas()
        {
            jsonPath = Utils.GetMapPath(BaseConfigs.GetSitePath + "\\javascript\\locations.js");
        }

        public static areas GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new areas();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
        /// <summary>
        /// 获取全部区县信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistrictList()
        {
            DataTable ddata = new DataTable();

            if (dataconfig.EnableCaching != 1)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                ddata = cache.RetrieveObject("/SAS/Districts") as DataTable;
                if (ddata == null)
                {
                    ddata = SAS.Data.DataProvider.Areas.GetDistrict();
                    cache.AddObject("/SAS/Districts", ddata);
                }
            }
            else
            {
                SAS.Cache.SASDataCache datacache = SAS.Cache.SASDataCache.GetCacheService();
                string cachekey = "Districts";
                ddata = datacache.GetDataCache(cachekey) as DataTable;

                if (ddata == null)
                {
                    ddata = SAS.Data.DataProvider.Areas.GetDistrict();
                    AggregateCacheDependency cd = DependencyFacade.GetDistrictDependency();
                    datacache.SetDataCache(cachekey, ddata, cd);
                }
            }
            return ddata;
        }

        /// <summary>
        /// 获取全部城市信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCityList()
        {
            DataTable cdata = new DataTable();
            if (dataconfig.EnableCaching != 1)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                cdata = cache.RetrieveObject("/SAS/Citys") as DataTable;

                if (cdata == null)
                {
                    cdata = SAS.Data.DataProvider.Areas.GetCity();
                    cache.AddObject("/SAS/Citys", cdata);
                }
            }
            else
            {
                SAS.Cache.SASDataCache datacache = SAS.Cache.SASDataCache.GetCacheService();
                string cachekey = "Citys";
                cdata = datacache.GetDataCache(cachekey) as DataTable;

                if (cdata == null)
                {
                    cdata = SAS.Data.DataProvider.Areas.GetDistrict();
                    AggregateCacheDependency cd = DependencyFacade.GetCityDependency();
                    datacache.SetDataCache(cachekey, cdata, cd);
                }
            }

            return cdata;
        }

        /// <summary>
        /// 获取全部省份信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProvinceList()
        {
            DataTable pdata = new DataTable();
            if (dataconfig.EnableCaching != 1)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                pdata = cache.RetrieveObject("/SAS/Provinces") as DataTable;

                if (pdata == null)
                {
                    pdata = SAS.Data.DataProvider.Areas.GetProvince();
                    cache.AddObject("/SAS/Provinces", pdata);
                }
            }
            else
            {
                SAS.Cache.SASDataCache datacache = SAS.Cache.SASDataCache.GetCacheService();
                string cachekey = "Provinces";
                pdata = datacache.GetDataCache(cachekey) as DataTable;

                if (pdata == null)
                {
                    pdata = SAS.Data.DataProvider.Areas.GetDistrict();
                    AggregateCacheDependency cd = DependencyFacade.GetProvinceDependency();
                    datacache.SetDataCache(cachekey, pdata, cd);
                }
            }
            return pdata;
        }

        /// <summary>
        /// 首页展现城市
        /// </summary>
        /// <returns></returns>
        public static DataRow[] GetIndexCity()
        {
            DataTable dt = GetCityList();
            return dt.Select("ProvinceID = 11");
        }

        /// <summary>
        /// 根据省份ID获取地区ID集合
        /// </summary>
        /// <param name="provinceid"></param>
        /// <returns></returns>
        public static string GetDistrictIDByProvince(int provinceid)
        {
            string cityidlist = "";
            string districtlist = "";

            foreach (DataRow dr in GetCityList().Select("ProvinceID = " + provinceid))
            {
                cityidlist += dr["CityID"].ToString() + ",";
            }
            if (cityidlist != "")
            {
                cityidlist = cityidlist.Trim(',');
                foreach (DataRow ddr in GetDistrictList().Select("CityID IN (" + cityidlist + ")"))
                {
                    districtlist += ddr["DistrictID"].ToString() + ",";
                }
            }

            return districtlist.Trim(',');
        }

        /// <summary>
        /// 根据市级ID获取地区ID集合
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public static string GetDistrictIDByCity(int cityid)
        {
            string districtlist = "";
            foreach (DataRow dr in GetDistrictList().Select("CityID = " + cityid))
            {
                districtlist += dr["DistrictID"].ToString() + ",";
            }
            return districtlist.Trim(',');
        }

        /// <summary>
        /// 获取级联字符串
        /// </summary>
        /// <param name="districtID">区县ID</param>
        /// <returns></returns>
        public static string GetCascadeString(int districtID)
        {
            int dID = districtID;
            int cID = 0;
            int pID = 0;
            DataTable ddata = GetDistrictList();
            DataTable cdate = GetCityList();

            DataRow[] ddr = ddata.Select("DistrictID = " + districtID);
            if (ddr.Length > 0) cID = TypeConverter.StrToInt(ddr[0]["CityID"].ToString(), 0);
            else return "";

            DataRow[] cdr = cdate.Select("CityID = " + cID);
            if (cdr.Length > 0) pID = TypeConverter.StrToInt(cdr[0]["ProvinceID"].ToString(), 0);
            else return "";

            if (pID == 0) return "";

            return dID + "," + cID + "," + pID;
        }

        /// <summary>
        /// 返回城市字符串
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static string returnCity(int parentid)
        {
            return returnCity("", parentid);
        }

        /// <summary>
        /// 返回城市字符串
        /// </summary>
        /// <param name="defaultvalue"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static string returnCity(string defaultvalue, int parentid)
        {
            string returnMessage = "<option value=\"0\" selected>请选择城市...</option>";
            if (defaultvalue != "") returnMessage = "<option value=\"0\">请选择城市...</option>";

            DataTable cdata = GetCityList();

            foreach (DataRow dr in cdata.Select("[ProvinceID] = " + parentid))
            {
                if (defaultvalue == dr["CityID"].ToString()) returnMessage += "<option value=\"" + dr["CityID"] + "\" selected>" + dr["CityName"] + "</option>";
                else returnMessage += "<option value=\"" + dr["CityID"] + "\">" + dr["CityName"] + "</option>";
            }

            return returnMessage;
        }

        /// <summary>
        /// 返回城市字符串
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static string returnDistrict(int parentid)
        {
            return returnDistrict("", parentid);
        }

        public static string returnDistrict(string defaultvalue, int parentid)
        {
            string returnMessage = "<option value=\"0\" selected>请选择地区...</option>";
            if (defaultvalue != "") returnMessage = "<option value=\"0\">请选择地区...</option>";
            DataTable ddata = GetDistrictList();

            foreach (DataRow dr in ddata.Select("[CityID] = " + parentid))
            {
                if (defaultvalue == dr["DistrictID"].ToString()) returnMessage += "<option value=\"" + dr["DistrictID"] + "\" selected>" + dr["DistrictName"] + "</option>";
                else returnMessage += "<option value=\"" + dr["DistrictID"] + "\">" + dr["DistrictName"] + "</option>";
            }

            return returnMessage;
        }

        /// <summary>
        /// 返回省份字符串
        /// </summary>
        /// <returns></returns>
        public static string returnProvinces()
        {
            return returnProvinces("");
        }

        /// <summary>
        /// 返回省份字符串（含有省份默认值）
        /// </summary>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static string returnProvinces(string defaultvalue)
        {
            string returnMessage = "<option value=\"0\" selected>请选择省份...</option>";
            if (defaultvalue != "") returnMessage = "<option value=\"0\">请选择省份...</option>";
            DataTable pdata = GetProvinceList();

            foreach(DataRow dr in pdata.Rows)
            {
                if (defaultvalue == dr["ProvinceID"].ToString()) returnMessage += "<option value=\"" + dr["ProvinceID"] + "\" selected>" + dr["ProvinceName"] + "</option>";
                else returnMessage += "<option value=\"" + dr["ProvinceID"] + "\">" + dr["ProvinceName"] + "</option>";
            }

            return returnMessage;
        }

        /// <summary>
        /// 生成商品分类表的JSON文件
        /// </summary>
        /// <returns>是否写入成功</returns>
        public override bool WriteJsonFile()
        {
            StringBuilder sb_areas = new StringBuilder("var provinces = ");

            sb_areas.Append(Utils.DataTableToJSON(GetProvinceList()));

            sb_areas.Append("var citys = ");

            sb_areas.Append(Utils.DataTableToJSON(GetCityList()));

            sb_areas.Append("var districts = ");

            sb_areas.Append(Utils.DataTableToJSON(GetDistrictList()));

            return base.WriteJsonFile(jsonPath, sb_areas);
        }
    }
}
