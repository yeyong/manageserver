using System;

namespace SAS.Common
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    public class SASException : Exception
    {
        public SASException()
        {

        }

        public SASException(string msg)
            : base(msg)
        {

        }
    }
}
