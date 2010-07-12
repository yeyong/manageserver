using System;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// Picture Data Structure.
    /// </summary>
    [Serializable]
    public class Picture : BaseObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("deleted")]
        public string Deleted { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("picture_category_id")]
        public long PictureCategoryId { get; set; }

        [XmlElement("picture_id")]
        public long PictureId { get; set; }

        [XmlElement("picture_path")]
        public string PicturePath { get; set; }

        [XmlElement("pixel")]
        public string Pixel { get; set; }

        [XmlElement("sizes")]
        public long Sizes { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}
