using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity.Domain;
using SAS.Entity;

namespace SAS.Plugin.TaoBao
{
    /// <summary>
    /// 淘宝控制插件类
    /// </summary>    
    public abstract class TaoBaoPluginBase : PluginBase
    {
        /// <summary>
        /// 获取Taobao类目列表
        /// </summary>
        public abstract System.Collections.Generic.List<ItemCat> GetItemCatCache(long cid);
        /// <summary>
        /// 获取淘宝客商品列表
        /// </summary>
        public abstract System.Collections.Generic.List<TaobaokeItem> GetTaoBaoKeItemList(string numiidlist);
        /// <summary>
        /// 获取商品类目信息
        /// </summary>
        public abstract CategoryInfo GetCategoryInfo(int cid);
        /// <summary>
        /// 获取商品类别全部信息
        /// </summary>
        public abstract DataTable GetAllCategoryList();
        /// <summary>
        /// 创建商品类别
        /// </summary>
        public abstract int CreateCategoryInfo(CategoryInfo cinfo);
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
        public abstract System.Collections.Generic.List<TaobaokeItem> GetItemListByCondition(int cid, string keyword, string startmoney, string endmoney, string startcredit, string endcredit, string startrate, string endrate, string startnum, string endnum, int pagesize, int currentpage, out long itemcount);
        /// <summary>
        /// 修改商品类别
        /// </summary>
        public abstract void UpdateCategoryInfo(CategoryInfo cinfo);
        /// <summary>
        /// 创建推荐信息
        /// </summary>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public abstract int CreateRecommendInfo(int cid, int chanelid, string rtitle, string rcontent, int rtype);
        /// <summary>
        /// 获取推荐实体
        /// </summary>
        public abstract RecommendInfo GetRecommendInfo(int id);
         /// <summary>
        /// 根据条件获取推荐信息
        /// </summary>
        public abstract DataTable GetRecommendsByCond(string conditions);
        /// <summary>
        /// 设置推荐搜索条件
        /// </summary>
        public abstract string GetRecommendCondition(bool islike, string rtitle, int rcategory, int rchanel, bool iscreatedate, string startcreate, string endcreate, bool isupdatedate, string startupdate, string endupdate);
        /// <summary>
        /// 更新推荐信息
        /// </summary>
        /// <param name="id">推荐ID</param>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public abstract void UpdateRecommendInfo(int id, int cid, int chanelid, string rtitle, string rcontent, int rtype);
        /// <summary>
        /// 搜索并增加或更新店铺信息
        /// </summary>
        /// <param name="nickname">卖家昵称</param>
        public abstract int SearchAndAddShop(string nickname);
        /// <summary>
        /// 根据条件获取淘宝店铺分页信息
        /// </summary>
        public abstract SAS.Common.Generic.List<ShopDetailInfo> GetTaoBaoShopsPage(string conditions, int pagesize, int pageindex, string ordercolumn, string ordertype);
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
        public abstract string GetTaoBaoShopCondition(string shoptitle, string shopnick, string province, string city, int startscore, int endscore, int startcredit, int endcredit, int startrate, int endrate);
        /// <summary>
        /// 根据条件获取淘宝店铺数量
        /// </summary>
        public abstract int GetTaoBaoShopCountByCondition(string conditions);
    }
}
