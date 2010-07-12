using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// TaobaokeItem Data Structure.
    /// </summary>
    [Serializable]
    public class TaobaokeItem : BaseObject
    {
        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        [XmlElement("commission")]
        public string Commission { get; set; }

        [XmlElement("commission_num")]
        public string CommissionNum { get; set; }

        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        [XmlElement("commission_volume")]
        public string CommissionVolume { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("item_location")]
        public string ItemLocation { get; set; }

        [XmlElement("keyword_click_url")]
        public string KeywordClickUrl { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("seller_credit_score")]
        public long SellerCreditScore { get; set; }

        [XmlElement("shop_click_url")]
        public string ShopClickUrl { get; set; }

        [XmlElement("taobaoke_cat_click_url")]
        public string TaobaokeCatClickUrl { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("volume")]
        public long Volume { get; set; }
    }
}
