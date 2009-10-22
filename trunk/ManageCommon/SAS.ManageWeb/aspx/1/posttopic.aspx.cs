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
    public class posttopic : SAS.Web.UI.BasePage
    {
        /// <summary>
        /// 主题信息
        /// </summary>
        public TopicInfo topic = new TopicInfo();
        /// <summary>
        /// 帖子信息
        /// </summary>
        public PostInfo postinfo = new PostInfo();
        /// <summary>
        /// 
        /// </summary>
        GeneralConfigInfo config;
        /// <summary>
        /// 是否为主题帖
        /// </summary>
        public bool isfirstpost = true;
        /// <summary>
        /// 开启html功能
        /// </summary>
        public int htmlon = 0;
        /// <summary>
        /// 是否解析表情
        /// </summary>
        public int smileyoff;
        /// <summary>
        /// 所属版块信息
        /// </summary>
        public ForumInfo forum = new ForumInfo();
        /// <summary>
        /// 当前版块的分页id
        /// </summary>
        public int forumpageid = SASRequest.GetInt("forumpage", 1);
        /// <summary>
        /// 所属板块Id
        /// </summary>
        public int forumid = SASRequest.GetInt("forumid", -1);
        /// <summary>
        /// 是否受发帖灌水限制
        /// </summary>
        public int disablepost = 0;
        /// <summary>
        /// 主题内容
        /// </summary>
        public string message = "";
        /// <summary>
        /// 主题分类选项字串
        /// </summary>
        public string topictypeselectoptions;
        /// <summary>
        /// 是否允许Html标题
        /// </summary>
        public bool canhtmltitle = false;
        /// <summary>
        /// 当前帖的Html标题
        /// </summary>
        public string htmltitle = "";
        /// <summary>
        /// 本版是否可用Tag
        /// </summary>
        public bool enabletag = false;
        /// <summary>
        /// 标签
        /// </summary>
        public string topictags = "";
        /// <summary>
        /// 是否允许 [img] 标签
        /// </summary>
        public int allowimg;
        /// <summary>
        /// 是否允许发表主题
        /// </summary>
        public bool allowposttopic = true;
        /// <summary>
        /// 是否解析URL
        /// </summary>
        public int parseurloff = 0;
        /// <summary>
        /// 是否解析 Discuz!NT 代码
        /// </summary>
        public int bbcodeoff = 1;
        /// <summary>
        /// 是否使用签名
        /// </summary>
        public int usesig = LogicUtils.GetCookie("sigstatus") == "0" ? 0 : 1;
        /// <summary>
        /// 是否允许上传附件
        /// </summary>
        public bool canpostattach;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public ShortUserInfo userinfo = new ShortUserInfo();
        public int topicid = 0;
        public bool needaudit = false;
        public int fromindex = SASRequest.GetInt("fromindex", 0);

        protected override void ShowPage()
        {
            if (oluserinfo.ol_ug_id == 4)
            {
                AddErrLine("你所在的用户组，为禁止发言"); return;
            }

            if (userid > 0)
            {
                userinfo = Users.GetShortUserInfo(userid);
            }

            #region 获取并检查版块信息
            forum = Forums.GetForumInfo(forumid);
            if (forum == null || forum.Layer == 0)
            {
                forum = new ForumInfo();//如果不初始化对象，则会报错
                allowposttopic = false;
                AddErrLine("错误的论坛ID"); return;
            }

            pagetitle = Utils.RemoveHtml(forum.Name);
            enabletag = (config.Enabletag & forum.Allowtag) == 1;

            if (forum.Applytopictype == 1)  //启用主题分类
                topictypeselectoptions = Forums.GetCurrentTopicTypesOption(forum.Fid, forum.Topictypes);

            if (forum.Password != "" && Utils.MD5(forum.Password) != LogicUtils.GetCookie("forum" + forumid + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                SetBackLink(base.ShowForumAspxRewrite(forumid, 0)); return;
            }
            needaudit = UserAuthority.NeedAudit(forum, useradminid, userid);
            smileyoff = 1 - forum.Allowsmilies;
            bbcodeoff = (forum.Allowbbcode == 1 && usergroupinfo.Ug_allowcusbbcode == 1) ? 0 : 1;
            allowimg = forum.Allowimgcode;
            #endregion

            if (ispost)
            {
            }
            else //非提交操作
                AddLinkCss(BaseConfigs.GetSitePath + "templates/" + templatepath + "/editor.css", "css");
        }
    }
}
