using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.skus.get
    /// </summary>
    public class ItemSkusGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Iids { get; set; }
        public string Nick { get; set; }
        public string NumIids { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.skus.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("iids", this.Iids);
            parameters.Add("nick", this.Nick);
            parameters.Add("num_iids", this.NumIids);
            return parameters;
        }

        #endregion
    }
}
