using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.update.listing
    /// </summary>
    public class ItemUpdateListingRequest : INTWRequest
    {
        public string Iid { get; set; }
        public Nullable<int> Num { get; set; }
        public Nullable<long> NumIid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.update.listing";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("iid", this.Iid);
            parameters.Add("num", this.Num);
            parameters.Add("num_iid", this.NumIid);
            return parameters;
        }

        #endregion
    }
}
