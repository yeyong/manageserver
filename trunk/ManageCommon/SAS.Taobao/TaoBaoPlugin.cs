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
        /// <param name="itemcount">返回总数量</param>
        public override List<TaobaokeItem> GetItemListByCondition(int cid, string keyword, string startmoney, string endmoney, string startcredit, string endcredit, string startrate, string endrate, string startnum, string endnum, int pagesize, int currentpage, out long itemcount)
        {
            return TaoBaos.GetItemList(cid, keyword, startmoney, endmoney, startcredit, endcredit, startrate, endrate, startnum, endnum, pagesize, currentpage, out itemcount);
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
    }
}
