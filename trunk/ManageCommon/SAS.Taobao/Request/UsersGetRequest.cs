using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.users.get
    /// </summary>
    public class UsersGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public string Nicks { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.users.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("nicks", this.Nicks);
            return parameters;
        }

        #endregion
    }
}
