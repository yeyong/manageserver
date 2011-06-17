using System;

namespace SAS.Entity.InfoPlatform
{
    /// <summary>
    /// 信息发布平台会员信息实体
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        #region Model
        private int _userid;
        private string _loginname;
        private string _sex;
        private string _password;
        private string _oldPassword;
        private string _question;
        private string _answer;
        private int _gradeid;
        private bool _lockuedup;
        private string _linkname;
        private string _mobilephone;
        private string _email;
        private string _qq;
        private string _msn;
        private string _tel_international;
        private string _tel_districtnumber;
        private string _tel_telephone;
        private string _tel_ext;
        private string _fax_international;
        private string _fax_districtnumber;
        private string _fax_telephone;
        private string _fax_ext;
        private string _department;
        private string _position;
        private DateTime _regdate;
        private string _companyname;
        private string _companynature;
        private string _businessmodel;
        private string _dealinadd;
        private string _product;
        private string _industry;
        private string _summary;
        private bool _verify;
        private string _country;
        private string _province;
        private string _city;
        private string _area;
        private string _street;
        private string _postalcode;
        private string _url;
        private int _capital;
        private DateTime _established;
        private string _registeraddress;
        private string _corporate;
        private DateTime _startdate;
        private DateTime _enddate;
        private string _logo;
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword
        {
            set { _oldPassword = value; }
            get { return _oldPassword; }
        }
        /// <summary>
        /// 安全问题
        /// </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }
        /// <summary>
        /// 安全答案
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 会员等级ID
        /// </summary>
        public int GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Lockuedup
        {
            set { _lockuedup = value; }
            get { return _lockuedup; }
        }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LinkName
        {
            set { _linkname = value; }
            get { return _linkname; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// MSN
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 国际区号
        /// </summary>
        public string Tel_International
        {
            set { _tel_international = value; }
            get { return _tel_international; }
        }
        /// <summary>
        /// 区号
        /// </summary>
        public string Tel_DistrictNumber
        {
            set { _tel_districtnumber = value; }
            get { return _tel_districtnumber; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel_Telephone
        {
            set { _tel_telephone = value; }
            get { return _tel_telephone; }
        }
        /// <summary>
        /// 分机号
        /// </summary>
        public string Tel_Ext
        {
            set { _tel_ext = value; }
            get { return _tel_ext; }
        }

        /// <summary>
        /// 传真国际区号
        /// </summary>
        public string Fax_International
        {
            set { _fax_international = value; }
            get { return _fax_international; }
        }
        /// <summary>
        /// 传真区号
        /// </summary>
        public string Fax_DistrictNumber
        {
            set { _fax_districtnumber = value; }
            get { return _fax_districtnumber; }
        }
        /// <summary>
        /// 传真电话
        /// </summary>
        public string Fax_Telephone
        {
            set { _fax_telephone = value; }
            get { return _fax_telephone; }
        }
        /// <summary>
        /// 传真分机号
        /// </summary>
        public string Fax_Ext
        {
            set { _fax_ext = value; }
            get { return _fax_ext; }
        }


        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegDate
        {
            set { _regdate = value; }
            get { return _regdate; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string CompanyNature
        {
            set { _companynature = value; }
            get { return _companynature; }
        }
        /// <summary>
        /// 经营模式
        /// </summary>
        public string BusinessModel
        {
            set { _businessmodel = value; }
            get { return _businessmodel; }
        }
        /// <summary>
        /// 主要经营地址
        /// </summary>
        public string DealinAdd
        {
            set { _dealinadd = value; }
            get { return _dealinadd; }
        }
        /// <summary>
        /// 主营产品
        /// </summary>
        public string Product
        {
            set { _product = value; }
            get { return _product; }
        }
        /// <summary>
        /// 主营行业
        /// </summary>
        public string Industry
        {
            set { _industry = value; }
            get { return _industry; }
        }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 审核
        /// </summary>
        public bool Verify
        {
            set { _verify = value; }
            get { return _verify; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 地区
        /// </summary>
        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 街道地址
        /// </summary>
        public string Street
        {
            set { _street = value; }
            get { return _street; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Postalcode
        {
            set { _postalcode = value; }
            get { return _postalcode; }
        }
        /// <summary>
        /// 公司网址
        /// </summary>
        public string URL
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 注册资本
        /// </summary>
        public int Capital
        {
            set { _capital = value; }
            get { return _capital; }
        }
        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime Established
        {
            set { _established = value; }
            get { return _established; }
        }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterAddress
        {
            set { _registeraddress = value; }
            get { return _registeraddress; }
        }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string Corporate
        {
            set { _corporate = value; }
            get { return _corporate; }
        }
        /// <summary>
        /// 服务开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 服务截至时间
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 公司LOGO
        /// </summary>
        public string Logo
        {
            set { _logo = value; }
            get { return _logo; }
        }
        #endregion Model
    }
}
