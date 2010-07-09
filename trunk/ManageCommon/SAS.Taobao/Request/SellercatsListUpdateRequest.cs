using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.sellercats.list.update
    /// </summary>
    public class SellercatsListUpdateRequest : INTWRequest
    {
        public Nullable<int> Cid { get; set; }
        public string Name { get; set; }
        public string PictUrl { get; set; }
        public Nullable<int> SortOrder { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.sellercats.list.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("name", this.Name);
            parameters.Add("pict_url", this.PictUrl);
            parameters.Add("sort_order", this.SortOrder);
            return parameters;
        }

        #endregion
    }
}
