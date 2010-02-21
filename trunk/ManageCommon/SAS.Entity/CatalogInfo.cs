using System;

namespace SAS.Entity
{
    /// <summary>
    /// 行业类别
    /// </summary>
    [Serializable]
    public class CatalogInfo
    {
        #region Model
        private int _id;
        private string _name;
        private int _parentid;
        private string _parentlist;
        private int _sort;
        private string _cllogo;
        private int _displayorder;
        private int _haschild;
        private int _companycount;
        /// <summary>
        /// 类别ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 行业类别名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 上级ID
        /// </summary>
        public int parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 上级ID列表
        /// </summary>
        public string parentlist
        {
            set { _parentlist = value; }
            get { return _parentlist; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 行业类别Logo
        /// </summary>
        public string cllogo
        {
            set { _cllogo = value; }
            get { return _cllogo; }
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
        /// 是否包含子类别
        /// </summary>
        public int haschild
        {
            set { _haschild = value; }
            get { return _haschild; }
        }
        /// <summary>
        /// 类别下企业数量
        /// </summary>
        public int companycount
        {
            set { _companycount = value; }
            get { return _companycount; }
        }
        #endregion Model
    }
}
