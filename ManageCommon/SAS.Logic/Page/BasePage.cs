﻿using System;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Config;
using SAS.Config.Provider;
using SAS.Entity;
using SAS.Logic;
using SAS.Plugin;
using SAS.Plugin.Album;

namespace SAS.Logic
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        protected internal GeneralConfigInfo config;
        /// <summary>
        /// 当前用户的用户组信息
        /// </summary>
        protected internal UserGroupInfo usergroupinfo;
        /// <summary>
        /// 在线用户信息
        /// </summary>
        protected internal OnlineUserInfo oluserinfo;
        /// <summary>
        /// 当前用户的用户名
        /// </summary>
        protected internal string username;
        /// <summary>
        /// 当前用户的密码
        /// </summary>
        protected internal string password;
        /// <summary>
        /// 当前用户的特征串
        /// </summary>
        protected internal string userkey;
        /// <summary>
        /// 当前用户的用户ID
        /// </summary>
        protected internal int userid;
        /// <summary>
        /// 当前用户的在线表ID
        /// </summary>
        protected internal int olid;
        /// <summary>
        /// 当前用户的用户组ID
        /// </summary>
        protected internal int usergroupid;
        /// <summary>
        /// 当前用户的管理权限，1为管理员，2为超版，3为版主，-1为特殊组。
        /// 如果需要获得admingroup信息，请勿使用此值，使用usergroupid作为条件查询即可
        /// </summary>
        protected internal int useradminid;
        /// <summary>
        /// 当前用户的最后发短消息时间
        /// </summary>
        protected internal string lastpostpmtime;
        /// <summary>
        /// 当前用户的最后搜索时间
        /// </summary>
        protected internal string lastsearchtime;
        /// <summary>
        /// 当前用户所使用的短信息铃声编号
        /// </summary>
        protected internal int pmsound;
        /// <summary>
        /// 当前页面是否被POST请求
        /// </summary>
        protected internal bool ispost;
        /// <summary>
        /// 当前页面是否被GET请求
        /// </summary>
        protected internal bool isget;
        /// <summary>
        /// 当前用户的短消息数目
        /// </summary>
        protected internal int newpmcount = 0;
        /// <summary>
        /// 当前用户的短消息数目
        /// </summary>
        protected internal int realnewpmcount = 0;
        /// <summary>
        /// 当前页面标题
        /// </summary>
        protected internal string pagetitle = "页面";
        /// <summary>
        /// 模板id
        /// </summary>
        protected internal int templateid;
        /// <summary>
        /// 模板风格选择列表框选项
        /// </summary>
        protected internal string templatelistboxoptions;
        /// <summary>
        /// 当前模板路径
        /// </summary>
        protected internal string templatepath;
        /// <summary>
        /// 当前日期
        /// </summary>
        protected internal string nowdate;
        /// <summary>
        /// 当前时间
        /// </summary>
        protected internal string nowtime;
        /// <summary>
        /// 当前日期时间
        /// </summary>
        protected internal string nowdatetime;
        /// <summary>
        /// 当前页面Meta字段内容
        /// </summary>
        protected internal string meta = "";
        /// <summary>
        /// 当前页面Link字段内容
        /// </summary>
        protected internal string link;
        /// <summary>
        /// 当前页面显示样式
        /// </summary>
        protected internal string stylecodes = "";
        /// <summary>
        /// 当前页面中增加script
        /// </summary>
        protected internal string script;
        /// <summary>
        /// 当前页面检查到的错误数
        /// </summary>
        protected internal int page_err = 0;
        /// <summary>
        /// 提示文字
        /// </summary>
        protected internal string msgbox_text = "";
        /// <summary>
        /// 用户中心提示文字
        /// </summary>
        protected internal string usercpmsgbox_text = "";
        /// <summary>
        /// 是否显示回退的链接
        /// </summary>
        protected internal string msgbox_showbacklink = "true";
        /// <summary>
        /// 回退链接的内容
        /// </summary>
        protected internal string msgbox_backlink = "javascript:history.back(-1);";
        /// <summary>
        /// 返回到的页面url地址
        /// </summary>
        protected internal string msgbox_url = "";
        /// <summary>
        /// 当前在线人数
        /// </summary>
        protected internal int onlineusercount = 1;
        /// <summary>
        ///	头部广告
        /// </summary>
        protected internal string headerad = "";
        /// <summary>
        /// 底部广告
        /// </summary>
        protected internal string footerad = "";
        /// <summary>
        /// 页面内容
        /// </summary>
        protected internal System.Text.StringBuilder templateBuilder = new System.Text.StringBuilder();
        /// <summary>
        /// 是否为需检测校验码页面
        /// </summary>
        protected bool isseccode = true;
        /// <summary>
        /// 是否为游客缓存页
        /// </summary>
        protected int isguestcachepage = 0;
        /// <summary>
        /// 导航主菜单
        /// </summary>
        protected string mainnavigation;
        /// <summary>
        /// 导航子菜单
        /// </summary>
        protected System.Data.DataTable subnavigation;
        /// <summary>
        /// 带有子菜单的主导航菜单
        /// </summary>
        protected string[] mainnavigationhassub;
        /// <summary>
        /// 当前页面开始载入时间(毫秒)
        /// </summary>
        private DateTime m_starttick;
        /// <summary>
        /// 当前页面执行时间(毫秒)
        /// </summary>
        private double m_processtime;
        /// <summary>
        /// 当前页面名称
        /// </summary>
        public string pagename = SASRequest.GetPageName();
        /// <summary>
        /// 空间地址
        /// </summary>
        public string spaceurl = "";
        /// <summary>
        /// 相册地址
        /// </summary>
        public string albumurl = "";
        /// <summary>
        /// 论坛地址
        /// </summary>
        public string forumurl = "";
        /// <summary>
        /// 查询次数统计
        /// </summary>
        public int querycount = 0;
        /// <summary>
        /// 论坛相对根的路径
        /// </summary>
        public string forumpath = BaseConfigs.GetSitePath;
        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        public string rooturl = Utils.GetRootUrl(BaseConfigs.GetSitePath);
        /// <summary>
        /// 淘之购域名
        /// </summary>
        public string taogouurl = TaoBaoConfigs.GetTaoBaoUrl;
        /// <summary>
        /// 新闻链接
        /// </summary>
        public string newsurl = GeneralConfigs.GetNEWCMSUrl();
        /// <summary>
        /// 商记链接
        /// </summary>
        public string shangjiurl = GeneralConfigs.GetSHANGJIUrl();
        /// <summary>
        /// 用户头像
        /// </summary>
        public string useravatar = "";
        // <summary>
        /// 返回ajax格式结果
        /// </summary>
        public int inajax = SASRequest.GetInt("inajax", 0);
        /// <summary>
        /// 浮动窗体
        /// </summary>
        public int infloat = SASRequest.GetInt("infloat", 0);
        /// <summary>
        /// 收录总数
        /// </summary>
        public int allcount = 0;
        /// <summary>
        /// 审核通过
        /// </summary>
        public int passcount = 0;
        /// <summary>
        /// 今日收录
        /// </summary>
        public int todaycount = 0;
        /// <summary>
        /// 待审核数
        /// </summary>
        public int waitcount = 0;
#if DEBUG
        public string querydetail = "";
#endif

        /// <summary>
        /// 获得游客缓存
        /// </summary>
        /// <param name="pagename"></param>
        /// <returns></returns>
        private int GetCachePage(string pagename)
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            SAS.Cache.ICacheStrategy ics = new SASCacheStrategy();
            ics.TimeOut = config.Guestcachepagetimeout;
            cache.LoadCacheStrategy(ics);
            string str = cache.RetrieveObject("/SAS/GuestCachePage/" + pagename) as string;
            cache.LoadDefaultCacheStrategy();
            if (str != null && str.Length > 1)
            {
                System.Web.HttpContext.Current.Response.Write(str);
                System.Web.HttpContext.Current.Response.End();
                return 2;
            }
            return 1;
        }

        /// <summary>
        /// 判断页面是否需要游客缓存页
        /// </summary>
        /// <param name="pagename"></param>
        /// <returns>不需要返回false</returns>
        private bool GetUserCachePage(string pagename)
        {
            switch (pagename)
            {
                case "index.aspx":
                    isguestcachepage = GetCachePage(pagename);
                    break;
                case "albumindex.aspx":
                    isguestcachepage = GetCachePage(pagename);
                    break;
                case "showtopic.aspx":
                    int pageid = SASRequest.GetQueryInt("page", 1);
                    int topicid = SASRequest.GetQueryInt("topicid", 0);
                    if (pageid == 1 && SASRequest.GetParamCount() == 2 && topicid > 0 && LogicUtils.ResponseTopicCacheFile(topicid, config.Guestcachepagetimeout))
                    {
                        //TopicStats.Track(topicid, 1);
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// 校验用户是否可以访问站点
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserPermission()
        {
            if (onlineusercount >= config.Maxonlines && useradminid != 1 && pagename != "login.aspx" && pagename != "logout.aspx")
            {
                ShowMessage("抱歉,目前访问人数太多,你暂时无法访问站点.", 0);
                return false;
            }

            if (usergroupinfo.ug_allowvisit != 1 && useradminid != 1 && pagename != "login.aspx" && pagename != "register.aspx" && pagename != "logout.aspx" && pagename != "activationuser.aspx")
            {
                ShowMessage("抱歉, 您所在的用户组不允许访问站点", 2);
                return false;
            }

            // 如果IP访问列表有设置则进行判断
            if (config.Ipaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipaccess, "\n");
                if (!Utils.InIPArray(SASRequest.GetIP(), regctrl))
                {
                    ShowMessage("抱歉, 系统设置了IP访问列表限制, 您无法访问本站点", 0);
                    return false;
                }
            }


            // 如果IP访问列表有设置则进行判断
            if (config.Ipdenyaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(config.Ipdenyaccess, "\n");
                if (Utils.InIPArray(SASRequest.GetIP(), regctrl))
                {
                    ShowMessage("由于您严重违反了论坛的相关规定, 已被禁止访问.", 2);
                    return false;
                }
            }

            //　如果当前用户请求页面不是登录页面并且当前用户非管理员并且论坛设定了时间段,当时间在其中的一个时间段内,则跳转到论坛登录页面
            //if (useradminid != 1 && pagename != "login.aspx" && pagename != "logout.aspx" && usergroupinfo.Disableperiodctrl != 1)
            //{
            //    if (Scoresets.BetweenTime(config.Visitbanperiods))
            //    {
            //        ShowMessage("在此时间段内不允许访问本论坛", 2);
            //        return false;
            //    }
            //}
            return true;
        }


        /// <summary>
        /// 校验验证码
        /// </summary>
        private bool ValidateVerifyCode()
        {
            if (SASRequest.GetString("vcode") == "")
            {
                if (pagename == "showforum.aspx")
                {
                    //版块如不设置密码,必无校验码
                    //return;
                }
                else if (pagename.EndsWith("ajax.aspx"))
                {
                    if (SASRequest.GetString("t") == "quickreply")
                    {
                        ResponseAjaxVcodeError();
                        return false;
                    }
                }
                else
                {
                    if (SASRequest.GetString("loginsubmit") == "true" && pagename == "login.aspx")//添加快捷登陆方式的验证码判断
                    {
                        //快速登录时不报错
                    }
                    else if (SASRequest.GetFormString("agree") == "true" && pagename == "register.aspx")
                    {
                        //同意注册协议也不受此限制
                    }
                    else
                    {
                        AddErrLine("验证码错误");
                        return false;
                    }
                }
            }
            else
            {

                if (!OnlineUsers.CheckUserVerifyCode(olid, SASRequest.GetString("vcode")))
                {
                    if (pagename.EndsWith("ajax.aspx"))
                    {
                        ResponseAjaxVcodeError();
                        return false;
                    }
                    else
                    {
                        AddErrLine("验证码错误");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 从config文件中获取并设置论坛, 空间, 相册的不带页面名称的url路径
        /// (返回绝对或相对地址)
        /// </summary>
        private void LoadUrlConfig()
        {
            spaceurl = config.Spaceurl.ToLower();
            if (spaceurl.IndexOf("http://") == 0)
            {
                if (spaceurl.EndsWith("aspx"))
                    spaceurl = spaceurl.Substring(0, spaceurl.LastIndexOf('/')) + "/";
                else if (!spaceurl.EndsWith("/"))
                    spaceurl = spaceurl + "/";
            }
            else
                spaceurl = "";

            albumurl = config.Albumurl.ToLower();
            if (albumurl.IndexOf("http://") == 0)
            {
                if (albumurl.EndsWith("aspx"))
                    albumurl = albumurl.Substring(0, albumurl.LastIndexOf('/')) + "/";
                else if (!albumurl.EndsWith("/"))
                    albumurl = albumurl + "/";
            }
            else
                albumurl = "";

            forumurl = config.Siteurl.ToLower();
            if (forumurl.IndexOf("http://") == 0)
            {
                if (forumurl.EndsWith("aspx") || forumurl.EndsWith("htm") || forumurl.EndsWith("html"))
                    forumurl = forumurl.Substring(0, forumurl.LastIndexOf('/')) + "/";
                else if (!forumurl.EndsWith("/"))
                    forumurl = forumurl + "/";
            }
            else
                forumurl = BaseConfigs.GetSitePath;
        }

        /// <summary>
        /// BasePage类构造函数
        /// </summary>
        public BasePage()
        {
            config = GeneralConfigs.GetConfig();
            //if (SpacePluginProvider.GetInstance() == null)
            //    config.Enablespace = 0;
            if (AlbumPluginProvider.GetInstance() == null)
                config.Enablealbum = 0;
            //if (MallPluginProvider.GetInstance() == null)
            //    config.Enablemall = 0;
            LoadUrlConfig();
            userid = Utils.StrToInt(LogicUtils.GetCookie("userid"), -1);

            //清空当前页面查询统计
#if DEBUG
            SAS.Data.DbHelper.QueryCount = 0;
            SAS.Data.DbHelper.QueryDetail = "";
#endif

            // 如果启用游客页面缓存，则对游客输出缓存页
            if (userid == -1 && config.Guestcachepagetimeout > 0 && GetUserCachePage(pagename))
                return;
            AddMetaInfo(config.Seokeywords, config.Seodescription, config.Seohead);

            if (config.Nocacheheaders == 1)
            {
                System.Web.HttpContext.Current.Response.BufferOutput = false;
                System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                System.Web.HttpContext.Current.Response.Expires = 0;
                System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            }

            //当为forumlist.aspx或forumindex.aspx,可能出现在线并发问题,这时系统会延时2秒
            if ((pagename != "zshy.aspx") && (pagename != "index.aspx"))
                oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
            else
            {
                try
                {
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                }
                catch
                {
                    System.Threading.Thread.Sleep(2000);
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                }
            }

            userid = oluserinfo.Ol_ps_id;
            usergroupid = oluserinfo.Ol_ug_id;
            username = oluserinfo.Ol_name;
            password = oluserinfo.Ol_password;
            userkey = password.Length > 16 ? password.Substring(4, 8).Trim() : "";
            //lastposttime = oluserinfo.Lastposttime;
            lastpostpmtime = oluserinfo.Ol_lastpostpmtime;
            lastsearchtime = oluserinfo.Ol_lastsearchtime;
            olid = oluserinfo.Ol_id;

            //确保头像可以取到
            //if (userid > 0)
            //    useravatar = Avatars.GetAvatarUrl(userid.ToString(), AvatarSize.Small);

            if (Utils.InArray(SASRequest.GetString("selectedtemplateid"), Templates.GetValidTemplateIDList()))
                templateid = SASRequest.GetInt("selectedtemplateid", 0);
            else if (Utils.InArray(Utils.GetCookie(Utils.GetTemplateCookieName()), Templates.GetValidTemplateIDList()))
                templateid = Utils.StrToInt(Utils.GetCookie(Utils.GetTemplateCookieName()), config.Templateid);

            if (templateid == 0)
                templateid = config.Templateid;

            pmsound = Utils.StrToInt(LogicUtils.GetCookie("pmsound"), 0);

            usergroupinfo = UserGroups.GetUserGroupInfo(usergroupid);

            // 取得用户权限id,1管理员,2超版,3版主,0普通组,-1特殊组
            useradminid = usergroupinfo.ug_pg_id;

            mainnavigation = Navs.GetNavigationString(userid, useradminid);
            subnavigation = Navs.GetSubNavigation();
            mainnavigationhassub = Navs.GetMainNavigationHasSub();

            // 如果论坛关闭且当前用户请求页面不是登录页面且用户非管理员, 则跳转至论坛关闭信息页
            if (config.Closed == 1 && pagename != "login.aspx" && pagename != "logout.aspx" && pagename != "register.aspx" && useradminid != 1)
            {
                ShowMessage(1);
                return;
            }

            onlineusercount = (userid != -1) ? OnlineUsers.GetOnlineAllUserCount() : OnlineUsers.GetCacheOnlineAllUserCount();

            //校验用户是否可以访问站点
            if (!ValidateUserPermission())
                return;

            //更新用户在线时长
            if (userid != -1)
                OnlineUsers.UpdateOnlineTime(config.Oltimespan, userid);

            templatepath = Templates.GetTemplateItem(templateid).Directory;
            nowdate = Utils.GetDate();
            nowtime = Utils.GetTime();
            nowdatetime = Utils.GetDateTime();
            ispost = SASRequest.IsPost();
            isget = SASRequest.IsGet();
            link = "";
            script = "";

            templatelistboxoptions = Caches.GetTemplateListBoxOptionsCache();

            string originalTemplate = string.Format("<li><a href=\"###\" onclick=\"window.location.href='{0}showtemplate.aspx?templateid={1}'\">",
                                   "", BaseConfigs.GetSitePath, templateid);
            string newTemplate = string.Format("<li class=\"current\"><a href=\"###\" onclick=\"window.location.href='{0}showtemplate.aspx?templateid={1}'\">",
                                     BaseConfigs.GetSitePath, templateid);
            templatelistboxoptions = templatelistboxoptions.Replace(originalTemplate, newTemplate);

            isseccode = Utils.InArray(pagename, config.Seccodestatus);
            //headerad = Advertisements.GetOneHeaderAd("", 0);
            //footerad = Advertisements.GetOneFooterAd("", 0);

            //校验验证码
            if (isseccode && ispost && !ValidateVerifyCode())
                return;

            //newtopicminute = config.Viewnewtopicminute;
            m_starttick = DateTime.Now;

            Companies.GetCompanyCountSum(out allcount, out passcount, out todaycount, out waitcount);

            ShowPage();

            m_processtime = DateTime.Now.Subtract(m_starttick).TotalMilliseconds / 1000;

            querycount = SAS.Data.DbHelper.QueryCount;
            SAS.Data.DbHelper.QueryCount = 0;

#if DEBUG
            querydetail = SAS.Data.DbHelper.QueryDetail;
            SAS.Data.DbHelper.QueryDetail = "";
#endif
        }

        #region 子方法
        
        /// <summary>
        /// 输出Ajax验证码错误信息
        /// </summary>
        private static void ResponseAjaxVcodeError()
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ContentType = "Text/XML";
            System.Web.HttpContext.Current.Response.Expires = 0;

            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Text.StringBuilder xmlnode = new System.Text.StringBuilder();
            xmlnode.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            xmlnode.Append("<error>验证码错误</error>");
            System.Web.HttpContext.Current.Response.Write(xmlnode.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        public void SetMetaRefresh()
        {
            SetMetaRefresh(2, msgbox_url);
        }

        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        /// <param name="sec">时间(秒)</param>
        public void SetMetaRefresh(int sec)
        {
            SetMetaRefresh(sec, msgbox_url);
        }

        /// <summary>
        /// 设置页面定时转向
        /// </summary>
        /// <param name="sec">时间(秒)</param>
        /// <param name="url">转向地址</param>
        public void SetMetaRefresh(int sec, string url)
        {
            if (infloat != 1)
            {
                meta = meta + "\r\n<meta http-equiv=\"refresh\" content=\"" + sec.ToString() + "; url=" + url + "\" />";
            }
            //AddScript("window.setInterval('location.replace(\"" + url + "\");'," + (sec*1000).ToString() + ");");
        }

        /// <summary>
        /// 插入指定路径的CSS
        /// </summary>
        /// <param name="url">CSS路径</param>
        public void AddLinkCss(string url)
        {
            link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />";//测试link
        }

        public void AddLinkRss(string url, string title)
        {
            link = link + "\r\n<link rel=\"alternate\" type=\"application/rss+xml\" title=\"" + title + "\" href=\"" + url + "\" />";

        }

        /// <summary>
        /// 插入样式
        /// </summary>
        /// <param name="csscode"></param>
        public void AddStyleCss(string csscode)
        {
            stylecodes = stylecodes + "\r\n<style type=\"text/css\" media=\"all\">\r\n" + csscode + "\r\n</style>";
        }

        /// <summary>
        /// 插入指定路径的CSS
        /// </summary>
        /// <param name="url">CSS路径</param>
        public void AddLinkCss(string url, string linkid)
        {
            link = link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" id=\"" + linkid + "\" />";
        }


        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script></script>的脚本主体字符串</param>
        public void AddScript(string scriptstr)
        {
            AddScript(scriptstr, "javascript");
        }

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script>
        /// <param name="scripttype">脚本类型(值为：vbscript或javascript,默认为javascript)</param>
        public void AddScript(string scriptstr, string scripttype)
        {
            if (!scripttype.ToLower().Equals("vbscript") && !scripttype.ToLower().Equals("vbscript"))
            {
                scripttype = "javascript";
            }
            script = script + "\r\n<script type=\"text/" + scripttype + "\">" + scriptstr + "</script>";
        }

        /// <summary>
        /// 插入指定Meta
        /// </summary>
        /// <param name="metastr">Meta项</param>
        public void AddMeta(string metastr)
        {
            meta = meta + "\r\n<meta " + metastr + " />";
        }

        /// <summary>
        /// 更新页面Meta
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void UpdateMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            string[] metaArray = Utils.SplitString(meta, "\r\n");
            //设置为空,并在下面代码中进行重新赋值
            meta = "";
            foreach (string metaoption in metaArray)
            {
                //找出keywords关键字
                if (metaoption.ToLower().IndexOf("name=\"keywords\"") > 0)
                {
                    if (Seokeywords != null && Seokeywords.Trim() != "")
                    {
                        meta += "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
                        continue;
                    }
                }

                //找出description关键字
                if (metaoption.ToLower().IndexOf("name=\"description\"") > 0)
                {
                    if (Seodescription != null && Seodescription.Trim() != "")
                    {
                        meta += "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
                        continue;
                    }
                }

                meta = meta + metaoption + "\r\n";
            }

            // meta = meta + Seohead;
        }


        /// <summary>
        /// 添加页面Meta信息
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public void AddMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            if (Seokeywords != "")
            {
                meta = meta + "<meta name=\"keywords\" content=\"" + Utils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
            }
            if (Seodescription != "")
            {
                meta = meta + "<meta name=\"description\" content=\"" + Utils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
            }
            meta = meta + Seohead;
        }

        /// <summary>
        /// 增加错误提示
        /// </summary>
        /// <param name="errinfo">错误提示信息</param>
        public void AddErrLine(string errinfo)
        {
            if (msgbox_text.Length == 0)
            {
                msgbox_text = msgbox_text + errinfo;
            }
            else
            {
                msgbox_text = msgbox_text + "<br />" + errinfo;
            }
            page_err++;
        }

        /// <summary>
        /// 增加提示信息
        /// </summary>
        /// <param name="strinfo">提示信息</param>
        public void AddMsgLine(string strinfo)
        {
            if (msgbox_text.Length == 0)
            {
                msgbox_text = msgbox_text + strinfo;
            }
            else
            {
                msgbox_text = msgbox_text + "<br />" + strinfo;
            }
        }

        /// <summary>
        /// 增加提示信息
        /// </summary>
        /// <param name="strinfo">提示信息</param>
        public void MsgForward(string forwardName, bool spJump)
        {
            if (config.Quickforward == 1 && infloat == 0)
            {
                if (Utils.InArray(forwardName, config.Msgforwardlist))
                {
                    if (spJump)
                    {
                        System.Web.HttpContext.Current.Response.Redirect(msgbox_url);
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.Redirect(forumpath + msgbox_url);
                    }
                }
            }
        }

        public void MsgForward(string forwardName)
        {
            MsgForward(forwardName, false);
        }

        //public void AddUserCpMsgLine(string strinfo)
        //{
        //    if (usercpmsgbox_text.Length == 0)
        //    {
        //        usercpmsgbox_text = usercpmsgbox_text + strinfo;
        //    }
        //    else
        //    {
        //        usercpmsgbox_text = usercpmsgbox_text + "<br />" + strinfo;
        //    }
        //}

        /// <summary>
        /// 格式化字节格式
        /// </summary>
        /// <param name="byteStr"></param>
        /// <returns></returns>
        public string FormatBytes(double bytes)
        {
            if (bytes > 1073741824)
            {
                return ((Math.Round((bytes / 1073741824) * 100) / 100).ToString() + " G");
            }
            else if (bytes > 1048576)
            {
                return ((Math.Round((bytes / 1048576) * 100) / 100).ToString() + " M");
            }
            else if (bytes > 1024)
            {
                return ((Math.Round((bytes / 1024) * 100) / 100).ToString() + " K");
            }
            else
            {
                return (bytes.ToString() + " Bytes");
            }
        }

        /// <summary>
        /// 格式化字节格式
        /// </summary>
        /// <param name="byteStr"></param>
        /// <returns></returns>
        public string FormatBytes(string byteStr)
        {
            return FormatBytes((double)TypeConverter.StrToInt(byteStr));
        }


        /// <summary>
        /// 是否已经发生错误
        /// </summary>
        /// <returns>有错误则返回true, 无错误则返回false</returns>
        public bool IsErr()
        {
            return page_err > 0;
        }

        /// <summary>
        /// 设置要转向的url
        /// </summary>
        /// <param name="strurl">要转向的url</param>
        public void SetUrl(string strurl)
        {
            msgbox_url = strurl;
        }
        /// <summary>
        /// 设置回退链接的内容
        /// </summary>
        /// <param name="strback">回退链接的内容</param>
        public void SetBackLink(string strback)
        {
            msgbox_backlink = strback;
        }

        /// <summary>
        /// 设置是否显示回退链接
        /// </summary>
        /// <param name="link">要显示则为true, 否则为false</param>
        public void SetShowBackLink(bool link)
        {
            if (link)
            {
                msgbox_showbacklink = "true";
            }
            else
            {
                msgbox_showbacklink = "false";
            }
        }

        public void ShowMessage(byte mode)
        {
            ShowMessage("", mode);
        }

        public void ShowMessage(string hint, byte mode)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head><title>");
            string title;
            string body;
            switch (mode)
            {
                case 1:
                    title = "论坛已关闭";
                    body = config.Closedreason;
                    break;
                case 2:
                    title = "禁止访问";
                    body = hint;
                    break;
                default:
                    title = "提示";
                    body = hint;
                    break;
            }
            System.Web.HttpContext.Current.Response.Write(title);
            System.Web.HttpContext.Current.Response.Write(" - ");
            System.Web.HttpContext.Current.Response.Write(config.Sitetitle);
            System.Web.HttpContext.Current.Response.Write(" - Powered by 天狼星工作室</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            System.Web.HttpContext.Current.Response.Write(meta);
            System.Web.HttpContext.Current.Response.Write("<style type=\"text/css\"><!-- body { margin: 20px; font-family: Tahoma, Verdana; font-size: 14px; color: #333333; background-color: #FFFFFF; }a {color: #1F4881;text-decoration: none;}--></style></head><body><div style=\"border: #cccccc solid 1px; padding: 20px; width: 500px; margin:auto\" align=\"center\">");
            System.Web.HttpContext.Current.Response.Write(body);
            System.Web.HttpContext.Current.Response.Write("</div><br /><br /><br /><div style=\"border: 0px; padding: 0px; width: 500px; margin:auto\"><strong>当前服务器时间:</strong> ");
            System.Web.HttpContext.Current.Response.Write(Utils.GetDateTime());
            System.Web.HttpContext.Current.Response.Write("<br /><strong>当前页面</strong> ");
            System.Web.HttpContext.Current.Response.Write(pagename);
            System.Web.HttpContext.Current.Response.Write("<br /><strong>可选择操作:</strong> ");
            if (userkey == "")
            {
                System.Web.HttpContext.Current.Response.Write("<a href=\"login.aspx\">登录</a> | <a href=\"register.aspx\">注册</a>");
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("<a href=\"logout.aspx?userkey=" + userkey + "\">退出</a>");
                if (useradminid == 1)
                {
                    System.Web.HttpContext.Current.Response.Write(" | <a href=\"logout.aspx?userkey=" + userkey + "\">系统管理</a>");
                }
            }
            System.Web.HttpContext.Current.Response.Write("</div></body></html>");
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 得到当前页面的载入时间供模板中调用(单位:毫秒)
        /// </summary>
        /// <returns>当前页面的载入时间</returns>
        public double Processtime
        {
            get { return m_processtime; }
        }

        #endregion

        /// <summary>
        /// 页面处理虚方法
        /// </summary>
        protected virtual void ShowPage()
        {
            return;
        }
        
        /// <summary>
        /// OnUnload事件处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
            if (isguestcachepage == 1)
            {
                switch (pagename)
                {
                    case "index.aspx":
                        SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                        SAS.Cache.ICacheStrategy ics = new SASCacheStrategy();
                        ics.TimeOut = config.Guestcachepagetimeout;
                        cache.LoadCacheStrategy(ics);
                        string str = cache.RetrieveObject("/SAS/GuestCachePage/" + pagename) as string;
                        if (str == null && templateBuilder.Length > 1 && templateid == config.Templateid)
                        {
                            templateBuilder.Append("\r\n\r\n<!-- Sirius studio CachedPage (Created: " + Utils.GetDateTime() + ") -->");
                            cache.AddObject("/SAS/GuestCachePage/" + pagename, templateBuilder.ToString());
                        }
                        cache.LoadDefaultCacheStrategy();

                        break;

                    case "showtopic.aspx":
                        int topicid = SASRequest.GetQueryInt("topicid", 0);
                        int pageid = SASRequest.GetQueryInt("page", 1);
                        if (pageid == 1 && SASRequest.GetParamCount() == 2 && topicid > 0 && templateid == config.Templateid)
                        {
                            templateBuilder.Append("\r\n\r\n<!-- Discuz!NT CachedPage (Created: " + Utils.GetDateTime() + ") -->");
                            LogicUtils.CreateTopicCacheFile(topicid, templateBuilder.ToString());
                        }
                        break;
                    default:
                        //
                        break;
                }
            }
#if DEBUG
            else
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Write(templateBuilder.Replace("</body>", "<div>注意: 以下为数据查询分析工具，正式站点使用请使用官方发布版本或自行Release编译。</div>" + querydetail + "</body>").ToString());
                System.Web.HttpContext.Current.Response.End();
            }
#endif
            base.OnUnload(e);
        }

        /// <summary>
        /// 控件初始化时计算执行时间
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (isguestcachepage == 1)
            {
                m_processtime = 0;
            }
            base.OnInit(e);
        }

        protected string aspxrewriteurl = "";

        #region aspxrewrite 配置



        /// <summary>
        /// 设置关于showforum页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowForumAspxRewrite(string forumid, int pageid)
        {
            return ShowForumAspxRewrite(Utils.StrToInt(forumid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }


        protected string ShowForumAspxRewrite(int forumid, int pageid)
        {
            return Urls.ShowForumAspxRewrite(forumid, pageid);
        }

        protected string ShowForumAspxRewrite(string pathlist, int forumid, int pageid)
        {
            return Urls.ShowForumAspxRewrite(pathlist, forumid, pageid);
        }

        protected string ShowForumAspxRewrite(int forumid, int pageid, string rewritename)
        {
            return Urls.ShowForumAspxRewrite(forumid, pageid, rewritename);
        }


        /// <summary>
        /// 设置关于showtopic页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowTopicAspxRewrite(string topicid, int pageid)
        {
            return ShowTopicAspxRewrite(Utils.StrToInt(topicid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        protected string ShowTopicAspxRewrite(int topicid, int pageid)
        {
            return Urls.ShowTopicAspxRewrite(topicid, pageid);
        }

        protected string ShowDebateAspxRewrite(string topicid)
        {
            return ShowDebateAspxRewrite(Utils.StrToInt(topicid, 0));
        }

        protected string ShowDebateAspxRewrite(int topicid)
        {
            return Urls.ShowDebateAspxRewrite(topicid);
        }

        /// <summary>
        /// 设置关于showbonus页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowBonusAspxRewrite(string topicid, int pageid)
        {
            return ShowBonusAspxRewrite(Utils.StrToInt(topicid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        /// <summary>
        /// 设置关于showbonus页面链接的显示样式
        /// </summary>
        /// <param name="topicid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowBonusAspxRewrite(int topicid, int pageid)
        {
            return Urls.ShowBonusAspxRewrite(topicid, pageid);
        }


        protected string UserInfoAspxRewrite(int userid)
        {
            return Urls.UserInfoAspxRewrite(userid);
        }

        /// <summary>
        /// 设置关于userinfo页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string UserInfoAspxRewrite(string userid)
        {
            return UserInfoAspxRewrite(Utils.StrToInt(userid, 0));
        }


        protected string RssAspxRewrite(int forumid)
        {
            return Urls.RssAspxRewrite(forumid);
        }

        /// <summary>
        /// 设置关于userinfo页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string RssAspxRewrite(string forumid)
        {
            return RssAspxRewrite(Utils.StrToInt(forumid, 0));
        }



        /// <summary>
        /// 设置关于showgoods页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowGoodsAspxRewrite(string goodsid)
        {
            return ShowGoodsAspxRewrite(Utils.StrToInt(goodsid, 0));
        }

        protected string ShowGoodsAspxRewrite(int goodsid)
        {
            return Urls.ShowGoodsAspxRewrite(goodsid);
        }


        /// <summary>
        /// 设置关于showgoods页面链接的显示样式
        /// </summary>
        /// <param name="forumid"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        protected string ShowGoodsListAspxRewrite(string categoryid, int pageid)
        {
            return ShowGoodsListAspxRewrite(Utils.StrToInt(categoryid, 0),
                                        pageid <= 0 ? 0 : pageid);
        }

        protected string ShowGoodsListAspxRewrite(int categoryid, int pageid)
        {
            return Urls.ShowGoodsListAspxRewrite(categoryid, pageid);
        }
        #endregion
    }
}
