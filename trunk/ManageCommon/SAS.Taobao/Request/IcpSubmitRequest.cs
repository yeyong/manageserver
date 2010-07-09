using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.icp.submit
    /// </summary>
    public class IcpSubmitRequest : INTWRequest
    {
        public string CompanyAddress { get; set; }
        public string CompanyCertNo { get; set; }
        public Nullable<int> CompanyCertType { get; set; }
        public Nullable<int> CompanyCity { get; set; }
        public Nullable<int> CompanyDistrict { get; set; }
        public Nullable<int> CompanyKind { get; set; }
        public string CompanyMasterCertNo { get; set; }
        public Nullable<int> CompanyMasterCertType { get; set; }
        public string CompanyMasterEmail { get; set; }
        public string CompanyMasterMobile { get; set; }
        public string CompanyMasterName { get; set; }
        public string CompanyMasterPhone { get; set; }
        public string CompanyMasterUnicom { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> CompanyState { get; set; }
        public string CompanySuperior { get; set; }
        public string SiteDomain { get; set; }
        public string SiteHomePage { get; set; }
        public string SiteIp { get; set; }
        public string SiteMasterCertNo { get; set; }
        public Nullable<int> SiteMasterCertType { get; set; }
        public string SiteMasterEmail { get; set; }
        public string SiteMasterMobile { get; set; }
        public string SiteMasterName { get; set; }
        public string SiteMasterPhone { get; set; }
        public string SiteMasterUnicom { get; set; }
        public string SiteName { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.icp.submit";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("company_address", this.CompanyAddress);
            parameters.Add("company_cert_no", this.CompanyCertNo);
            parameters.Add("company_cert_type", this.CompanyCertType);
            parameters.Add("company_city", this.CompanyCity);
            parameters.Add("company_district", this.CompanyDistrict);
            parameters.Add("company_kind", this.CompanyKind);
            parameters.Add("company_master_cert_no", this.CompanyMasterCertNo);
            parameters.Add("company_master_cert_type", this.CompanyMasterCertType);
            parameters.Add("company_master_email", this.CompanyMasterEmail);
            parameters.Add("company_master_mobile", this.CompanyMasterMobile);
            parameters.Add("company_master_name", this.CompanyMasterName);
            parameters.Add("company_master_phone", this.CompanyMasterPhone);
            parameters.Add("company_master_unicom", this.CompanyMasterUnicom);
            parameters.Add("company_name", this.CompanyName);
            parameters.Add("company_state", this.CompanyState);
            parameters.Add("company_superior", this.CompanySuperior);
            parameters.Add("site_domain", this.SiteDomain);
            parameters.Add("site_home_page", this.SiteHomePage);
            parameters.Add("site_ip", this.SiteIp);
            parameters.Add("site_master_cert_no", this.SiteMasterCertNo);
            parameters.Add("site_master_cert_type", this.SiteMasterCertType);
            parameters.Add("site_master_email", this.SiteMasterEmail);
            parameters.Add("site_master_mobile", this.SiteMasterMobile);
            parameters.Add("site_master_name", this.SiteMasterName);
            parameters.Add("site_master_phone", this.SiteMasterPhone);
            parameters.Add("site_master_unicom", this.SiteMasterUnicom);
            parameters.Add("site_name", this.SiteName);
            return parameters;
        }

        #endregion
    }
}
