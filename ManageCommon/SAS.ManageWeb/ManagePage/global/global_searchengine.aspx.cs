﻿using System;
using System.Web.UI;

using SAS.Control;
using SAS.Logic;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 搜索引擎优化
    /// </summary>
    public partial class global_searchengine : AdminPage
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

            GeneralConfigInfo configInfo = GeneralConfigs.GetConfig();
            seotitle.Text = configInfo.Seotitle.ToString();
            seokeywords.Text = configInfo.Seokeywords.ToString();
            seodescription.Text = configInfo.Seodescription.ToString();
            seohead.Text = configInfo.Seohead.ToString();
            archiverstatus.SelectedValue = configInfo.Archiverstatus.ToString();

            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region 保存设置信息

            if (this.CheckCookie())
            {
                GeneralConfigInfo configInfo = GeneralConfigs.GetConfig();

                configInfo.Seotitle = seotitle.Text;
                configInfo.Seokeywords = seokeywords.Text;
                configInfo.Seodescription = seodescription.Text;
                configInfo.Seohead = seohead.Text;
                configInfo.Archiverstatus = Convert.ToInt16(archiverstatus.SelectedValue);

                GeneralConfigs.Serialiaze(configInfo, Server.MapPath("../../config/general.config"));

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "搜索引擎优化设置", "");

                base.RegisterStartupScript("PAGE", "window.location.href='global_searchengine.aspx';");
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
        }

        #endregion
    }
}
