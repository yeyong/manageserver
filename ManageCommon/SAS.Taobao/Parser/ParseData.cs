using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Taobao.Parser
{
    /// <summary>
    /// 解释数据。
    /// </summary>
    public class ParseData
    {
        /// <summary>
        /// API名称
        /// </summary>
        public string Api { get; set; }

        /// <summary>
        /// 列表名称
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// 元素名称
        /// </summary>
        public string ItemName { get; set; }

        public ParseData(string api, string itemName)
        {
            this.Api = api;
            this.ItemName = itemName;
        }

        public ParseData(string api, string listName, string itemName)
        {
            this.Api = api;
            this.ListName = listName;
            this.ItemName = itemName;
        }
    }
}
