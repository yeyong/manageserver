using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.caturl.get
    /// </summary>
    public class TaobaokeCaturlGetRequest : INTWRequest
    {
        public Nullable<int> Cid { get; set; }
        public string Nick { get; set; }
        public string OuterCode { get; set; }
        public string Q { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.caturl.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("nick", this.Nick);
            parameters.Add("outer_code", this.OuterCode);
            parameters.Add("q", this.Q);
            return parameters;
        }

        #endregion
    }
}
