using System;
using System.Data;
using System.Text;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 名片配置数据操作
    /// </summary>
    public class CardConfigs
    {
        /// <summary>
        /// 名片配置文件模板ID更新操作
        /// </summary>
        /// <param name="templateIDList"></param>
        public static void UpdateCardConfigTemplateID(string templateIDList)
        {
            DatabaseProvider.GetInstance().UpdateCardConfigTemplateID(templateIDList);
        }
    }
}
