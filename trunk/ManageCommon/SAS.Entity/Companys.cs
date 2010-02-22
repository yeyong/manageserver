using System;

namespace SAS.Entity
{
    /// <summary>
    /// 企业信息实体
    /// </summary>
    [Serializable]
    public class Companys
    {
        #region Model
        private int _en_id;
        private string _en_name;
        private string _en_main;
        private int _en_type;
        private int _en_enco;
        private int _en_sell;
        private string _en_address;
        private int _en_areas;
        private string _en_desc;
        private string _en_post;
        private string _en_mobile;
        private string _en_phone;
        private string _en_fax;
        private string _en_mail;
        private string _en_web;
        private string _en_corp;
        private string _en_contact;
        private string _en_update;
        private int _en_status;
        private string _en_reason;
        private int _en_level;
        private int _en_accesses;
        private int _en_credits;
        private string _en_logo;
        private string _en_music;
        private string _reg_capital;
        private string _reg_address;
        private string _reg_code;
        private string _reg_organ;
        private string _reg_year;
        private string _reg_date;
        private string _en_builddate;
        private int _en_visble;
        private string _en_createdate;
        private string _en_cataloglist;
        /// <summary>
        /// 企业ID
        /// </summary>
        public int en_id
        {
            set { _en_id = value; }
            get { return _en_id; }
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string en_name
        {
            set { _en_name = value; }
            get { return _en_name; }
        }
        /// <summary>
        /// 主营商品(选择产品类别,可作多选)
        /// </summary>
        public string en_main
        {
            set { _en_main = value; }
            get { return _en_main; }
        }
        /// <summary>
        /// 企业类别(0(默认),生产商;1,代理经销商;2,个人经销商;3,门店;4,原料商;5,分销商;6,服务站;7,其他)
        /// </summary>
        public int en_type
        {
            set { _en_type = value; }
            get { return _en_type; }
        }
        /// <summary>
        /// 企业经济类型(0(默认),有限责任公司;1,股份有限公司;2,国营;3,集团公司)
        /// </summary>
        public int en_enco
        {
            set { _en_enco = value; }
            get { return _en_enco; }
        }
        /// <summary>
        /// 产销数量(吨)
        /// </summary>
        public int en_sell
        {
            set { _en_sell = value; }
            get { return _en_sell; }
        }
        /// <summary>
        /// 公司详细地址
        /// </summary>
        public string en_address
        {
            set { _en_address = value; }
            get { return _en_address; }
        }
        /// <summary>
        /// 所在区域（省市等）
        /// </summary>
        public int en_areas
        {
            set { _en_areas = value; }
            get { return _en_areas; }
        }
        /// <summary>
        /// 企业概述
        /// </summary>
        public string en_desc
        {
            set { _en_desc = value; }
            get { return _en_desc; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string en_post
        {
            set { _en_post = value; }
            get { return _en_post; }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string en_mobile
        {
            set { _en_mobile = value; }
            get { return _en_mobile; }
        }
        /// <summary>
        /// 公司联系电话（固话）
        /// </summary>
        public string en_phone
        {
            set { _en_phone = value; }
            get { return _en_phone; }
        }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string en_fax
        {
            set { _en_fax = value; }
            get { return _en_fax; }
        }
        /// <summary>
        /// 企业邮箱
        /// </summary>
        public string en_mail
        {
            set { _en_mail = value; }
            get { return _en_mail; }
        }
        /// <summary>
        /// 企业网址
        /// </summary>
        public string en_web
        {
            set { _en_web = value; }
            get { return _en_web; }
        }
        /// <summary>
        /// 企业法人
        /// </summary>
        public string en_corp
        {
            set { _en_corp = value; }
            get { return _en_corp; }
        }
        /// <summary>
        /// 主要联系人
        /// </summary>
        public string en_contact
        {
            set { _en_contact = value; }
            get { return _en_contact; }
        }
        /// <summary>
        /// 最近信息更新时间
        /// </summary>
        public string en_update
        {
            set { _en_update = value; }
            get { return _en_update; }
        }
        /// <summary>
        /// 企业状态((默认1),服务审批中;0,审批未通过;2,审批通过)
        /// </summary>
        public int en_status
        {
            set { _en_status = value; }
            get { return _en_status; }
        }
        /// <summary>
        /// 审批未通过原因
        /// </summary>
        public string en_reason
        {
            set { _en_reason = value; }
            get { return _en_reason; }
        }
        /// <summary>
        /// 企业级别认定(0(默认),普通级别;1,会员级;2,高级会员级;3,贵宾会员级);权限设定时使用
        /// </summary>
        public int en_level
        {
            set { _en_level = value; }
            get { return _en_level; }
        }
        /// <summary>
        /// 企业信息浏览量(站内统计备用)
        /// </summary>
        public int en_accesses
        {
            set { _en_accesses = value; }
            get { return _en_accesses; }
        }
        /// <summary>
        /// 企业信誉度
        /// </summary>
        public int en_credits
        {
            set { _en_credits = value; }
            get { return _en_credits; }
        }
        /// <summary>
        /// 企业logo
        /// </summary>
        public string en_logo
        {
            set { _en_logo = value; }
            get { return _en_logo; }
        }
        /// <summary>
        /// 企业背景音乐
        /// </summary>
        public string en_music
        {
            set { _en_music = value; }
            get { return _en_music; }
        }
        /// <summary>
        /// 注册资本
        /// </summary>
        public string reg_capital
        {
            set { _reg_capital = value; }
            get { return _reg_capital; }
        }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string reg_address
        {
            set { _reg_address = value; }
            get { return _reg_address; }
        }
        /// <summary>
        /// 注册号
        /// </summary>
        public string reg_code
        {
            set { _reg_code = value; }
            get { return _reg_code; }
        }
        /// <summary>
        /// 注册机关
        /// </summary>
        public string reg_organ
        {
            set { _reg_organ = value; }
            get { return _reg_organ; }
        }
        /// <summary>
        /// 最近年检时间
        /// </summary>
        public string reg_year
        {
            set { _reg_year = value; }
            get { return _reg_year; }
        }
        /// <summary>
        /// 营业期限
        /// </summary>
        public string reg_date
        {
            set { _reg_date = value; }
            get { return _reg_date; }
        }
        /// <summary>
        /// 公司成立时间
        /// </summary>
        public string en_builddate
        {
            set { _en_builddate = value; }
            get { return _en_builddate; }
        }
        /// <summary>
        /// 企业开启状态（默认0，关闭；1，开启）
        /// </summary>
        public int en_visble
        {
            set { _en_visble = value; }
            get { return _en_visble; }
        }
        /// <summary>
        /// 信息创建时间
        /// </summary>
        public string en_createdate
        {
            set { _en_createdate = value; }
            get { return _en_createdate; }
        }
        /// <summary>
        /// 行业类别列表
        /// </summary>
        public string en_cataloglist
        {
            set { _en_cataloglist = value; }
            get { return _en_cataloglist; }
        }
        #endregion Model
    }
}
