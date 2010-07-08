using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// Order Data Structure.
    /// </summary>
    [Serializable]
    public class Order : BaseObject
    {
        [XmlElement("adjust_fee")]
        public string AdjustFee { get; set; }

        [XmlElement("buyer_rate")]
        public string BuyerRate { get; set; }

        [XmlElement("discount_fee")]
        public string DiscountFee { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("item_meal_id")]
        public long ItemMealId { get; set; }

        [XmlElement("item_meal_name")]
        public string ItemMealName { get; set; }

        [XmlElement("item_memo")]
        public string ItemMemo { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num")]
        public int Num { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("outer_iid")]
        public string OuterIid { get; set; }

        [XmlElement("outer_sku_id")]
        public string OuterSkuId { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }

        [XmlElement("refund_status")]
        public string RefundStatus { get; set; }

        [XmlElement("seller_rate")]
        public string SellerRate { get; set; }

        [XmlElement("seller_type")]
        public string SellerType { get; set; }

        [XmlElement("sku_id")]
        public string SkuId { get; set; }

        [XmlElement("sku_properties_name")]
        public string SkuPropertiesName { get; set; }

        [XmlElement("snapshot")]
        public string Snapshot { get; set; }

        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("timeout_action_time")]
        public string TimeoutActionTime { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}
