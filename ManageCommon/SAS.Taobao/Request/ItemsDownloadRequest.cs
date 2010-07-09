using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.items.download
    /// </summary>
    public class ItemsDownloadRequest : INTWRequest
    {
        public string ApproveStatus { get; set; }
        public Nullable<long> Cid { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string Q { get; set; }
        public string SellerCids { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.items.download";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("approve_status", this.ApproveStatus);
            parameters.Add("cid", this.Cid);
            parameters.Add("end_date", this.EndDate);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("q", this.Q);
            parameters.Add("seller_cids", this.SellerCids);
            parameters.Add("start_date", this.StartDate);
            return parameters;
        }

        #endregion
    }
}
