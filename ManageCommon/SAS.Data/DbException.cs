using System;
using System.Text;

namespace SAS.Data
{
    public class DbException : SAS.Common.SASException
    {
        public DbException(string message)
            : base(message)
        {
        }

        public int Number
        {
            get { return 0; }
        }
    }
}
