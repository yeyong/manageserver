using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb
{
    public class activities : CompanyPage
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        protected int actid = SASRequest.GetInt("actid", 0);
        /// <summary>
        /// 活动实体信息
        /// </summary>
        protected ActivityInfo actinfo = new ActivityInfo();

        protected override void ShowPage()
        {
            actinfo = Activities.GetActivityInfo(actid);
            if (actinfo == null)
            {
                AddErrLine("该活动不存在！");
                return;
            }
            if (System.DateTime.Parse(actinfo.Endtime) < System.DateTime.Now)
            {
                AddErrLine("该活动已结束！");
            }
            if (actinfo.Enabled == 0)
            {
                AddErrLine("该活动已关闭！");
            }
            if (page_err > 0) return;
            pagetitle = actinfo.Atitle + actinfo.Seotitle;
            UpdateMetaInfo(actinfo.Seokeyword, actinfo.Seodesc, "");
            AddStyleCss(actinfo.Stylecode);
            AddfootScript(actinfo.Scriptcode);
        }
    }
}
