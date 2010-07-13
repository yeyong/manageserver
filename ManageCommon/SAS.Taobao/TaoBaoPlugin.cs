using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using SAS.Plugin.TaoBao;
using SAS.Common;
using SAS.Entity.Domain;
using SAS.Config;

namespace SAS.Taobao
{
    public class TaoBaoPlugin : TaoBaoPluginBase
    {
        /// <summary>
        /// 获取类目列表
        /// </summary>
        public override List<ItemCat> GetItemCatCache()
        {
            return TaoBaos.GetItemCatCache();
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
    }
}
