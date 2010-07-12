using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;

using SAS.Config;
using SAS.Data;
using SAS.Entity;
using SAS.Common;

namespace SAS.Taobao.Data
{
    public class DataProvider
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取商品类别
        /// </summary>
        public IDataReader GetCategoryInfo(int cid)
        {
            DbParameter param = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, cid);
            string commandText = string.Format("SELECT {0} FROM [{1}category] WHERE [cid]=@id",
                                                DbFields.CATEGORY,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 获取商品类别全部信息
        /// </summary>
        public DataTable GetAllCategoryList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}category]", DbFields.CATEGORY,  BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
    }
}
