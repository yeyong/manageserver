﻿using System;
using System.Web.UI;

using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Common;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_edithelpclass : AdminPage
    {
        public int id = SASRequest.GetInt("id", 0);
        public HelpInfo helpinfo = new HelpInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                helpinfo = Helps.GetMessage(id);

                if ((this.username != null) && (this.username != ""))
                {
                    if (id == 0)
                    {
                        return;
                    }
                    else
                    {
                        poster.Text = this.username;
                        orderby.Text = helpinfo.Orderby.ToString();
                        title.Text = helpinfo.Title;
                    }
                }
            }
        }

        protected void updateclass_Click(object sender, EventArgs e)
        {
            Helps.UpdateHelp(this.id, title.Text, "", 0, int.Parse(orderby.Text));
            Response.Redirect("global_helplist.aspx");
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.updateclass.Click += new EventHandler(this.updateclass_Click);
        }

        #endregion
    }
}
