using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.trade.snapshot.get
    /// </summary>
    public class TradeSnapshotGetRequest : INTWRequest
    {
        public string Fields { get; set; }
        public Nullable<long> Tid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.trade.snapshot.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("tid", this.Tid);
            return parameters;
        }

        #endregion
    }
}
