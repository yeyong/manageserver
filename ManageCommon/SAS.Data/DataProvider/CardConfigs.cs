using System;
using System.Data;
using System.Text;

using SAS.Entity;
using SAS.Common;
using SAS.Common.Generic;

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
        /// 获取名片配置列表
        /// </summary>
        /// <returns></returns>
        public static List<CardConfigInfo> GetCardConfigList()
        {
            List<CardConfigInfo> info = new List<CardConfigInfo>();
            IDataReader reader = GetCardConfigData();
            while (reader.Read())
            {
                CardConfigInfo cci = new CardConfigInfo();
                cci.id = TypeConverter.ObjectToInt(reader["id"], 0);
                cci.ccname = reader["ccname"].ToString().Trim();
                cci.tid = TypeConverter.ObjectToInt(reader["tid"], 1);
                cci.hasflash = TypeConverter.ObjectToInt(reader["hasflash"], 0);
                cci.hasimage = TypeConverter.ObjectToInt(reader["hasimage"], 0);
                cci.hasjs = TypeConverter.ObjectToInt(reader["hasjs"], 0);
                cci.hassilverlight = TypeConverter.ObjectToInt(reader["hassilverlight"], 0);
                cci.showparams = reader["showparams"].ToString().Trim();
                cci.createdate = reader["createdate"].ToString();
                cci.vailddate = reader["vailddate"].ToString();
                info.Add(cci);
            }
            reader.Close();
            return info;
        }

        /// <summary>
        /// 修改名片配置信息
        /// </summary>
        public static void UpdateCardConfig(CardConfigInfo cci)
        {
            DatabaseProvider.GetInstance().UpdateCardConfig(cci);
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
