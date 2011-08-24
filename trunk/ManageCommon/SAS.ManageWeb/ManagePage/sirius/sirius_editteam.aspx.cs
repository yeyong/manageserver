﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Cache;
using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.Sirius;

namespace SAS.ManageWeb.ManagePage
{
    public partial class sirius_editteam : AdminPage
    {
        public TeamInfo teamin = new TeamInfo();
        private SiriusPluginBase spb = SiriusPluginProvider.GetInstance();

        public void LoadTeamInfo(int tid)
        {
            teamin = spb.GetTeamByTeamID(tid);

            templateid.AddTableData(Templates.GetValidTemplateList(), "tp_name", "tp_id");
            templateid.SelectedValue = teamin.Templateid.ToString();

            teamurl.Text = teamin.Teamdomain;

            name.Text = teamin.Name;
            status.SelectedValue = teamin.Stutas.ToString();
            moderators.Text = teamin.TeamMember.ToString().Trim();
            teamImg.Text = teamin.Imgs.ToString();
            seokeywords.Text = teamin.Seokeywords;
            seodescription.Text = teamin.Seodescription;
            teamBio.Text = teamin.Bio;
            content1.Text = teamin.Content1;
            content2.Text = teamin.Content2;
            content3.Text = teamin.Content3;
        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void DeleteUserGroupInf_Click(object sender, EventArgs e)
        {

        }

        private void UpdateUserGroupInf_Click(object sender, EventArgs e)
        {
            #region 更新团队信息

            if (this.CheckCookie())
            {
                teamin = spb.GetTeamByTeamID(SASRequest.GetInt("teamID", 0));

                teamin.Name = name.Text.Trim();
                teamin.Teamdomain = teamurl.Text.Trim();
                teamin.Templateid = Convert.ToInt32(templateid.SelectedValue);
                teamin.Imgs = teamImg.Text.Trim();
                teamin.Bio = teamBio.Text.Trim();
                teamin.UpdateDate = System.DateTime.Now.ToString();
                teamin.Content1 = content1.Text;
                teamin.Content2 = content2.Text;
                teamin.Content3 = content3.Text;
                teamin.TeamMember = moderators.Text.Trim();
                teamin.Stutas = Convert.ToInt16(status.SelectedValue);
                teamin.Displayorder = 0;
                teamin.Seodescription = seodescription.Text.Trim();
                teamin.Seokeywords = seokeywords.Text.Trim();

                string message = "";
                if (spb.UpdateTeamInfo(teamin,out message))
                {
                    if (message == "")
                    {
                        base.RegisterStartupScript("PAGE", "self.location.href='sirius_manageteam.aspx';");
                    }
                    else
                    {
                        base.RegisterStartupScript("PAGE", "alert('用户:" + message + "不存在或不是指定用户组,因为无法设为团队成员');self.location.href='sirius_manageteam.aspx';");
                    }
                    AdminVistLogs.InsertLog(userid, username, usergroupid, grouptitle, ip, "修改团队信息", "更新团队团队信息：" + teamin.Name);
                }
                else
                {
                    base.RegisterStartupScript("PAGE", "alert('更新团队信息失败!');self.location.href='sirius_manageteam.aspx';");
                }
            }

            #endregion
        }

        private void InitializeComponent()
        {
            this.TabControl1.InitTabPage();
            this.UpdateUserGroupInf.Click += new EventHandler(this.UpdateUserGroupInf_Click);
            this.DeleteUserGroupInf.Click += new EventHandler(this.DeleteUserGroupInf_Click);
            if (SASRequest.GetString("teamID") != "")
            {
                LoadTeamInfo(SASRequest.GetInt("teamID", 0));
            }
            else
            {
                Response.Redirect("sirius_manageteam.aspx");
            }

        }

        #endregion     
    }
}