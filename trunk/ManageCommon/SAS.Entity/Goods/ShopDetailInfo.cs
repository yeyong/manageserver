using System;

namespace SAS.Entity
{
    /// <summary>
    /// 店铺详细信息
    /// </summary>
    [Serializable]
    public class ShopDetailInfo
    {
        #region Model
        private long _sid;
        private long _user_id;
        private long _cid;
        private string _nick;
        private string _title;
        private string _item_score;
        private string _service_score;
        private string _delivery_score;
        private string _shop_desc;
        private string _bulletin;
        private string _pic_path;
        private string _created;
        private string _modified;
        private string _promoted_type;
        private bool _consumer_protection;
        private string _shop_status;
        private string _shop_type;
        private int _shop_level;
        private int _shop_score;
        private long _total_num;
        private long _good_num;
        private string _shop_country = "";
        private string _shop_province = "";
        private string _shop_city = "";
        private string _shop_address = "";
        private string _commission_rate;
        private string _click_url;
        private string _relategoods = "";
        /// <summary>
        /// 淘宝店铺ID
        /// </summary>
        public long sid
        {
            set { _sid = value; }
            get { return _sid; }
        }
        /// <summary>
        /// 店铺用户ID
        /// </summary>
        public long user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 所属类目ID
        /// </summary>
        public long cid
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string nick
        {
            set { _nick = value; }
            get { return _nick; }
        }
        /// <summary>
        /// 店铺标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 商品描述评分
        /// </summary>
        public string item_score
        {
            set { _item_score = value; }
            get { return _item_score; }
        }
        /// <summary>
        /// 服务态度评分
        /// </summary>
        public string service_score
        {
            set { _service_score = value; }
            get { return _service_score; }
        }
        /// <summary>
        /// 发货速度评分
        /// </summary>
        public string delivery_score
        {
            set { _delivery_score = value; }
            get { return _delivery_score; }
        }
        /// <summary>
        /// 店铺描述
        /// </summary>
        public string shop_desc
        {
            set { _shop_desc = value; }
            get { return _shop_desc; }
        }
        /// <summary>
        /// 店铺公告
        /// </summary>
        public string bulletin
        {
            set { _bulletin = value; }
            get { return _bulletin; }
        }
        /// <summary>
        /// 店标地址。返回相对路径，可以用"http://logo.taobao.com/shop-logo"来拼接成绝对路径
        /// </summary>
        public string pic_path
        {
            set { _pic_path = value; }
            get { return _pic_path; }
        }
        /// <summary>
        /// 开店时间
        /// </summary>
        public string created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modified
        {
            set { _modified = value; }
            get { return _modified; }
        }
        /// <summary>
        /// 有无实名认证。可选值:authentication(实名认证),not authentication(没有认证)
        /// </summary>
        public string promoted_type
        {
            set { _promoted_type = value; }
            get { return _promoted_type; }
        }
        /// <summary>
        /// 是否参加消保
        /// </summary>
        public bool consumer_protection
        {
            set { _consumer_protection = value; }
            get { return _consumer_protection; }
        }
        /// <summary>
        /// 状态。可选值:normal(正常),inactive(未激活),delete(删除),reeze(冻结),supervise(监管)
        /// </summary>
        public string shop_status
        {
            set { _shop_status = value; }
            get { return _shop_status; }
        }
        /// <summary>
        /// 用户类型。可选值:B(B商家),C(C商家)
        /// </summary>
        public string shop_type
        {
            set { _shop_type = value; }
            get { return _shop_type; }
        }
        /// <summary>
        /// 信誉等级
        /// </summary>
        public int shop_level
        {
            set { _shop_level = value; }
            get { return _shop_level; }
        }
        /// <summary>
        /// 信用总分
        /// </summary>
        public int shop_score
        {
            set { _shop_score = value; }
            get { return _shop_score; }
        }
        /// <summary>
        /// 收到的评价总条数。取值范围:大于零的整数
        /// </summary>
        public long total_num
        {
            set { _total_num = value; }
            get { return _total_num; }
        }
        /// <summary>
        /// 收到的好评总条数。取值范围:大于零的整数
        /// </summary>
        public long good_num
        {
            set { _good_num = value; }
            get { return _good_num; }
        }
        /// <summary>
        /// 所在国家
        /// </summary>
        public string shop_country
        {
            set { _shop_country = value; }
            get { return _shop_country; }
        }
        /// <summary>
        /// 所在省份
        /// </summary>
        public string shop_province
        {
            set { _shop_province = value; }
            get { return _shop_province; }
        }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string shop_city
        {
            set { _shop_city = value; }
            get { return _shop_city; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string shop_address
        {
            set { _shop_address = value; }
            get { return _shop_address; }
        }
        /// <summary>
        /// 淘宝客店铺佣金比率
        /// </summary>
        public string commission_rate
        {
            set { _commission_rate = value; }
            get { return _commission_rate; }
        }
        /// <summary>
        /// 推广链接
        /// </summary>
        public string click_url
        {
            set { _click_url = value; }
            get { return _click_url; }
        }
        /// <summary>
        /// 相关商品（ID|名称|价格|图片）
        /// </summary>
        public string relategoods
        {
            set { _relategoods = value; }
            get { return _relategoods; }
        }
        #endregion Model
    }
}
