using System;

namespace SAS.Entity
{
    /// <summary>
    /// 商品类别信息
    /// </summary>
    [Serializable]
    public class CategoryInfo
    {
        #region Model
        private int _cid = 0;
        private string _name = "";
        private int _parentid = 0;
        private string _parentlist = "";
        private string _cg_img = "";
        private int _sort;
        private string _cg_prefix = "";
        private int _cg_status;
        private int _displayorder;
        private int _haschild;
        private string _cg_relatetype = "";
        private string _cg_relateclass = "";
        private string _cg_relatebrand = "";
        private string _cg_desc = "";
        private string _cg_keyword = "";
        private int _goodcount = 0;
        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int Cid
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 父类ID(没有父类默认为0)
        /// </summary>
        public int Parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 父级列表
        /// </summary>
        public string Parentlist
        {
            set { _parentlist = value; }
            get { return _parentlist; }
        }
        /// <summary>
        /// 类别图片
        /// </summary>
        public string Cg_img
        {
            set { _cg_img = value; }
            get { return _cg_img; }
        }
        /// <summary>
        /// 类别深度(默认为1)
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 类别前缀
        /// </summary>
        public string Cg_prefix
        {
            set { _cg_prefix = value; }
            get { return _cg_prefix; }
        }
        /// <summary>
        /// 类别状态(默认0,正常;1,暂停使用;2,前台隐藏)
        /// </summary>
        public int Cg_status
        {
            set { _cg_status = value; }
            get { return _cg_status; }
        }
        /// <summary>
        /// 类别排序(默认0)
        /// </summary>
        public int Displayorder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 是否包含子类
        /// </summary>
        public int Haschild
        {
            set { _haschild = value; }
            get { return _haschild; }
        }
        /// <summary>
        /// 关联的类型或属性
        /// </summary>
        public string Cg_relatetype
        {
            set { _cg_relatetype = value; }
            get { return _cg_relatetype; }
        }
        /// <summary>
        /// 关联类别
        /// </summary>
        public string Cg_relateclass
        {
            set { _cg_relateclass = value; }
            get { return _cg_relateclass; }
        }
        /// <summary>
        /// 关联品牌
        /// </summary>
        public string Cg_relatebrand
        {
            set { _cg_relatebrand = value; }
            get { return _cg_relatebrand; }
        }
        /// <summary>
        /// 类别描述
        /// </summary>
        public string Cg_desc
        {
            set { _cg_desc = value; }
            get { return _cg_desc; }
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Cg_keyword
        {
            set { _cg_keyword = value; }
            get { return _cg_keyword; }
        }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Goodcount
        {
            set { _goodcount = value; }
            get { return _goodcount; }
        }
        #endregion Model
    }
}
