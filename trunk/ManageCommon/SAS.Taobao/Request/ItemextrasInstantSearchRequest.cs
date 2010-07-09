using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.itemextras.instant.search
    /// </summary>
    public class ItemextrasInstantSearchRequest : INTWRequest
    {
        public string ApproveStatus { get; set; }
        public string EndPrice { get; set; }
        public string Fields { get; set; }
        public string NumIids { get; set; }
        public Nullable<long> Options { get; set; }
        public string OrderBy { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string Q { get; set; }
        public string SellerCids { get; set; }
        public Nullable<long> ShopId { get; set; }
        public string StartPrice { get; set; }
        public string Type { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.itemextras.instant.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("approve_status", this.ApproveStatus);
            parameters.Add("end_price", this.EndPrice);
            parameters.Add("fields", this.Fields);
            parameters.Add("num_iids", this.NumIids);
            parameters.Add("options", this.Options);
            parameters.Add("order_by", this.OrderBy);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("q", this.Q);
            parameters.Add("seller_cids", this.SellerCids);
            parameters.Add("shop_id", this.ShopId);
            parameters.Add("start_price", this.StartPrice);
            parameters.Add("type", this.Type);
            return parameters;
        }

        #endregion
    }
}
