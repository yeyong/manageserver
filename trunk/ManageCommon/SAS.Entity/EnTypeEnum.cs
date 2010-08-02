using System;

namespace SAS.Entity
{
    /// <summary>
    /// 枚举操作
    /// </summary>
    public class EnumCatch
    {
        /// <summary>
        /// 获取广告枚举实例名称
        /// </summary>
        /// <param name="obj"></param>
        public static string GetADTpyeName(AdType obj)
        {
            return GetADTpyeName(Convert.ToInt16(obj));
        }

        /// <summary>
        /// 获取广告枚举实例名称
        /// </summary>
        public static string GetADTpyeName(int intvalue)
        {
            switch (intvalue)
            {
                case 1: return "头部横幅广告";
                case 2: return "尾部横幅广告";
                case 3: return "页内文字广告";
                case 4: return "信息页内广告";
                case 5: return "浮动广告";
                case 6: return "对联广告";
                case 7: return "首页头部活动广告";
                case 8: return "首页页内文字广告";
                case 9: return "首页页内轮显图片广告";
                case 10: return "首页页内图片广告";
                case 11: return "淘之购首页头部广告";
                case 12: return "淘之购首页广告";
                case 13: return "淘之购潮货汇广告";
                case 14: return "淘之购品牌馆广告";
                case 15: return "淘之购女人馆广告";
                case 16: return "淘之购女人馆广告";
                case 17: return "淘之购商品列表页广告";
                case 18: return "淘之购商品详细页广告";
                default: return "";
            }
        }
        /// <summary>
        /// 获取企业类型名称
        /// </summary>
        public static string GetCompanyType(EnTypeEnum ete)
        {
            return GetCompanyType(Convert.ToInt16(ete));
        }
        /// <summary>
        /// 获取企业类型名称
        /// </summary>
        public static string GetCompanyType(int ete)
        {
            string cname = "";
            switch (ete)
            {
                case 1:
                    cname = "生产商";
                    break;
                case 2:
                    cname = "代理经销商";
                    break;
                case 3:
                    cname = "个人经销商";
                    break;
                case 4:
                    cname = "门店";
                    break;
                case 5:
                    cname = "原料商";
                    break;
                case 6:
                    cname = "分销商";
                    break;
                case 7:
                    cname = "服务站";
                    break;
                case 8:
                    cname = "其他";
                    break;
            }
            return cname;
        }
        /// <summary>
        /// 获取企业经济类型名称
        /// </summary>
        public static string GetEnCommType(CommTypeEnum cte)
        {
            return GetEnCommType(Convert.ToInt16(cte));
        }
        /// <summary>
        /// 获取企业经济类型名称
        /// </summary>
        public static string GetEnCommType(int cte)
        {
            string ecname = "";
            switch (cte)
            {
                case 1:
                    ecname = "有限责任公司";
                    break;
                case 2:
                    ecname = "股份有限公司";
                    break;
                case 3:
                    ecname = "国营公司";
                    break;
                case 4:
                    ecname = "集团公司";
                    break;
                case 5:
                    ecname = "合资企业";
                    break;
                case 6:
                    ecname = "外企";
                    break;
            }
            return ecname;
        }
        /// <summary>
        /// 获取活动专题类型
        /// </summary>
        public static string GetActivityType(ActivityType ate)
        {
            return GetActivityType(Convert.ToInt16(ate));
        }
        /// <summary>
        /// 获取活动专题类型
        /// </summary>
        public static string GetActivityType(int ate)
        {
            string atname = "";
            switch (ate)
            {
                case 1:
                    atname = "首页活动专题";
                    break;
                case 2:
                    atname = "黄页活动专题";
                    break;
                case 3:
                    atname = "名片页活动专题";
                    break;
                case 4:
                    atname = "淘之购活动专题";
                    break;
            }
            return atname;
        }
        /// <summary>
        /// 获取淘之购频道
        /// </summary>
        public static string GetTaoChanel(TaoChanel enumobj)
        {
            return GetTaoChanel(Convert.ToInt16(enumobj));
        }
          /// <summary>
        /// 获取淘之购频道
        /// </summary>
        public static string GetTaoChanel(int intobj)
        {
            switch (intobj)
            {
                case 1: return "首页频道";
                case 2: return "潮货汇";
                case 3: return "品牌馆";
                case 4: return "专题馆";
                case 5: return "信誉铺";
                case 6: return "女人馆";
                case 7: return "淘清凉";
                case 8: return "列表页";
                case 9: return "详细页";
                case 10: return "频道页";
                case 11: return "其他";
                default: return "";
            }
        }
    }

    /// <summary>
    /// 企业类型枚举
    /// </summary>
    public enum EnTypeEnum
    {
        /// <summary>
        /// 生产商
        /// </summary>
        Manufacturer = 1,
        /// <summary>
        /// 代理经销商
        /// </summary>
        Dealers = 2,
        /// <summary>
        /// 个人经销商
        /// </summary>
        IndividualDealer = 3,
        /// <summary>
        /// 门店
        /// </summary>
        Store = 4,
        /// <summary>
        /// 原料商
        /// </summary>
        MaterialSupplier = 5,
        /// <summary>
        /// 分销商
        /// </summary>
        Distributor = 6,
        /// <summary>
        /// 服务站
        /// </summary>
        Workstation = 7,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 8
    }
    /// <summary>
    /// 企业经济类型
    /// </summary>
    public enum CommTypeEnum
    {
        /// <summary>
        /// 有限责任公司
        /// </summary>
        CoLtd = 1,
        /// <summary>
        /// 股份有限公司
        /// </summary>
        CoStock = 2,
        /// <summary>
        /// 国营公司
        /// </summary>
        CoState = 3,
        /// <summary>
        /// 集团公司
        /// </summary>
        CoGroup = 4,
        /// <summary>
        /// 合资企业
        /// </summary>
        CoJoint = 5,
        /// <summary>
        /// 外企
        /// </summary>
        CoForeign = 6
    }
    /// <summary>
    /// 活动类型
    /// </summary>
    public enum ActivityType
    {
        /// <summary>
        /// 首页活动专题
        /// </summary>
        IndexActivity = 1,
        /// <summary>
        /// 黄页活动专题
        /// </summary>
        HYActivity = 2,
        /// <summary>
        /// 名片页活动专题
        /// </summary>
        CardActivity = 3,
        /// <summary>
        /// 淘之购活动专题
        /// </summary>
        TaobaoActivity = 4
    }
    /// <summary>
    /// 广告类型
    /// </summary>
    public enum AdType
    {
        /// <summary>
        /// 头部横幅广告
        /// </summary>
        HeaderAd = 1,
        /// <summary>
        /// 尾部横幅广告
        /// </summary>
        FooterAd = 2,
        /// <summary>
        /// 页内文字广告
        /// </summary>
        PageWordAd = 3,
        /// <summary>
        /// 信息页内广告
        /// </summary>
        InPostAd = 4,
        /// <summary>
        /// 浮动广告
        /// </summary>
        FloatAd = 5,
        /// <summary>
        /// 对联广告
        /// </summary>
        DoubleAd = 6,
        /// <summary>
        /// 首页头部活动广告
        /// </summary>
        IndexHeaderAd = 7,
        /// <summary>
        /// 首页页内文字广告
        /// </summary>
        IndexWordPageAd = 8,
        /// <summary>
        /// 首页页内轮显图片广告
        /// </summary>
        IndexImageTrunAd = 9,
        /// <summary>
        /// 首页页内图片广告
        /// </summary>
        IndexImageAd = 10,
        /// <summary>
        /// 淘之购首页头部广告
        /// </summary>
        TaoIndexHeaderAD = 11,
        /// <summary>
        /// 淘之购首页广告
        /// </summary>
        TaoIndexAD = 12,
        /// <summary>
        /// 淘之购潮货汇广告
        /// </summary>
        TaoTrend = 13,
        /// <summary>
        /// 淘之购品牌馆广告
        /// </summary>
        TaoBrand = 14,
        /// <summary>
        /// 淘之购信誉铺广告
        /// </summary>
        TaoCredit = 15,
        /// <summary>
        /// 淘之购女人馆广告
        /// </summary>
        TaoWomen = 16,
        /// <summary>
        /// 淘之购商品列表页广告
        /// </summary>
        TaoItemList = 17,
        /// <summary>
        /// 淘之购商品详细页广告
        /// </summary>
        TaoItemDetail = 18
    }
    /// <summary>
    /// 淘之购相关频道
    /// </summary>
    public enum TaoChanel
    {
        /// <summary>
        /// 首页频道
        /// </summary>
        Index = 1,
        /// <summary>
        /// 潮货汇
        /// </summary>
        Trend = 2,
        /// <summary>
        /// 品牌馆
        /// </summary>
        Brand = 3,
        /// <summary>
        /// 专题馆
        /// </summary>
        Topic = 4,
        /// <summary>
        /// 信誉铺
        /// </summary>
        Credit = 5,
        /// <summary>
        /// 女人馆
        /// </summary>
        Woman = 6,
        /// <summary>
        /// 淘清凉
        /// </summary>
        Amoy = 7,
        /// <summary>
        /// 列表页
        /// </summary>
        List = 8,
        /// <summary>
        /// 详细页
        /// </summary>
        Detail = 9,
        /// <summary>
        /// 频道页
        /// </summary>
        Chanel = 10,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 11
    }
}
