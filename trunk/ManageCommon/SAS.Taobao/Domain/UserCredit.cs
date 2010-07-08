using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// UserCredit Data Structure.
    /// </summary>
    [Serializable]
    public class UserCredit : BaseObject
    {
        [XmlElement("good_num")]
        public int GoodNum { get; set; }

        [XmlElement("level")]
        public int Level { get; set; }

        [XmlElement("score")]
        public int Score { get; set; }

        [XmlElement("total_num")]
        public int TotalNum { get; set; }
    }
}
