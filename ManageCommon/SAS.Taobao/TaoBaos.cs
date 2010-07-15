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
        private const string SAS_USERNICK = "yeyong2086521";

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
            return new NTWXmlRestClient("http://gw.api.tbsandbox.com/router/rest", "sandbox_c_1", "taobao1234");
        }

        public static NTWXmlRestClient GetProductTopClient()
        {
            //return new NTWXmlRestClient("http://gw.api.taobao.com/router/rest", "12097501", "551096b28b1631251639544b70590ee6");
            return new NTWXmlRestClient("http://gw.api.taobao.com/router/rest", "12005076", "64292c42ca49632200289324fba42572");
        }

        private static NTWXmlRestClient client = GetProductTopClient();

        /// <summary>
        /// 获取类目列表
        /// </summary>
        public static List<ItemCat> GetItemCatCache(long cid)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            List<ItemCat> itemcatlist = cache.RetrieveObject("SAS/Taobao/ItemCats_" + cid) as List<ItemCat>;
            if (itemcatlist == null)
            {
                ItemcatsGetRequest igr = new ItemcatsGetRequest();
                igr.Fields = "cid,parent_cid,name,is_parent,status,sort_order";
                igr.ParentCid = 0;
                try
                {
                    PageList<ItemCat> pageitems = client.ItemcatsGet(igr);
                    itemcatlist = pageitems.Content;
                    SAS.Cache.ICacheStrategy ica = new TaoBaoCacheStrategy();
                    ica.TimeOut = 20000;
                    cache.AddObject("SAS/Taobao/ItemCats_" + cid, itemcatlist);
                }
                catch (NTWException e)
                {
                    return null;
                }
            }
            return itemcatlist;
        }

        /// <summary>
        /// 根据条件获取商品分页信息
        /// </summary>
        /// <param name="cid">类别ID</param>
        /// <param name="keyword">关键字</param>
        /// <param name="startmoney">最低价格</param>
        /// <param name="endmoney">最高价格</param>
        /// <param name="startcredit">最低信誉度</param>
        /// <param name="endcredit">最高信誉度</param>
        /// <param name="startrate">最低佣金率</param>
        /// <param name="endrate">最高佣金率</param>
        /// <param name="startnum">最低推广量</param>
        /// <param name="endnum">最高推广量</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <param name="currentpage">当前页码</param>
        /// <param name="itemcount">返回总数量</param>
        public static List<TaobaokeItem> GetItemList(int cid, string keyword, string startmoney, string endmoney, string startcredit, string endcredit, string startrate, string endrate, string startnum, string endnum, int pagesize, int currentpage, out long itemcount)
        {
            itemcount = 0;
            TaobaokeItemsGetRequest tgr = new TaobaokeItemsGetRequest();
            tgr.Fields = "iid,num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,keyword_click_url";
            tgr.Nick = SAS_USERNICK;
            tgr.Cid = cid;
            tgr.Keyword = keyword;

            if (!string.IsNullOrEmpty(startmoney) && !string.IsNullOrEmpty(endmoney))
            {
                tgr.StartPrice = startmoney;
                tgr.EndPrice = endmoney;
            }

            if (!string.IsNullOrEmpty(startcredit) && !string.IsNullOrEmpty(endcredit))
            {
                tgr.StartCredit = startcredit;
                tgr.EndCredit = endcredit;
            }

            if (!string.IsNullOrEmpty(startrate) && !string.IsNullOrEmpty(endrate))
            {
                tgr.StartCommissionRate = startrate;
                tgr.EndCommissionRate = endrate;
            }

            if (!string.IsNullOrEmpty(startnum) && !string.IsNullOrEmpty(endnum))
            {
                tgr.StartCommissionNum = startnum;
                tgr.EndCommissionNum = endnum;
            }

            tgr.PageSize = pagesize;
            tgr.PageNo = currentpage;
            PageList<TaobaokeItem> pageitems = client.TaobaokeItemsGet(tgr);
            itemcount = pageitems.TotalResults;
            return pageitems.Content;
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

        #region 推荐recommend操作
        /// <summary>
        /// 创建推荐信息
        /// </summary>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public static int CreateRecommendInfo(int cid, int chanelid, string rtitle, string rcontent, int rtype)
        {
            return Data.DbProvider.GetInstance().CreateRecommendInfo(cid, chanelid, rtitle, rcontent, rtype);
        }
        /// <summary>
        /// 根据条件获取推荐信息
        /// </summary>
        public static DataTable GetRecommendsByCond(string conditions)
        {
            return Data.DbProvider.GetInstance().GetRecommendList(conditions);
        }
        /// <summary>
        /// 设置推荐搜索条件
        /// </summary>
        public static string GetRecommendCondition(bool islike, string rtitle, int rcategory, int rchanel, bool iscreatedate, string startcreate, string endcreate, bool isupdatedate, string startupdate, string endupdate)
        {
            return Data.DbProvider.GetInstance().GetRecommendCondition(islike, rtitle, rcategory, rchanel, iscreatedate, startcreate, endcreate, isupdatedate, startupdate, endupdate);
        }
        #endregion
    }
}
