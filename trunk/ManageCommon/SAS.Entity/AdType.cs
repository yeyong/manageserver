using System;

namespace SAS.Entity
{
    /// <summary>
    /// 广告类型枚举
    /// </summary>
    public enum AdType
    {
        /// <summary>
        /// 头部横幅广告
        /// </summary>
        HeaderAD = 1,
        /// <summary>
        /// 尾部横幅广告
        /// </summary>
        FooterAD = 2,
        /// <summary>
        /// 页内文字广告
        /// </summary>
        PageWordAd = 3,
        /// <summary>
        /// 详述页内广告
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
        /// Silverlight媒体广告
        /// </summary>
        MediaAd = 7,
        /// <summary>
        /// 详述页通栏广告
        /// </summary>
        PostLeaderboardAd = 8,
        /// <summary>
        /// 分类间广告
        /// </summary>
        InForumAd = 9
    }
}
