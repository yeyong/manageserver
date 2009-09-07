using System;
using System.Text;

namespace SAS.Common.XML
{
    public class InvalidXmlException : SASException
    {
        public InvalidXmlException(string message)
            : base(message)
        {
        }
    }
}
