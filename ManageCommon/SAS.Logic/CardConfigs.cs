using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 名片配置文件操作
    /// </summary>
    public class CardConfigs
    {
        /// <summary>
        /// 添加名片配置
        /// </summary>
        /// <param name="cci"></param>
        public static void InsertCardConfig(CardConfigInfo cci)
        {
            SAS.Data.DataProvider.CardConfigs.InsertCardConfig(cci);
        }

        /// <summary>
        /// 获取名片配置列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCardConfigList()
        {
            DataTable cardconfiginfo = new DataTable();
            cardconfiginfo.Columns.Add("id", System.Type.GetType("System.Int32"));
            cardconfiginfo.Columns.Add("ccname", System.Type.GetType("System.String"));
            cardconfiginfo.Columns.Add("tid", System.Type.GetType("System.Int32"));
            cardconfiginfo.Columns.Add("hasflash", System.Type.GetType("System.Int16"));
            cardconfiginfo.Columns.Add("hasimage", System.Type.GetType("System.Int16"));
            cardconfiginfo.Columns.Add("hasjs", System.Type.GetType("System.Int16"));
            cardconfiginfo.Columns.Add("hassilverlight", System.Type.GetType("System.Int16"));
            cardconfiginfo.Columns.Add("showparams", System.Type.GetType("System.String"));
            cardconfiginfo.Columns.Add("createdate", System.Type.GetType("System.String"));
            cardconfiginfo.Columns.Add("vailddate", System.Type.GetType("System.String"));
            cardconfiginfo.Columns.Add("directory",System.Type.GetType("System.String"));
            cardconfiginfo.Columns.Add("name",System.Type.GetType("System.String"));
            cardconfiginfo.Columns.Add("currentfile",System.Type.GetType("System.String"));
            IDataReader reader = SAS.Data.DataProvider.CardConfigs.GetCardConfigData();
            while (reader.Read())
            {
                DataRow dr = cardconfiginfo.NewRow();
                dr["id"] = Utils.StrToInt(reader["id"], 0);
                dr["ccname"] = reader["ccname"].ToString();
                dr["tid"] = Utils.StrToInt(reader["tid"], 0);
                dr["hasflash"] = Utils.StrToInt(reader["hasflash"], 0);
                dr["hasimage"] = Utils.StrToInt(reader["hasimage"], 0);
                dr["hasjs"] = Utils.StrToInt(reader["hasjs"], 0);
                dr["hassilverlight"] = Utils.StrToInt(reader["hassilverlight"], 0);
                dr["showparams"] = reader["showparams"].ToString();
                dr["createdate"] = reader["createdate"].ToString();
                dr["vailddate"] = Utils.GetStandardDate(reader["vailddate"].ToString());
                dr["directory"] = reader["directory"].ToString();
                dr["name"] = reader["name"].ToString();
                dr["currentfile"] = reader["currentfile"].ToString();
                cardconfiginfo.Rows.Add(dr);
            }
            reader.Close();
            return cardconfiginfo;
        }

        /// <summary>
        /// 获取名片实体
        /// </summary>
        /// <returns></returns>
        public static CardConfigInfo GetCardConfigInfo(int ccid)
        {
            foreach (CardConfigInfo cci in SAS.Data.DataProvider.CardConfigs.GetCardConfigList())
            {
                if (cci.id == ccid) return cci;
            }
            return null;
        }

        /// <summary>
        /// 删除名片配置信息
        /// </summary>
        /// <param name="cardconfigid"></param>
        public static void DeleteCardConfig(int cardconfigid)
        {
            SAS.Data.DataProvider.CardConfigs.DeleteCardConfig(cardconfigid);
        }

        /// <summary>
        /// 修改名片配置信息
        /// </summary>
        /// <param name="cci"></param>
        public static void UpdateCardConfig(CardConfigInfo cci)
        {
            SAS.Data.DataProvider.CardConfigs.UpdateCardConfig(cci);
        }
    }
}
