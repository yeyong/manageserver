using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.sku.get
    /// </summary>
    public class ItemSkuGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Nick { get; set; }
        public Nullable<long> NumIid { get; set; }
        public Nullable<long> SkuId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.sku.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nick", this.Nick);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("sku_id", this.SkuId);
            return parameters;
        }

        #endregion
    }
}
