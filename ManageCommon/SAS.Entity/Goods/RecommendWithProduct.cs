using System;

using SAS.Entity.Domain;

namespace SAS.Entity
{
    /// <summary>
    /// 推荐联合商品信息实体
    /// </summary>
    [Serializable]
    public class RecommendWithProduct
    {
        private int _id;
        private string _ctitle;
        private System.Collections.Generic.List<TaobaokeItem> _item;
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
        /// 商品信息实体
        /// </summary>
        public System.Collections.Generic.List<TaobaokeItem> item
        {
            set { _item = value; }
            get { return _item; }
        }
    }
}
