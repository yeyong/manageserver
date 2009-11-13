using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

using SAS.Config;
using SAS.Common;
using SAS.Logic;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 系统登陆
    /// </summary>
    public partial class syslogin : Page
    {
        /// <summary>
        /// 当前登陆用户的在线ID
        /// </summary>
        public int olid;
        /// <summary>
        /// 论坛配置文件变量
        /// </summary>
        protected internal GeneralConfigInfo config;
        /// <summary>
        /// 页面尾部信息
        /// </summary>
        public string footer = "";

        public syslogin()
        {
            #region 加载尾部信息
            footer = "<div align=\"center\" style=\" padding-top:60px;font-size:11px; font-family: Arial\">";
            footer += "<hr style=\"height:1; width:600; height:1; color:#CCCCCC\" />Powered by ";
            footer += "<a style=\"COLOR: #000000\" href=\"http://www.sirius.org.cn\" target=\"_blank\">";
            footer += Utils.GetAssemblyProductName();
            footer += "</a> &nbsp;&copy; 2009-";
            footer += Utils.GetAssemblyCopyright().Split(',')[0];
            footer += ", <a style=\"COLOR: #000000;font-weight:bold\" href=\"http://www.sirius.org.cn\" target=\"_blank\">Studio after 80s.</a></div>";
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserName.Attributes.Remove("class");
            PassWord.Attributes.Remove("class");
            UserName.AddAttributes("style", "width:200px");
            PassWord.AddAttributes("style", "width:200px");

            config = GeneralConfigs.GetConfig();

            OnlineUserInfo oluserinfo = SAS.Logic.OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);

            olid = oluserinfo.Ol_id;

            if (!Page.IsPostBack)
            {
                #region 如果IP访问列表有设置则进行判断
                if (config.Adminipaccess.Trim() != "")
                {
                    string[] regctrl = Utils.SplitString(config.Adminipaccess, "\n");
                    if (!Utils.InIPArray(SASRequest.GetIP(), regctrl))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<br /><br /><div style=\"width:100%\" align=\"center\"><div align=\"center\" style=\"width:600px; border:1px dotted #FF6600; background-color:#FFFCEC; margin:auto; padding:20px;\">");
                        sb.Append("<img src=\"images/hint.gif\" border=\"0\" alt=\"提示:\" align=\"absmiddle\" />&nbsp; 您的IP地址不在系统允许的范围之内</div></div>");
                        Response.Write(sb.ToString());
                        Response.End();
                        return;
                    }
                }
                #endregion

                #region 用户身份判断
                UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(oluserinfo.Ol_ug_id);
                if (oluserinfo.Ol_ps_id <= 0 || usergroupinfo.ug_pg_id != 1)
                {
                    string message = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
                    message += "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>无法确认您的身份</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">";
                    message += "<link href=\"styles/default.css\" type=\"text/css\" rel=\"stylesheet\"></head><script type=\"text/javascript\">if(top.location!=self.location){top.location.href = \"syslogin.aspx\";}</script><body><br /><br /><div style=\"width:100%\" align=\"center\">";
                    message += "<div align=\"center\" style=\"width:600px; border:1px dotted #FF6600; background-color:#FFFCEC; margin:auto; padding:20px;\"><img src=\"images/hint.gif\" border=\"0\" alt=\"提示:\" align=\"absmiddle\" width=\"11\" height=\"13\" /> &nbsp;";
                    message += "无法确认您的身份, 请<a href=\"../login.aspx\">登录</a></div></div></body></html>";
                    Response.Write(message);
                    Response.End();
                    return;
                }
                #endregion

                #region 显示相关页面登陆提交信息
                if (Context.Request.Cookies["sasadmin"] == null || Context.Request.Cookies["sasadmin"]["key"] == null ||
                    LogicUtils.GetCookiePassword(Context.Request.Cookies["sasadmin"]["key"].ToString(), config.Passwordkey) !=
                    (oluserinfo.Ol_password + SAS.Logic.Users.GetUserInfo(oluserinfo.Ol_ps_id).Ps_secques + oluserinfo.Ol_ps_id.ToString()))
                {
                    Msg.Text = "<p class=\"adlrt1 zi1\" style=\" float:right; letter-spacing:1px;\" align=\"absMiddle\"><span class=\"adlrt1tu adbg\"></span>请重新进行管理员登录</p>";
                }

                if (oluserinfo.Ol_ps_id > 0 && usergroupinfo.ug_pg_id == 1 && oluserinfo.Ol_name.Trim() != "")
                {
                    UserName.Text = oluserinfo.Ol_name;
                    UserName.AddAttributes("readonly", "true");
                    UserName.CssClass = "nofocus";
                    UserName.Attributes.Add("onfocus", "this.className='nofocus';");
                    UserName.Attributes.Add("onblur", "this.className='nofocus';");
                }

                if (SASRequest.GetString("result") == "1")
                {
                    Msg.Text = "<p class=\"adlrt1 zi1\" style=\" float:right; letter-spacing:1px;\" align=\"absMiddle\"><span class=\"adlrt1tu adbg\"></span>用户不存在或密码错误</p>";
                    return;
                }

                if (SASRequest.GetString("result") == "2")
                {
                    Msg.Text = "<p class=\"adlrt1 zi1\" style=\" float:right; letter-spacing:1px;\" align=\"absMiddle\"><span class=\"adlrt1tu adbg\"></span>用户不是管理员身分,因此无法登陆后台</p>";
                    return;
                }

                if (SASRequest.GetString("result") == "3")
                {
                    Msg.Text = "<p class=\"adlrt1 zi1\" style=\" float:right; letter-spacing:1px;\" align=\"absMiddle\"><span class=\"adlrt1tu adbg\"></span>验证码错误,请重新输入</p>";
                    return;
                }

                if (SASRequest.GetString("result") == "4")
                {
                    Msg.Text = "";
                    return;
                }
                #endregion
            }

            if (Page.IsPostBack)
                VerifyLoginInf();//对提供的信息进行验证
            else
                Response.Redirect("syslogin.aspx?result=4");
        }

        public void VerifyLoginInf()
        {
            if (!SAS.Logic.OnlineUsers.CheckUserVerifyCode(olid, SASRequest.GetString("vcode")))
            {
                Response.Redirect("syslogin.aspx?result=3");
                return;
            }

            UserInfo userInfo = null;
            //if (config.Passwordmode == 1)
            //    userInfo = Users.GetUserInfo(Users.CheckDvBbsPassword(SASRequest.GetString("username"), SASRequest.GetString("password")));
            //else 
            if (config.Passwordmode == 0)
                userInfo = Users.GetUserInfo(Users.CheckPassword(SASRequest.GetString("username"), Utils.MD5(SASRequest.GetString("password")), false));
            //else//第三方加密验证模式
                //userInfo = Users.CheckThirdPartPassword(SASRequest.GetString("username"), SASRequest.GetString("password"), -1, null);

            if (userInfo != null)
            {
                UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(userInfo.Ps_ug_id);

                if (usergroupinfo.ug_pg_id == 1)
                {
                    LogicUtils.WriteUserCookie(userInfo.Ps_id, 1440, GeneralConfigs.GetConfig().Passwordkey);

                    UserGroupInfo userGroupInfo = AdminUserGroups.AdminGetUserGroupInfo(userInfo.Ps_ug_id);

                    HttpCookie cookie = new HttpCookie("sasadmin");
                    cookie.Values["key"] = LogicUtils.SetCookiePassword(userInfo.Ps_password + userInfo.Ps_secques + userInfo.Ps_id, config.Passwordkey);
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    HttpContext.Current.Response.AppendCookie(cookie);

                    AdminVistLogs.InsertLog(userInfo.Ps_id, userInfo.Ps_name, userInfo.Ps_ug_id, userGroupInfo.ug_name, SASRequest.GetIP(), "后台管理员登陆", "");

                    try
                    {
                        SoftInfo.LoadSoftInfo();
                    }
                    catch
                    {
                        Response.Write("<script type=\"text/javascript\">top.location.href='index.aspx';</script>");
                        Response.End();
                    }

                    //升级general.config文件
                    try
                    {
                        GeneralConfigs.Serialiaze(GeneralConfigs.GetConfig(), Server.MapPath("../config/general.config"));
                    }
                    catch { }

                    Response.Write("<script type=\"text/javascript\">top.location.href='index.aspx';</script>");
                    Response.End();
                }
                else
                    Response.Redirect("syslogin.aspx?result=2");
            }
            else
                Response.Redirect("syslogin.aspx?result=1");
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.SavePageStateToPersistenceMedium(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            object o = new object();
            try
            {
                o = base.LoadPageStateFromPersistenceMedium();
            }
            catch
            {
                o = null;
            }
            return o;
        }
    }


}
