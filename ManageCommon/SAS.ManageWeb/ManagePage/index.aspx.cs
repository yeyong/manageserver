using System;
using System.Web;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    public partial class index:System.Web.UI.Page
    {
        protected internal GeneralConfigInfo config;

        public int olid;

        protected void Page_Load(object sender, EventArgs e)
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

            //获取当前用户的在线信息
            OnlineUserInfo oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
            olid = oluserinfo.ol_id;


            #region 进行权限判断

            UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(oluserinfo.ol_ug_id);
            if (oluserinfo.ol_ps_id <= 0 || usergroupinfo.ug_pg_id != 1)
            {
                Context.Response.Redirect(BaseConfigs.GetSitePath + "ManagePage/syslogin.aspx");
                return;
            }

            string secques = Users.GetUserInfo(oluserinfo.ol_ps_id).ps_secques;
            // 管理员身份验证
            if (Context.Request.Cookies["sasadmin"] == null || Context.Request.Cookies["sasadmin"]["key"] == null || LogicUtils.GetCookiePassword(Context.Request.Cookies["sasadmin"]["key"].ToString(), config.Passwordkey) != (oluserinfo.ol_password + secques + oluserinfo.ol_ps_id))
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
