using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.refund.get
    /// </summary>
    public class RefundGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public Nullable<long> RefundId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.refund.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("refund_id", this.RefundId);
            return parameters;
        }

        #endregion
    }
}
