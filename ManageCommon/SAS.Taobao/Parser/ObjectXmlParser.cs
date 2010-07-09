using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using SAS.Taobao.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Parser
{
    /// <summary>
    /// TOP XML对象响应通用解释器。
    /// </summary>
    public class ObjectXmlParser<T> : INTWParser<T>
    {
        private ParseData parseData;

        public ObjectXmlParser(ParseData parseData)
        {
            this.parseData = parseData;
        }

        #region INTWParser<T> Members

        public T Parse(string body)
        {
            XmlAttributes rootAttrs = new XmlAttributes();
            rootAttrs.XmlRoot = new XmlRootAttribute(NTWUtil.GetRootElement(parseData.Api));

            XmlAttributes objAttrs = new XmlAttributes();
            objAttrs.XmlElements.Add(new XmlElementAttribute(parseData.ItemName, typeof(T)));

            XmlAttributeOverrides attrOvrs = new XmlAttributeOverrides();
            attrOvrs.Add(typeof(PageList<T>), rootAttrs);
            attrOvrs.Add(typeof(PageList<T>), "Content", objAttrs);

            XmlSerializer serializer = new XmlSerializer(typeof(PageList<T>), attrOvrs);
            object obj = serializer.Deserialize(new StringReader(body));
            return (obj as PageList<T>).FirstResult;
        }

        #endregion
    }
}
