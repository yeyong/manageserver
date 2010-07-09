using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.shop.update
    /// </summary>
    public class ShopUpdateRequest : INTWRequest
    {
        public string Bulletin { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.shop.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("bulletin", this.Bulletin);
            parameters.Add("desc", this.Desc);
            parameters.Add("title", this.Title);
            return parameters;
        }

        #endregion
    }
}
