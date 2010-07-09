using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.sku.update
    /// </summary>
    public class ItemSkuUpdateRequest : INTWRequest
    {
        public string Iid { get; set; }
        public string ItemPrice { get; set; }
        public string Lang { get; set; }
        public Nullable<long> NumIid { get; set; }
        public string OuterId { get; set; }
        public string Price { get; set; }
        public string Properties { get; set; }
        public Nullable<long> Quantity { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.sku.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("iid", this.Iid);
            parameters.Add("item_price", this.ItemPrice);
            parameters.Add("lang", this.Lang);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("price", this.Price);
            parameters.Add("properties", this.Properties);
            parameters.Add("quantity", this.Quantity);
            return parameters;
        }

        #endregion
    }
}
