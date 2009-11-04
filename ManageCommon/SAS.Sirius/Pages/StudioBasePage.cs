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

namespace SAS.Sirius.Pages
{
    /// <summary>
    /// 团队站点基类
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

        public string filerooturl = "http://www.sirius.com/";
    }
}
