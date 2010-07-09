using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.items.all.get
    /// </summary>
    public class ItemsAllGetRequest : INTWRequest
    {
        public Nullable<long> Cid { get; set; }
        public string Fields { get; set; }
        public string OrderBy { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string Q { get; set; }
        public string SellerCids { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.items.all.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("fields", this.Fields);
            parameters.Add("order_by", this.OrderBy);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("q", this.Q);
            parameters.Add("seller_cids", this.SellerCids);
            return parameters;
        }

        #endregion
    }
}
