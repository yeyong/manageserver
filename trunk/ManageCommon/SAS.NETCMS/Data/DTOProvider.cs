using System;
using System.Data;
using System.Text;

using SAS.Cache;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.NETCMS.Data
{
    public class DTOProvider
    {
        public static List<NewsContent> GetNewsEntity(IDataReader reader)
        {
            List<NewsContent> newslist = new List<NewsContent>();
            while (reader.Read())
            {
                NewsContent newinfo = new NewsContent();
                newinfo.ID = TypeConverter.ObjectToInt(reader["id"], 0);
                newinfo.NewsID = reader["newsid"].ToString();
                newinfo.NewsTitle = reader["newstitle"].ToString();
                newslist.Add(newinfo);
            }
            reader.Close();
            return newslist;
        }
    }
}
