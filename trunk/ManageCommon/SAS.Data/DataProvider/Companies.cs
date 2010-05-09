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
        /// 获取企业信息集合
        /// </summary>
        /// <returns></returns>
        public static List<Companys> GetCompanyList()
        {
            List<Companys> companylist = new List<Companys>();
            DataTable dt = GetCompanyAllList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Companys _companyInfo = new Companys();
                    _companyInfo.En_id = TypeConverter.StrToInt(dr["en_id"].ToString(), 0);
                    _companyInfo.En_name = dr["en_name"].ToString();
                    _companyInfo.En_main = dr["en_main"].ToString();
                    _companyInfo.En_type = TypeConverter.StrToInt(dr["en_type"].ToString(), 0);
                    _companyInfo.En_enco = TypeConverter.StrToInt(dr["en_enco"].ToString(), 0);
                    _companyInfo.En_sell = TypeConverter.StrToInt(dr["en_sell"].ToString(), 0);
                    _companyInfo.En_address = dr["en_address"].ToString();
                    _companyInfo.En_areas = TypeConverter.StrToInt(dr["en_areas"].ToString(), 0);
                    _companyInfo.En_desc = dr["en_desc"].ToString();
                    _companyInfo.En_post = dr["en_post"].ToString();
                    _companyInfo.En_mobile = dr["en_mobile"].ToString();
                    _companyInfo.En_phone = dr["en_phone"].ToString();
                    _companyInfo.En_fax = dr["en_fax"].ToString();
                    _companyInfo.En_mail = dr["en_mail"].ToString();
                    _companyInfo.En_web = dr["en_web"].ToString();
                    _companyInfo.En_corp = dr["en_corp"].ToString();
                    _companyInfo.En_contact = dr["en_contact"].ToString();
                    _companyInfo.En_update = Utils.GetStandardDateTime(dr["en_update"].ToString());
                    _companyInfo.En_status = TypeConverter.StrToInt(dr["en_status"].ToString());
                    _companyInfo.En_reason = dr["en_reason"].ToString();
                    _companyInfo.En_level = TypeConverter.StrToInt(dr["en_level"].ToString(), 0);
                    _companyInfo.En_accesses = TypeConverter.StrToInt(dr["en_accesses"].ToString(), 0);
                    _companyInfo.En_credits = TypeConverter.StrToInt(dr["en_credits"].ToString(), 0);
                    _companyInfo.En_logo = dr["en_logo"].ToString();
                    _companyInfo.En_music = dr["en_music"].ToString();
                    _companyInfo.Reg_capital = dr["reg_capital"].ToString();
                    _companyInfo.Reg_address = dr["reg_address"].ToString();
                    _companyInfo.Reg_code = dr["reg_code"].ToString();
                    _companyInfo.Reg_organ = dr["reg_organ"].ToString();
                    _companyInfo.Reg_year = Utils.GetStandardDate(dr["reg_year"].ToString());
                    _companyInfo.Reg_date = dr["reg_date"].ToString();
                    _companyInfo.En_builddate = Utils.GetStandardDate(dr["en_builddate"].ToString());
                    _companyInfo.En_visble = TypeConverter.StrToInt(dr["en_visble"].ToString(), 0);
                    _companyInfo.En_createdate = Utils.GetStandardDateTime(dr["en_createdate"].ToString());
                    _companyInfo.En_cataloglist = dr["en_cataloglist"].ToString();
                    _companyInfo.Configid = TypeConverter.StrToInt(dr["configid"].ToString());
                    companylist.Add(_companyInfo);
                }
            }
            return companylist;
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

        /// <summary>
        /// 企业搜索条件
        /// </summary>
        /// <param name="catalogid">行业类别ID</param>
        /// <param name="arealist">所在地区列表</param>
        /// <param name="typeid">企业类型ID</param>
        /// <param name="regyear">注册年限</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public static string GetCompanyCondition(int catalogid, string arealist, int typeid, int regyear, string keyword)
        {
            return DatabaseProvider.GetInstance().GetCompanyCondition(catalogid, arealist, typeid, regyear, keyword);
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
        public static DataTable GetCompanyPageList(int pageindex, int pagesize, string ordercolumn, string ordertype, string conditions)
        {
            return DatabaseProvider.GetInstance().GetCompanyPageList(pageindex, pagesize, ordercolumn, ordertype, conditions);
        }
    }
}
