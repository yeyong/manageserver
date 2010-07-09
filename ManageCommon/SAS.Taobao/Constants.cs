using System;

namespace SAS.Taobao
{
    /// <summary>
    /// 公用常量类。
    /// </summary>
    public abstract class Constants
    {
        /// <summary>
        /// 默认时间格式。
        /// </summary>
        public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 获取客户端应用授权码地址。
        /// </summary>
        public const string NTW_AUTH_URL = "http://container.open.taobao.com/container?authcode=";
    }
}
