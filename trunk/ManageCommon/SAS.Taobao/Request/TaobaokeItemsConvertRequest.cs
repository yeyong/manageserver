using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.items.convert
    /// </summary>
    public class TaobaokeItemsConvertRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Iids { get; set; }
        public string Nick { get; set; }
        public string OuterCode { get; set; }
        public string NumIids { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.items.convert";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("iids", this.Iids);
            parameters.Add("nick", this.Nick);
            parameters.Add("outer_code", this.OuterCode);
            parameters.Add("num_iids", this.NumIids);
            return parameters;
        }

        #endregion
    }
}
