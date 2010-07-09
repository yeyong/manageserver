using SAS.Taobao.Parser;
using SAS.Taobao.Request;

namespace SAS.Taobao
{
    /// <summary>
    /// 客户端。
    /// </summary>
    public interface INTWClient
    {
        /// <summary>
        /// 执行TOP公开API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的TOP API请求</param>
        /// <param name="parser">与API请求响应相对应的解释器接口实现</param>
        /// <returns>领域对象</returns>
        T Execute<T>(INTWRequest request, INTWParser<T> parser);

        /// <summary>
        /// 执行TOP隐私API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的TOP API请求</param>
        /// <param name="parser">与API请求响应相对应的解释器接口实现</param>
        /// <param name="session">用户会话码</param>
        /// <returns>领域对象</returns>
        T Execute<T>(INTWRequest request, INTWParser<T> parser, string session);
    }
}
