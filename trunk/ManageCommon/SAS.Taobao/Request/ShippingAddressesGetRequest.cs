using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.shipping.addresses.get
    /// </summary>
    public class ShippingAddressesGetRequest : INTWRequest
    {
        public string Fields { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.shipping.addresses.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            return parameters;
        }

        #endregion
    }
}
