using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.itemcats.get
    /// </summary>
    public class ItemcatsGetRequest : INTWRequest
    {
        public string Cids { get; set; }
        public Nullable<DateTime> Datetime { get; set; }
        public string Fields { get; set; }
        public Nullable<long> ParentCid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.itemcats.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("cids", this.Cids);
            parameters.Add("datetime", this.Datetime);
            parameters.Add("fields", this.Fields);
            parameters.Add("parent_cid", this.ParentCid);
            return parameters;
        }

        #endregion
    }
}
