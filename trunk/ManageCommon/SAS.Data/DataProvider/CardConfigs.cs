using System;
using System.Data;
using System.Text;

using SAS.Entity;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 名片配置数据操作
    /// </summary>
    public class CardConfigs
    {
        /// <summary>
        /// 删除名片配置信息
        /// </summary>
        /// <param name="cardconfigid"></param>
        public static void DeleteCardConfig(int cardconfigid)
        {
            DatabaseProvider.GetInstance().DeleteCardConfig(cardconfigid);
        }

        /// <summary>
        /// 增加企业名片配置信息
        /// </summary>
        /// <param name="cci"></param>
        public static void InsertCardConfig(CardConfigInfo cci)
        {
            DatabaseProvider.GetInstance().InsertCardConfig(cci);
        }

        /// <summary>
        /// 获取名片配置文件列表
        /// </summary>
        /// <returns></returns>
        public static IDataReader GetCardConfigData()
        {
            return DatabaseProvider.GetInstance().GetCardConfigData();
        }

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
