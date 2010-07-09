using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.items.get
    /// </summary>
    public class TaobaokeItemsGetRequest : INTWRequest
    {
        public string Area { get; set; }
        public string AutoSend { get; set; }
        public Nullable<int> Cid { get; set; }
        public string EndCommissionNum { get; set; }
        public string EndCommissionRate { get; set; }
        public string EndCredit { get; set; }
        public string EndPrice { get; set; }
        public string Fields { get; set; }
        public string Guarantee { get; set; }
        public string Keyword { get; set; }
        public string Nick { get; set; }
        public string OuterCode { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public string Pid { get; set; }
        public string Sort { get; set; }
        public string StartCommissionNum { get; set; }
        public string StartCommissionRate { get; set; }
        public string StartCredit { get; set; }
        public string StartPrice { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("area", this.Area);
            parameters.Add("auto_send", this.AutoSend);
            parameters.Add("cid", this.Cid);
            parameters.Add("end_commissionNum", this.EndCommissionNum);
            parameters.Add("end_commissionRate", this.EndCommissionRate);
            parameters.Add("end_credit", this.EndCredit);
            parameters.Add("end_price", this.EndPrice);
            parameters.Add("fields", this.Fields);
            parameters.Add("guarantee", this.Guarantee);
            parameters.Add("keyword", this.Keyword);
            parameters.Add("nick", this.Nick);
            parameters.Add("outer_code", this.OuterCode);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("pid", this.Pid);
            parameters.Add("sort", this.Sort);
            parameters.Add("start_commissionNum", this.StartCommissionNum);
            parameters.Add("start_commissionRate", this.StartCommissionRate);
            parameters.Add("start_credit", this.StartCredit);
            parameters.Add("start_price", this.StartPrice);
            return parameters;
        }

        #endregion
    }
}
