using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.icp.id.submit
    /// </summary>
    public class IcpIdSubmitRequest : INTWRequest
    {
        public string IcpId { get; set; }
        public string IcpState { get; set; }
        public string SiteDomain { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.icp.id.submit";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("icp_id", this.IcpId);
            parameters.Add("icp_state", this.IcpState);
            parameters.Add("site_domain", this.SiteDomain);
            return parameters;
        }

        #endregion
    }
}
