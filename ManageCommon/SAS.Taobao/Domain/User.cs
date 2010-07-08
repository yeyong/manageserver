using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// User Data Structure.
    /// </summary>
    [Serializable]
    public class User : BaseObject
    {
        [XmlElement("alipay_account")]
        public string AlipayAccount { get; set; }

        [XmlElement("alipay_bind")]
        public string AlipayBind { get; set; }

        [XmlElement("alipay_no")]
        public string AlipayNo { get; set; }

        [XmlElement("auto_repost")]
        public string AutoRepost { get; set; }

        [XmlElement("birthday")]
        public string Birthday { get; set; }

        [XmlElement("buyer_credit")]
        public UserCredit BuyerCredit { get; set; }

        [XmlElement("consumer_protection")]
        public bool ConsumerProtection { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("has_more_pic")]
        public bool HasMorePic { get; set; }

        [XmlElement("id_card")]
        public string IdCard { get; set; }

        [XmlElement("item_img_num")]
        public int ItemImgNum { get; set; }

        [XmlElement("item_img_size")]
        public int ItemImgSize { get; set; }

        [XmlElement("last_visit")]
        public string LastVisit { get; set; }

        [XmlElement("location")]
        public Location Location { get; set; }

        [XmlElement("mobile")]
        public string Mobile { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("promoted_type")]
        public string PromotedType { get; set; }

        [XmlElement("prop_img_num")]
        public int PropImgNum { get; set; }

        [XmlElement("prop_img_size")]
        public int PropImgSize { get; set; }

        [XmlElement("real_name")]
        public string RealName { get; set; }

        [XmlElement("seller_credit")]
        public UserCredit SellerCredit { get; set; }

        [XmlElement("sex")]
        public string Sex { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("uid")]
        public string Uid { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}
