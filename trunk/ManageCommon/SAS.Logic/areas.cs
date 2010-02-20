using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;
using System.Xml.Serialization;
using System.Data;

using SAS.Common;
using SAS.Data;
using SAS.Common.Generic;

namespace SAS.Logic
{
    /// <summary>
    /// 地区操作
    /// </summary>
    public class areas
    {
        /// <summary>
        /// 获取全部区县信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistrictList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable ddata = cache.RetrieveObject("/SAS/Districts") as DataTable;

            if (ddata == null)
            {
                ddata = SAS.Data.DataProvider.Areas.GetDistrict();
                cache.AddObject("/SAS/Districts", ddata);
            }

            return ddata;
        }

        /// <summary>
        /// 获取全部城市信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCityList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable cdata = cache.RetrieveObject("/SAS/Citys") as DataTable;

            if (cdata == null)
            {
                cdata = SAS.Data.DataProvider.Areas.GetCity();
                cache.AddObject("/SAS/Citys", cdata);
            }

            return cdata;
        }

        /// <summary>
        /// 获取全部省份信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProvinceList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable pdata = cache.RetrieveObject("/SAS/Provinces") as DataTable;

            if (pdata == null)
            {
                pdata = SAS.Data.DataProvider.Areas.GetProvince();
                cache.AddObject("/SAS/Provinces", pdata);
            }
            return pdata;
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
    }
}
