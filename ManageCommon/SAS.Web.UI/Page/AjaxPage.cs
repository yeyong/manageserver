using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Cache;
using SAS.Config;

namespace SAS.Web.UI
{
    /// <summary>
    /// Ajax相关功能操作类
    /// </summary>
    public class AjaxPage : Page
    {
        GeneralConfigInfo config;

        public AjaxPage()
        {
            config = GeneralConfigs.GetConfig();
            //如果是Flash提交
            if (Utils.StrIsNullOrEmpty(SASRequest.GetUrlReferrer()))
            {
                string[] input = DecodeUid(SASRequest.GetString("input")).Split(','); //下标0为Uid，1为Olid
                UserInfo userInfo = Users.GetUserInfo(TypeConverter.StrToInt((input[0])));
                if (userInfo == null || SASRequest.GetString("appid") != Utils.MD5(userInfo.Ps_name + userInfo.Ps_password + userInfo.Ps_id + input[1]))
                    return;
            }
            else if (LogicUtils.IsCrossSitePost(SASRequest.GetUrlReferrer(), SASRequest.GetHost())) //如果是跨站提交...
                return;

            string type = SASRequest.GetString("t");
            if (Utils.InArray(type, "forumtree,topictree,quickreply,report,getdebatepostpage,confirmbuyattach,getnewpms,getnewnotifications,getajaxforums,checkuserextcredit,diggdebates"))
            {
                //如果需要验证用户身份，跳转至继承了PageBase的页面
                try
                {
                    HttpContext.Current.Server.Transfer("sessionajax.aspx?t=" + type);
                }
                catch //子页面请求错误，期待更好方案
                { }
                return;
            }

            switch (type)
            {
                case "smilies":
                    GetSmilies();
                    break;
                case "getprovice":
                    break;
            }
        }

        #region 头像

        /// <summary>
        /// 解码Uid
        /// </summary>
        /// <param name="encodeUid"></param>
        /// <returns></returns>
        private string DecodeUid(string encodeUid)
        {
            return DES.Decode(encodeUid.Replace(' ', '+'), config.Passwordkey);
        }

        #endregion

        /// <summary>
        /// 输出表情字符串
        /// </summary>
        private void GetSmilies()
        {
            //如果不是提交...
            if (LogicUtils.IsCrossSitePost()) return;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("{" + Caches.GetSmiliesCache() + "}");
            HttpContext.Current.Response.End();
        }
    }
}
