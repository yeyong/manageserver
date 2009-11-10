using System;

namespace SAS.Entity
{
    /// <summary>
    /// 团队信息实体
    /// </summary>
    [Serializable]
    public class TeamInfo
    {
        #region Model
        private int _teamid;
        private string _name;
        private string _teamdomain;
        private int _templateid;
        private string _builddate;
        private string _createdate;
        private string _updatedate;
        private string _imgs;
        private string _bio;
        private string _content1;
        private string _content2;
        private string _content3;
        private string _content4;
        private int _stutas;
        private int _pageviews;
        private int _displayorder;
        private string _teammember;
        private string _seokeywords;
        private string _seodescription;
        /// <summary>
        /// 团队ID
        /// </summary>
        public int TeamID
        {
            set { _teamid = value; }
            get { return _teamid; }
        }
        /// <summary>
        /// 团队名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string teamdomain
        {
            set { _teamdomain = value; }
            get { return _teamdomain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int templateid
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime buildDate
        {
            set { _builddate = value; }
            get { return _builddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime updateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string imgs
        {
            set { _imgs = value; }
            get { return _imgs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bio
        {
            set { _bio = value; }
            get { return _bio; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content1
        {
            set { _content1 = value; }
            get { return _content1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content2
        {
            set { _content2 = value; }
            get { return _content2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content3
        {
            set { _content3 = value; }
            get { return _content3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content4
        {
            set { _content4 = value; }
            get { return _content4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int stutas
        {
            set { _stutas = value; }
            get { return _stutas; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int pageviews
        {
            set { _pageviews = value; }
            get { return _pageviews; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int displayorder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string teamMember
        {
            set { _teammember = value; }
            get { return _teammember; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string seokeywords
        {
            set { _seokeywords = value; }
            get { return _seokeywords; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string seodescription
        {
            set { _seodescription = value; }
            get { return _seodescription; }
        }
        #endregion Model

    }
}
