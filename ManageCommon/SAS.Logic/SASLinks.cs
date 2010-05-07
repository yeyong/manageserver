﻿using System.Text.RegularExpressions;

namespace SAS.Logic
{
    /// <summary>
    /// 友情链接操作类
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
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");

            return Data.DataProvider.SASLinks.CreateSASLink(displayOrder, name, url, note, logo);
        }

        /// <summary>
        /// 获取全部链接
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable GetSASLinks()
        {
            return Data.DataProvider.SASLinks.GetSASLinks();
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
            Regex r = new Regex("(http|https)://([\\w-]+\\.)+[\\w-]+(/[\\w-./?%&=]*)?");
            if (name == "" || !r.IsMatch(url.Replace("'", "''")))
            {
                return -1;
            }
            return Data.DataProvider.SASLinks.UpdateSASLink(id, displayorder, name, url, note, logo);
        }


        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="SASlinkidlist">链接ID列表</param>
        /// <returns></returns>
        public static int DeleteSASLink(string SASlinkidlist)
        {
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");

            return Data.DataProvider.SASLinks.DeleteSASLink(SASlinkidlist);
        }
    }
}