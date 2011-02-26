using System;

namespace SAS.Entity
{ 
    /// <summary>
    /// 团队活动实体
    /// </summary>
    [Serializable]
    public class TeamActInfo
    {
        #region Model
        private int _id;
        private string _name;
        private string _start;
        private string _end;
        private string _shortdesc;
        private string _img;
        private string _imgbak;
        private int _teamid;
        private int _atype;
        private string _piccollect;
        /// <summary>
        /// 活动Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string Start
        {
            set { _start = value; }
            get { return _start; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string End
        {
            set { _end = value; }
            get { return _end; }
        }
        /// <summary>
        /// 活动简述
        /// </summary>
        public string Shortdesc
        {
            set { _shortdesc = value; }
            get { return _shortdesc; }
        }
        /// <summary>
        /// 活动图片
        /// </summary>
        public string Img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 活动背景图片
        /// </summary>
        public string Imgbak
        {
            set { _imgbak = value; }
            get { return _imgbak; }
        }
        /// <summary>
        /// 发起团队id
        /// </summary>
        public int Teamid
        {
            set { _teamid = value; }
            get { return _teamid; }
        }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int Atype
        {
            set { _atype = value; }
            get { return _atype; }
        }
        /// <summary>
        /// 图片组
        /// </summary>
        public string Piccollect
        {
            set { _piccollect = value; }
            get { return _piccollect; }
        }
        #endregion Model
    }
}
