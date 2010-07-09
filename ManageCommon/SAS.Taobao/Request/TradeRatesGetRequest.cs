using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.traderates.get
    /// </summary>
    public class TraderatesGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string RateType { get; set; }
        public string Result { get; set; }
        public string Role { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.traderates.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("rate_type", this.RateType);
            parameters.Add("result", this.Result);
            parameters.Add("role", this.Role);
            return parameters;
        }

        #endregion
    }
}
