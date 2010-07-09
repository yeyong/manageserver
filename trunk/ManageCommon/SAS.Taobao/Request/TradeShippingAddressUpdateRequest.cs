using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.trade.shippingaddress.update
    /// </summary>
    public class TradeShippingaddressUpdateRequest : INTWRequest
    {
        public string ReceiverAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverDistrict { get; set; }
        public string ReceiverMobile { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverZip { get; set; }
        public Nullable<long> Tid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.trade.shippingaddress.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("receiver_address", this.ReceiverAddress);
            parameters.Add("receiver_city", this.ReceiverCity);
            parameters.Add("receiver_district", this.ReceiverDistrict);
            parameters.Add("receiver_mobile", this.ReceiverMobile);
            parameters.Add("receiver_name", this.ReceiverName);
            parameters.Add("receiver_phone", this.ReceiverPhone);
            parameters.Add("receiver_state", this.ReceiverState);
            parameters.Add("receiver_zip", this.ReceiverZip);
            parameters.Add("tid", this.Tid);
            return parameters;
        }

        #endregion
    }
}
