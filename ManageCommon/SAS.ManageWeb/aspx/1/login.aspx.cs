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
    public partial class login : BasePage
    {
        GeneralConfigInfo config;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void ShowPage()
        {
            pagetitle = "登录页面";
            config = GeneralConfigs.GetConfig();
            virtualLogin();
            base.ShowPage();
        }

        /// <summary>
        /// 测试用登录临时方法
        /// </summary>
        public void virtualLogin()
        {

            

            LogicUtils.WriteUserCookie(
                                1,
                                TypeConverter.StrToInt(SASRequest.GetString("expires"), -1),
                                config.Passwordkey,
                                SASRequest.GetInt("templateid", 0),
                                SASRequest.GetInt("loginmode", -1));
            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0, config.Onlinetimeout);
            LoginLogs.DeleteLoginLog(SASRequest.GetIP());
            Users.UpdateUserCreditsAndVisit(1, SASRequest.GetIP());
        }
    }
}
