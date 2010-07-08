using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// ItemExtra Data Structure.
    /// </summary>
    [Serializable]
    public class ItemExtra : BaseObject
    {
        [XmlElement("approve_status")]
        public string ApproveStatus { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("delist_time")]
        public string DelistTime { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("eid")]
        public long Eid { get; set; }

        [XmlElement("feature")]
        public string Feature { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("item_num")]
        public int ItemNum { get; set; }

        [XmlElement("item_options")]
        public long ItemOptions { get; set; }

        [XmlElement("item_pic_url")]
        public string ItemPicUrl { get; set; }

        [XmlElement("list_time")]
        public string ListTime { get; set; }

        [XmlElement("memo")]
        public string Memo { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("options")]
        public long Options { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("reserve_price")]
        public string ReservePrice { get; set; }

        [XmlElement("seller_cids")]
        public string SellerCids { get; set; }

        [XmlElement("shop_id")]
        public long ShopId { get; set; }

        [XmlArray("skus")]
        [XmlArrayItem("sku")]
        public List<Sku> Skus { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}
