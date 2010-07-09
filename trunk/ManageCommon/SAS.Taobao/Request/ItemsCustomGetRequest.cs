using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.items.custom.get
    /// </summary>
    public class ItemsCustomGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string OuterId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.items.custom.get";
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
