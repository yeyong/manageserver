using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// TaobaokeReport Data Structure.
    /// </summary>
    [Serializable]
    public class TaobaokeReport : BaseObject
    {
        [XmlArray("taobaoke_report_members")]
        [XmlArrayItem("taobaoke_report_member")]
        public List<TaobaokeReportMember> TaobaokeReportMembers { get; set; }
    }
}
