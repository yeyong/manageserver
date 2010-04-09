using System;
using System.Text;
using System.Data;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;
namespace SAS.ManageWeb
{
    /// <summary>
    /// 企业名片图片
    /// </summary>
    public class cardimg : CompanyPage
    {
        #region 变量声明
        /// <summary>
        /// 企业名片id
        /// </summary>
        public int qycardid = SASRequest.GetInt("qycardid", -1);
        #endregion
        protected override void ShowPage()
        {
            pagetitle = "企业图片式名片";
            if (qycardid == -1)
            {
                AddErrLine("无效的企业名片ID");
                return;
            }
        }
    }
}
