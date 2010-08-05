using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 友情链接数据操作
    /// </summary>
    public class SASLinks
    {
        /// <summary>
        /// 添加友情链接
        /// </summary>
        /// <param name="displayOrder">序号</param>
        /// <param name="name">链接名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="note">注释</param>
        /// <param name="logo">图片地址</param>
        /// <returns></returns>
        public static int CreateSASLink(int displayOrder, string name, string url, string note, string logo)
        {
            return DatabaseProvider.GetInstance().AddSASLink(displayOrder, name, url, note, logo);
        }

        /// <summary>
        /// 获取全部链接
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSASLinks()
        {
            return DatabaseProvider.GetInstance().GetSASLinks();
        }

        /// <summary>
        /// 更新友情链接
        /// </summary>
        /// <param name="displayOrder">序号</param>
        /// <param name="name">链接名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="note">注释</param>
        /// <param name="logo">图片地址</param>
        /// <returns></returns>
        public static int UpdateSASLink(int id, int displayorder, string name, string url, string note, string logo)
        {
            return DatabaseProvider.GetInstance().UpdateSASLink(id, displayorder, name, url, note, logo);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="SASlinkidlist">链接ID列表</param>
        /// <returns></returns>
        public static int DeleteSASLink(string SASlinkidlist)
        {
            return DatabaseProvider.GetInstance().DeleteSASLink(SASlinkidlist);
        }

        /// <summary>
        /// 获取全部友情链接
        /// </summary>
        public static System.Collections.Generic.List<FriendLinkInfo> GetAllLinks()
        {
            System.Collections.Generic.List<FriendLinkInfo> flist = new System.Collections.Generic.List<FriendLinkInfo>();
            IDataReader reader = DatabaseProvider.GetInstance().GetAllLinks();
            while (reader.Read())
            {
                FriendLinkInfo finfo = new FriendLinkInfo();
                finfo.id = TypeConverter.ObjectToInt(reader["id"]);
                finfo.displayorder = TypeConverter.ObjectToInt(reader["displayorder"]);
                finfo.name = reader["name"].ToString();
                finfo.linkurl = reader["linkurl"].ToString();
                finfo.note = reader["note"].ToString();
                finfo.logo = reader["logo"].ToString();
                flist.Add(finfo);
            }
            reader.Close();
            return flist;
        }
    }
}
