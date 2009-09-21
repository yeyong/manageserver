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
    public partial class login : SAS.Web.UI.BasePage
    {
        GeneralConfigInfo config;
        protected void Page_Load(object sender, EventArgs e)
        {
            config = GeneralConfigs.GetConfig();
        }

        /// <summary>
        /// 测试用登录临时方法
        /// </summary>
        public void virtualLogin()
        {

            

            LogicUtils.WriteUserCookie(
                                new Guid("4b89cbf8-4c1d-408d-9bd1-4855dbd43553"),
                                TypeConverter.StrToInt(SASRequest.GetString("expires"), -1),
                                config.Passwordkey,
                                SASRequest.GetInt("templateid", 0),
                                SASRequest.GetInt("loginmode", -1));
            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0, config.Onlinetimeout);
            LoginLogs.DeleteLoginLog(SASRequest.GetIP());
            Users.UpdateUserCreditsAndVisit(new Guid("4b89cbf8-4c1d-408d-9bd1-4855dbd43553"), SASRequest.GetIP());
        }
    }
}
