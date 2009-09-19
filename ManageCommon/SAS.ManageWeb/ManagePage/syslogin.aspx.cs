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

            olid = oluserinfo.ol_id;

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
                UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(oluserinfo.ol_ug_id);
                if (oluserinfo.ol_ps_id == new Guid("00000000-0000-0000-0000-000000000000") || usergroupinfo.ug_pg_id != 1)
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
            }
        }
    }
}
