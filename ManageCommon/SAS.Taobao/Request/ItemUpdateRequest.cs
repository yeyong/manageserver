using System;
using System.Collections.Generic;

using SAS.Taobao.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.update
    /// </summary>
    public class ItemUpdateRequest : INTWUploadRequest
    {
        public string ApproveStatus { get; set; }
        public Nullable<long> AuctionPoint { get; set; }
        public string AutoFill { get; set; }
        public Nullable<bool> AutoRepost { get; set; }
        public Nullable<long> Cid { get; set; }
        public string Desc { get; set; }
        public string EmsFee { get; set; }
        public string ExpressFee { get; set; }
        public string FreightPayer { get; set; }
        public Nullable<bool> HasDiscount { get; set; }
        public Nullable<bool> HasInvoice { get; set; }
        public Nullable<bool> HasShowcase { get; set; }
        public Nullable<bool> HasWarranty { get; set; }
        public string Iid { get; set; }
        public FileItem Image { get; set; }
        public string Increment { get; set; }
        public string InputPids { get; set; }
        public string InputStr { get; set; }
        public Nullable<bool> Is3D { get; set; }
        public Nullable<bool> IsEx { get; set; }
        public Nullable<bool> IsReplaceSku { get; set; }
        public Nullable<bool> IsTaobao { get; set; }
        public string Lang { get; set; }
        public Nullable<DateTime> ListTime { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public Nullable<int> Num { get; set; }
        public Nullable<long> NumIid { get; set; }
        public string OuterId { get; set; }
        public string PicPath { get; set; }
        public string PostFee { get; set; }
        public Nullable<long> PostageId { get; set; }
        public string Price { get; set; }
        public Nullable<long> ProductId { get; set; }
        public string PropertyAlias { get; set; }
        public string Props { get; set; }
        public string SellerCids { get; set; }
        public string SkuOuterIds { get; set; }
        public string SkuPrices { get; set; }
        public string SkuProperties { get; set; }
        public string SkuQuantities { get; set; }
        public string StuffStatus { get; set; }
        public string Title { get; set; }
        public Nullable<int> ValidThru { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("approve_status", this.ApproveStatus);
            parameters.Add("auction_point", this.AuctionPoint);
            parameters.Add("auto_fill", this.AutoFill);
            parameters.Add("auto_repost", this.AutoRepost);
            parameters.Add("cid", this.Cid);
            parameters.Add("desc", this.Desc);
            parameters.Add("ems_fee", this.EmsFee);
            parameters.Add("express_fee", this.ExpressFee);
            parameters.Add("freight_payer", this.FreightPayer);
            parameters.Add("has_discount", this.HasDiscount);
            parameters.Add("has_invoice", this.HasInvoice);
            parameters.Add("has_showcase", this.HasShowcase);
            parameters.Add("has_warranty", this.HasWarranty);
            parameters.Add("iid", this.Iid);
            parameters.Add("increment", this.Increment);
            parameters.Add("input_pids", this.InputPids);
            parameters.Add("input_str", this.InputStr);
            parameters.Add("is_3D", this.Is3D);
            parameters.Add("is_ex", this.IsEx);
            parameters.Add("is_replace_sku", this.IsReplaceSku);
            parameters.Add("is_taobao", this.IsTaobao);
            parameters.Add("lang", this.Lang);
            parameters.Add("list_time", this.ListTime);
            parameters.Add("location.city", this.LocationCity);
            parameters.Add("location.state", this.LocationState);
            parameters.Add("num", this.Num);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("pic_path", this.PicPath);
            parameters.Add("post_fee", this.PostFee);
            parameters.Add("postage_id", this.PostageId);
            parameters.Add("price", this.Price);
            parameters.Add("product_id", this.ProductId);
            parameters.Add("property_alias", this.PropertyAlias);
            parameters.Add("props", this.Props);
            parameters.Add("seller_cids", this.SellerCids);
            parameters.Add("sku_outer_ids", this.SkuOuterIds);
            parameters.Add("sku_prices", this.SkuPrices);
            parameters.Add("sku_properties", this.SkuProperties);
            parameters.Add("sku_quantities", this.SkuQuantities);
            parameters.Add("stuff_status", this.StuffStatus);
            parameters.Add("title", this.Title);
            parameters.Add("valid_thru", this.ValidThru);
            return parameters;
        }

        #endregion

        #region INTWUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("image", this.Image);
            return parameters;
        }

        #endregion
    }
}
