using System;
using System.Text;
using System.Data.Common;
using System.Data;

using SAS.Entity;
using SAS.Data;
using SAS.Common.Generic;

namespace SAS.Logic
{
    /// <summary>
    /// 帮助信息管理
    /// </summary>
    public class Helps
    {
        /// <summary>
        /// 帮助信息树形列表
        /// </summary>
        private static List<HelpInfo> helpListTree = null;

        /// <summary>
        /// 获取帮助列表
        /// </summary>
        /// <returns>帮助列表</returns>
        public static List<HelpInfo> GetHelpList()
        {
            helpListTree = new List<HelpInfo>();
            List<HelpInfo> helpList = SAS.Data.DataProvider.Help.GetHelpList();
            CreateHelpTree(helpList, 0);

            return helpListTree;
        }

        /// <summary>
        /// 递归加载帮助信息树形列表
        /// </summary>
        /// <param name="helpList">源帮助信息列表</param>
        /// <param name="id">当前要递归的父节点helpid信息()</param>
        private static void CreateHelpTree(List<HelpInfo> helpList, int id)
        {
            foreach (HelpInfo helpInfo in helpList)
            {
                if (helpInfo.Pid == id)
                {
                    helpListTree.Add(helpInfo);
                    CreateHelpTree(helpList, helpInfo.Id);
                }
            }
        }

        /// <summary>
        /// 获取帮助内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns>帮助内容</returns>
        public static HelpInfo GetMessage(int id)
        {
            return id > 0 ? SAS.Data.DataProvider.Help.GetMessage(id) : null;
        }

        /// <summary>
        /// 更新帮助信息
        /// </summary>
        /// <param name="id">帮助ID</param>
        /// <param name="title">帮助标题</param>
        /// <param name="message">帮助内容</param>
        /// <param name="pid">帮助</param>
        /// <param name="orderby">排序方式</param>
        public static void UpdateHelp(int id, string title, string message, int pid, int orderby)
        {
            if (id > 0)
                SAS.Data.DataProvider.Help.UpdateHelp(id, title, message, pid, orderby);
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helplist");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helpindex");
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoIndexHelp", true);
        }

        /// <summary>
        /// 增加帮助
        /// </summary>
        /// <param name="title">帮助标题</param>
        /// <param name="message">帮助内容</param>
        /// <param name="pid">帮助</param>
        public static void AddHelp(string title, string message, int pid)
        {
            SAS.Data.DataProvider.Help.AddHelp(title, message, pid);
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helplist");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helpindex");
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoIndexHelp", true);
        }

        /// <summary>
        /// 删除帮助
        /// </summary>
        /// <param name="idlist">帮助ID序列</param>
        public static void DelHelp(string idlist)
        {
            SAS.Data.DataProvider.Help.DelHelp(idlist);
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helplist");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helpindex");
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoIndexHelp", true);
        }

        /// <summary>
        /// 获取帮助信息（缓存）
        /// </summary>
        public static List<HelpInfo> GetAllHelpList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<HelpInfo> helplist = cache.RetrieveObject("/SAS/helplist") as List<HelpInfo>;

            if (helplist == null || helplist.Count == 0)
            {
                helplist = SAS.Data.DataProvider.Help.GetHelpList();
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 1440;
                cache.AddObject("/SAS/helplist", helplist);
            }

            return helplist;
        }

        /// <summary>
        /// 获取首页帮助
        /// </summary>
        public static List<HelpInfo> GetIndexHelpList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<HelpInfo> helplist = cache.RetrieveObject("/SAS/helpindex") as List<HelpInfo>;

            if (helplist == null || helplist.Count == 0)
            {
                helplist = SAS.Data.DataProvider.Help.GetIndexHelpList(5);
                SAS.Cache.ICacheStrategy ica = new SASCacheStrategy();
                ica.TimeOut = 1440;
                cache.AddObject("/SAS/helpindex", helplist);
            }

            return helplist;
        }
        /// <summary>
        /// 获取淘之购帮助
        /// </summary>
        public static System.Collections.Generic.List<HelpInfo> GetTaoIndexHelp()
        {
            System.Collections.Generic.List<HelpInfo> helplist = new System.Collections.Generic.List<HelpInfo>();
            helplist = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/TaoIndexHelp") as System.Collections.Generic.List<HelpInfo>;

            if (helplist == null)
            {
                helplist = SAS.Data.DataProvider.Help.GetHelpList().FindAll(new Predicate<HelpInfo>(delegate(HelpInfo subhelp) { return subhelp.Pid == 1; }));
                SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/TaoIndexHelp", helplist);
            }
            return helplist;
        }

        /// <summary>
        /// 获取站内关于相关帮助
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Generic.List<HelpInfo> GetCommonHelp()
        {
            return GetAllHelpList().FindAll(new Predicate<HelpInfo>(delegate(HelpInfo subhelp) { return subhelp.Pid == 1; }));
        }

        /// <summary>
        /// 获取帮助实体信息
        /// </summary>
        /// <param name="helpid"></param>
        /// <returns></returns>
        public static HelpInfo GetHelpInfo(int helpid)
        {
            return GetAllHelpList().Find(new Predicate<HelpInfo>(delegate(HelpInfo subhelp) { return subhelp.Id == helpid; }));
        }

        /// <summary>
        /// 返回帮助的分类列表的SQL语句
        /// </summary>
        /// <returns>帮助的分类列表的SQL语句</returns>
        public static DataTable GetHelpTypes()
        {
            return SAS.Data.DataProvider.Help.GetHelpTypes();
        }


        /// <summary>
        /// 获取帮助分类以及相应帮助主题
        /// </summary>
        /// <param name="helpid"></param>
        /// <returns>帮助分类以及相应帮助主题</returns>
        public static List<HelpInfo> GetHelpList(int helpid)
        {
            List<HelpInfo> result = new List<HelpInfo>();
            foreach (HelpInfo helpInfo in GetHelpList())
            {
                if (helpInfo.Id == helpid || helpInfo.Pid == helpid)
                    result.Add(helpInfo);
            }
            return result;
        }

        /// <summary>
        /// 更新帮助序号
        /// </summary>
        /// <param name="orderlist">排序号</param>
        /// <param name="idlist">帮助Id</param>
        public static bool UpOrder(string[] orderlist, string[] idlist)
        {
            if (orderlist.Length != idlist.Length)
                return false;

            foreach (string s in orderlist)
            {
                if (SAS.Common.Utils.IsNumeric(s) == false)
                    return false;
            }
            for (int i = 0; i < idlist.Length; i++)
            {
                SAS.Data.DataProvider.Help.UpdateOrder(orderlist[i].ToString(), idlist[i].ToString());
            }
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helplist");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/helpindex");
            SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoIndexHelp", true);
            return true;
        }
    }
}
