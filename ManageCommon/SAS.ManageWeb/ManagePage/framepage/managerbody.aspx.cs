using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Threading;
using System.Xml;

using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Common.XML;

namespace SAS.ManageWeb.ManagePage
{
    public partial class managerbody : SAS.Web.UI.AdminPage
    {
        public int olid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                config = GeneralConfigs.GetConfig();
                // 如果IP访问列表有设置则进行判断
                if (config.Adminipaccess.Trim() != "")
                {
                    string[] regctrl = Utils.SplitString(config.Adminipaccess, "\n");
                    if (!Utils.InIPArray(SASRequest.GetIP(), regctrl))
                    {
                        Context.Response.Redirect(BaseConfigs.GetSitePath + "ManagePage/syslogin.aspx");
                        return;
                    }
                }

                //获取当前用户的在线否?
                OnlineUserInfo oluserinfo = new OnlineUserInfo();
                try
                {
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                }
                catch
                {
                    Thread.Sleep(2000);
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                }


                #region 进行权限判断

                UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(oluserinfo.ol_ug_id);
                if (oluserinfo.ol_ps_id <= 0 || usergroupinfo.ug_pg_id != 1)
                {
                    Context.Response.Redirect(BaseConfigs.GetSitePath + "ManagePage/syslogin.aspx");
                    return;
                }

                string secques = Users.GetUserInfo(oluserinfo.ol_ps_id).Ps_secques;
                // 管理员身份验?
                if (Context.Request.Cookies["sasadmin"] == null || Context.Request.Cookies["sasadmin"]["key"] == null || LogicUtils.GetCookiePassword(Context.Request.Cookies["sasadmin"]["key"].ToString(), config.Passwordkey) != (oluserinfo.ol_password + secques + oluserinfo.ol_ps_id.ToString()))
                {
                    Context.Response.Redirect(BaseConfigs.GetSitePath + "ManagePage/syslogin.aspx");
                    return;
                }
                else
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["sasadmin"];
                    cookie.Values["key"] = LogicUtils.SetCookiePassword(oluserinfo.ol_password + secques + oluserinfo.ol_ps_id.ToString(), config.Passwordkey);
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    HttpContext.Current.Response.AppendCookie(cookie);
                }

                #endregion
            }
        }
    }
}
