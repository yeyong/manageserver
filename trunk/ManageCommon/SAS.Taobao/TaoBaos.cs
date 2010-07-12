using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using SAS.Entity;
using SAS.Entity.Domain;
using SAS.Common;
using SAS.Config;
using SAS.Cache;
using SAS.Taobao.Data;
using SAS.Taobao.Util;
using SAS.Taobao.Request;

namespace SAS.Taobao
{
    /// <summary>
    /// 淘宝操作类
    /// </summary>
    public class TaoBaos
    {
        private const string TOP_AUTHORIZE_URL = "http://open.taobao.com/isv/authorize.php";
        private const string TOP_CONTAINER_URL = "http://container.sandbox.taobao.com/container";

        public static NTWXmlRestClient GetDevelopTopClient()
        {
            return new NTWXmlRestClient("http://192.168.206.110:8080/top/private/services/rest", "sns", "sns");
        }

        public static NTWXmlRestClient GetTestingTopClient()
        {
            return new NTWXmlRestClient("http://api.daily.taobao.net/top/private/services/rest", "test", "test");
        }

        public static NTWXmlRestClient GetSandboxTopClient()
        {
            return new NTWXmlRestClient("http://gw.api.tbsandbox.com/router/rest", "sns", "sns");
        }

        public static NTWXmlRestClient GetProductTopClient()
        {
            return new NTWXmlRestClient("http://gw.api.taobao.com/router/rest", "12097501", "551096b28b1631251639544b70590ee6");
        }

        private static NTWXmlRestClient client = GetSandboxTopClient();

        /// <summary>
        /// 获取类目列表
        /// </summary>
        public static List<ItemCat> GetItemCatCache()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<ItemCat> itemcatlist = cache.RetrieveObject("SAS/Taobao/ItemCats") as List<ItemCat>;
            if (itemcatlist == null)
            {
                ItemcatsGetRequest igr = new ItemcatsGetRequest();
                igr.Fields="cid,parent_cid,name,is_parent,status,sort_order";
                igr.ParentCid = 0;
                PageList<ItemCat> pageitems = client.ItemcatsGet(igr);
                itemcatlist = client.ItemcatsGet(igr).Content;
                List<ItemCat> templist = new List<ItemCat>();
                foreach (ItemCat iteminfo in itemcatlist)
                {
                    if (iteminfo.IsParent)
                    {
                        ItemcatsGetRequest subigr = new ItemcatsGetRequest();
                        subigr.Fields = "cid,parent_cid,name,is_parent,status,sort_order";
                        subigr.ParentCid = iteminfo.Cid;
                        PageList<ItemCat> subpageitems = client.ItemcatsGet(subigr);
                        if (subpageitems.Content.Count > 0) templist.AddRange(subpageitems.Content);
                    }
                }
                itemcatlist.InsertRange(itemcatlist.Count - 1, templist);
                cache.AddObject("SAS/Taobao/ItemCats", itemcatlist);
            }
            return itemcatlist;
        }

        /// <summary>
        /// 获取相关父类下子类别的数量
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static long GetItemCatCount(long parentid)
        {
            ItemcatsGetRequest igr = new ItemcatsGetRequest();
            igr.Fields = "cid,parent_cid,name,is_parent,status,sort_order";
            igr.ParentCid = parentid;
            PageList<ItemCat> pageitems = client.ItemcatsGet(igr);
            return pageitems.TotalResults;
        }

        /// <summary>
        /// 获取测试环境下的用户会话授权码。
        /// </summary>
        /// <param name="appkey">应用编号</param>
        /// <param name="nick">用户昵称</param>
        /// <param name="url">回调地址</param>
        /// <returns>用户会话授权码</returns>
        public static string GetSandboxSessionKey(string appkey, string nick, string url)
        {
            IDictionary<string, string> authCodeParams = new Dictionary<string, string>();
            authCodeParams.Add("appkey", appkey);
            authCodeParams.Add("nick", nick);
            authCodeParams.Add("url", url);

            string authCodeRsp = WebUtils.DoPost(TOP_AUTHORIZE_URL, authCodeParams);
            string authCodePattern = "<input type=\"text\" id=\"autoInput\" value=\"(.+?)\" style=\".+?\">";
            Match authCodeResult = Regex.Match(authCodeRsp, authCodePattern);
            string authCode = authCodeResult.Groups[1].Value;

            IDictionary<string, string> sessionParams = new Dictionary<string, string>();
            sessionParams.Add("authcode", Uri.UnescapeDataString(authCode));
            string sessionRsp = WebUtils.DoGet(TOP_CONTAINER_URL, sessionParams);
            Console.WriteLine("xxxx");

            string sessionPattern = "&top_session=(\\w+?)&";
            Match sessionResult = Regex.Match(sessionRsp, sessionPattern);
            string sessionKey = sessionResult.Groups[1].Value;

            return sessionKey;
        }

        #region 商品类别category操作
        /// <summary>
        /// 创建商品类别
        /// </summary>
        public static int CreateCateInfo(CategoryInfo cinfo)
        {
            return Data.DbProvider.GetInstance().CreateCategoryInfo(cinfo);
        }
        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <param name="cid"></param>
        public static CategoryInfo GetCategoryInfo(int cid)
        {
            return cid > 0 ? DTOProvider.GetCategoryInfoEntity(Data.DbProvider.GetInstance().GetCategoryInfo(cid)) : null;
        }
        /// <summary>
        /// 获取全部商品类别信息
        /// </summary>
        public static DataTable GetAllCategoryList()
        {
            return Data.DbProvider.GetInstance().GetAllCategoryList();
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        public static void UpdateCategoryInfo(CategoryInfo cinfo)
        {
            Data.DbProvider.GetInstance().UpdateCategoryInfo(cinfo);
        }
        #endregion
    }
}
