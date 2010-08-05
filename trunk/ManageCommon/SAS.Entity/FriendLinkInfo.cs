using System;

namespace SAS.Entity
{
    /// <summary>
    /// 友情链接信息
    /// </summary>
    [Serializable]
    public class FriendLinkInfo
    {
        private int _id;
        private int _displayorder;
        private string _name;
        private string _linkurl;
        private string _note;
        private string _logo;
        /// <summary>
        /// 友情ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int displayorder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 友情文字
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 友情链接
        /// </summary>
        public string linkurl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 图标
        /// </summary>
        public string logo
        {
            set { _logo = value; }
            get { return _logo; }
        }
    }
}
