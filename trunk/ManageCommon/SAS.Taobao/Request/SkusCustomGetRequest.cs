using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.skus.custom.get
    /// </summary>
    public class SkusCustomGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string OuterId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.skus.custom.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("outer_id", this.OuterId);
            return parameters;
        }

        #endregion
    }
}
