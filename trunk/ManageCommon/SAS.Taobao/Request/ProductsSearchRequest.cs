using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.products.search
    /// </summary>
    public class ProductsSearchRequest : INTWRequest
    {
        public Nullable<long> Cid { get; set; }
        public string Fields { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string Props { get; set; }
        public string Q { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.products.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("fields", this.Fields);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("props", this.Props);
            parameters.Add("q", this.Q);
            return parameters;
        }

        #endregion
    }
}
