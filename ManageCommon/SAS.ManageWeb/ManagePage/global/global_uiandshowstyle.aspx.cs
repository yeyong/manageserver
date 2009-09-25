﻿using System;
using System.Web.UI;
using System.Collections;

using SAS.Common;
using SAS.Control;
using SAS.Logic;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_uiandshowstyle : SAS.Web.UI.AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadConfigInfo();
            }
        }

        public void LoadConfigInfo()
        {
            #region 加载配置信息

            templateid.Attributes.Add("onchange", "LoadImage(this.selectedIndex)");
            GeneralConfigInfo configInfo = GeneralConfigs.GetConfig();
            //templateid.SelectedValue = configInfo.Templateid.ToString();
            stylejump.SelectedValue = configInfo.Stylejump.ToString();
            browsecreatetemplate.SelectedValue = configInfo.BrowseCreateTemplate.ToString();
            templateid.AddTableData(Templates.GetValidTemplateList(), "name", "templateid");


            templateid.SelectedValue = configInfo.Templateid.ToString();

            templateid.Items.RemoveAt(0);
            string scriptstr = "<script type=\"text/javascript\">\r\n";
            scriptstr += "images = new Array();\r\n";
            for (int i = 0; i < templateid.Items.Count; i++)
            {
                scriptstr += "images[" + i + "]=\"../../templates/" + templateid.Items[i].Text + "/about.png\";\r\n";
            }
            scriptstr += "</script>";
            base.RegisterStartupScript("", scriptstr);
            preview.Src = "../../templates/" + templateid.SelectedItem.Text + "/about.png";
            isframeshow.SelectedValue = configInfo.Isframeshow.ToString();
            whosonlinestatus.SelectedValue = configInfo.Whosonlinestatus.ToString();
            maxonlinelist.Text = configInfo.Maxonlinelist.ToString();
            forumjump.SelectedValue = configInfo.Forumjump.ToString();
            if (configInfo.Onlinetimeout >= 0) showauthorstatusinpost.SelectedValue = "2";
            else showauthorstatusinpost.SelectedValue = "1";
            onlinetimeout.Text = Math.Abs(configInfo.Onlinetimeout).ToString();
            smileyinsert.SelectedValue = configInfo.Smileyinsert.ToString();
            visitedforums.Text = configInfo.Visitedforums.ToString();
            moddisplay.SelectedValue = configInfo.Moddisplay.ToString();
            showsignatures.SelectedValue = configInfo.Showsignatures.ToString();
            showavatars.SelectedValue = configInfo.Showavatars.ToString();
            showimages.SelectedValue = configInfo.Showimages.ToString();
            maxsigrows.Text = configInfo.Maxsigrows.ToString();
            smiliesmax.Text = configInfo.Smiliesmax.ToString();
            viewnewtopicminute.Text = configInfo.Viewnewtopicminute.ToString();
            whosonlinecontact.SelectedValue = configInfo.Whosonlinecontract.ToString();
            postnocustom.Text = configInfo.Postnocustom;
            allowfloatwin.SelectedValue = configInfo.Allowfloatwin.ToString();
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region 保存设置信息

            if (this.CheckCookie())
            {
                SortedList sl = new SortedList();
                sl.Add("无动作离线时间", onlinetimeout.Text);
                sl.Add("最大签名高度", maxsigrows.Text);
                sl.Add("显示最近访问论坛数量", visitedforums.Text);
                sl.Add("帖子中同一表情符出现的最大次数", smiliesmax.Text);

                foreach (DictionaryEntry s in sl)
                {
                    if (!Utils.IsInt(s.Value.ToString()))
                    {
                        base.RegisterStartupScript("", "<script>alert('输入错误:" + s.Key.ToString() + ",只能是0或者正整数');window.location.href='forum_uisetting.aspx';</script>");
                        return;
                    }
                }

                if (Convert.ToInt32(onlinetimeout.Text) <= 0)
                {
                    base.RegisterStartupScript("", "<script>alert('无动作离线时间必须大于0');</script>");
                    return;
                }
                if (Convert.ToInt16(maxsigrows.Text) > 9999 || (Convert.ToInt16(maxsigrows.Text) < 0))
                {
                    base.RegisterStartupScript("", "<script>alert('最大签名高度只能在0-9999之间');window.location.href='.aspx';</script>");
                    return;
                }


                if (Convert.ToInt16(visitedforums.Text) > 9999 || (Convert.ToInt16(visitedforums.Text) < 0))
                {
                    base.RegisterStartupScript("", "<script>alert('显示最近访问论坛数量只能在0-9999之间');window.location.href='forum_uisetting.aspx';</script>");
                    return;
                }


                if (Convert.ToInt16(smiliesmax.Text) > 1000 || (Convert.ToInt16(smiliesmax.Text) < 0))
                {
                    base.RegisterStartupScript("", "<script>alert('帖子中同一表情符出现的最大次数只能在0-1000之间');window.location.href='forum_uisetting.aspx';</script>");
                    return;
                }

                if (Convert.ToInt16(viewnewtopicminute.Text) > 14400 || (Convert.ToInt16(viewnewtopicminute.Text) < 5))
                {
                    base.RegisterStartupScript("", "<script>alert('查看新帖的设置必须在5-14400之间');window.location.href='forum_uisetting.aspx';</script>");
                    return;
                }

                GeneralConfigInfo configInfo = GeneralConfigs.GetConfig();

                configInfo.Templateid = Convert.ToInt16(templateid.SelectedValue);
                configInfo.Subforumsindex = 1;
                configInfo.Stylejump = Convert.ToInt16(stylejump.SelectedValue);
                configInfo.BrowseCreateTemplate = Convert.ToInt32(browsecreatetemplate.SelectedValue);
                configInfo.Isframeshow = Convert.ToInt16(isframeshow.SelectedValue);
                configInfo.Whosonlinestatus = Convert.ToInt16(whosonlinestatus.SelectedValue);

                if (showauthorstatusinpost.SelectedValue == "1") configInfo.Onlinetimeout = 0 - Convert.ToInt32(onlinetimeout.Text);
                else configInfo.Onlinetimeout = Convert.ToInt16(onlinetimeout.Text);

                configInfo.Maxonlinelist = Convert.ToInt16(maxonlinelist.Text);
                configInfo.Forumjump = Convert.ToInt16(forumjump.SelectedValue);
                configInfo.Smileyinsert = Convert.ToInt16(smileyinsert.SelectedValue);
                configInfo.Visitedforums = Convert.ToInt16(visitedforums.Text);
                configInfo.Moddisplay = Convert.ToInt16(moddisplay.SelectedValue);
                configInfo.Showsignatures = Convert.ToInt16(showsignatures.SelectedValue);
                configInfo.Showavatars = Convert.ToInt16(showavatars.SelectedValue);
                configInfo.Showimages = Convert.ToInt16(showimages.SelectedValue);
                configInfo.Smiliesmax = Convert.ToInt16(smiliesmax.Text);
                configInfo.Maxsigrows = Convert.ToInt16(maxsigrows.Text);
                configInfo.Viewnewtopicminute = Convert.ToInt16(viewnewtopicminute.Text);
                configInfo.Whosonlinecontract = Convert.ToInt16(whosonlinecontact.SelectedValue);
                configInfo.Postnocustom = postnocustom.Text;
                configInfo.Allowfloatwin = Convert.ToInt16(allowfloatwin.SelectedValue);

                GeneralConfigs.Serialiaze(configInfo, Server.MapPath("../../config/general.config"));
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "界面与显示方式设置", "");
                base.RegisterStartupScript("PAGE", "window.location.href='global_uiandshowstyle.aspx';");
            }

            #endregion
        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }

        #endregion
    }
}
