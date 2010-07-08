using System;
using System.Xml.Serialization;

namespace SAS.Taobao.Domain
{
    /// <summary>
    /// PictureCategory Data Structure.
    /// </summary>
    [Serializable]
    public class PictureCategory : BaseObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("picture_category_id")]
        public long PictureCategoryId { get; set; }

        [XmlElement("picture_category_name")]
        public string PictureCategoryName { get; set; }

        [XmlElement("position")]
        public long Position { get; set; }

        [XmlElement("total")]
        public long Total { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}
