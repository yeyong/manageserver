using System;

namespace SAS.Entity
{
    /// <summary>
    /// 评论实体信息
    /// </summary>
    [Serializable]
    public class CommentInfo
    {
        #region Model
        private int _commentid;
        private int _objid;
        private string _username;
        private int _userid;
        private string _userip;
        private string _commentdate;
        private string _content;
        private int _parentid;
        private int _scored;
        /// <summary>
        /// 评论ID
        /// </summary>
        public int commentid
        {
            set { _commentid = value; }
            get { return _commentid; }
        }
        /// <summary>
        /// 评论对象ID（企业或其他对象）
        /// </summary>
        public int objid
        {
            set { _objid = value; }
            get { return _objid; }
        }
        /// <summary>
        /// 评论用户名
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 评论用户ID（默认-1，游客）
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 评论用户IP
        /// </summary>
        public string userip
        {
            set { _userip = value; }
            get { return _userip; }
        }
        /// <summary>
        /// 评论时间
        /// </summary>
        public string commentdate
        {
            set { _commentdate = value; }
            get { return _commentdate; }
        }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 上级评论
        /// </summary>
        public int parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 评论评分
        /// </summary>
        public int scored
        {
            set { _scored = value; }
            get { return _scored; }
        }
        #endregion Model
    }
}
