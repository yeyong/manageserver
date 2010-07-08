using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// TaobaokeShop Data Structure.
    /// </summary>
    [Serializable]
    public class TaobaokeShop : BaseObject
    {
        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        [XmlElement("commission_rate")]
        public string CommissionRate { get; set; }

        [XmlElement("shop_title")]
        public string ShopTitle { get; set; }

        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
