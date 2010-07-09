using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.video.delete
    /// </summary>
    public class ItemVideoDeleteRequest : INTWRequest
    {
        public Nullable<long> Id { get; set; }
        public string Iid { get; set; }
        public string Lang { get; set; }
        public Nullable<long> NumIid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.video.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("iid", this.Iid);
            parameters.Add("lang", this.Lang);
            parameters.Add("num_iid", this.NumIid);
            return parameters;
        }

        #endregion
    }
}
