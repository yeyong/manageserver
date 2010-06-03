using System;

namespace SAS.Entity
{
    /// <summary>
    /// 活动专题实体信息
    /// </summary>
    [Serializable]
    public class ActivityInfo
    {
        #region Model
        private int _id;
        private string _atitle = "";
        private string _stylecode = "";
        private string _desccode = "";
        private string _scriptcode = "";
        private string _begintime;
        private string _endtime;
        private int _atype;
        private int _enabled;
        private string _seotitle = "";
        private string _seodesc = "";
        private string _seokeyword = "";
        private string _createdate;

        public ActivityInfo Clone()
        {
            return (ActivityInfo)this.MemberwiseClone();
        }
        /// <summary>
        /// 活动专题ID
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Atitle
        {
            set { _atitle = value; }
            get { return _atitle; }
        }
        /// <summary>
        /// 活动样式
        /// </summary>
        public string Stylecode
        {
            set { _stylecode = value; }
            get { return _stylecode; }
        }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string Desccode
        {
            set { _desccode = value; }
            get { return _desccode; }
        }
        /// <summary>
        /// 活动脚本
        /// </summary>
        public string Scriptcode
        {
            set { _scriptcode = value; }
            get { return _scriptcode; }
        }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string Begintime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string Endtime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 活动类型（默认1，首页活动；2，黄页活动；3，名片页活动；4，淘之购活动）
        /// </summary>
        public int Atype
        {
            set { _atype = value; }
            get { return _atype; }
        }
        /// <summary>
        /// 开启状态（默认0，关闭；1，开启）
        /// </summary>
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 搜索优化标题
        /// </summary>
        public string Seotitle
        {
            set { _seotitle = value; }
            get { return _seotitle; }
        }
        /// <summary>
        /// 搜索优化内容
        /// </summary>
        public string Seodesc
        {
            set { _seodesc = value; }
            get { return _seodesc; }
        }
        /// <summary>
        /// 搜索优化关键字
        /// </summary>
        public string Seokeyword
        {
            set { _seokeyword = value; }
            get { return _seokeyword; }
        }
        /// <summary>
        /// 活动创建时间
        /// </summary>
        public string Createdate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model
    }
}
