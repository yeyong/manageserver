using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.notify.user.authorize
    /// </summary>
    public class NotifyUserAuthorizeRequest : INTWRequest
    {
        public Nullable<int> Duration { get; set; }
        public string Email { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.notify.user.authorize";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("duration", this.Duration);
            parameters.Add("email", this.Email);
            return parameters;
        }

        #endregion
    }
}
