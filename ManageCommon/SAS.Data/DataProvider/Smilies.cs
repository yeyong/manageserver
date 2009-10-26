using System;
using System.Text;
using System.Data;

using SAS.Common;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 表情符数据操作类
    /// </summary>
    public class Smilies
    {
        /// <summary>
        /// 得到表情符数据,包括表情分类
        /// </summary>
        /// <returns>表情符表</returns>
        public static DataTable GetSmiliesListDataTable()
        {
            return DatabaseProvider.GetInstance().GetSmiliesListDataTable();
        }

        /// <summary>
        /// 获得表情分类列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSmilieTypes()
        {
            return DatabaseProvider.GetInstance().GetSmilieTypes();
        }
    }
}
