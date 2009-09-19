using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Entity
{
    [Serializable]
    public class IpInfo
    {
        private int _id = 0;
        private int _ip1 = 0;
        private int _ip2 = 0;
        private int _ip3 = 0;
        private int _ip4 = 0;
        private string _username = "";
        private string _dateline = "";
        private string _expiration = "";
        private string _location = "";

        /// <summary>
        /// 禁用表ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// ip1
        /// </summary>
        public int Ip1
        {
            get { return _ip1; }
            set { _ip1 = value; }
        }

        /// <summary>
        /// ip2
        /// </summary>
        public int Ip2
        {
            get { return _ip2; }
            set { _ip2 = value; }
        }

        /// <summary>
        /// ip3
        /// </summary>
        public int Ip3
        {
            get { return _ip3; }
            set { _ip3 = value; }
        }

        /// <summary>
        /// ip4
        /// </summary>
        public int Ip4
        {
            get { return _ip4; }
            set { _ip4 = value; }
        }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string Dateline
        {
            get { return _dateline; }
            set { _dateline = value; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string Expiration
        {
            get { return _expiration; }
            set { _expiration = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
