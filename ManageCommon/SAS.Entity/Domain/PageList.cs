using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SAS.Entity.Domain
{
    /// <summary>
    /// TOP响应列表
    /// </summary>
    /// <typeparam name="T">任何一种可序列化的领域对象</typeparam>
    [Serializable]
    public class PageList<T>
    {
        /// <summary>
        /// 所有记录数，主要用于分页显示。
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        /// <summary>
        /// 解释后的具体对象。
        /// </summary>
        public List<T> Content { get; set; }

        /// <summary>
        /// 取得响应列表中的第一个对象，如果没有返回空引用。
        /// </summary>
        public T FirstResult
        {
            get
            {
                if (Content != null && Content.Count > 0)
                {
                    return Content[0];
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
