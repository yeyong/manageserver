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
    /// <summary>
    /// 企业部分信息
    /// </summary>
    public class tipmsg : CompanyPage
    {
        /// <summary>
        /// 需要展开的企业信息类型默认0为联系信息，1为认证信息
        /// </summary>
        protected int msgtype = SASRequest.GetInt("msgtype", 0);
        /// <summary>
        /// 企业ID
        /// </summary>
        protected int enid = SASRequest.GetInt("msgenid", 0);
        protected Companys tipmsgeninfo;
        protected override void ShowPage()
        {
            if (enid > 0)
            {
                tipmsgeninfo = Companies.GetCompanyCacheInfo(enid);
                if (tipmsgeninfo == null) AddErrLine("企业信息参数错误！");
            }
            else
            {
                AddErrLine("企业信息参数错误！");
            }
        }
    }
}
