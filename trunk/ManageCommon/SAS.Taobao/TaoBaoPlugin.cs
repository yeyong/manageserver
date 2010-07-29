using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using SAS.Plugin.TaoBao;
using SAS.Common;
using SAS.Entity;
using SAS.Entity.Domain;
using SAS.Config;

namespace SAS.Taobao
{
    public class TaoBaoPlugin : TaoBaoPluginBase
    {
        /// <summary>
        /// 获取类目列表
        /// </summary>
        public override List<ItemCat> GetItemCatCache(long cid)
        {
            return TaoBaos.GetItemCatCache(cid);
        }
        /// <summary>
        /// 获取淘宝客商品列表
        /// </summary>
        public override List<TaobaokeItem> GetTaoBaoKeItemList(string numiidlist)
        {
            return TaoBaos.GetTaoBaoKeItemList(numiidlist);
        }
        /// <summary>
        /// 获取商品类目信息
        /// </summary>
        public override SAS.Entity.CategoryInfo GetCategoryInfo(int cid)
        {
            return TaoBaos.GetCategoryInfo(cid);
        }
        /// <summary>
        /// 获取全部商品类别信息
        /// </summary>
        public override DataTable GetAllCategoryList()
        {
            return TaoBaos.GetAllCategoryList();
        }
        /// <summary>
        /// 获取有效类别集合
        /// </summary>
        public override SAS.Common.Generic.List<CategoryInfo> GetVaildCategoryList()
        {
            return TaoBaos.GetVaildCategoryList();
        }
        /// <summary>
        /// 创建商品类别
        /// </summary>
        public override int CreateCategoryInfo(SAS.Entity.CategoryInfo cinfo)
        {
            return TaoBaos.CreateCateInfo(cinfo);
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
        /// <param name="sortstr">排序</param>
        /// <param name="itemcount">返回总数量</param>
        public override List<TaobaokeItem> GetItemListByCondition(int cid, string keyword, string startmoney, string endmoney, string startcredit, string endcredit, string startrate, string endrate, string startnum, string endnum, int pagesize, int currentpage, string sortstr, out long itemcount)
        {
            return TaoBaos.GetItemList(cid, keyword, startmoney, endmoney, startcredit, endcredit, startrate, endrate, startnum, endnum, pagesize, currentpage, sortstr, out itemcount);
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        public override void UpdateCategoryInfo(SAS.Entity.CategoryInfo cinfo)
        {
            TaoBaos.UpdateCategoryInfo(cinfo);
        }
        /// <summary>
        /// 创建推荐信息
        /// </summary>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public override int CreateRecommendInfo(int cid, int chanelid, string rtitle, string rcontent, int rtype)
        {
            return TaoBaos.CreateRecommendInfo(cid, chanelid, rtitle, rcontent, rtype);
        }
        /// <summary>
        /// 获取推荐实体
        /// </summary>
        public override RecommendInfo GetRecommendInfo(int id)
        {
            return TaoBaos.GetRecommendInfo(id);
        }
        /// <summary>
        /// 设置推荐搜索条件
        /// </summary>
        public override string GetRecommendCondition(bool islike, string rtitle, int rcategory, int rchanel, bool iscreatedate, string startcreate, string endcreate, bool isupdatedate, string startupdate, string endupdate)
        {
            return TaoBaos.GetRecommendCondition(islike, rtitle, rcategory, rchanel, iscreatedate, startcreate, endcreate, isupdatedate, startupdate, endupdate);
        }
        /// <summary>
        /// 根据条件获取推荐信息
        /// </summary>
        public override DataTable GetRecommendsByCond(string conditions)
        {
            return TaoBaos.GetRecommendsByCond(conditions);
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
        public override void UpdateRecommendInfo(int id, int cid, int chanelid, string rtitle, string rcontent, int rtype)
        {
            TaoBaos.UpdateRecommendInfo(id, cid, chanelid, rtitle, rcontent, rtype);
        }
        /// <summary>
        /// 搜索并增加或更新店铺信息
        /// </summary>
        /// <param name="nickname">卖家昵称</param>
        public override int SearchAndAddShop(string nickname)
        {
            return TaoBaos.SearchAndAddShop(nickname);
        }
        /// <summary>
        /// 根据条件获取淘宝店铺分页信息
        /// </summary>
        public override SAS.Common.Generic.List<ShopDetailInfo> GetTaoBaoShopsPage(string conditions, int pagesize, int pageindex, string ordercolumn, string ordertype)
        {
            return TaoBaos.GetTaoBaoShopsPage(conditions, pagesize, pageindex, ordercolumn, ordertype);
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
        public override string GetTaoBaoShopCondition(string shoptitle, string shopnick, string province, string city, int startscore, int endscore, int startcredit, int endcredit, int startrate, int endrate)
        {
            return TaoBaos.GetTaoBaoShopCondition(shoptitle, shopnick, province, city, startscore, endscore, startcredit, endcredit, startrate, endrate);
        }
        /// <summary>
        /// 根据条件获取淘宝店铺数量
        /// </summary>
        public override int GetTaoBaoShopCountByCondition(string conditions)
        {
            return TaoBaos.GetTaoBaoShopCountByCondition(conditions);
        }
        /// <summary>
        /// 根据店铺ID集合获取店铺信息
        /// </summary>
        /// <param name="ids"></param>
        public override SAS.Common.Generic.List<ShopDetailInfo> GetTaoBaoShopListByIds(string ids)
        {
            return TaoBaos.GetTaoBaoShopListByIds(ids);
        }
        /// <summary>
        /// 创建商品品牌
        /// </summary>
        public override int CreateGoodsBrand(GoodsBrandInfo ginfo)
        {
            return TaoBaos.CreateGoodsBrand(ginfo);
        }
        /// <summary>
        /// 根据条件返回品牌数量
        /// </summary>
        public override int GetGoodsBrandCountByCond(string conditions)
        {
            return TaoBaos.GetGoodsBrandCountByCond(conditions);
        }
        /// <summary>
        /// 根据条件返回品牌分页集合
        /// </summary>
        public override DataTable GetGoodsBrandByPage(string conditions, int pagesize, int pageindex)
        {
            return TaoBaos.GetGoodsBrandByPage(conditions, pagesize, pageindex);
        }
        /// <summary>
        /// 返回品牌查询条件
        /// </summary>
        /// <param name="isspell">是否同时查询别名</param>
        /// <param name="brandname">品牌名称</param>
        /// <param name="relateclass">关联类别</param>
        /// <param name="brandstatus">状态</param>
        public override string GetGoodsBrandSearchCondition(bool isspell, string brandname, string relateclass, int brandstatus)
        {
            return TaoBaos.GetGoodsBrandSearchCondition(isspell, brandname, relateclass, brandstatus);
        }
        /// <summary>
        /// 设置品牌批量开启
        /// </summary>
        /// <param name="idlist"></param>
        public override void SetGoodsBrandListStart(string idlist)
        {
            TaoBaos.SetGoodsBrandListStart(idlist);
        }
        /// <summary>
        /// 设置品牌批量关闭
        /// </summary>
        /// <param name="idlist"></param>
        public override void SetGoodsBrandListStop(string idlist)
        {
            TaoBaos.SetGoodsBrandListStop(idlist);
        }
        /// <summary>
        /// 获取品牌实体
        /// </summary>
        public override GoodsBrandInfo GetGoodsBrandInfo(int id)
        {
            return TaoBaos.GetGoodsBrandInfo(id);
        }
        /// <summary>
        /// 更新品牌信息
        /// </summary>
        public override void UpdateGoodsBrand(GoodsBrandInfo ginfo)
        {
            TaoBaos.UpdateGoodsBrand(ginfo);
        }
    }
}
