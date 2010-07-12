using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using SAS.Entity.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Parser
{
    /// <summary>
    /// XML列表对象响应通用解释器。
    /// </summary>
    public class ListXmlParser<T> : INTWParser<PageList<T>>
    {
        private ParseData parseData;

        public ListXmlParser(ParseData parseData)
        {
            this.parseData = parseData;
        }

        #region INTWParser<PageList<T>> Members

        public PageList<T> Parse(string body)
        {
            XmlAttributes rootAttrs = new XmlAttributes();
            rootAttrs.XmlRoot = new XmlRootAttribute(NTWUtil.GetRootElement(parseData.Api));

            XmlAttributes listAttrs = new XmlAttributes();
            listAttrs.XmlArray = new XmlArrayAttribute(parseData.ListName);
            listAttrs.XmlArrayItems.Add(new XmlArrayItemAttribute(parseData.ItemName, typeof(T)));

            XmlAttributeOverrides attrOvrs = new XmlAttributeOverrides();
            attrOvrs.Add(typeof(PageList<T>), rootAttrs);
            attrOvrs.Add(typeof(PageList<T>), "Content", listAttrs);

            XmlSerializer serializer = new XmlSerializer(typeof(PageList<T>), attrOvrs);
            object obj = serializer.Deserialize(new StringReader(body));
            return obj as PageList<T>;
        }

        #endregion
    }
}
