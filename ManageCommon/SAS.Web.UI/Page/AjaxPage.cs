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
using Newtonsoft.Json;

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
                case "checkenname":
                    CheckCompanyName();
                    break;
                case "getcompanycomment":
                    GetCompanyComment(SASRequest.GetInt("qyid", 0), SASRequest.GetInt("pagesize", 10), SASRequest.GetInt("pageindex", 1));
                    break;
                case "getcompanycommentscored":
                    GetCompanyCommentScored(SASRequest.GetInt("qyid", 0));
                    break;
                case "getcompanyinfobyflash":
                    GetCompanyInfoByFlash(SASRequest.GetInt("objid", 0));
                    break;
            }
        }

        /// <summary>
        /// 获取flash数据
        /// </summary>
        /// <param name="objid"></param>
        private void GetCompanyInfoByFlash(int objid)
        {
            //Companys companyinfo = Companies.GetCompanyCacheInfo(objid);

            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
            //if (companyinfo == null)
            //{
            //    xmlnode.Append("<data><error>企业信息读取错误！请您与管理员联系！</error></data>");
            //    ResponseText(xmlnode);
            //    return;
            //}
            //else
            //{
                xmlnode.Append("<data><error>企业信息读取错误！请您与管理员联系！</error></data>");
                ResponseXML(xmlnode);
                //return;
            //}
        }

        /// <summary>
        /// 获取企业评论
        /// </summary>
        /// <param name="qyid"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        private void GetCompanyComment(int qyid, int pagesize, int pageindex)
        {
            #region 企业评论
            pageindex = (pageindex < 0) ? 1 : pageindex;
            pagesize = (pagesize < 0 || pagesize > 25) ? 25 : pagesize;

            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(Comments.GetCommentListJosn(qyid, pagesize, pageindex).ToString().Trim(';'));
            HttpContext.Current.Response.End();
            #endregion
        }

        private void GetCompanyCommentScored(int qyid)
        {
            #region 企业评分
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(Comments.GetCommentScoredJosn(qyid).ToString());
            HttpContext.Current.Response.End();
            #endregion
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
        /// <summary>
        /// 检查企业申请名称
        /// </summary>
        private void CheckCompanyName()
        {
            if (SASRequest.GetString("qyname").Trim() == "") return;
            string result = "0";
            string tmpUsername = SASRequest.GetString("qyname").Trim();
            if (Companies.ExistCompanyName(tmpUsername) == 0) result = "1";
            ResponseJSON(result);
        }

        #region Helper
        private void ResponseText(string text)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(text);
            HttpContext.Current.Response.End();
        }

        private void ResponseText(StringBuilder builder)
        {
            ResponseText(builder.ToString());
        }
        /// <summary>
        /// 向页面输出xml内容
        /// </summary>
        /// <param name="xmlnode">xml内容</param>
        private void ResponseXML(System.Text.StringBuilder xmlnode)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "Text/XML";
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Write(xmlnode.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 输出json内容
        /// </summary>
        /// <param name="json"></param>
        private void ResponseJSON(string json)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "application/json";
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Write(json);
            System.Web.HttpContext.Current.Response.End();
        }

        private void ResponseJSON<T>(T jsonobj)
        {
            ResponseJSON(JavaScriptConvert.SerializeObject(jsonobj));
        }
        #endregion
    }
}
