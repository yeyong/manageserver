using System;

namespace SAS.Entity
{
    /// <summary>
    /// 用户信息（系统使用）
    /// </summary>
    public class ShortUserInfo
    {
        #region Model
        private int _ps_id;
        private int _ps_en_id;
        private string _ps_name;

        private string _ps_nickname;
        private int _ps_gender;
        private string _ps_password;
        private string _ps_pay_pass;
        private string _ps_init;
        private string _ps_company;
        private string _ps_question;
        private string _ps_answer;
        private bool _ps_islock;
        private string _ps_createdate;
        private string _ps_lastlogin;
        private string _ps_lastchangepass;
        private string _ps_lockdate;
        private string _ps_email;
        private string _ps_prev_email;
        private string _ps_regip;
        private string _ps_loginip;
        private int _ps_star;
        private int _ps_credits;
        private int _ps_scores;
        private int _ps_pg_id;

        private int _ps_ug_id;

        private int _ps_tempid;
        private int _ps_isemail;
        private int _ps_bdsound;
        private int _ps_status;

        private int _ps_onlinetime;

        private bool _ps_isdetail;
        private bool _ps_iscreater;
        private int _ps_creater;
        private int _ps_newmess;
        private string _ps_lastactivity;
        private string _ps_secques;
        private int _ps_pageviews;
        private int _ps_issign;
        private ReceivePMSettingType _ps_newsletter;
        private int _ps_invisible;
        private int _ps_newpm;
        private string _ps_salt;//用来二次MD5的字段

        /// <summary>
        /// 用户ID
        /// </summary>
        public int Ps_id
        {
            set { _ps_id = value; }
            get { return _ps_id; }
        }

        /// <summary>
        /// 所属商家实体ID（0（默认），个人用户）
        /// </summary>
        public int Ps_en_id
        {
            set { _ps_en_id = value; }
            get { return _ps_en_id; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Ps_name
        {
            set { _ps_name = value; }
            get { return _ps_name; }
        }

        /// <summary>
        /// 用户性别
        /// </summary>
        public int Ps_gender
        {
            set { _ps_gender = value; }
            get { return _ps_gender; }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Ps_nickName
        {
            set { _ps_nickname = value; }
            get { return _ps_nickname; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Ps_password
        {
            set { _ps_password = value; }
            get { return _ps_password; }
        }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string Ps_pay_pass
        {
            set { _ps_pay_pass = value; }
            get { return _ps_pay_pass; }
        }

        /// <summary>
        /// 初始密码（重置密码）
        /// </summary>
        public string Ps_init
        {
            set { _ps_init = value; }
            get { return _ps_init; }
        }

        /// <summary>
        /// 所在公司
        /// </summary>
        public string Ps_company
        {
            set { _ps_company = value; }
            get { return _ps_company; }
        }

        /// <summary>
        /// 安全保护问题（加密）
        /// </summary>
        public string Ps_question
        {
            set { _ps_question = value; }
            get { return _ps_question; }
        }

        /// <summary>
        /// 安全保护答案（加密）
        /// </summary>
        public string Ps_answer
        {
            set { _ps_answer = value; }
            get { return _ps_answer; }
        }

        /// <summary>
        /// 帐户是否被锁定（0（默认），没有；1，被锁定）
        /// </summary>
        public bool Ps_isLock
        {
            set { _ps_islock = value; }
            get { return _ps_islock; }
        }

        /// <summary>
        /// 帐户创建时间
        /// </summary>
        public string Ps_createDate
        {
            set { _ps_createdate = value; }
            get { return _ps_createdate; }
        }

        /// <summary>
        /// 帐户最后一次登陆时间
        /// </summary>
        public string Ps_lastLogin
        {
            set { _ps_lastlogin = value; }
            get { return _ps_lastlogin; }
        }

        /// <summary>
        /// 最后一次修改密码时间
        /// </summary>
        public string Ps_lastChangePass
        {
            set { _ps_lastchangepass = value; }
            get { return _ps_lastchangepass; }
        }

        /// <summary>
        /// 最后一次被锁定时间
        /// </summary>
        public string Ps_lockDate
        {
            set { _ps_lockdate = value; }
            get { return _ps_lockdate; }
        }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Ps_email
        {
            set { _ps_email = value; }
            get { return _ps_email; }
        }

        /// <summary>
        /// 最后一次修改的邮箱地址
        /// </summary>
        public string Ps_prev_email
        {
            set { _ps_prev_email = value; }
            get { return _ps_prev_email; }
        }

        /// <summary>
        /// 注册IP
        /// </summary>
        public string Ps_regIP
        {
            set { _ps_regip = value; }
            get { return _ps_regip; }
        }

        /// <summary>
        /// 上一次登录IP
        /// </summary>
        public string Ps_loginIP
        {
            set { _ps_loginip = value; }
            get { return _ps_loginip; }
        }

        /// <summary>
        /// 用户星级
        /// </summary>
        public int Ps_star
        {
            set { _ps_star = value; }
            get { return _ps_star; }
        }

        /// <summary>
        /// 信誉度
        /// </summary>
        public int Ps_credits
        {
            set { _ps_credits = value; }
            get { return _ps_credits; }
        }

        /// <summary>
        /// 积分
        /// </summary>
        public int Ps_scores
        {
            set { _ps_scores = value; }
            get { return _ps_scores; }
        }

        /// <summary>
        /// 用户组别ID
        /// </summary>
        public int Ps_pg_id
        {
            set { _ps_pg_id = value; }
            get { return _ps_pg_id; }
        }

        /// <summary>
        /// 用户组ID
        /// </summary>
        public int Ps_ug_id
        {
            set { _ps_ug_id = value; }
            get { return _ps_ug_id; }
        }

        /// <summary>
        /// 模板ID（默认0，默认模板）
        /// </summary>
        public int Ps_tempID
        {
            set { _ps_tempid = value; }
            get { return _ps_tempid; }
        }

        /// <summary>
        /// 是否显示邮箱（默认1，显示；0，不显示）
        /// </summary>
        public int Ps_isEmail
        {
            set { _ps_isemail = value; }
            get { return _ps_isemail; }
        }

        /// <summary>
        /// 弹出消息声音
        /// </summary>
        public int Ps_bdSound
        {
            set { _ps_bdsound = value; }
            get { return _ps_bdsound; }
        }

        /// <summary>
        /// 状态（默认0，不在线；1，在线；2，隐身）
        /// </summary>
        public int Ps_status
        {
            set { _ps_status = value; }
            get { return _ps_status; }
        }

        /// <summary>
        /// 在线时长
        /// </summary>
        public int Ps_onlinetime
        {
            set { _ps_onlinetime = value; }
            get { return _ps_onlinetime; }
        }

        /// <summary>
        /// 是否显示详细信息（默认1，显示；0，不显示）
        /// </summary>
        public bool Ps_isDetail
        {
            set { _ps_isdetail = value; }
            get { return _ps_isdetail; }
        }

        /// <summary>
        /// 是否为创始人（默认0，不是）
        /// </summary>
        public bool Ps_isCreater
        {
            set { _ps_iscreater = value; }
            get { return _ps_iscreater; }
        }

        /// <summary>
        /// 创建人（0，为自创）
        /// </summary>
        public int Ps_creater
        {
            set { _ps_creater = value; }
            get { return _ps_creater; }
        }

        /// <summary>
        /// 新短消息数
        /// </summary>
        public int Ps_newMess
        {
            set { _ps_newmess = value; }
            get { return _ps_newmess; }
        }

        /// <summary>
        /// 最后操作时间
        /// </summary>
        public string Ps_lastactivity
        {
            set { _ps_lastactivity = value; }
            get { return _ps_lastactivity; }
        }

        /// <summary>
        /// 安全提问码
        /// </summary>
        public string Ps_secques
        {
            set { _ps_secques = value.Trim(); }
            get { return _ps_secques; }
        }

        /// <summary>
        /// 页面浏览量
        /// </summary>
        public int Ps_pageviews
        {
            set { _ps_pageviews = value; }
            get { return _ps_pageviews; }
        }

        /// <summary>
        /// 是否启用个性签名
        /// </summary>
        public int Ps_issign
        {
            set { _ps_issign = value; }
            get { return _ps_issign; }
        }

        /// <summary>
        /// 短消息接收类型
        /// </summary>
        public ReceivePMSettingType Ps_newsletter
        {
            set { _ps_newsletter = value; }
            get { return _ps_newsletter; }
        }

        /// <summary>
        /// 是否隐身
        /// </summary>
        public int Ps_invisible
        {
            set { _ps_invisible = value; }
            get { return _ps_invisible; }
        }

        /// <summary>
        /// 是否有新消息
        /// </summary>
        public int Ps_newpm
        {
            set { _ps_newpm = value; }
            get { return _ps_newpm; }
        }

        /// <summary>
        /// 二次MD5加密时用到的随机值
        /// </summary>
        public string Ps_salt
        {
            set { _ps_salt = value; }
            get { return _ps_salt; }
        }

        #endregion Model
    }

    /// <summary>
    /// 用户全部信息（包括详细信息）
    /// </summary>
    public class UserInfo : ShortUserInfo
    {
        #region Model
        private int _pd_id;
        private string _pd_name;
        private string _pd_birthday;
        private string _pd_msn;
        private string _pd_qq;
        private string _pd_skype;
        private string _pd_yahoo;
        private string _pd_sign;
        private int _pd_logo;
        private string _pd_phone;
        private string _pd_mobile;
        private string _pd_website;

        private string _pd_authstr;
        private string _pd_authtime;
        private int _pd_authflag;
        private string _pd_idcard;
        private string _pd_bio;

        private int _tm_light;
        private string _tm_sx;
        private string _tm_constellation;
        private string _tm_education;
        private string _tm_professional;
        private string _tm_specialty;
        private string _tm_hobby;
        private int _tm_teamage;
        private string _tm_image;
        private string _tm_imgbak;
        private string _tm_smallimg;
        private string _tm_sign;
        private string _tm_selfdesc;
        private string _tm_selfenjoy;
        private string _tm_figure;
        private string _tm_location;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int Pd_id
        {
            set { _pd_id = value; }
            get { return _pd_id; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Pd_name
        {
            set { _pd_name = value; }
            get { return _pd_name; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Pd_birthday
        {
            set { _pd_birthday = value; }
            get { return _pd_birthday; }
        }

        /// <summary>
        /// MSN
        /// </summary>
        public string Pd_MSN
        {
            set { _pd_msn = value; }
            get { return _pd_msn; }
        }

        /// <summary>
        /// QQ
        /// </summary>
        public string Pd_QQ
        {
            set { _pd_qq = value; }
            get { return _pd_qq; }
        }

        /// <summary>
        /// Skype
        /// </summary>
        public string Pd_Skype
        {
            set { _pd_skype = value; }
            get { return _pd_skype; }
        }

        /// <summary>
        /// Yahoo
        /// </summary>
        public string Pd_Yahoo
        {
            set { _pd_yahoo = value; }
            get { return _pd_yahoo; }
        }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string Pd_sign
        {
            set { _pd_sign = value; }
            get { return _pd_sign; }
        }

        /// <summary>
        /// 头像ID
        /// </summary>
        public int Pd_logo
        {
            set { _pd_logo = value; }
            get { return _pd_logo; }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Pd_phone
        {
            set { _pd_phone = value; }
            get { return _pd_phone; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Pd_mobile
        {
            set { _pd_mobile = value; }
            get { return _pd_mobile; }
        }

        /// <summary>
        /// 个人主页
        /// </summary>
        public string Pd_website
        {
            set { _pd_website = value; }
            get { return _pd_website; }
        }

        /// <summary>
        /// 用户验证码
        /// </summary>
        public string Pd_authstr
        {
            set { _pd_authstr = value; }
            get { return _pd_authstr; }
        }

        /// <summary>
        /// 用户验证时间
        /// </summary>
        public string Pd_authtime
        {
            set { _pd_authtime = value; }
            get { return _pd_authtime; }
        }

        /// <summary>
        /// 使用标志(0,未使用;1 用户邮箱验证及用户信息激活, 2 用户密码找回)
        /// </summary>
        public int Pd_authflag
        {
            set { _pd_authflag = value; }
            get { return _pd_authflag; }
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Pd_idcard
        {
            set { _pd_idcard = value; }
            get { return _pd_idcard; }
        }

        /// <summary>
        /// 用户简介
        /// </summary>
        public string Pd_bio
        {
            set { _pd_bio = value; }
            get { return _pd_bio; }
        }

        /// <summary>
        /// 星光亮度
        /// </summary>
        public int Tm_light
        {
            set { _tm_light = value; }
            get { return _tm_light; }
        }
        /// <summary>
        /// 生肖
        /// </summary>
        public string Tm_sx
        {
            set { _tm_sx = value; }
            get { return _tm_sx; }
        }
        /// <summary>
        /// 星座
        /// </summary>
        public string Tm_constellation
        {
            set { _tm_constellation = value; }
            get { return _tm_constellation; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string Tm_education
        {
            set { _tm_education = value; }
            get { return _tm_education; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Tm_professional
        {
            set { _tm_professional = value; }
            get { return _tm_professional; }
        }
        /// <summary>
        /// 特长
        /// </summary>
        public string Tm_specialty
        {
            set { _tm_specialty = value; }
            get { return _tm_specialty; }
        }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Tm_hobby
        {
            set { _tm_hobby = value; }
            get { return _tm_hobby; }
        }
        /// <summary>
        /// 团队年龄
        /// </summary>
        public int Tm_teamage
        {
            set { _tm_teamage = value; }
            get { return _tm_teamage; }
        }
        /// <summary>
        /// 形象照片
        /// </summary>
        public string Tm_image
        {
            set { _tm_image = value; }
            get { return _tm_image; }
        }
        /// <summary>
        /// 形象背景
        /// </summary>
        public string Tm_imgbak
        {
            set { _tm_imgbak = value; }
            get { return _tm_imgbak; }
        }
        /// <summary>
        /// 列表图
        /// </summary>
        public string Tm_smallimg
        {
            set { _tm_smallimg = value; }
            get { return _tm_smallimg; }
        }
        /// <summary>
        /// 星系意义
        /// </summary>
        public string Tm_sign
        {
            set { _tm_sign = value; }
            get { return _tm_sign; }
        }
        /// <summary>
        /// 自我描述
        /// </summary>
        public string Tm_selfdesc
        {
            set { _tm_selfdesc = value; }
            get { return _tm_selfdesc; }
        }
        /// <summary>
        /// 自我畅享
        /// </summary>
        public string Tm_selfenjoy
        {
            set { _tm_selfenjoy = value; }
            get { return _tm_selfenjoy; }
        }
        /// <summary>
        /// 身材
        /// </summary>
        public string Tm_figure
        {
            set { _tm_figure = value; }
            get { return _tm_figure; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Tm_location
        {
            set { _tm_location = value; }
            get { return _tm_location; }
        }
        #endregion Model
    }
}
