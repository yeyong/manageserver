﻿using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 企业信息操作
    /// </summary>
    public class Companies
    {
        /// <summary>
        /// 添加企业信息
        /// </summary>
        /// <param name="_companyInfo"></param>
        /// <returns>返回企业信息ID</returns>
        public static int CreateCompany(Companys _companyInfo)
        {
            return DatabaseProvider.GetInstance().CreateCompany(_companyInfo);
        }

        /// <summary>
        /// 获取企业实体信息（有省市区）
        /// </summary>
        public static Companys GetCompanyInfo(int enid)
        {
            return LoadSingleCompanyInfo(DatabaseProvider.GetInstance().GetCompanyByID(enid));
        }

        /// <summary>
        /// 企业信息实体化
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Companys LoadSingleCompanyInfo(IDataReader reader)
        {
            Companys _companyInfo = null;
            if (reader.Read())
            {
                _companyInfo = new Companys();
                _companyInfo.En_id = TypeConverter.StrToInt(reader["en_id"].ToString(), 0);
                _companyInfo.En_name = reader["en_name"].ToString();
                _companyInfo.En_main = reader["en_main"].ToString();
                _companyInfo.En_type = TypeConverter.StrToInt(reader["en_type"].ToString(), 0);
                _companyInfo.En_enco = TypeConverter.StrToInt(reader["en_enco"].ToString(), 0);
                _companyInfo.En_sell = TypeConverter.StrToInt(reader["en_sell"].ToString(), 0);
                _companyInfo.En_address = reader["en_address"].ToString();
                _companyInfo.En_areas = TypeConverter.StrToInt(reader["en_areas"].ToString(), 0);
                _companyInfo.En_desc = reader["en_desc"].ToString();
                _companyInfo.En_post = reader["en_post"].ToString();
                _companyInfo.En_mobile = reader["en_mobile"].ToString();
                _companyInfo.En_phone = reader["en_phone"].ToString();
                _companyInfo.En_fax = reader["en_fax"].ToString();
                _companyInfo.En_mail = reader["en_mail"].ToString();
                _companyInfo.En_web = reader["en_web"].ToString();
                _companyInfo.En_corp = reader["en_corp"].ToString();
                _companyInfo.En_contact = reader["en_contact"].ToString();
                _companyInfo.En_update = Utils.GetStandardDateTime(reader["en_update"].ToString());
                _companyInfo.En_status = TypeConverter.StrToInt(reader["en_status"].ToString());
                _companyInfo.En_reason = reader["en_reason"].ToString();
                _companyInfo.En_level = TypeConverter.StrToInt(reader["en_level"].ToString(), 0);
                _companyInfo.En_accesses = TypeConverter.StrToInt(reader["en_accesses"].ToString(), 0);
                _companyInfo.En_credits = TypeConverter.StrToInt(reader["en_credits"].ToString(), 0);
                _companyInfo.En_logo = reader["en_logo"].ToString();
                _companyInfo.En_music = reader["en_music"].ToString();
                _companyInfo.Reg_capital = reader["reg_capital"].ToString();
                _companyInfo.Reg_address = reader["reg_address"].ToString();
                _companyInfo.Reg_code = reader["reg_code"].ToString();
                _companyInfo.Reg_organ = reader["reg_organ"].ToString();
                _companyInfo.Reg_year = Utils.GetStandardDate(reader["reg_year"].ToString());
                _companyInfo.Reg_date = reader["reg_date"].ToString();
                _companyInfo.En_builddate = Utils.GetStandardDate(reader["en_builddate"].ToString());
                _companyInfo.En_visble = TypeConverter.StrToInt(reader["en_visble"].ToString(), 0);
                _companyInfo.En_createdate = Utils.GetStandardDateTime(reader["en_createdate"].ToString());
                _companyInfo.En_cataloglist = reader["en_cataloglist"].ToString();
                _companyInfo.Configid = TypeConverter.StrToInt(reader["configid"].ToString());
                //_companyInfo.ProvinceName = reader["ProvinceName"].ToString();
                //_companyInfo.CityName = reader["CityName"].ToString();
                //_companyInfo.DistrictName = reader["DistrictName"].ToString();
            }
            reader.Close();
            return _companyInfo;
        }

        /// <summary>
        /// 企业信息实体化
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Companys LoadSingleCompanyInfoWithDetail(IDataReader reader)
        {
            Companys _companyInfo = null;
            if (reader.Read())
            {
                _companyInfo = new Companys();
                _companyInfo.En_id = TypeConverter.StrToInt(reader["en_id"].ToString(), 0);
                _companyInfo.En_name = reader["en_name"].ToString();
                _companyInfo.En_main = reader["en_main"].ToString();
                _companyInfo.En_type = TypeConverter.StrToInt(reader["en_type"].ToString(), 0);
                _companyInfo.En_enco = TypeConverter.StrToInt(reader["en_enco"].ToString(), 0);
                _companyInfo.En_sell = TypeConverter.StrToInt(reader["en_sell"].ToString(), 0);
                _companyInfo.En_address = reader["en_address"].ToString();
                _companyInfo.En_areas = TypeConverter.StrToInt(reader["en_areas"].ToString(), 0);
                _companyInfo.En_desc = reader["en_desc"].ToString();
                _companyInfo.En_post = reader["en_post"].ToString();
                _companyInfo.En_mobile = reader["en_mobile"].ToString();
                _companyInfo.En_phone = reader["en_phone"].ToString();
                _companyInfo.En_fax = reader["en_fax"].ToString();
                _companyInfo.En_mail = reader["en_mail"].ToString();
                _companyInfo.En_web = reader["en_web"].ToString();
                _companyInfo.En_corp = reader["en_corp"].ToString();
                _companyInfo.En_contact = reader["en_contact"].ToString();
                _companyInfo.En_update = Utils.GetStandardDateTime(reader["en_update"].ToString());
                _companyInfo.En_status = TypeConverter.StrToInt(reader["en_status"].ToString());
                _companyInfo.En_reason = reader["en_reason"].ToString();
                _companyInfo.En_level = TypeConverter.StrToInt(reader["en_level"].ToString(), 0);
                _companyInfo.En_accesses = TypeConverter.StrToInt(reader["en_accesses"].ToString(), 0);
                _companyInfo.En_credits = TypeConverter.StrToInt(reader["en_credits"].ToString(), 0);
                _companyInfo.En_logo = reader["en_logo"].ToString();
                _companyInfo.En_music = reader["en_music"].ToString();
                _companyInfo.Reg_capital = reader["reg_capital"].ToString();
                _companyInfo.Reg_address = reader["reg_address"].ToString();
                _companyInfo.Reg_code = reader["reg_code"].ToString();
                _companyInfo.Reg_organ = reader["reg_organ"].ToString();
                _companyInfo.Reg_year = Utils.GetStandardDate(reader["reg_year"].ToString());
                _companyInfo.Reg_date = reader["reg_date"].ToString();
                _companyInfo.En_builddate = Utils.GetStandardDate(reader["en_builddate"].ToString());
                _companyInfo.En_visble = TypeConverter.StrToInt(reader["en_visble"].ToString(), 0);
                _companyInfo.En_createdate = Utils.GetStandardDateTime(reader["en_createdate"].ToString());
                _companyInfo.En_cataloglist = reader["en_cataloglist"].ToString();
                _companyInfo.Configid = TypeConverter.StrToInt(reader["configid"].ToString());
                _companyInfo.ProvinceName = reader["ProvinceName"].ToString();
                _companyInfo.CityName = reader["CityName"].ToString();
                _companyInfo.DistrictName = reader["DistrictName"].ToString();
            }
            reader.Close();
            return _companyInfo;
        }

        /// <summary>
        /// 企业信息实体化
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Companys LoadCompanyInfo(IDataReader reader)
        {
            Companys _companyInfo = new Companys();
            _companyInfo.En_id = TypeConverter.StrToInt(reader["en_id"].ToString(), 0);
            _companyInfo.En_name = reader["en_name"].ToString();
            _companyInfo.En_main = reader["en_main"].ToString();
            _companyInfo.En_type = TypeConverter.StrToInt(reader["en_type"].ToString(), 0);
            _companyInfo.En_enco = TypeConverter.StrToInt(reader["en_enco"].ToString(), 0);
            _companyInfo.En_sell = TypeConverter.StrToInt(reader["en_sell"].ToString(), 0);
            _companyInfo.En_address = reader["en_address"].ToString();
            _companyInfo.En_areas = TypeConverter.StrToInt(reader["en_areas"].ToString(), 0);
            _companyInfo.En_desc = reader["en_desc"].ToString();
            _companyInfo.En_post = reader["en_post"].ToString();
            _companyInfo.En_mobile = reader["en_mobile"].ToString();
            _companyInfo.En_phone = reader["en_phone"].ToString();
            _companyInfo.En_fax = reader["en_fax"].ToString();
            _companyInfo.En_mail = reader["en_mail"].ToString();
            _companyInfo.En_web = reader["en_web"].ToString();
            _companyInfo.En_corp = reader["en_corp"].ToString();
            _companyInfo.En_contact = reader["en_contact"].ToString();
            _companyInfo.En_update = Utils.GetStandardDateTime(reader["en_update"].ToString());
            _companyInfo.En_status = TypeConverter.StrToInt(reader["en_status"].ToString());
            _companyInfo.En_reason = reader["en_reason"].ToString();
            _companyInfo.En_level = TypeConverter.StrToInt(reader["en_level"].ToString(), 0);
            _companyInfo.En_accesses = TypeConverter.StrToInt(reader["en_accesses"].ToString(), 0);
            _companyInfo.En_credits = TypeConverter.StrToInt(reader["en_credits"].ToString(), 0);
            _companyInfo.En_logo = reader["en_logo"].ToString();
            _companyInfo.En_music = reader["en_music"].ToString();
            _companyInfo.Reg_capital = reader["reg_capital"].ToString();
            _companyInfo.Reg_address = reader["reg_address"].ToString();
            _companyInfo.Reg_code = reader["reg_code"].ToString();
            _companyInfo.Reg_organ = reader["reg_organ"].ToString();
            _companyInfo.Reg_year = Utils.GetStandardDate(reader["reg_year"].ToString());
            _companyInfo.Reg_date = reader["reg_date"].ToString();
            _companyInfo.En_builddate = Utils.GetStandardDate(reader["en_builddate"].ToString());
            _companyInfo.En_visble = TypeConverter.StrToInt(reader["en_visble"].ToString(), 0);
            _companyInfo.En_createdate = Utils.GetStandardDateTime(reader["en_createdate"].ToString());
            _companyInfo.En_cataloglist = reader["en_cataloglist"].ToString();
            _companyInfo.Configid = TypeConverter.StrToInt(reader["configid"].ToString());
            _companyInfo.ProvinceName = reader["ProvinceName"].ToString();
            _companyInfo.CityName = reader["CityName"].ToString();
            _companyInfo.DistrictName = reader["DistrictName"].ToString();
            return _companyInfo;
        }
        /// <summary>
        /// 企业信息实体化
        /// </summary>
        public static Companys LoadCompanyInfoWithoutCity(IDataReader reader)
        {
            return LoadCompanyInfoWithoutCity(reader, false);
        }
        /// <summary>
        /// 企业信息实体化
        /// </summary>
        public static Companys LoadCompanyInfoWithoutCity(IDataReader reader,bool isscored)
        {
            Companys _companyInfo = new Companys();
            _companyInfo.En_id = TypeConverter.StrToInt(reader["en_id"].ToString(), 0);
            _companyInfo.En_name = reader["en_name"].ToString();
            _companyInfo.En_main = reader["en_main"].ToString();
            _companyInfo.En_type = TypeConverter.StrToInt(reader["en_type"].ToString(), 0);
            _companyInfo.En_enco = TypeConverter.StrToInt(reader["en_enco"].ToString(), 0);
            _companyInfo.En_sell = TypeConverter.StrToInt(reader["en_sell"].ToString(), 0);
            _companyInfo.En_address = reader["en_address"].ToString();
            _companyInfo.En_areas = TypeConverter.StrToInt(reader["en_areas"].ToString(), 0);
            _companyInfo.En_desc = reader["en_desc"].ToString();
            _companyInfo.En_post = reader["en_post"].ToString();
            _companyInfo.En_mobile = reader["en_mobile"].ToString();
            _companyInfo.En_phone = reader["en_phone"].ToString();
            _companyInfo.En_fax = reader["en_fax"].ToString();
            _companyInfo.En_mail = reader["en_mail"].ToString();
            _companyInfo.En_web = reader["en_web"].ToString();
            _companyInfo.En_corp = reader["en_corp"].ToString();
            _companyInfo.En_contact = reader["en_contact"].ToString();
            _companyInfo.En_update = Utils.GetStandardDateTime(reader["en_update"].ToString());
            _companyInfo.En_status = TypeConverter.StrToInt(reader["en_status"].ToString());
            _companyInfo.En_reason = reader["en_reason"].ToString();
            _companyInfo.En_level = TypeConverter.StrToInt(reader["en_level"].ToString(), 0);
            _companyInfo.En_accesses = TypeConverter.StrToInt(reader["en_accesses"].ToString(), 0);
            _companyInfo.En_credits = TypeConverter.StrToInt(reader["en_credits"].ToString(), 0);
            _companyInfo.En_logo = reader["en_logo"].ToString();
            _companyInfo.En_music = reader["en_music"].ToString();
            _companyInfo.Reg_capital = reader["reg_capital"].ToString();
            _companyInfo.Reg_address = reader["reg_address"].ToString();
            _companyInfo.Reg_code = reader["reg_code"].ToString();
            _companyInfo.Reg_organ = reader["reg_organ"].ToString();
            _companyInfo.Reg_year = Utils.GetStandardDate(reader["reg_year"].ToString());
            _companyInfo.Reg_date = reader["reg_date"].ToString();
            _companyInfo.En_builddate = Utils.GetStandardDate(reader["en_builddate"].ToString());
            _companyInfo.En_visble = TypeConverter.StrToInt(reader["en_visble"].ToString(), 0);
            _companyInfo.En_createdate = Utils.GetStandardDateTime(reader["en_createdate"].ToString());
            _companyInfo.En_cataloglist = reader["en_cataloglist"].ToString();
            _companyInfo.Configid = TypeConverter.StrToInt(reader["configid"].ToString());
            if (isscored) _companyInfo.EnScored = reader["en_scored"].ToString();
            return _companyInfo;
        }
        /// <summary>
        /// 企业信息实体化
        /// </summary>
        public static Companys LoadCompanyInfo(DataRow reader)
        {
            Companys _companyInfo = new Companys();
            _companyInfo.En_id = TypeConverter.StrToInt(reader["en_id"].ToString(), 0);
            _companyInfo.En_name = reader["en_name"].ToString();
            _companyInfo.En_main = reader["en_main"].ToString();
            _companyInfo.En_type = TypeConverter.StrToInt(reader["en_type"].ToString(), 0);
            _companyInfo.En_enco = TypeConverter.StrToInt(reader["en_enco"].ToString(), 0);
            _companyInfo.En_sell = TypeConverter.StrToInt(reader["en_sell"].ToString(), 0);
            _companyInfo.En_address = reader["en_address"].ToString();
            _companyInfo.En_areas = TypeConverter.StrToInt(reader["en_areas"].ToString(), 0);
            _companyInfo.En_desc = reader["en_desc"].ToString();
            _companyInfo.En_post = reader["en_post"].ToString();
            _companyInfo.En_mobile = reader["en_mobile"].ToString();
            _companyInfo.En_phone = reader["en_phone"].ToString();
            _companyInfo.En_fax = reader["en_fax"].ToString();
            _companyInfo.En_mail = reader["en_mail"].ToString();
            _companyInfo.En_web = reader["en_web"].ToString();
            _companyInfo.En_corp = reader["en_corp"].ToString();
            _companyInfo.En_contact = reader["en_contact"].ToString();
            _companyInfo.En_update = Utils.GetStandardDateTime(reader["en_update"].ToString());
            _companyInfo.En_status = TypeConverter.StrToInt(reader["en_status"].ToString());
            _companyInfo.En_reason = reader["en_reason"].ToString();
            _companyInfo.En_level = TypeConverter.StrToInt(reader["en_level"].ToString(), 0);
            _companyInfo.En_accesses = TypeConverter.StrToInt(reader["en_accesses"].ToString(), 0);
            _companyInfo.En_credits = TypeConverter.StrToInt(reader["en_credits"].ToString(), 0);
            _companyInfo.En_logo = reader["en_logo"].ToString();
            _companyInfo.En_music = reader["en_music"].ToString();
            _companyInfo.Reg_capital = reader["reg_capital"].ToString();
            _companyInfo.Reg_address = reader["reg_address"].ToString();
            _companyInfo.Reg_code = reader["reg_code"].ToString();
            _companyInfo.Reg_organ = reader["reg_organ"].ToString();
            _companyInfo.Reg_year = Utils.GetStandardDate(reader["reg_year"].ToString());
            _companyInfo.Reg_date = reader["reg_date"].ToString();
            _companyInfo.En_builddate = Utils.GetStandardDate(reader["en_builddate"].ToString());
            _companyInfo.En_visble = TypeConverter.StrToInt(reader["en_visble"].ToString(), 0);
            _companyInfo.En_createdate = Utils.GetStandardDateTime(reader["en_createdate"].ToString());
            _companyInfo.En_cataloglist = reader["en_cataloglist"].ToString();
            _companyInfo.Configid = TypeConverter.StrToInt(reader["configid"].ToString());
            return _companyInfo;
        }

        /// <summary>
        /// 根据ID获取企业信息实体
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static Companys GetCompanyInfoByID(int enid)
        {
            return LoadSingleCompanyInfoWithDetail(DatabaseProvider.GetInstance().GetCompanyInfo(enid));
        }

        /// <summary>
        /// 根据企业名称获得企业信息实体
        /// </summary>
        /// <param name="enname"></param>
        /// <returns></returns>
        public static Companys GetCompanyInfoByName(string enname)
        {
            return LoadSingleCompanyInfo(DatabaseProvider.GetInstance().GetCompanyByName(enname));
        }

        /// <summary>
        /// 获取企业信息集合
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanyAllList()
        {
            return DatabaseProvider.GetInstance().GetCompanyAllList();
        }

        /// <summary>
        /// 企业数据分页操作
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <param name="ordercolumn">排序列名</param>
        /// <param name="ordertype">排序方式</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public static DataTable GetCompanyPageList(int pageindex, int pagesize, string conditions)
        {
            return DatabaseProvider.GetInstance().GetCompanyPageList(pageindex, pagesize, conditions);
        }

        /// <summary>
        /// 获取企业数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static int GetCompanyCountByCondition(string conditions)
        {
            return DatabaseProvider.GetInstance().GetCompanyCountByConditions(conditions);
        }

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool UpdateCompanyStatus(string enidlist, int status)
        {
            return DatabaseProvider.GetInstance().UpdateCompanyStatus(enidlist, status);
        }

        /// <summary>
        /// 更新企业信息
        /// </summary>
        /// <param name="_company"></param>
        /// <returns></returns>
        public static bool UpdateCompany(Companys _company)
        {
            return DatabaseProvider.GetInstance().UpdateCompany(_company);
        }

        /// <summary>
        /// 更新企业浏览次数
        /// </summary>
        /// <param name="enid"></param>
        /// <param name="viewcount"></param>
        /// <returns></returns>
        public static int UpdataCompanyViewCount(int enid, int viewcount)
        {
            return DatabaseProvider.GetInstance().UpdateCompanyViewCount(enid, viewcount);
        }

        /// <summary>
        /// 企业搜索条件
        /// </summary>
        /// <param name="catalogid">行业类别ID</param>
        /// <param name="arealist">所在地区列表</param>
        /// <param name="typeid">企业类型ID</param>
        /// <param name="regyear">注册年限</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public static string GetCompanyCondition(string arealist, int typeid, int regyear, string keyword)
        {
            return DatabaseProvider.GetInstance().GetCompanyCondition(arealist, typeid, regyear, keyword);
        }

        /// <summary>
        /// 企业搜索条件
        /// </summary>
        /// <param name="islike">是否模糊搜索</param>
        /// <param name="enname">企业名称</param>
        /// <param name="enstatus">审核状态</param>
        /// <param name="isbuilddate">是否查找创建时间</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="envisible">开启状态</param>
        public static string GetCompanySearchList(bool islike, string enname, int enstatus, bool isbuilddate, string starttime, string endtime, int envisible)
        {
            return DatabaseProvider.GetInstance().Global_CompanyGrid_SearchCondition(islike, enname, enstatus, isbuilddate, starttime, endtime, envisible);
        }

        /// <summary>
        /// 获取企业信息列表（有省市区）
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCompanyALLList()
        {
            return DatabaseProvider.GetInstance().GetCompanyList();
        }
        /// <summary>
        /// 根据类别获取企业信息列表（有省市区）
        /// </summary>
        public static DataTable GetCompanyListByCatalog(int catalogid)
        {
            return DatabaseProvider.GetInstance().GetCompanyListByCatalogID(catalogid);
        }

        /// <summary>
        /// 获取企业分页信息表
        /// </summary>
        /// <param name="catalogid">类别ID</param>
        /// <param name="pagesize">每页大小</param>
        /// <param name="pageindex">当前页码</param>
        /// <param name="ordercolumn">排序行</param>
        /// <param name="ordertype">排序方式</param>
        /// <param name="conditions">条件</param>
        public static List<Companys> GetCompanyListPage(int catalogid, int pagesize, int pageindex, string ordercolumn, string ordertype, string conditions)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetCompanyListPageByCatalog(catalogid, pagesize, pageindex, ordercolumn, ordertype, conditions);
            List<Companys> companylist = new List<Companys>();

            while (reader.Read())
            {
                companylist.Add(LoadCompanyInfo(reader));
            }

            reader.Close();
            return companylist;
        }
        /// <summary>
        /// 根据行业类别获取企业数量
        /// </summary>
        /// <param name="catalogid"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static int GetCompanyCountByCatalog(int catalogid, string conditions)
        {
            return DatabaseProvider.GetInstance().GetCompanyCountByCatalog(catalogid, conditions);
        }
        /// <summary>
        /// 更新评论总数
        /// </summary>
        /// <param name="qyid"></param>
        /// <param name="counts"></param>
        public static void UpdateCompanyCommentCount(int qyid, int counts)
        {
            DatabaseProvider.GetInstance().UpdateCompanyCommentCount(qyid, counts);
        }

        /// <summary>
        /// 根据城市获取企业信息
        /// </summary>
        public static List<Companys> GetCompanyListByCity(int cityid, int nums)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetCompanyListByCity(cityid, nums);
            List<Companys> companylist = new List<Companys>();

            while (reader.Read())
            {
                companylist.Add(LoadCompanyInfoWithoutCity(reader));
            }

            reader.Close();
            return companylist;
        }

        /// <summary>
        /// 获取最新加盟企业信息
        /// </summary>
        /// <param name="nums">数量</param>
        /// <param name="ordercolumn">排序列</param>
        /// <param name="ordertype">排序类型（true，倒序）</param>
        public static List<Companys> GetCompanyListByOrder(int nums, string ordercolumn, bool ordertype)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetCompanyListByOrder(nums, ordercolumn, ordertype);
            List<Companys> companylist = new List<Companys>();

            while (reader.Read())
            {
                companylist.Add(LoadCompanyInfoWithoutCity(reader));
            }

            reader.Close();
            return companylist;
        }

        /// <summary>
        /// 根据类型获取企业信息
        /// </summary>
        /// <param name="entype"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static List<Companys> GetCompanyListByType(int entype, int nums)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetCompanyListByType(entype, nums);
            List<Companys> companylist = new List<Companys>();

            while (reader.Read())
            {
                companylist.Add(LoadCompanyInfoWithoutCity(reader));
            }

            reader.Close();
            return companylist;
        }
        /// <summary>
        /// 根据评分获取企业信息
        /// </summary>
        public static List<Companys> GetCompanyByScored()
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetScoredCompany(8);
            List<Companys> companylist = new List<Companys>();
            while (reader.Read())
            {
                companylist.Add(LoadCompanyInfoWithoutCity(reader, true));
            }
            reader.Close();
            return companylist;
        }
        /// <summary>
        /// 获取企业信息统计数量
        /// </summary>
        public static DataTable GetCompanyCountSum()
        {
            return DatabaseProvider.GetInstance().GetCompanyCountSum();
        }
        /// <summary>
        /// 根据城市、类别获取企业信息
        /// </summary>
        public static List<Companys> GetCompanyByCityCatalog(int city, int cid, int nums)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetCompanyByCityCatalog(city, cid, nums);
            List<Companys> companylist = new List<Companys>();
            while (reader.Read())
            {
                companylist.Add(LoadCompanyInfoWithoutCity(reader));
            }
            reader.Close();
            return companylist;
        }
    }
}
