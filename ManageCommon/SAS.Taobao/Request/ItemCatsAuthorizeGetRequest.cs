using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.itemcats.authorize.get
    /// </summary>
    public class ItemcatsAuthorizeGetRequest : INTWRequest
    {
        public string Fields { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.itemcats.authorize.get";
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
