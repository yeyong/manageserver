using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.notify.subscribemessage.get
    /// </summary>
    public class NotifySubscribemessageGetRequest : INTWRequest
    {
        public string Fields { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.notify.subscribemessage.get";
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
