using System;
using System.Text;

namespace SAS.Entity
{
    /// <summary>
    /// 帮助信息实体
    /// </summary>
    public class HelpInfo
    {
        private int _id;
        private string _title;
        private string _message;
        private int _pid;
        private int _orderby;

        /// <summary>
        /// 排序方式
        /// </summary>
        public int Orderby
        {

            set { _orderby = value; }
            get { return _orderby; }

        }
        /// <summary>
        /// 帮助ID
        /// </summary>
        public int Id
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        /// <summary>
        /// 帮助标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }

        }
        /// <summary>
        /// 帮助内容
        /// </summary>
        public string Message
        {
            set { _message = value; }
            get { return _message; }

        }

        /// <summary>
        /// 帖子ID
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
    }
}
