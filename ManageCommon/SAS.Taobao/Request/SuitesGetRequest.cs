using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.suites.get
    /// </summary>
    public class SuitesGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string ServiceCode { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.suites.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("service_code", this.ServiceCode);
            return parameters;
        }

        #endregion
    }
}
