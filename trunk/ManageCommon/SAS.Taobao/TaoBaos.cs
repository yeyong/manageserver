using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

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
                itemcatlist = client.ItemcatsGet(igr).Content;
                SAS.Cache.ICacheStrategy ica = new DefaultCacheStrategy();
                ica.TimeOut = 300;
                cache.AddObject("SAS/Taobao/ItemCats", itemcatlist);
            }
            return itemcatlist;
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
    }
}
