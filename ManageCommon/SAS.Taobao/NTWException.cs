using System;
using System.Runtime.Serialization;

namespace SAS.Taobao
{
    /// <summary>
    /// 客户端异常。
    /// </summary>
    public class NTWException : Exception
    {
        private string errorCode;
        private string errorMsg;

        public NTWException()
            : base()
        {
        }

        public NTWException(string message)
            : base(message)
        {
        }

        protected NTWException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public NTWException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NTWException(string errorCode, string errorMsg)
            : base(errorCode + ":" + errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public string ErrorCode
        {
            get { return this.errorCode; }
        }

        public string ErrorMsg
        {
            get { return this.errorMsg; }
        }
    }
}
