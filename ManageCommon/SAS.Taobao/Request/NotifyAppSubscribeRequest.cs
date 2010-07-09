using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.notify.app.subscribe
    /// </summary>
    public class NotifyAppSubscribeRequest : INTWRequest
    {
        public Nullable<int> Duration { get; set; }
        public string Status { get; set; }
        public string Topics { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.notify.app.subscribe";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("duration", this.Duration);
            parameters.Add("status", this.Status);
            parameters.Add("topics", this.Topics);
            return parameters;
        }

        #endregion
    }
}
