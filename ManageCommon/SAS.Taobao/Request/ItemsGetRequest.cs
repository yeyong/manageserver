using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.items.get
    /// </summary>
    public class ItemsGetRequest : INTWRequest
    {
        public Nullable<long> Cid { get; set; }
        public Nullable<int> EndPrice { get; set; }
        public Nullable<int> EndScore { get; set; }
        public Nullable<int> EndVolume { get; set; }
        public string Fields { get; set; }
        public Nullable<bool> GenuineSecurity { get; set; }
        public Nullable<bool> Is3D { get; set; }
        public Nullable<bool> IsCod { get; set; }
        public Nullable<bool> IsMall { get; set; }
        public Nullable<bool> IsPrepay { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string Nicks { get; set; }
        public Nullable<bool> OneStation { get; set; }
        public string OrderBy { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<bool> PostFree { get; set; }
        public Nullable<long> ProductId { get; set; }
        public string PromotedService { get; set; }
        public string Props { get; set; }
        public string Q { get; set; }
        public Nullable<int> StartPrice { get; set; }
        public Nullable<int> StartScore { get; set; }
        public Nullable<int> StartVolume { get; set; }
        public string StuffStatus { get; set; }
        public Nullable<bool> WwStatus { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.items.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("end_price", this.EndPrice);
            parameters.Add("end_score", this.EndScore);
            parameters.Add("end_volume", this.EndVolume);
            parameters.Add("fields", this.Fields);
            parameters.Add("genuine_security", this.GenuineSecurity);
            parameters.Add("is_3D", this.Is3D);
            parameters.Add("is_cod", this.IsCod);
            parameters.Add("is_mall", this.IsMall);
            parameters.Add("is_prepay", this.IsPrepay);
            parameters.Add("location.city", this.LocationCity);
            parameters.Add("location.state", this.LocationState);
            parameters.Add("nicks", this.Nicks);
            parameters.Add("one_station", this.OneStation);
            parameters.Add("order_by", this.OrderBy);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("post_free", this.PostFree);
            parameters.Add("product_id", this.ProductId);
            parameters.Add("promoted_service", this.PromotedService);
            parameters.Add("props", this.Props);
            parameters.Add("q", this.Q);
            parameters.Add("start_price", this.StartPrice);
            parameters.Add("start_score", this.StartScore);
            parameters.Add("start_volume", this.StartVolume);
            parameters.Add("stuff_status", this.StuffStatus);
            parameters.Add("ww_status", this.WwStatus);
            return parameters;
        }

        #endregion
    }
}
