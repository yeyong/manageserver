using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Refund Data Structure.
    /// </summary>
    [Serializable]
    public class Refund : BaseObject
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("company_name")]
        public string CompanyName { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("good_return_time")]
        public string GoodReturnTime { get; set; }

        [XmlElement("good_status")]
        public string GoodStatus { get; set; }

        [XmlElement("has_good_return")]
        public bool HasGoodReturn { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num")]
        public int Num { get; set; }

        [XmlElement("oid")]
        public long Oid { get; set; }

        [XmlElement("order_status")]
        public string OrderStatus { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("reason")]
        public string Reason { get; set; }

        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        [XmlElement("refund_id")]
        public long RefundId { get; set; }

        [XmlElement("refund_remind_timeout")]
        public RefundRemindTimeout RefundRemindTimeout { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("shipping_type")]
        public string ShippingType { get; set; }

        [XmlElement("sid")]
        public string Sid { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }
    }
}
