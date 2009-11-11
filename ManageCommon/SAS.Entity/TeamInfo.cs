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
        private string _creater;
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
        /// 团队域名
        /// </summary>
        public string Teamdomain
        {
            set { _teamdomain = value; }
            get { return _teamdomain; }
        }
        /// <summary>
        /// 模板id
        /// </summary>
        public int Templateid
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 成立时间
        /// </summary>
        public string BuildDate
        {
            set { _builddate = value; }
            get { return _builddate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 团队图片地址
        /// </summary>
        public string Imgs
        {
            set { _imgs = value; }
            get { return _imgs; }
        }
        /// <summary>
        /// 团队简述
        /// </summary>
        public string Bio
        {
            set { _bio = value; }
            get { return _bio; }
        }
        /// <summary>
        /// 团队意义
        /// </summary>
        public string Content1
        {
            set { _content1 = value; }
            get { return _content1; }
        }
        /// <summary>
        /// 团队工作方向和工作内容
        /// </summary>
        public string Content2
        {
            set { _content2 = value; }
            get { return _content2; }
        }
        /// <summary>
        /// 人员职责和基本构成
        /// </summary>
        public string Content3
        {
            set { _content3 = value; }
            get { return _content3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content4
        {
            set { _content4 = value; }
            get { return _content4; }
        }
        /// <summary>
        /// 团队状态（默认1，正常；0，停用）
        /// </summary>
        public int Stutas
        {
            set { _stutas = value; }
            get { return _stutas; }
        }
        /// <summary>
        /// 查看次数
        /// </summary>
        public int Pageviews
        {
            set { _pageviews = value; }
            get { return _pageviews; }
        }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Displayorder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 成员名（逗号分割）
        /// </summary>
        public string TeamMember
        {
            set { _teammember = value; }
            get { return _teammember; }
        }
        /// <summary>
        /// 搜索优化关键字
        /// </summary>
        public string Seokeywords
        {
            set { _seokeywords = value; }
            get { return _seokeywords; }
        }
        /// <summary>
        /// 搜索优化描述信息
        /// </summary>
        public string Seodescription
        {
            set { _seodescription = value; }
            get { return _seodescription; }
        }
        /// <summary>
        /// 创建人（可以更改）拥有修改团队信息权限
        /// </summary>
        public string Creater
        {
            set { _creater = value; }
            get { return _creater; }
        }
        #endregion Model

    }
}
