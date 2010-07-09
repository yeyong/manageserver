using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.itemcat.features.get
    /// </summary>
    public class ItemcatFeaturesGetRequest : INTWRequest
    {
        public string AttrKeys { get; set; }
        public Nullable<long> Cid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.itemcat.features.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("attr_keys", this.AttrKeys);
            parameters.Add("cid", this.Cid);
            return parameters;
        }

        #endregion
    }
}
