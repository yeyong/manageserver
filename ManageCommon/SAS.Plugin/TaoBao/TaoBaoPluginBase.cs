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
        public abstract System.Collections.Generic.List<ItemCat> GetItemCatCache();
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
    }
}
