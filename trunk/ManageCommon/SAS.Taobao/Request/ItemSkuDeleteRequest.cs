using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.sku.delete
    /// </summary>
    public class ItemSkuDeleteRequest : INTWRequest
    {
        public string Iid { get; set; }
        public Nullable<int> ItemNum { get; set; }
        public string ItemPrice { get; set; }
        public string Lang { get; set; }
        public Nullable<long> NumIid { get; set; }
        public string Properties { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.sku.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("iid", this.Iid);
            parameters.Add("item_num", this.ItemNum);
            parameters.Add("item_price", this.ItemPrice);
            parameters.Add("lang", this.Lang);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("properties", this.Properties);
            return parameters;
        }

        #endregion
    }
}
