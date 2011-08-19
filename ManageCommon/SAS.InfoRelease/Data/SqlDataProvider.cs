using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

using SAS.Entity.InfoPlatform;

namespace SAS.InfoRelease.Data
{
    public class DataProvider : IDataProvider
    {
        /// <summary>
        /// 添加注册时信息
        /// </summary>
        public int InsertUser(UserInfo eui)
        {
            SqlParameter[] commandParameters = new SqlParameter[]{                               
				new SqlParameter("@LoginName", eui.LoginName),
				new SqlParameter("@Sex",eui.Sex),
				new SqlParameter("@Password",eui.Password),
				new SqlParameter("@Question", eui.Question),
				new SqlParameter("@Answer", eui.Answer),
				new SqlParameter("@GradeID", eui.GradeID),
				new SqlParameter("@Lockuedup", eui.Lockuedup),
				new SqlParameter("@LinkName", eui.LinkName),
				new SqlParameter("@MobilePhone", eui.MobilePhone),
				new SqlParameter("@Email", eui.Email),
				new SqlParameter("@QQ", eui.QQ),
				new SqlParameter("@MSN", eui.MSN),
				new SqlParameter("@Tel_International", eui.Tel_International),
				new SqlParameter("@Tel_DistrictNumber", eui.Tel_DistrictNumber),
				new SqlParameter("@Tel_Telephone", eui.Tel_Telephone),
				new SqlParameter("@Tel_Ext", eui.Tel_Ext),
            	new SqlParameter("@Fax_International", eui.Fax_International),
				new SqlParameter("@Fax_DistrictNumber", eui.Fax_DistrictNumber),
				new SqlParameter("@Fax_Telephone", eui.Fax_Telephone),
				new SqlParameter("@Fax_Ext", eui.Fax_Ext),
				new SqlParameter("@Department", eui.Department),
				new SqlParameter("@Position", eui.Position),
				//new SqlParameter("@RegDate", eui.RegDate),
				new SqlParameter("@CompanyName", eui.CompanyName),
				new SqlParameter("@CompanyNature", eui.CompanyNature),
				new SqlParameter("@BusinessModel", eui.BusinessModel),
				new SqlParameter("@DealinAdd", eui.DealinAdd),
				new SqlParameter("@Product", eui.Product),
				new SqlParameter("@Industry", eui.Industry),
				new SqlParameter("@Summary", eui.Summary),
				new SqlParameter("@Verify", eui.Verify),
				new SqlParameter("@Country", eui.Country),
				new SqlParameter("@Province", eui.Province),
				new SqlParameter("@City", eui.City),
				new SqlParameter("@Area", eui.Area),
				new SqlParameter("@Street", eui.Street),
				new SqlParameter("@Postalcode", eui.Postalcode),
				new SqlParameter("@URL", eui.URL),
				new SqlParameter("@Capital", eui.Capital),
				new SqlParameter("@Established",eui.Established),
				new SqlParameter("@RegisterAddress", eui.RegisterAddress),
				new SqlParameter("@Corporate", eui.Corporate),
				//new SqlParameter("@StartDate", eui.StartDate),
				//new SqlParameter("@EndDate", eui.EndDate),
				//new SqlParameter("@Logo", eui.Logo)            
             };
            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SP_U_UserInfo_Add", commandParameters);
        }

        public UserInfo GetUserByName(string lname)
        {
            SqlParameter[] param = new SqlParameter[] 
            { 
                new SqlParameter("@strWhere", "where LoginName='" + lname + "'"), 
                new SqlParameter("@strTableName", "U_UserInfo"), 
                new SqlParameter("@strOrder", "") 
            };
            UserInfo info = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "SP_SelectByWhere", param))
            {
                if (!reader.Read())
                {
                    return info;
                }
                info = new UserInfo();

                //会员信息
                info.UserID = Convert.ToInt32(reader["UserID"].ToString());
                info.LoginName = reader["LoginName"].ToString();
                info.Password = reader["password"].ToString();
                info.Question = reader["Question"].ToString();
                info.Answer = reader["Answer"].ToString();
                if (reader["GradeID"].ToString() != "")
                    info.GradeID = int.Parse(reader["GradeID"].ToString());

                //返回联系人信息
                info.LinkName = reader["LinkName"].ToString();
                info.Sex = reader["Sex"].ToString();
                info.Department = reader["Department"].ToString();
                info.Position = reader["Position"].ToString();
                info.Tel_International = reader["Tel_International"].ToString();
                info.Tel_DistrictNumber = reader["Tel_DistrictNumber"].ToString();
                info.Tel_Telephone = reader["Tel_Telephone"].ToString();
                info.Tel_Ext = reader["Tel_Ext"].ToString();
                info.Fax_International = reader["Fax_International"].ToString();
                info.Fax_DistrictNumber = reader["Fax_DistrictNumber"].ToString();
                info.Fax_Telephone = reader["Fax_Telephone"].ToString();
                info.Fax_Ext = reader["Fax_Ext"].ToString();
                info.MobilePhone = reader["MobilePhone"].ToString();
                info.Email = reader["EMail"].ToString();
                info.QQ = reader["QQ"].ToString();
                info.MSN = reader["MSN"].ToString();

                //返回公司信息
                info.CompanyName = reader["CompanyName"].ToString();
                info.CompanyNature = reader["CompanyNature"].ToString();
                info.BusinessModel = reader["BusinessModel"].ToString();
                if (reader["Capital"].ToString() != "")
                    info.Capital = int.Parse(reader["Capital"].ToString());
                if (reader["Established"].ToString() != "")
                    info.Established = Convert.ToDateTime(reader["Established"].ToString());
                info.RegisterAddress = reader["RegisterAddress"].ToString();
                info.Country = reader["Country"].ToString();
                info.Province = reader["Province"].ToString();
                info.City = reader["City"].ToString();
                info.Area = reader["Area"].ToString();
                info.DealinAdd = reader["DealinAdd"].ToString();
                info.Postalcode = reader["Postalcode"].ToString();
                info.Product = reader["Product"].ToString();
                info.Industry = reader["Industry"].ToString();
                info.Summary = reader["Summary"].ToString();
                info.Street = reader["Street"].ToString();
                info.URL = reader["URL"].ToString();
                info.Corporate = reader["Corporate"].ToString();
                info.Logo = reader["Logo"].ToString();
                info.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                info.EndDate = DateTime.Parse(reader["EndDate"].ToString());
            }
            return info;
        }

        /// <summary>
        /// 修改企业会员信息
        /// </summary>
        public int UpdateUser(UserInfo eui)
        {
            SqlParameter[] commandParameters = new SqlParameter[]{                               
				new SqlParameter("@UserID", eui.UserID),
                new SqlParameter("@GradeID", eui.GradeID),
				new SqlParameter("@CompanyName", eui.CompanyName),
				new SqlParameter("@CompanyNature", eui.CompanyNature),
				new SqlParameter("@BusinessModel", eui.BusinessModel),
				new SqlParameter("@DealinAdd", eui.DealinAdd),
				new SqlParameter("@Product", eui.Product),
				new SqlParameter("@Industry", eui.Industry),
				new SqlParameter("@Summary", eui.Summary),
				new SqlParameter("@Country", eui.Country),
				new SqlParameter("@Province", eui.Province),
				new SqlParameter("@City", eui.City),
				new SqlParameter("@Area", eui.Area),
				new SqlParameter("@Street", eui.Street),
				new SqlParameter("@Postalcode", eui.Postalcode),
				new SqlParameter("@URL", eui.URL),
				new SqlParameter("@Capital", eui.Capital),
				new SqlParameter("@Established",eui.Established),
				new SqlParameter("@RegisterAddress", eui.RegisterAddress),
				new SqlParameter("@Corporate", eui.Corporate),
                new SqlParameter("@Logo", eui.Logo)
             };
            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SP_U_UserInfo_CorpInfo_Update", commandParameters);
        }
    }
}
