using System;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.Album;

namespace SAS.ManageWeb
{
    public class posttopic : BasePage
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
        /// 是否需要登录
        /// </summary>
        public bool needlogin = false;
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
        /// 相册列表
        /// </summary>
        public DataTable albumlist;
        /// <summary>
        /// 是否允许同时发布到相册
        /// </summary>
        public bool caninsertalbum = false;
        /// <summary>
        /// 是否允许上传附件
        /// </summary>
        public bool canpostattach;
        /// <summary>
        /// 允许的附件类型和大小数组
        /// </summary>
        public string attachextensions;
        /// <summary>
        /// 允许的附件类型
        /// </summary>
        public string attachextensionsnosize;
        /// <summary>
        /// 今天可上传附件大小
        /// </summary>
        public int attachsize;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public ShortUserInfo userinfo = new ShortUserInfo();
        /// <summary>
        /// 权限校验提示信息
        /// </summary>
        string msg = "";
        public int topicid = 0;
        public bool needaudit = false;
        public int fromindex = SASRequest.GetInt("fromindex", 0);
        AlbumPluginBase apb = AlbumPluginProvider.GetInstance();

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
            forumid = 2;
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

            #region  附件信息绑定
            //得到用户可以上传的文件类型
            string attachmentTypeSelect = Attachments.GetAllowAttachmentType(usergroupinfo, forum);
            attachextensions = Attachments.GetAttachmentTypeArray(attachmentTypeSelect);
            attachextensionsnosize = Attachments.GetAttachmentTypeString(attachmentTypeSelect);
            //得到今天允许用户上传的附件总大小(字节)
            int MaxTodaySize = (userid > 0 ? MaxTodaySize = Attachments.GetUploadFileSizeByuserid(userid) : 0);
            attachsize = usergroupinfo.Ug_maxsizeperday - MaxTodaySize;//今天可上传得大小
            //是否有上传附件的权限
            canpostattach = UserAuthority.PostAttachAuthority(forum, usergroupinfo, userid, ref msg);

            if (canpostattach && (userinfo != null && userinfo.Ps_id > 0) && apb != null && config.Enablealbum == 1 &&
            (UserGroups.GetUserGroupInfo(userinfo.Ps_ug_id).ug_maxspacephotosize - apb.GetPhotoSizeByUserid(userid) > 0))
            {
                caninsertalbum = true;
                albumlist = apb.GetSpaceAlbumByUserId(userid);
            }
            #endregion


            if (ispost)
            {
            }
            else //非提交操作
                AddLinkCss(BaseConfigs.GetSitePath + "templates/" + templatepath + "/editor.css", "css");
        }
    }
}
