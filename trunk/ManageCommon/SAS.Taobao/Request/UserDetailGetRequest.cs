using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.user.detail.get
    /// </summary>
    public class UserDetailGetRequest : INTWRequest
    {
        public string AlipayNo { get; set; }
        public string Fields { get; set; }
        public string Nick { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.user.detail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("alipay_no", this.AlipayNo);
            parameters.Add("fields", this.Fields);
            parameters.Add("nick", this.Nick);
            return parameters;
        }

        #endregion
    }
}
