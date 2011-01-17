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

namespace SAS.NETCMS.Data
{
    public class DataProvider : DbBase
    {
        private const string newpre = "NT_";

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
        /// 获取新闻集合
        /// </summary>       
        public IDataReader GetNewsList(int newscount, string ordercol, string ordertype)
        {
            string commandText = String.Format("SELECT TOP {0} [Id],[NewsID],[NewsTitle] FROM {1}News WHERE [isLock] = 0 AND [isRecyle] = 0 ORDER BY {2} {3}", newscount, newpre, ordercol == "" ? "[id]" : ordercol, ordertype == "desc" ? ordertype : "");
            return NewsDbHelper.ExecuteReader(CommandType.Text, commandText);
        }

    }
}
