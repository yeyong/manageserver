﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Caching;

using SAS.Entity;
using SAS.Entity.Domain;
using SAS.Common;
using SAS.Config;
using SAS.Cache;
using SAS.Taobao.Data;
using SAS.Taobao.Util;
using SAS.Taobao.Request;
using SAS.Cache.CacheDependencyFactory;

namespace SAS.Taobao
{
    /// <summary>
    /// 淘宝操作类
    /// </summary>
    public class TaoBaos
    {
        private static DataCacheConfigInfo dataconfig = DataCacheConfigs.GetConfig();
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
            List<ItemCat> itemcatlist = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/Taobao/ItemCats_" + cid) as List<ItemCat>;
            if (itemcatlist == null)
            {
                ItemcatsGetRequest igr = new ItemcatsGetRequest();
                igr.Fields = "cid,parent_cid,name,is_parent,status,sort_order";
                igr.ParentCid = cid;
                try
                {
                    PageList<ItemCat> pageitems = client.ItemcatsGet(igr);
                    itemcatlist = pageitems.Content;
                    SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/Taobao/ItemCats_" + cid, itemcatlist);
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
        /// 获取淘宝客商品列表
        /// </summary>
        public static List<TaobaokeItem> GetTaoBaoKeItemList(string numiidlist)
        {
            TaobaokeItemsConvertRequest tgr = new TaobaokeItemsConvertRequest();
            tgr.Fields = "iid,num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,keyword_click_url";
            tgr.Nick = SAS_USERNICK;
            tgr.NumIids = numiidlist;
            PageList<TaobaokeItem> pageitems = client.TaobaokeItemsConvert(tgr);
            return pageitems.Content;
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
        /// <summary>
        /// 获取有效类别集合
        /// </summary>
        public static SAS.Common.Generic.List<CategoryInfo> GetVaildCategoryList()
        {
            List<CategoryInfo> categorylist = new List<CategoryInfo>();
            string cachekeys = "categorylist";
            if (dataconfig.EnableCaching != 1)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                categorylist = cache.RetrieveObject("/SAS/" + cachekeys) as List<CategoryInfo>;
                if (categorylist == null || categorylist.Count == 0)
                {
                    categorylist = DTOProvider.GetCategoryListEntity(Data.DbProvider.GetInstance().GetVaildCategoryList());
                    cache.AddObject("/SAS/" + cachekeys, categorylist);
                }
            }
            else
            {
                SAS.Cache.SASDataCache datacache = SAS.Cache.SASDataCache.GetCacheService();
                categorylist = datacache.GetDataCache(cachekeys) as List<CategoryInfo>;
                if (categorylist == null || categorylist.Count == 0)
                {
                    categorylist = DTOProvider.GetCategoryListEntity(Data.DbProvider.GetInstance().GetVaildCategoryList());
                    AggregateCacheDependency cd = DependencyFacade.GetCategoryDependency();
                    datacache.SetDataCache(cachekeys, categorylist, cd);
                }
            }
            return DTOProvider.GetCategoryListEntity(Data.DbProvider.GetInstance().GetVaildCategoryList());
        }
        /// <summary>
        /// 获取有效类别集合（根据父级）
        /// </summary>
        public static List<CategoryInfo> GetCategoryListByParentID(int pid)
        {
            return GetVaildCategoryList().FindAll(new Predicate<CategoryInfo>(delegate(CategoryInfo cinfo) { return cinfo.Parentid == pid; }));
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
        /// <summary>
        /// 获取推荐实体
        /// </summary>
        public static RecommendInfo GetRecommendInfo(int id)
        {
            return DTOProvider.GetRecommendInfoEntity(Data.DbProvider.GetInstance().GetRecommendInfo(id));
        }
        /// <summary>
        /// 获取全部推荐实体（缓存方式）
        /// </summary>
        public static List<RecommendInfo> GetAllRecommendList()
        {
            List<RecommendInfo> rinfolist = new List<RecommendInfo>();
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();            
            //rinfolist = cache.RetrieveObject("/SAS/RecommendList") as List<RecommendInfo>;
            rinfolist = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/RecommendList") as List<RecommendInfo>;
            if (rinfolist == null)
            {
                rinfolist = DTOProvider.GetRecommendListEntity(Data.DbProvider.GetInstance().GetAllRecommendList());
                SAS.Cache.ICacheStrategy ica = new TaoBaoCacheStrategy();
                ica.TimeOut = 1440;
                //cache.AddObject("/SAS/RecommendList", rinfolist);
                SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/RecommendList", rinfolist);
            }
            return rinfolist;
        }
        /// <summary>
        /// 获取推荐信息（根据频道，类别）
        /// </summary>
        /// <param name="chanel">频道</param>
        /// <param name="classid">类别</param>
        public static List<RecommendInfo> GetRecommendList(int chanel, int classid)
        {
            if (chanel > -1 && classid > -1) return GetAllRecommendList().FindAll(new Predicate<RecommendInfo>(delegate(RecommendInfo rinfo) { return rinfo.relatechanel == chanel && rinfo.relatecategory == classid; }));
            else if (chanel > -1) return GetAllRecommendList().FindAll(new Predicate<RecommendInfo>(delegate(RecommendInfo rinfo) { return rinfo.relatechanel == chanel; }));
            else if (classid > -1) return GetAllRecommendList().FindAll(new Predicate<RecommendInfo>(delegate(RecommendInfo rinfo) { return rinfo.relatecategory == classid; }));
            else return new List<RecommendInfo>();
        }
        /// <summary>
        /// 更新推荐信息
        /// </summary>
        /// <param name="id">推荐ID</param>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public static void UpdateRecommendInfo(int id, int cid, int chanelid, string rtitle, string rcontent, int rtype)
        {
            Data.DbProvider.GetInstance().UpdateRecommendInfo(id, cid, chanelid, rtitle, rcontent, rtype);
        }
        #endregion

        #region 淘宝店铺操作
        /// <summary>
        /// 搜索并增加或更新店铺信息
        /// </summary>
        /// <param name="nickname">卖家昵称</param>
        public static int SearchAndAddShop(string nickname)
        {
            UserGetRequest ugr = new UserGetRequest();
            ugr.Fields = "user_id,nick,seller_credit,location,type,promoted_type,status,consumer_protection";
            ugr.Nick = nickname;
            User userinfo = new User();
            try
            {
                userinfo = client.UserGet(ugr);
            }
            catch (NTWException ntwe1)
            {
                userinfo = null;
            }
            if (userinfo == null) return 0;
            Location locainfo = userinfo.Location;
            UserCredit ucredit = userinfo.SellerCredit;

            ShopGetRequest sgr = new ShopGetRequest();
            sgr.Fields = "sid,cid,title,nick,desc,bulletin,pic_path,created,modified,shop_score";
            sgr.Nick = nickname;
            Shop shopinfo = new Shop();
            try
            {
                shopinfo = client.ShopGet(sgr);
            }
            catch (NTWException ntwe2)
            {
                shopinfo = null;
            }
            if (shopinfo == null) return 0;
            ShopScore shopscore = shopinfo.ShopScore;

            TaobaokeShopsConvertRequest tcr = new TaobaokeShopsConvertRequest();
            tcr.Fields = "user_id,shop_title,click_url,commission_rate";
            tcr.Nick = SAS_USERNICK;
            tcr.Sids = shopinfo.Sid;
            PageList<TaobaokeShop> tks = client.TaobaokeShopsConvert(tcr);
            if (tks.Content.Count == 0) return 0;
            TaobaokeShop tshopinfo = tks.Content[0];

            ShopDetailInfo sinfo = new ShopDetailInfo();
            sinfo.sid = long.Parse(shopinfo.Sid.Trim());
            sinfo.user_id = long.Parse(tshopinfo.UserId.Trim());
            sinfo.cid = long.Parse(shopinfo.Cid.Trim());
            sinfo.nick = shopinfo.Nick;
            sinfo.title = shopinfo.Title;
            sinfo.item_score = TypeConverter.ObjectToInt(shopscore.ItemScore, 0);
            sinfo.service_score = TypeConverter.ObjectToInt(shopscore.ServiceScore, 0);
            sinfo.delivery_score = TypeConverter.ObjectToInt(shopscore.DeliveryScore, 0);
            sinfo.shop_desc = shopinfo.Desc == null ? "" : shopinfo.Desc;
            sinfo.bulletin = shopinfo.Bulletin == null ? "" : shopinfo.Bulletin;
            sinfo.pic_path = shopinfo.PicPath == null ? "" : shopinfo.PicPath;
            sinfo.created = shopinfo.Created == null ? "" : shopinfo.Created; ;
            sinfo.modified = shopinfo.Modified == null ? "" : shopinfo.Modified; ;
            sinfo.promoted_type = userinfo.PromotedType == null ? "" : userinfo.PromotedType;
            sinfo.consumer_protection = userinfo.ConsumerProtection;
            sinfo.shop_status = userinfo.Status == null ? "" : userinfo.Status;
            sinfo.shop_type = userinfo.Type;
            sinfo.shop_level = ucredit.Level;
            sinfo.shop_score = ucredit.Score;
            sinfo.total_num = ucredit.TotalNum;
            sinfo.good_num = ucredit.GoodNum;
            sinfo.shop_country = locainfo.Country == null ? "" : locainfo.Country;
            sinfo.shop_province = locainfo.State == null ? "" : locainfo.State;
            sinfo.shop_city = locainfo.City == null ? "" : locainfo.City;
            sinfo.shop_address = locainfo.Address == null ? "" : locainfo.Address;
            sinfo.commission_rate = tshopinfo.CommissionRate == null ? "" : tshopinfo.CommissionRate;
            sinfo.click_url = tshopinfo.ClickUrl == null ? "" : tshopinfo.ClickUrl;
            return CollectionTaoBaoShop(sinfo);
        }
        /// <summary>
        /// 店铺信息收集
        /// </summary>
        /// <returns>返回1，更新店铺信息；返回2，增加店铺信息</returns>
        public static int CollectionTaoBaoShop(ShopDetailInfo sinfo)
        {
            return Data.DbProvider.GetInstance().CollectionTaobaoShops(sinfo);
        }
        /// <summary>
        /// 根据条件获取淘宝店铺分页信息
        /// </summary>
        public static SAS.Common.Generic.List<ShopDetailInfo> GetTaoBaoShopsPage(string conditions, int pagesize, int pageindex, string ordercolumn, string ordertype)
        {
            if (ordercolumn == "")
            {
                ordercolumn = "shop_level";
            }
            return DTOProvider.GetTaoBaoShopList(Data.DbProvider.GetInstance().GetTaoBaoShopListPage(conditions, pagesize, pageindex, ordercolumn, ordertype));
        }
        /// <summary>
        /// 获取店铺搜索条件
        /// </summary>
        /// <param name="shoptitle">店铺标题名称</param>
        /// <param name="shopnick">卖家昵称</param>
        /// <param name="province">所在省份</param>
        /// <param name="city">所在市</param>
        /// <param name="startscore">最小评分</param>
        /// <param name="endstartscore">最大评分</param>
        /// <param name="startcredit">最小信誉值</param>
        /// <param name="endcredit">最大信誉值</param>
        /// <param name="startrate">最小佣金率</param>
        /// <param name="endrate">最大佣金率</param>
        public static string GetTaoBaoShopCondition(string shoptitle, string shopnick, string province, string city, int startscore, int endscore, int startcredit, int endcredit, int startrate, int endrate)
        {
            return Data.DbProvider.GetInstance().GetTaoBaoShopCondition(shoptitle, shopnick, province, city, startscore, endscore, startcredit, endcredit, startrate, endrate);
        }
        /// <summary>
        /// 根据条件获取淘宝店铺数量
        /// </summary>
        public static int GetTaoBaoShopCountByCondition(string conditions)
        {
            return Data.DbProvider.GetInstance().GetTaoBaoShopCount(conditions);
        }
        /// <summary>
        /// 根据店铺ID集合获取店铺信息
        /// </summary>
        public static SAS.Common.Generic.List<ShopDetailInfo> GetTaoBaoShopListByIds(string ids)
        {
            if (ids == "") return new SAS.Common.Generic.List<ShopDetailInfo>();
            return DTOProvider.GetTaoBaoShopList(Data.DbProvider.GetInstance().GetTaoBaoShopList(ids));
        }
        /// <summary>
        /// 根据频道类别获取店铺信息
        /// </summary>
        public static SAS.Common.Generic.List<ShopDetailInfo> GetTaoBaoShopListByRecommend(int chanel, int classid)
        {
            SAS.Common.Generic.List<ShopDetailInfo> shoplist = new SAS.Common.Generic.List<ShopDetailInfo>();
            shoplist = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/ShopList/Chanel_" + chanel + "/Class_" + classid) as SAS.Common.Generic.List<ShopDetailInfo>;
            if (shoplist == null)
            {
                List<RecommendInfo> rlist = GetRecommendList(chanel, classid).FindAll(new Predicate<RecommendInfo>(delegate(RecommendInfo rdinfo) { return rdinfo.ctype == 2; }));
                string shopidlist = "";
                foreach (RecommendInfo rinfo in rlist)
                {
                    shopidlist += rinfo.ccontent + ",";
                }
                shoplist = GetTaoBaoShopListByIds(shopidlist.Trim().Trim(','));
                SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/ShopList/Chanel_" + chanel + "/Class_" + classid, shoplist);
            }
            return shoplist;
        }
        #endregion

        #region 商品品牌goodsbrand操作
        /// <summary>
        /// 创建商品品牌
        /// </summary>
        public static int CreateGoodsBrand(GoodsBrandInfo ginfo)
        {
            return Data.DbProvider.GetInstance().CreateGoodsBrandInfo(ginfo);
        }
        /// <summary>
        /// 根据条件返回品牌数量
        /// </summary>
        public static int GetGoodsBrandCountByCond(string conditions)
        {
            return Data.DbProvider.GetInstance().GetGoodsBrandCountByCond(conditions);
        }
        /// <summary>
        /// 根据条件返回品牌分页集合
        /// </summary>
        public static DataTable GetGoodsBrandByPage(string conditions, int pagesize, int pageindex)
        {
            return Data.DbProvider.GetInstance().GetGoodsBrandByPage(conditions, pagesize, pageindex);
        }
        /// <summary>
        /// 返回品牌查询条件
        /// </summary>
        /// <param name="isspell">是否同时查询别名</param>
        /// <param name="brandname">品牌名称</param>
        /// <param name="relateclass">关联类别</param>
        /// <param name="brandstatus">状态</param>
        public static string GetGoodsBrandSearchCondition(bool isspell, string brandname, string relateclass, int brandstatus)
        {
            return Data.DbProvider.GetInstance().GetGoodsBrandSearchCondition(isspell, brandname, relateclass, brandstatus);
        }
        /// <summary>
        /// 设置品牌批量开启
        /// </summary>
        /// <param name="idlist"></param>
        public static void SetGoodsBrandListStart(string idlist)
        {
            Data.DbProvider.GetInstance().UpdateGoodsBrandStatus(idlist, 1);
        }
        /// <summary>
        /// 设置品牌批量关闭
        /// </summary>
        /// <param name="idlist"></param>
        public static void SetGoodsBrandListStop(string idlist)
        {
            Data.DbProvider.GetInstance().UpdateGoodsBrandStatus(idlist, 0);
        }
        /// <summary>
        /// 获取品牌实体
        /// </summary>
        public static GoodsBrandInfo GetGoodsBrandInfo(int id)
        {
            return DTOProvider.GetGoodsBrandInfoEntity(Data.DbProvider.GetInstance().GetGoodsBrandInfo(id));
        }
        /// <summary>
        /// 根据类别获取品牌信息集合
        /// </summary>
        public static List<GoodsBrandInfo> GetGoodsBrandListByClass(int classid)
        {
            SAS.Common.Generic.List<GoodsBrandInfo> brandlist = new SAS.Common.Generic.List<GoodsBrandInfo>();
            brandlist = SAS.Cache.WebCacheFactory.GetWebCache().Get("/SAS/GoodsBrand/Class_" + classid) as SAS.Common.Generic.List<GoodsBrandInfo>;
            if (brandlist == null)
            {
                brandlist = DTOProvider.GetGoodsBrandListEntity(Data.DbProvider.GetInstance().GetGoodsBrandListByClass(classid));
                SAS.Cache.WebCacheFactory.GetWebCache().Add("/SAS/GoodsBrand/Class_" + classid, brandlist);
            }
            return brandlist;
        }
        /// <summary>
        /// 更新品牌信息
        /// </summary>
        public static void UpdateGoodsBrand(GoodsBrandInfo ginfo)
        {
            Data.DbProvider.GetInstance().UpdateGoodsBrand(ginfo);
        }
        #endregion
    }
}
