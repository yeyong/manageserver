using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// Item Data Structure.
    /// </summary>
    [Serializable]
    public class Item : BaseObject
    {
        [XmlElement("approve_status")]
        public string ApproveStatus { get; set; }

        [XmlElement("auction_point")]
        public long AuctionPoint { get; set; }

        [XmlElement("auto_fill")]
        public string AutoFill { get; set; }

        [XmlElement("auto_repost")]
        public bool AutoRepost { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("delist_time")]
        public string DelistTime { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("detail_url")]
        public string DetailUrl { get; set; }

        [XmlElement("ems_fee")]
        public string EmsFee { get; set; }

        [XmlElement("express_fee")]
        public string ExpressFee { get; set; }

        [XmlElement("freight_payer")]
        public string FreightPayer { get; set; }

        [XmlElement("has_discount")]
        public bool HasDiscount { get; set; }

        [XmlElement("has_invoice")]
        public bool HasInvoice { get; set; }

        [XmlElement("has_showcase")]
        public bool HasShowcase { get; set; }

        [XmlElement("has_warranty")]
        public bool HasWarranty { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("increment")]
        public string Increment { get; set; }

        [XmlElement("input_pids")]
        public string InputPids { get; set; }

        [XmlElement("input_str")]
        public string InputStr { get; set; }

        [XmlElement("is_3D")]
        public bool Is3D { get; set; }

        [XmlElement("is_ex")]
        public bool IsEx { get; set; }

        [XmlElement("is_taobao")]
        public bool IsTaobao { get; set; }

        [XmlElement("is_timing")]
        public bool IsTiming { get; set; }

        [XmlElement("is_virtual")]
        public bool IsVirtual { get; set; }

        [XmlArray("item_imgs")]
        [XmlArrayItem("item_img")]
        public List<ItemImg> ItemImgs { get; set; }

        [XmlElement("list_time")]
        public string ListTime { get; set; }

        [XmlElement("location")]
        public Location Location { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num")]
        public int Num { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("one_station")]
        public bool OneStation { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        [XmlElement("postage_id")]
        public long PostageId { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlArray("prop_imgs")]
        [XmlArrayItem("prop_img")]
        public List<PropImg> PropImgs { get; set; }

        [XmlElement("property_alias")]
        public string PropertyAlias { get; set; }

        [XmlElement("props")]
        public string Props { get; set; }

        [XmlElement("props_name")]
        public string PropsName { get; set; }

        [XmlElement("score")]
        public long Score { get; set; }

        [XmlElement("second_kill")]
        public string SecondKill { get; set; }

        [XmlElement("seller_cids")]
        public string SellerCids { get; set; }

        [XmlArray("skus")]
        [XmlArrayItem("sku")]
        public List<Sku> Skus { get; set; }

        [XmlElement("stuff_status")]
        public string StuffStatus { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("valid_thru")]
        public int ValidThru { get; set; }

        [XmlArray("videos")]
        [XmlArrayItem("video")]
        public List<Video> Videos { get; set; }

        [XmlElement("volume")]
        public long Volume { get; set; }
    }
}
