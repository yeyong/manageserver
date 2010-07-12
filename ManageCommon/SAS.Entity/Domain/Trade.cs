using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Trade Data Structure.
    /// </summary>
    [Serializable]
    public class Trade : BaseObject
    {
        [XmlElement("adjust_fee")]
        public string AdjustFee { get; set; }

        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("available_confirm_fee")]
        public string AvailableConfirmFee { get; set; }

        [XmlElement("buyer_alipay_no")]
        public string BuyerAlipayNo { get; set; }

        [XmlElement("buyer_email")]
        public string BuyerEmail { get; set; }

        [XmlElement("buyer_flag")]
        public int BuyerFlag { get; set; }

        [XmlElement("buyer_memo")]
        public string BuyerMemo { get; set; }

        [XmlElement("buyer_message")]
        public string BuyerMessage { get; set; }

        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        [XmlElement("buyer_obtain_point_fee")]
        public int BuyerObtainPointFee { get; set; }

        [XmlElement("buyer_rate")]
        public bool BuyerRate { get; set; }

        [XmlElement("cod_fee")]
        public string CodFee { get; set; }

        [XmlElement("cod_status")]
        public string CodStatus { get; set; }

        [XmlElement("commission_fee")]
        public string CommissionFee { get; set; }

        [XmlElement("consign_time")]
        public string ConsignTime { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("discount_fee")]
        public string DiscountFee { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("has_post_fee")]
        public bool HasPostFee { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("is_3D")]
        public bool Is3D { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num")]
        public int Num { get; set; }

        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public List<Order> Orders { get; set; }

        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        [XmlElement("payment")]
        public string Payment { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("point_fee")]
        public int PointFee { get; set; }

        [XmlElement("post_fee")]
        public string PostFee { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("real_point_fee")]
        public int RealPointFee { get; set; }

        [XmlElement("received_payment")]
        public string ReceivedPayment { get; set; }

        [XmlElement("receiver_address")]
        public string ReceiverAddress { get; set; }

        [XmlElement("receiver_city")]
        public string ReceiverCity { get; set; }

        [XmlElement("receiver_district")]
        public string ReceiverDistrict { get; set; }

        [XmlElement("receiver_mobile")]
        public string ReceiverMobile { get; set; }

        [XmlElement("receiver_name")]
        public string ReceiverName { get; set; }

        [XmlElement("receiver_phone")]
        public string ReceiverPhone { get; set; }

        [XmlElement("receiver_state")]
        public string ReceiverState { get; set; }

        [XmlElement("receiver_zip")]
        public string ReceiverZip { get; set; }

        [XmlElement("seller_alipay_no")]
        public string SellerAlipayNo { get; set; }

        [XmlElement("seller_email")]
        public string SellerEmail { get; set; }

        [XmlElement("seller_flag")]
        public int SellerFlag { get; set; }

        [XmlElement("seller_memo")]
        public string SellerMemo { get; set; }

        [XmlElement("seller_mobile")]
        public string SellerMobile { get; set; }

        [XmlElement("seller_name")]
        public string SellerName { get; set; }

        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        [XmlElement("seller_phone")]
        public string SellerPhone { get; set; }

        [XmlElement("seller_rate")]
        public bool SellerRate { get; set; }

        [XmlElement("shipping_type")]
        public string ShippingType { get; set; }

        [XmlElement("sid")]
        public string Sid { get; set; }

        [XmlElement("snapshot")]
        public string Snapshot { get; set; }

        [XmlElement("snapshot_url")]
        public string SnapshotUrl { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("tid")]
        public long Tid { get; set; }

        [XmlElement("timeout_action_time")]
        public string TimeoutActionTime { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("total_fee")]
        public string TotalFee { get; set; }

        [XmlElement("trade_memo")]
        public string TradeMemo { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}
