using System;

namespace SAS.Entity
{
    /// <summary>
    /// 商品品牌信息
    /// </summary>
    [Serializable]
    public class GoodsBrandInfo
    {
        private int _id;
        private string _bname;
        private string _spell;
        private string _website;
        private string _bcompany;
        private int _order;
        private string _logo;
        private string _img;
        private string _keyword;
        private string _shortdesc;
        private string _detaildesc;
        private int _status;
        private string _relateclass;
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string bname
        {
            set { _bname = value; }
            get { return _bname; }
        }
        /// <summary>
        /// 品牌拼音或英文拼写（前台检索时使用）
        /// </summary>
        public string spell
        {
            set { _spell = value; }
            get { return _spell; }
        }
        /// <summary>
        /// 品牌官方网址
        /// </summary>
        public string website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 品牌官方公司
        /// </summary>
        public string bcompany
        {
            set { _bcompany = value; }
            get { return _bcompany; }
        }
        /// <summary>
        /// 品牌排序
        /// </summary>
        public int order
        {
            set { _order = value; }
            get { return _order; }
        }
        /// <summary>
        /// 品牌logo
        /// </summary>
        public string logo
        {
            set { _logo = value; }
            get { return _logo; }
        }
        /// <summary>
        /// 品牌大图
        /// </summary>
        public string img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 品牌关键字(用于搜索引擎优化)
        /// </summary>
        public string keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 品牌简述
        /// </summary>
        public string shortdesc
        {
            set { _shortdesc = value; }
            get { return _shortdesc; }
        }
        /// <summary>
        /// 品牌详述
        /// </summary>
        public string detaildesc
        {
            set { _detaildesc = value; }
            get { return _detaildesc; }
        }
        /// <summary>
        /// 品牌状态(默认1,正常;0,前台不显示,暂停使用)
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 品牌相关类别
        /// </summary>
        public string relateclass
        {
            set { _relateclass = value; }
            get { return _relateclass; }
        }
    }
}
