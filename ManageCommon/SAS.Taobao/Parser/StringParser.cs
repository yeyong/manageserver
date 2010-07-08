namespace NTW.Taobao.Parser
{
    /// <summary>
    /// 不作任何处理，直接返回响应字符串。
    /// </summary>
    public class StringParser : INTWParser<string>
    {
        #region INTWParser<string> Members

        public string Parse(string body)
        {
            return body;
        }

        #endregion
    }
}
