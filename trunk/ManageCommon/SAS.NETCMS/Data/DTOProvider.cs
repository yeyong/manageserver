using System;
using System.Data;
using System.Text;

using SAS.Cache;
using SAS.Config;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.NETCMS.Data
{
    public class DTOProvider
    {
        private static GeneralConfigInfo ginfo = GeneralConfigs.GetConfig();

        public static List<NewsContent> GetNewsEntity(IDataReader reader)
        {
            List<NewsContent> newslist = new List<NewsContent>();
            while (reader.Read())
            {
                NewsContent newinfo = new NewsContent();
                PubClassInfo classinfo = NETCMS.GetNewsClassInfo(reader["ClassID"].ToString());
                newinfo.ID = TypeConverter.ObjectToInt(reader["id"], 0);
                newinfo.NewsID = reader["newsid"].ToString();
                newinfo.NewsTitle = reader["newstitle"].ToString();
                newinfo.NewsUrl = GetNewsUrl(reader["newsid"].ToString(), reader["SavePath"].ToString(), classinfo.SavePath + "/" + classinfo.SaveClassframe, reader["FileName"].ToString(), reader["FileEXName"].ToString());
                newslist.Add(newinfo);
            }
            reader.Close();
            return newslist;
        }

        public static List<PubClassInfo> GetNewsClassEntity(IDataReader reader)
        {
            List<PubClassInfo> classlist = new List<PubClassInfo>();
            while (reader.Read())
            {
                PubClassInfo classinfo = new PubClassInfo();
                classinfo.ID = TypeConverter.ObjectToInt(reader["id"], 0);
                classinfo.ClassID = reader["ClassID"].ToString();
                classinfo.ClassCName = reader["ClassCName"].ToString();
                classinfo.ClassEName = reader["ClassEName"].ToString();
                classinfo.ParentID = reader["ParentID"].ToString();
                classinfo.SavePath = reader["SavePath"].ToString();
                classinfo.SaveClassframe = reader["SaveClassframe"].ToString();
                classlist.Add(classinfo);
            }
            reader.Close();
            return classlist;
        }

        public static string GetNewsUrl(string NewsID, string SavePath, string SaveClassframe, string FileName, string FileEXName)
        {
            string str_temppath = "";
            if (SaveClassframe == "//")
            {
                str_temppath = "/" + SavePath + "/" + FileName + FileEXName;
            }
            else
            {
                str_temppath = "/" + SaveClassframe + "/" + SavePath + "/" + FileName + FileEXName;
            }
            str_temppath = ginfo.NETCMSUrl.Trim('/') + str_temppath.Replace("//", "/");
            return str_temppath;
        }
    }
}
