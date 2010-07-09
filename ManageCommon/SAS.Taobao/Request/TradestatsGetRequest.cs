using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.tradestats.get
    /// </summary>
    public class TradestatsGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Nick { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.tradestats.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nick", this.Nick);
            return parameters;
        }

        #endregion
    }
}
