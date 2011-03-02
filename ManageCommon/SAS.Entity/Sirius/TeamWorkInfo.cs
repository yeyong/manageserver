using System;

namespace SAS.Entity
{ 
    /// <summary>
    /// 团队成果实体
    /// </summary>
    [Serializable]
    public class TeamWorkInfo
    {
        #region Model
        private int _id;
        private string _name;
        private string _start;
        private string _end;
        private string _worddesc;
        private string _url;
        private string _img;
        private string _imgbak;
        private int _teamid;
        private string _members;
        /// <summary>
        /// 成果ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 成果名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 作品开始时间
        /// </summary>
        public string Start
        {
            set { _start = value; }
            get { return _start; }
        }
        /// <summary>
        /// 作品结束时间
        /// </summary>
        public string End
        {
            set { _end = value; }
            get { return _end; }
        }
        /// <summary>
        /// 作品描述
        /// </summary>
        public string Worddesc
        {
            set { _worddesc = value; }
            get { return _worddesc; }
        }
        /// <summary>
        /// 作品地址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 作品列表图
        /// </summary>
        public string Img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 作品背景图
        /// </summary>
        public string Imgbak
        {
            set { _imgbak = value; }
            get { return _imgbak; }
        }
        /// <summary>
        /// 作品参与团队
        /// </summary>
        public int Teamid
        {
            set { _teamid = value; }
            get { return _teamid; }
        }
        /// <summary>
        /// 作品参与人员
        /// </summary>
        public string Members
        {
            set { _members = value; }
            get { return _members; }
        }
        #endregion Model
    }
}
