using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.items.list.get
    /// </summary>
    public class ItemsListGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Iids { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.items.list.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("iids", this.Iids);
            return parameters;
        }

        #endregion
    }
}
