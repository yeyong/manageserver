using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.logistics.orders.detail.get
    /// </summary>
    public class LogisticsOrdersDetailGetRequest : INTWRequest
    {
        public string BuyerNick { get; set; }
        public Nullable<DateTime> EndCreated { get; set; }
        public string Fields { get; set; }
        public string FreightPayer { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string ReceiverName { get; set; }
        public string SellerConfirm { get; set; }
        public Nullable<DateTime> StartCreated { get; set; }
        public string Status { get; set; }
        public Nullable<long> Tid { get; set; }
        public string Type { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.logistics.orders.detail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("buyer_nick", this.BuyerNick);
            parameters.Add("end_created", this.EndCreated);
            parameters.Add("fields", this.Fields);
            parameters.Add("freight_payer", this.FreightPayer);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("receiver_name", this.ReceiverName);
            parameters.Add("seller_confirm", this.SellerConfirm);
            parameters.Add("start_created", this.StartCreated);
            parameters.Add("status", this.Status);
            parameters.Add("tid", this.Tid);
            parameters.Add("type", this.Type);
            return parameters;
        }

        #endregion
    }
}
