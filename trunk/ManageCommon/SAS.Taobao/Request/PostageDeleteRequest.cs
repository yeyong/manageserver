using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.postage.delete
    /// </summary>
    public class PostageDeleteRequest : INTWRequest
    {
        public Nullable<long> PostageId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.postage.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("postage_id", this.PostageId);
            return parameters;
        }

        #endregion
    }
}
