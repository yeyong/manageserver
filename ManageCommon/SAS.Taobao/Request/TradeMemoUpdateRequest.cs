using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.trade.memo.update
    /// </summary>
    public class TradeMemoUpdateRequest : INTWRequest
    {
        public Nullable<int> Flag { get; set; }
        public string Memo { get; set; }
        public Nullable<long> Tid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.trade.memo.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("flag", this.Flag);
            parameters.Add("memo", this.Memo);
            parameters.Add("tid", this.Tid);
            return parameters;
        }

        #endregion
    }
}
