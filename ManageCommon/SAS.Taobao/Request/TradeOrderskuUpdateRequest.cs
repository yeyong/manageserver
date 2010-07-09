using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.trade.ordersku.update
    /// </summary>
    public class TradeOrderskuUpdateRequest : INTWRequest
    {
        public Nullable<long> Oid { get; set; }
        public Nullable<long> SkuId { get; set; }
        public string SkuProps { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.trade.ordersku.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("oid", this.Oid);
            parameters.Add("sku_id", this.SkuId);
            parameters.Add("sku_props", this.SkuProps);
            return parameters;
        }

        #endregion
    }
}
