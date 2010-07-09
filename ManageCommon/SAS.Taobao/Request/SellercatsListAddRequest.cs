using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.sellercats.list.add
    /// </summary>
    public class SellercatsListAddRequest : INTWRequest
    {
        public string Name { get; set; }
        public Nullable<int> ParentCid { get; set; }
        public string PictUrl { get; set; }
        public Nullable<int> SortOrder { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.sellercats.list.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("name", this.Name);
            parameters.Add("parent_cid", this.ParentCid);
            parameters.Add("pict_url", this.PictUrl);
            parameters.Add("sort_order", this.SortOrder);
            return parameters;
        }

        #endregion
    }
}
