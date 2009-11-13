using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Sirius.Config;

namespace SAS.Sirius.Pages
{
    /// <summary>
    /// Sirius studio 站点基类
    /// </summary>
    public class StudioBasePage : BasePage
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = SASRequest.GetInt("page", 1);
        /// <summary>
        /// 分页数
        /// </summary>
        public int pagecount = 0;
        /// <summary>
        /// 页码
        /// </summary>
        public string pagenumbers = "";
        /// <summary>
        /// sirius配置信息
        /// </summary>
        public SiriusConfigInfo siriusconfig = new SiriusConfigInfo();
        /// <summary>
        /// 团队信息
        /// </summary>
        public TeamInfo teaminfo = new TeamInfo();

        public string filerooturl = "";

        public StudioBasePage()
        {
            siriusconfig = SiriusConfigs.GetConfig();
            filerooturl = siriusconfig.FileUrlAddress;
        }
    }
}
