﻿using System;
using System.Web.UI;
using System.Data;

using SAS.Common;
using SAS.Control;
using SAS.Logic;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 基本设置
    /// </summary>
    public partial class global_baseset : AdminPage
    {
        protected SAS.Control.RadioButtonList iisurlrewrite;
        protected bool haveAlbum;
        protected bool haveSpace;
        protected bool haveMall;

        protected void Page_Load(object sender, EventArgs e)
        {
            ////haveAlbum = AlbumPluginProvider.GetInstance() != null;
            ////haveSpace = SpacePluginProvider.GetInstance() != null;
            ////haveMall = MallPluginProvider.GetInstance() != null;
            if (!Page.IsPostBack)
            {
                LoadConfigInfo();
                closed.Items[0].Attributes.Add("onclick", "setStatus(true)");
                closed.Items[1].Attributes.Add("onclick", "setStatus(false)");
            }
        }

        public void LoadConfigInfo()
        {
            #region 加载配置信息
            GeneralConfigInfo configInfo = GeneralConfigs.GetConfig();
            forumtitle.Text = configInfo.Sitetitle.ToString();
            //forumurl.Text = configInfo.Forumurl.ToString();
            webtitle.Text = configInfo.Webtitle.ToString();
            weburl.Text = configInfo.Weburl.ToString();
            licensed.SelectedValue = configInfo.Licensed.ToString();
            closed.SelectedValue = configInfo.Closed.ToString();
            closedreason.Text = configInfo.Closedreason.ToString();
            icp.Text = configInfo.Icp.ToString();
            rssttl.Text = configInfo.Rssttl.ToString();
            sitemapttl.Text = configInfo.Sitemapttl.ToString();
            nocacheheaders.SelectedValue = configInfo.Nocacheheaders.ToString();
            debug.SelectedValue = configInfo.Debug.ToString();
            rssstatus.SelectedValue = configInfo.Rssstatus.ToString();
            sitemapstatus.SelectedValue = configInfo.Sitemapstatus.ToString();
            cachelog.SelectedValue = "0";
            fulltextsearch.SelectedValue = configInfo.Fulltextsearch.ToString();
            passwordmode.SelectedValue = configInfo.Passwordmode.ToString();
            bbcodemode.SelectedValue = configInfo.Bbcodemode.ToString();
            extname.Text = configInfo.Extname.Trim();
            enablesilverlight.SelectedValue = configInfo.Silverlight.ToString();
            EnableSpace.SelectedValue = configInfo.Enablespace.ToString();
            EnableAlbum.SelectedValue = configInfo.Enablealbum.ToString();
            //EnableMall.SelectedValue = configInfo.Enablemall.ToString();
            CookieDomain.Text = configInfo.CookieDomain.ToString();
            memliststatus.SelectedValue = configInfo.Memliststatus.ToString();
            Indexpage.SelectedIndex = Convert.ToInt32(configInfo.Indexpage.ToString());
            Linktext.Text = configInfo.Linktext;
            Statcode.Text = configInfo.Statcode;
            spacename.Text = configInfo.Spacename.ToString();
            //spaceurl.Text = configInfo.Spaceurl.ToString();
            albumname.Text = configInfo.Albumname.ToString();
            //albumurl.Text = configInfo.Albumurl.ToString();
            aspxrewrite.SelectedValue = configInfo.Aspxrewrite.ToString();
            iisurlrewrite.SelectedValue = configInfo.Iisurlrewrite.ToString();
            notificationreserveddays.Text = configInfo.Notificationreserveddays.ToString();
            maxindexsubforumcount.Text = configInfo.Maxindexsubforumcount.ToString();
            deletingexpireduserfrequency.Text = configInfo.Deletingexpireduserfrequency.ToString();
            ////if (!haveSpace)
            ////{
            ////    EnableSpace.Visible = false;
            ////    EnableSpaceLabel.Visible = false;
            ////}
            ////if (!haveAlbum)
            ////{
            ////    EnableAlbum.Visible = false;
            ////    EnableAlbumLabel.Visible = false;
            ////}
            onlineoptimization.SelectedValue = configInfo.Onlineoptimization.ToString();
            onlineusercountcacheminute.Text = configInfo.OnlineUserCountCacheMinute.ToString();
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region 保存信息
            if (this.CheckCookie())
            {
                if (extname.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('您未输入相应的伪静态url扩展名!');</script>");
                    return;
                }

                //if ((spaceurl.Text == "") || !spaceurl.Text.EndsWith(".aspx"))
                //{
                //    base.RegisterStartupScript("", "<script>alert('空间URL地址不能为空且必须以\".aspx结尾\"!');</script>");
                //    return;
                //}

                //if ((albumurl.Text == "") || !albumurl.Text.EndsWith(".aspx"))
                //{
                //    base.RegisterStartupScript("", "<script>alert('相册URL地址不能为空且必须以\".aspx结尾\"!');</script>");
                //    return;
                //}
                //if ((forumurl.Text == "") || !forumurl.Text.EndsWith(".aspx"))
                //{
                //    base.RegisterStartupScript("", "<script>alert('论坛URL地址不能为空且必须以\".aspx结尾\"!');</script>");
                //    return;
                //}
                if (!Utils.IsInt(notificationreserveddays.Text) || Utils.StrToInt(notificationreserveddays.Text, -1) < 0)
                {
                    base.RegisterStartupScript("", "<script>alert('通知保留天数只能为正数或0!');</script>");
                    return;
                }
                if (!Utils.IsInt(maxindexsubforumcount.Text) || Utils.StrToInt(maxindexsubforumcount.Text, -1) < 0)
                {
                    base.RegisterStartupScript("", "<script>alert('首页每个分类下最多显示版块数只能为正数或0!');</script>");
                    return;
                }
                if (!Utils.IsInt(deletingexpireduserfrequency.Text) || Utils.StrToInt(deletingexpireduserfrequency.Text, 0) < 1)
                {
                    base.RegisterStartupScript("", "<script>alert('删除离线用户频率只能为正数!');</script>");
                    return;
                }

                GeneralConfigInfo configInfo = GeneralConfigs.GetConfig();
                configInfo.Sitetitle = forumtitle.Text;
                //configInfo.Forumurl = forumurl.Text;
                configInfo.Notificationreserveddays = Utils.StrToInt(notificationreserveddays.Text, 0);
                configInfo.Maxindexsubforumcount = Utils.StrToInt(maxindexsubforumcount.Text, 0);
                configInfo.Deletingexpireduserfrequency = Utils.StrToInt(deletingexpireduserfrequency.Text, 1);
                configInfo.Webtitle = webtitle.Text;
                configInfo.Weburl = weburl.Text;
                configInfo.Licensed = Convert.ToInt16(licensed.SelectedValue);
                configInfo.Closed = Convert.ToInt16(closed.SelectedValue);
                configInfo.Closedreason = closedreason.Text;
                configInfo.Icp = icp.Text;
                configInfo.Rssttl = Convert.ToInt32(rssttl.Text);
                configInfo.Sitemapttl = Convert.ToInt32(sitemapttl.Text);
                configInfo.Nocacheheaders = Convert.ToInt16(nocacheheaders.SelectedValue);
                configInfo.Debug = Convert.ToInt16(debug.SelectedValue);
                configInfo.Rewriteurl = "";
                configInfo.Maxmodworksmonths = 3;
                configInfo.Rssstatus = Convert.ToInt16(rssstatus.SelectedValue);
                configInfo.Sitemapstatus = Convert.ToInt16(sitemapstatus.SelectedValue);
                configInfo.Cachelog = 0;
                if (fulltextsearch.SelectedValue == "1")
                {
                    string msg = "";
                    ////configInfo.Fulltextsearch = Databases.TestFullTextIndex(ref msg);
                    //configInfo.Fulltextsearch = 1;
                    //foreach (DataRow dr in Discuz.Forum.Posts.GetAllPostTableName().Rows)
                    //{
                    //    try
                    //    {
                    //       Databases.TestFullTextIndex(Utils.StrToInt(dr["id"], 0));
                    //    }
                    //    catch
                    //    {
                    //        base.RegisterStartupScript("", "<script>alert('您的数据库帖子表[" + BaseConfigs.GetTablePrefix + "posts" + dr["id"] + "]中暂未进行全文索引设置,因此使用数据库全文搜索无效');</script>");
                    //        configInfo.Fulltextsearch = 0;
                    //        break;
                    //    }
                    //}
                }
                else
                    configInfo.Fulltextsearch = 0;

                configInfo.Passwordmode = Convert.ToInt16(passwordmode.SelectedValue);
                configInfo.Bbcodemode = Convert.ToInt16(bbcodemode.SelectedValue);
                configInfo.Extname = extname.Text.Trim();
                configInfo.Silverlight = Convert.ToInt32(enablesilverlight.SelectedValue);
                configInfo.Enablespace = Convert.ToInt32(EnableSpace.SelectedValue);
                configInfo.Enablealbum = Convert.ToInt32(EnableAlbum.SelectedValue);
                //configInfo.Enablemall = Convert.ToInt32(EnableMall.SelectedValue);
                configInfo.CookieDomain = CookieDomain.Text;
                configInfo.Memliststatus = Convert.ToInt32(memliststatus.SelectedValue);
                configInfo.Indexpage = Convert.ToInt32(Indexpage.SelectedValue);
                configInfo.Linktext = Linktext.Text;
                configInfo.Statcode = Statcode.Text;
                configInfo.Spacename = spacename.Text;
                //configInfo.Spaceurl = spaceurl.Text;
                configInfo.Albumname = albumname.Text;
                //configInfo.Albumurl = albumurl.Text;
                configInfo.Aspxrewrite = Convert.ToInt16(aspxrewrite.SelectedValue);
                configInfo.Iisurlrewrite = Convert.ToInt16(iisurlrewrite.SelectedValue);
                configInfo.Onlineoptimization = Convert.ToInt32(onlineoptimization.SelectedValue);
                configInfo.OnlineUserCountCacheMinute = Convert.ToInt32(onlineusercountcacheminute.Text);
                GeneralConfigs.Serialiaze(configInfo, Server.MapPath("../../config/general.config"));
                Urls.config = configInfo;
                ////if (configInfo.Aspxrewrite == 1)
                ////    AdminForums.SetForumsPathList(true, configInfo.Extname);
                ////else
                ////    AdminForums.SetForumsPathList(false, configInfo.Extname);
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/ForumList");
                SAS.Logic.TopicStats.SetQueueCount();
                Caches.ReSetConfig();
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "基本设置", "");
                base.RegisterStartupScript("PAGE", "window.location.href='global_baseset.aspx';");
            }
            #endregion
        } 

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
            forumtitle.IsReplaceInvertedComma = false;
            //forumurl.IsReplaceInvertedComma = false;
            webtitle.IsReplaceInvertedComma = false;
            weburl.IsReplaceInvertedComma = false;
            icp.IsReplaceInvertedComma = false;
        }
        #endregion
    }
}
