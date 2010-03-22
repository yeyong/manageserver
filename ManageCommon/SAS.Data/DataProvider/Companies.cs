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
                _companyInfo.en_id = TypeConverter.StrToInt(reader["en_id"].ToString(), 0);
                _companyInfo.en_name = reader["en_name"].ToString();
                _companyInfo.en_main = reader["en_main"].ToString();
                _companyInfo.en_type = TypeConverter.StrToInt(reader["en_type"].ToString(), 0);
                _companyInfo.en_enco = TypeConverter.StrToInt(reader["en_enco"].ToString(), 0);
                _companyInfo.en_sell = TypeConverter.StrToInt(reader["en_sell"].ToString(), 0);
                _companyInfo.en_address = reader["en_address"].ToString();
                _companyInfo.en_areas = TypeConverter.StrToInt(reader["en_areas"].ToString(), 0);
                _companyInfo.en_desc = reader["en_desc"].ToString();
                _companyInfo.en_post = reader["en_post"].ToString();
                _companyInfo.en_mobile = reader["en_mobile"].ToString();
                _companyInfo.en_phone = reader["en_phone"].ToString();
                _companyInfo.en_fax = reader["en_fax"].ToString();
                _companyInfo.en_mail = reader["en_mail"].ToString();
                _companyInfo.en_web = reader["en_web"].ToString();
                _companyInfo.en_corp = reader["en_corp"].ToString();
                _companyInfo.en_contact = reader["en_contact"].ToString();
                _companyInfo.en_update = Utils.GetStandardDateTime(reader["en_update"].ToString());
                _companyInfo.en_status = TypeConverter.StrToInt(reader["en_status"].ToString());
                _companyInfo.en_reason = reader["en_reason"].ToString();
                _companyInfo.en_level = TypeConverter.StrToInt(reader["en_level"].ToString(), 0);
                _companyInfo.en_accesses = TypeConverter.StrToInt(reader["en_accesses"].ToString(), 0);
                _companyInfo.en_credits = TypeConverter.StrToInt(reader["en_credits"].ToString(), 0);
                _companyInfo.en_logo = reader["en_logo"].ToString();
                _companyInfo.en_music = reader["en_music"].ToString();
                _companyInfo.reg_capital = reader["reg_capital"].ToString();
                _companyInfo.reg_address = reader["reg_address"].ToString();
                _companyInfo.reg_code = reader["reg_code"].ToString();
                _companyInfo.reg_organ = reader["reg_organ"].ToString();
                _companyInfo.reg_year = Utils.GetStandardDate(reader["reg_year"].ToString());
                _companyInfo.reg_date = reader["reg_date"].ToString();
                _companyInfo.en_builddate = Utils.GetStandardDate(reader["en_builddate"].ToString());
                _companyInfo.en_visble = TypeConverter.StrToInt(reader["en_visble"].ToString(), 0);
                _companyInfo.en_createdate = Utils.GetStandardDateTime(reader["en_createdate"].ToString());
                _companyInfo.en_cataloglist = reader["en_cataloglist"].ToString();
                _companyInfo.configid = TypeConverter.StrToInt(reader["configid"].ToString());
            }
            reader.Close();
            return _companyInfo;
        }

        /// <summary>
        /// 根据ID获取企业信息实体
        /// </summary>
        /// <param name="enid"></param>
        /// <returns></returns>
        public static Companys GetCompanyInfoByID(int enid)
        {
            return LoadSingleCompanyInfo(DatabaseProvider.GetInstance().GetCompanyByID(enid));
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
    }
}
