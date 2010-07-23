using System;
using System.IO;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;

using SAS.Common;
using SAS.Config;
using SAS.Config.Provider;
using SAS.Entity;
using SAS.Logic;
using SAS.Plugin.TaoBao;

namespace SAS.Logic
{
    /// <summary>
    /// TaoBao页面
    /// </summary>
    public class TaoBaoPage : System.Web.UI.Page
    {
        /// <summary>
        /// 淘宝插件
        /// </summary>
        protected internal TaoBaoPluginBase tpb;
        /// <summary>
        /// 配置信息
        /// </summary>
        protected internal GeneralConfigInfo config;
        /// <summary>
        /// 淘之购配置信息
        /// </summary>
        protected internal TaoBaoConfigInfo taobaoconfig;
        /// <summary>
        /// 当前用户的用户ID
        /// </summary>
        protected internal int userid;
        /// <summary>
        /// 当前用户的管理权限，1为管理员，2为超版，3为版主，-1为特殊组。
        /// 如果需要获得admingroup信息，请勿使用此值，使用usergroupid作为条件查询即可
        /// </summary>
        protected internal int useradminid;
        /// <summary>
        /// 当前用户的在线表ID
        /// </summary>
        protected internal int olid;
        /// <summary>
        /// 当前用户所使用的短信息铃声编号
        /// </summary>
        protected internal int pmsound;
        /// <summary>
        /// 当前页面是否被POST请求
        /// </summary>
        protected internal bool ispost;
        /// <summary>
        /// 是否跳转
        /// </summary>
        protected internal bool isrefresh = false;
        /// <summary>
        /// 跳转时间
        /// </summary>
        private int refreshsec = 0;
        /// <summary>
        /// 跳转连接
        /// </summary>
        private string refreshurl = "";
        /// <summary>
        /// 当前页面是否被GET请求
        /// </summary>
        protected internal bool isget;
        /// <summary>
        /// 当前页面标题
        /// </summary>
        protected internal string pagetitle = "页面";
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
        /// 当前页面Meta keyword字段内容
        /// </summary>
        protected internal string seokeyword = "";
        /// <summary>
        /// 当前页面Meta description字段内容
        /// </summary>
        protected internal string seodescription = "";
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
        /// BasePage类构造函数
        /// </summary>
        public TaoBaoPage()
        {
            config = GeneralConfigs.GetConfig();
            taobaoconfig = TaoBaoConfigs.GetConfig();
            if (TaoBaoPluginProvider.GetInstance() != null)
            {
                tpb = TaoBaoPluginProvider.GetInstance();
            }
            //if (MallPluginProvider.GetInstance() == null)
            //    config.Enablemall = 0;
            userid = Utils.StrToInt(LogicUtils.GetCookie("userid"), -1);

            // 如果启用游客页面缓存，则对游客输出缓存页
            if (userid == -1 && config.Guestcachepagetimeout > 0 && GetUserCachePage(pagename))
                return;

            if (config.Nocacheheaders == 1)
            {
                System.Web.HttpContext.Current.Response.BufferOutput = false;
                System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                System.Web.HttpContext.Current.Response.Expires = 0;
                System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            }


            pmsound = Utils.StrToInt(LogicUtils.GetCookie("pmsound"), 0);

            mainnavigation = Navs.GetNavigationString(userid, useradminid);
            subnavigation = Navs.GetSubNavigation();
            mainnavigationhassub = Navs.GetMainNavigationHasSub();

            //校验用户是否可以访问站点
            if (!ValidateUserPermission())
                return;

            //更新用户在线时长
            if (userid != -1)
                OnlineUsers.UpdateOnlineTime(config.Oltimespan, userid);

            nowdate = Utils.GetDate();
            nowtime = Utils.GetTime();
            nowdatetime = Utils.GetDateTime();
            ispost = SASRequest.IsPost();
            isget = SASRequest.IsGet();
            link = "";
            script = "";        

            isseccode = Utils.InArray(pagename, config.Seccodestatus);

            //校验验证码
            if (isseccode && ispost && !ValidateVerifyCode())
                return;

            //newtopicminute = config.Viewnewtopicminute;
            m_starttick = DateTime.Now;

            ShowPage();

            m_processtime = DateTime.Now.Subtract(m_starttick).TotalMilliseconds / 1000;
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
            refreshsec = sec;
            refreshurl = url;
            isrefresh = true;
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
            System.Web.HttpContext.Current.Response.Write("<style type=\"text/css\"><!-- body { margin: 20px; font-family: Tahoma, Verdana; font-size: 14px; color: #333333; background-color: #FFFFFF; }a {color: #1F4881;text-decoration: none;}--></style></head><body><div style=\"border: #cccccc solid 1px; padding: 20px; width: 500px; margin:auto\" align=\"center\">");
            System.Web.HttpContext.Current.Response.Write(body);
            System.Web.HttpContext.Current.Response.Write("</div><br /><br /><br /><div style=\"border: 0px; padding: 0px; width: 500px; margin:auto\"><strong>当前服务器时间:</strong> ");
            System.Web.HttpContext.Current.Response.Write(Utils.GetDateTime());
            System.Web.HttpContext.Current.Response.Write("<br /><strong>当前页面</strong> ");
            System.Web.HttpContext.Current.Response.Write(pagename);
            System.Web.HttpContext.Current.Response.Write("<br /><strong>可选择操作:</strong> ");
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

            Page.Title = pagetitle + taobaoconfig.SeoTitle;
            HtmlMeta keywords = new HtmlMeta();
            keywords.Name = "keywords";
            keywords.Content = seokeyword + taobaoconfig.SeoKeyword;
            Page.Header.Controls.AddAt(0, keywords);
            HtmlMeta description = new HtmlMeta();
            description.Name = "description";
            description.Content = seodescription + taobaoconfig.SeoDescription;
            Page.Header.Controls.AddAt(1, description);

            if (isrefresh)
            {
                HtmlMeta refresh = new HtmlMeta();
                refresh.HttpEquiv = "refresh";
                refresh.Content = refreshsec.ToString() + "; url = " + refreshurl;
                Page.Header.Controls.Add(refresh);
            }

            base.OnInit(e);
        }
    }
}
