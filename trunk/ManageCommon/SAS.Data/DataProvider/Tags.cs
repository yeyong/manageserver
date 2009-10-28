using System;
using System.Text;
using System.Data;

using SAS.Entity;
using SAS.Common.Generic;
using SAS.Common;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 标签数据操作类
    /// </summary>
    public class Tags
    {

        /// <summary>
        /// 将reader转化为实体类
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static TagInfo LoadSingleTagInfo(IDataReader reader)
        {
            TagInfo tag = new TagInfo();
            tag.Tagid = TypeConverter.ObjectToInt(reader["tagid"]);
            tag.Tagname = reader["tagname"].ToString();
            tag.Userid = TypeConverter.ObjectToInt(reader["userid"]);
            tag.Postdatetime = Convert.ToDateTime(reader["postdatetime"]);
            tag.Orderid = TypeConverter.ObjectToInt(reader["orderid"]);
            tag.Color = reader["color"].ToString();
            tag.Count = TypeConverter.ObjectToInt(reader["count"]);
            tag.Fcount = TypeConverter.ObjectToInt(reader["fcount"]);
            tag.Pcount = TypeConverter.ObjectToInt(reader["pcount"]);
            tag.Scount = TypeConverter.ObjectToInt(reader["scount"]);
            tag.Vcount = TypeConverter.ObjectToInt(reader["vcount"]);

            return tag;
        }

        /// <summary>
        /// 获取标签信息(不存在返回null)
        /// </summary>
        /// <param name="tagid">标签id</param>
        /// <returns></returns>
        public static TagInfo GetTagInfo(int tagid)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetTagInfo(tagid);
            TagInfo tag = null;
            if (reader.Read())
                tag = LoadSingleTagInfo(reader);

            reader.Close();
            return tag;
        }

        /// <summary>
        /// 更新TAG
        /// </summary>
        /// <param name="tagid">标签ID</param>
        /// <param name="orderid">排序</param>
        /// <param name="color">颜色</param>
        public static void UpdateForumTags(int tagid, int orderid, string color)
        {
            DatabaseProvider.GetInstance().UpdateForumTags(tagid, orderid, color);
        }

        /// <summary>
        /// 返回论坛Tag列表
        /// </summary>
        /// <param name="tagname">查询关键字</param>
        /// <param name="type">全部0 锁定1 开放2</param>
        /// <returns></returns>
        public static DataTable GetForumTags(string tagName, int type)
        {
            return DatabaseProvider.GetInstance().GetForumTags(tagName, type);
        }

    }
}
