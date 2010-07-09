using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.get
    /// </summary>
    public class ItemGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Iid { get; set; }
        public string Nick { get; set; }
        public Nullable<long> NumIid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("iid", this.Iid);
            parameters.Add("nick", this.Nick);
            parameters.Add("num_iid", this.NumIid);
            return parameters;
        }

        #endregion
    }
}
