using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.items.detail.get
    /// </summary>
    public class TaobaokeItemsDetailGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Nick { get; set; }
        public string NumIids { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.items.detail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nick", this.Nick);
            parameters.Add("num_iids", this.NumIids);
            return parameters;
        }

        #endregion
    }
}
