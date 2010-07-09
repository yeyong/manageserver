using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.postage.get
    /// </summary>
    public class PostageGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Nick { get; set; }
        public Nullable<long> PostageId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.postage.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nick", this.Nick);
            parameters.Add("postage_id", this.PostageId);
            return parameters;
        }

        #endregion
    }
}
