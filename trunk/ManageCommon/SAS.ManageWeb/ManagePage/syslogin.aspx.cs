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

        }
    }
}
