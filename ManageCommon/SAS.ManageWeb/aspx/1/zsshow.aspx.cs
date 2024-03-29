﻿using System;
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
    /// 企业展示
    /// </summary>
    public class zsshow : CompanyPage
    {
        /// <summary>
        /// 企业详细信息
        /// </summary>
        protected Companys companyshowinfo = new Companys();
        protected int showenid = SASRequest.GetInt("enid", 0);
        /// <summary>
        /// 企业浏览次数
        /// </summary>
        protected int companyviews = 0;
        /// <summary>
        /// 评论数
        /// </summary>
        protected int commentcount = 0;

        protected override void ShowPage()
        {
            companyshowinfo = Companies.GetCompanyInfo(showenid);
            if (companyshowinfo == null)
            {
                AddErrLine("该企业信息不存在或已删除！");
                return;
            }
            companyshowinfo.En_desc = Utils.StrFormat(companyshowinfo.En_desc);
            commentcount = companyshowinfo.En_sell;
            if (companyshowinfo.En_status != 2)
            {
                AddErrLine("该企业审批尚未通过！");
            }
            if (companyshowinfo.En_visble != 1)
            {
                AddErrLine("企业已被关闭，请与管理员联系！");
            }
            if (page_err > 0) return;

            pagetitle = "浙商黄页|" + companyshowinfo.En_name;
            string m_keyword = companyshowinfo.En_main.Trim(',');
            string m_desc = Utils.CutString(Utils.RemoveHtml(companyshowinfo.En_desc), 0, 60);
            UpdateMetaInfo(m_keyword.Trim().Trim(',') + "," + companyshowinfo.En_name, companyshowinfo.En_name + "," + m_desc.Trim().Trim(','), "");

            if (ispost)
            {
                string commentmsg = SASRequest.GetString("commentmsg").Trim();
                string commentuser = SASRequest.GetString("nickname").Trim();
                int commentscore = SASRequest.GetInt("scores", 0);
                if (LogicUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }
                if (commentmsg.Length < 1)
                {
                    AddErrLine("评论内容不能为空！");
                    return;
                }
                if (commentmsg.Length < config.Minpostsize)
                {
                    AddErrLine("评论内容过少！");
                    return;
                }

                string lastcommenttime = Utils.GetCookie("lastcomment");
                if (lastcommenttime != "")
                {
                    int interval = Utils.StrDateDiffSeconds(lastcommenttime, config.Postinterval);
                    if (interval < 0)
                    {
                        AddErrLine("系统规定发帖间隔为"
                            + config.Postinterval.ToString()
                            + "秒, 您还需要等待 "
                            + (interval * -1).ToString()
                            + " 秒");
                        return;
                    }
                }

                CommentInfo cif = new CommentInfo();
                cif.objid = showenid;
                cif.username = commentuser == "" ? "匿名" : commentuser;
                cif.userid = userid;
                cif.userip = SASRequest.GetIP();
                cif.content = Utils.StrFormat(Utils.RemoveHtml(LogicUtils.BanWordFilter(commentmsg)));
                cif.parentid = 0;
                cif.scored = commentscore;
                cif.commentid = Comments.CreateComment(cif);
                Companies.UpdateCompanyCommentCount(showenid, 1);
                Utils.WriteCookie("lastcomment", System.DateTime.Now.ToString());
            }

            AddLinkCss(rooturl + "templates/" + templatepath + "/css/channels.css");
            AddLinkCss(rooturl + "templates/" + templatepath + "/css/jquery.cluetip.css");
            script += "\r\n<script src=\"" + rooturl + "javascript/sascommon.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + rooturl + "javascript/ajax.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + rooturl + "javascript/template_showcompany.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + rooturl + "javascript/jquery.cluetip-min.js\" type=\"text/javascript\"></script>";
            script += "\r\n<script src=\"" + rooturl + "javascript/jquery.ratingmin.js\" type=\"text/javascript\"></script>";

            string loadscript = "\r\n " + "var page_qyid = " + showenid + ";";
            loadscript += "\r\n " + "var comment_page_recordcount = " + commentcount + ";";
            loadscript += "\r\n " + "var comment_page_pagesize = 10;";
            loadscript += "\r\n " + "var comment_page_currentpage = 1;";
            loadscript += "\r\n " + "jQuery(document).ready(function() {";
            if (templateid == 1)
            {
                loadscript += "\r\n " + "jQuery('#put').find(\"span\").find(\"a\").cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title', closeText: '<img src=\"" + forumpath + "images/cross.png\" alt=\"close\" />',cursor: 'pointer', dropShadow: false});";
                loadscript += "\r\n " + "jQuery('#form1').find(\"input[type=text],textarea\").each(function(){";
                loadscript += "\r\n " + "  jQuery(this).blur(function(){jQuery(this).attr(\"class\",\"input2_soout\");});";
                loadscript += "\r\n " + "  jQuery(this).focus(function(){jQuery(this).attr(\"class\",\"input2_soon\");});";
                loadscript += "\r\n " + "});";
                loadscript += "\r\n " + "jQuery('#swinf').find(\".tswan\").click(function(){";
                loadscript += "\r\n " + "   var cssname = jQuery('#swinf').find(\"#swinfcot\").attr(\"class\");";
                loadscript += "\r\n " + "   var arrowicon = 'arrow-icon2.gif';";
                loadscript += "\r\n " + "   var showcss = 'tswnrxin';";
                loadscript += "\r\n " + "   var showzi = '点击隐藏部分介绍';";
                loadscript += "\r\n " + "   if (cssname == 'tswnrxin') {showcss = 'tswnr';arrowicon = 'arrow-icon1.gif';showzi = '点击查看全部介绍';}";
                loadscript += "\r\n " + "   jQuery(this).html(\"<em><img src='templates/" + templatepath + "/images/icon/\"+arrowicon+\"'/></em>\"+showzi);";
                loadscript += "\r\n " + "   jQuery('#swinf').find(\"#swinfcot\").removeClass().addClass(showcss);";
                loadscript += "\r\n " + "});";
            }
            else
            {
                loadscript += "\r\n " + "jQuery('#put').find(\"a\").cluetip({ activation: 'click', sticky: true, width: 350, positionBy: 'bottomTop', closePosition: 'title', closeText: '<img src=\"" + forumpath + "images/cross.png\" alt=\"close\" />',cursor: 'pointer', dropShadow: false});";
                loadscript += "\r\n " + "jQuery('#swinf').find(\".an\").click(function(){";
                loadscript += "\r\n " + "   var cssname = jQuery('#swinf').find(\"#swinfcot\").attr(\"class\");";
                loadscript += "\r\n " + "   var arrowicon = 'tu1';";
                loadscript += "\r\n " + "   var showcss = 'sw1nr1';";
                loadscript += "\r\n " + "   var showzi = '点击隐藏部分介绍';";
                loadscript += "\r\n " + "   if (cssname == 'sw1nr1') {showcss = 'sw1nr2';arrowicon = 'tu2';showzi = '点击查看全部介绍';}";
                loadscript += "\r\n " + "   jQuery('#swinf').find(\"em\").removeClass().addClass(arrowicon)";
                loadscript += "\r\n " + "   jQuery('#swinf').find(\"#swinfcot\").removeClass().addClass(showcss);";
                loadscript += "\r\n " + "   jQuery('#swinf').find(\"span\").html(showzi);";
                loadscript += "\r\n " + "});";
            }
            loadscript += "\r\n " + "jQuery('.starshow').rating({";
            loadscript += "\r\n " + "  callback: function(value, link){";
            loadscript += "\r\n " + "    if(typeof value == 'undefined'){jQuery('.fsw3').find('em').html(0);jQuery('input[name=scores]').val(0);}";
            loadscript += "\r\n " + "    else{jQuery('.fsw3').find('em').html(value);jQuery('input[name=scores]').val(value);}";
            loadscript += "\r\n " + "}});";
            loadscript += "\r\n " + "jQuery(this).gettop({objsrc:\"templates/" + templatepath + "/images/top.gif\",objhref:\"javascript:scrollTo(0,0)\"});";
            loadscript += "\r\n " + "});\r\n";
            loadscript += "\r\n " + "ajaxgetcommentscored(page_qyid);";
            loadscript += "\r\n " + "ajaxgetcomment(page_qyid,comment_page_pagesize,comment_page_currentpage," + templateid + ");";
            loadscript += "\r\n " + "function validate(form){";
            loadscript += "\r\n " + "   if(form.nickname.value==''){alert(\"请输入您的昵称！\");return false;}";
            loadscript += "\r\n " + "   if(form.commentmsg.value==''){alert(\"请输入您的评论！\");return false;}";
            loadscript += "\r\n " + "   if(form.commentmsg.value.length > 200){alert(\"评论在200字以内！\");return false;}";
            loadscript += "\r\n " + "   if(form.commentmsg.value.length < 10){alert(\"评论至少要10字以上！\");return false;}";
            loadscript += "\r\n " + "}";
            AddfootScript(loadscript);

            CompaniesStats.Track(showenid, 1);
            companyviews = companyshowinfo.En_accesses + 1 + (config.TopicQueueStats == 1 ? CompaniesStats.GetStoredCompanyViewCount(companyshowinfo.En_id) : 0);
            string lastviewids = "," + Utils.GetCookie("lastviews") + ",";

            if (!lastviewids.Contains("," + showenid + ","))
            {
                Utils.WriteCookie("lastviews", Utils.GetCookie("lastviews") + "," + showenid, 1440);
            }

        }
    }
}
