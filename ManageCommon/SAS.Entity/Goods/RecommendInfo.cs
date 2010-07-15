using System;

namespace SAS.Entity
{
    /// <summary>
	/// 推荐信息实体
	/// </summary>
    [Serializable]
    public class RecommendInfo
    {
        #region Model
        private int _id;
        private string _ctitle;
        private int _ctype;
        private int _relatechanel;
        private int _relatecategory;
        private string _ccontent;
        private string _createdatetime;
        private string _updatedatetime;
        /// <summary>
        /// 推荐ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 推荐标题
        /// </summary>
        public string ctitle
        {
            set { _ctitle = value; }
            get { return _ctitle; }
        }
        /// <summary>
        /// 推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）
        /// </summary>
        public int ctype
        {
            set { _ctype = value; }
            get { return _ctype; }
        }
        /// <summary>
        /// 相关频道
        /// </summary>
        public int relatechanel
        {
            set { _relatechanel = value; }
            get { return _relatechanel; }
        }
        /// <summary>
        /// 相关类别
        /// </summary>
        public int relatecategory
        {
            set { _relatecategory = value; }
            get { return _relatecategory; }
        }
        /// <summary>
        /// 推荐内容
        /// </summary>
        public string ccontent
        {
            set { _ccontent = value; }
            get { return _ccontent; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createdatetime
        {
            set { _createdatetime = value; }
            get { return _createdatetime; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public string updatedatetime
        {
            set { _updatedatetime = value; }
            get { return _updatedatetime; }
        }
        #endregion Model
    }
}
