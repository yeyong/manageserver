using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.shop.remainshowcase.get
    /// </summary>
    public class ShopRemainshowcaseGetRequest : INTWRequest
    {
        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.shop.remainshowcase.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            return parameters;
        }

        #endregion
    }
}
