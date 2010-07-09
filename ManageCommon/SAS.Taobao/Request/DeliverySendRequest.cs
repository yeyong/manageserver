using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.delivery.send
    /// </summary>
    public class DeliverySendRequest : INTWRequest
    {
        public string CompanyCode { get; set; }
        public string Fields { get; set; }
        public string Memo { get; set; }
        public string OrderType { get; set; }
        public string OutSid { get; set; }
        public string SellerAddress { get; set; }
        public string SellerAreaId { get; set; }
        public string SellerMobile { get; set; }
        public string SellerName { get; set; }
        public string SellerPhone { get; set; }
        public string SellerZip { get; set; }
        public Nullable<long> Tid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.delivery.send";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("company_code", this.CompanyCode);
            parameters.Add("fields", this.Fields);
            parameters.Add("memo", this.Memo);
            parameters.Add("order_type", this.OrderType);
            parameters.Add("out_sid", this.OutSid);
            parameters.Add("seller_address", this.SellerAddress);
            parameters.Add("seller_area_id", this.SellerAreaId);
            parameters.Add("seller_mobile", this.SellerMobile);
            parameters.Add("seller_name", this.SellerName);
            parameters.Add("seller_phone", this.SellerPhone);
            parameters.Add("seller_zip", this.SellerZip);
            parameters.Add("tid", this.Tid);
            return parameters;
        }

        #endregion
    }
}
