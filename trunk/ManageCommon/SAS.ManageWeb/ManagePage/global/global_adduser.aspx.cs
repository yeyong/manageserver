using System;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.PasswordMode;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_adduser : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region 初始化控件
                foreach (UserGroupInfo userGroupInfo in UserGroups.GetUserGroupList())
                    groupid.Items.Add(new ListItem(userGroupInfo.ug_name, userGroupInfo.ug_id.ToString()));
                AddUserInfo.Attributes.Add("onclick", "return IsValidPost();");
                //将积分设置数据加载到Javascript数组，在前台改变
                string scriptText = "var creditarray = new Array(";
                for (int i = 1; i < groupid.Items.Count; i++)
                {
                    scriptText += AdminUserGroups.AdminGetUserGroupInfo(Convert.ToInt32(groupid.Items[i].Value)).ug_scorehight.ToString() + ",";
                }
                scriptText = scriptText.TrimEnd(',') + ");";
                this.RegisterStartupScript("begin", "<script type='text/javascript'>" + scriptText + "</script>");
                groupid.Attributes.Add("onchange", "document.getElementById('" + credits.ClientID + "').value=creditarray[this.selectedIndex];");
                groupid.Items.RemoveAt(0);
                try
                {
                    groupid.SelectedValue = "10";
                }
                catch
                {
                    //当新手上路不存在时
                    groupid.SelectedValue = UserCredits.GetCreditsUserGroupId(0) != null ? UserCredits.GetCreditsUserGroupId(0).ug_id.ToString() : "3";
                }

                try
                {
                    UserGroupInfo _usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(Convert.ToInt32(groupid.SelectedValue));
                    credits.Text = _usergroupinfo.ug_scorehight.ToString();
                }
                catch
                {
                    ;
                }

                #endregion
            }
        }

        private void AddUserInfo_Click(object sender, EventArgs e)
        {
            #region 添加新用户信息
            if (this.CheckCookie())
            {
                if (userName.Text.Trim() == "" || password.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('用户名或密码为空,因此无法提交!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }
                if (!Utils.IsSafeSqlString(userName.Text))
                {
                    base.RegisterStartupScript("", "<script>alert('您输入的用户名包含不安全的字符,因此无法提交!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (PrivateMessages.SystemUserName == userName.Text)
                {
                    base.RegisterStartupScript("", "<script>alert('您不能创建该用户名,因为它是系统保留的用户名,请您输入其它的用户名!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (!Utils.IsValidEmail(email.Text.Trim()))
                {
                    base.RegisterStartupScript("", "<script>alert('E-mail为空或格式不正确,因此无法提交!');window.location='global_adduser.aspx';</script>");
                    return;
                }

                UserInfo userInfo = CreateUserInfo();

                if (AdminUsers.GetUserId(userName.Text) > 0)
                {
                    base.RegisterStartupScript("", "<script>alert('您所输入的用户名已被使用过, 请输入其他的用户名!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (!Users.ValidateEmail(email.Text))
                {
                    base.RegisterStartupScript("", "<script>alert('您所输入的邮箱地址已被使用过, 请输入其他的邮箱!');window.location.href='global_adduser.aspx';</script>");
                    return;
                }

                if (config.Passwordmode > 1 && PasswordModeProvider.GetInstance() != null)
                    PasswordModeProvider.GetInstance().CreateUserInfo(userInfo);
                else
                {
                    userInfo.Ps_password = Utils.MD5(userInfo.Ps_password);
                    AdminUsers.CreateUser(userInfo);
                }
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台添加用户", "用户名:" + userName.Text);

                string emailresult = null;
                if (sendemail.Checked)
                {
                    emailresult = SendEmail(email.Text);
                }
                base.RegisterStartupScript("PAGE", "window.location.href='global_usergrid.aspx';");
            }
            #endregion
        }

        private UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo();

            userInfo.Ps_name = userName.Text;
            userInfo.Ps_nickName = userName.Text;
            userInfo.Ps_password = password.Text;
            userInfo.Ps_pay_pass = password.Text;
            userInfo.Ps_init = password.Text;
            userInfo.Ps_secques = "";
            userInfo.Ps_isLock = false;
            userInfo.Ps_gender = 0;
            int selectgroupid = Convert.ToInt32(groupid.SelectedValue);
            userInfo.Ps_pg_id = AdminUserGroups.AdminGetUserGroupInfo(selectgroupid).ug_pg_id;
            userInfo.Ps_ug_id = selectgroupid;
            userInfo.Ps_company = "";
            userInfo.Ps_regIP = "";
            userInfo.Ps_createDate = Utils.GetDate();
            userInfo.Ps_lastChangePass = "";
            userInfo.Ps_lockDate = "";
            userInfo.Ps_loginIP = "";
            userInfo.Ps_lastactivity = Utils.GetDate();
            userInfo.Ps_lastLogin = Utils.GetDate();
            userInfo.Ps_onlinetime = 0;
            userInfo.Ps_pageviews = 0;
            userInfo.Ps_credits = Convert.ToInt32(credits.Text);
            userInfo.Ps_star = 0;
            userInfo.Ps_scores = 0;
            userInfo.Ps_email = email.Text;
            userInfo.Ps_prev_email = email.Text;
            userInfo.Ps_issign = 0;
            userInfo.Ps_tempID = GeneralConfigs.GetConfig().Templateid;
            userInfo.Ps_bdSound = 1;
            userInfo.Ps_isEmail = 1;
            userInfo.Ps_newsletter = (ReceivePMSettingType)7;
            userInfo.Ps_invisible = 0;
            userInfo.Ps_newpm = 0;
            userInfo.Ps_newMess = 0;
            userInfo.Ps_status = 0;
            userInfo.Ps_isDetail = true;
            userInfo.Ps_isCreater = false;
            userInfo.Ps_creater = userid;
            userInfo.Ps_salt = "0";

            userInfo.Pd_website = "";
            userInfo.Pd_QQ = "";
            userInfo.Pd_MSN = "";
            userInfo.Pd_Yahoo = "";
            userInfo.Pd_Skype = "";
            userInfo.Pd_bio = "";
            //userInfo.Avatar = reader["avatar"].ToString();
            //userInfo.Avatarwidth = TypeConverter.StrToInt(reader["avatarwidth"].ToString(), 0);
            //userInfo.Avatarheight = TypeConverter.StrToInt(reader["avatarheight"].ToString(), 0);
            userInfo.Pd_sign = userName.Text;
            userInfo.Pd_authstr = "";
            userInfo.Pd_name = realname.Text;
            userInfo.Pd_idcard = idcard.Text;
            userInfo.Pd_mobile = mobile.Text;
            userInfo.Pd_phone = phone.Text;
            //userInfo.Ignorepm = reader["ignorepm"].ToString();
            userInfo.Pd_birthday = "";
            userInfo.Pd_logo = 0;
           
            return userInfo;
        }

        public string SendEmail(string emailaddress)
        {
            #region 发送邮件
            bool send = Emails.SASSmtpMail(userName.Text, emailaddress, password.Text);
            if (send)
                return "您的密码已经成功发送到您的E-mail中, 请注意查收!";
            return "但发送邮件错误, 请您重新取回密码!";
            #endregion
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.AddUserInfo.Click += new EventHandler(this.AddUserInfo_Click);

            userName.IsReplaceInvertedComma = false;
            password.IsReplaceInvertedComma = false;
            email.IsReplaceInvertedComma = false;
        }

        #endregion
    }
}
