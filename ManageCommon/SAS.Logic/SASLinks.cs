using System;
using System.Text.RegularExpressions;

using SAS.Entity;

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
            //SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/LinkList", true);

            int rnum = Data.DataProvider.SASLinks.CreateSASLink(displayOrder, name, url, note, logo);

            if (rnum > 0)
            {
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TaoBaoLinkList");
            }

            return rnum;
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
            int rnum = Data.DataProvider.SASLinks.UpdateSASLink(id, displayorder, name, url, note, logo);

            if (rnum > 0)
            {
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TaoBaoLinkList");
            }

            return rnum;
        }

        /// <summary>
        /// 获取全部友情链接
        /// </summary>
        public static System.Collections.Generic.List<FriendLinkInfo> GetFriendLinks()
        {
            System.Collections.Generic.List<FriendLinkInfo> flinks = new System.Collections.Generic.List<FriendLinkInfo>();
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            flinks = cache.RetrieveObject("/SAS/TaoBaoLinkList") as System.Collections.Generic.List<FriendLinkInfo>;
            //flinks = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/LinkList") as System.Collections.Generic.List<FriendLinkInfo>;
            if (flinks == null)
            {
                flinks = Data.DataProvider.SASLinks.GetAllLinks();
                flinks = flinks.FindAll(new Predicate<FriendLinkInfo>(delegate(FriendLinkInfo finfo) { return finfo.displayorder >= 10; }));
                //SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/LinkList", flinks);
                cache.AddObject("/SAS/TaoBaoLinkList", flinks);
            }
            return flinks;
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="SASlinkidlist">链接ID列表</param>
        /// <returns></returns>
        public static int DeleteSASLink(string SASlinkidlist)
        {
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/SASLinkList");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TaoBaoLinkList");
            //SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/LinkList", true);
            return Data.DataProvider.SASLinks.DeleteSASLink(SASlinkidlist);
        }
    }
}
