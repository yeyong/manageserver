using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public partial class logout : BasePage
    {
        private string reurl = SASRequest.GetQueryString("reurl");

        protected override void ShowPage()
        {
            pagetitle = "用户退出";
            username = "游客";
            userid = -1;

            base.AddScript("if (top.document.getElementById('leftmenu')){ top.frames['leftmenu'].location.reload(); }");

            if (!SASRequest.IsPost() || reurl != "")
            {
                string r = (!Utils.StrIsNullOrEmpty(reurl)) ? reurl : "";

                if (reurl == "")
                    r = (SASRequest.GetUrlReferrer() == "" || SASRequest.GetUrlReferrer().IndexOf("login") > -1 || SASRequest.GetUrlReferrer().IndexOf("logout") > -1) ?
                            "index.aspx" : SASRequest.GetUrlReferrer();

                Utils.WriteCookie("reurl", (reurl == "" || reurl.IndexOf("login.aspx") > -1) ? r : reurl);
            }

            if (SASRequest.GetString("userkey") == userkey)
            {

                AddMsgLine("已经清除了您的登录信息, 稍后您将以游客身份返回首页");

                OnlineUsers.DeleteRows(olid);
                LogicUtils.ClearUserCookie();
                Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);

                System.Web.HttpContext.Current.Response.AppendCookie(new System.Web.HttpCookie("sasadmin"));
                MsgForward("logout_succeed");
            }
            else
                AddMsgLine("无法确定您的身份, 稍后返回首页");

            SetUrl(Utils.UrlDecode(LogicUtils.GetReUrl()));
            SetMetaRefresh();
            SetShowBackLink(false);
        }
    }
}
