using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Text.RegularExpressions;

using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Web.UI;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 快捷操作
    /// </summary>
    public partial class shortcut : AdminPage
    {
        public string filenamelist = "";
        protected bool isNew = false;
        protected bool isHotFix = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTemplateInfo();
            //加载论坛版块信息
            forumid.BuildTree(Forums.GetForumListForDataTable(), "name", "fid");
        }
        public void LoadTemplateInfo()
        {
            #region 加载模板路径信息

            DirectoryInfo dirinfo = new DirectoryInfo(Server.MapPath("../../templates/" + Templatepath.SelectedValue + "/"));

            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    string extname = file.Extension.ToLower();

                    if (extname.Equals(".htm") && (file.Name.IndexOf("_") != 0))
                    {
                        filenamelist += file.Name.Split('.')[0] + "|";
                    }
                }
            }

            #endregion
        }

        private void UpdateForumStatistics_Click(object sender, EventArgs e)
        {
            #region 更新统计信息

            if (this.CheckCookie())
            {
                Caches.ReSetStatistics();
                base.RegisterStartupScript("PAGE", "window.location.href='shortcut.aspx';");
            }

            #endregion
        }

        private void CreateTemplate_Click(object sender, EventArgs e)
        {
            #region 生成指定模板

            if (this.CheckCookie())
            {
                Globals.BuildTemplate(Templatepath.SelectedValue);

                base.RegisterStartupScript("PAGE", "window.location.href='shortcut.aspx';");
            }

            #endregion
        }

        private void UpdateCache_Click(object sender, EventArgs e)
        {
            #region 更新所有缓存

            if (this.CheckCookie())
            {
                Caches.ReSetAllCache();
                base.RegisterStartupScript("PAGE", "window.location.href='shortcut.aspx';");
            }

            #endregion
        }

        private void EditUserGroup_Click(object sender, EventArgs e)
        {
            #region 重定向到指定的用户组编辑页面

            if (Usergroupid.SelectedValue != "0")
            {
                int groupid = Convert.ToInt32(Usergroupid.SelectedValue);
                if (groupid >= 1 && groupid <= 3)
                {
                    Response.Redirect("../global/global_editadminusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }
                if (groupid >= 4 && groupid <= 8)
                {
                    Response.Redirect("../global/global_editsysadminusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }

                int radminid = UserGroups.GetUserGroupInfo(Utils.StrToInt(Usergroupid.SelectedValue, 0)).ug_pg_id;
                if (radminid == 0)
                {
                    Response.Redirect("../global/global_editusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }
                if (radminid > 0)
                {
                    Response.Redirect("../global/global_editadminusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }
                if (radminid < 0)
                {
                    Response.Redirect("../global/global_editusergroupspecial.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }

            }
            else
            {
                base.RegisterStartupScript("", "<script>alert('请您选择有效的用户组!');</script>");
            }

            #endregion
        }

        private void EditForum_Click(object sender, EventArgs e)
        {
            #region 重定向到指定的版块编辑页面

            if (forumid.SelectedValue != "0")
            {
                Response.Redirect("../forum/forum_EditForums.aspx?fid=" + forumid.SelectedValue);
            }
            else
            {
                base.RegisterStartupScript("", "<script>alert('请您选择有效的栏目版块!');</script>");
            }

            #endregion
        }

        private void EditUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("../global/global_usergrid.aspx?username=" + Username.Text.Trim());
        }

        private void UpdateTaoSiteMap_Click(object sender, EventArgs e)
        {
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TaoSiteMap");
            //SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoSiteMap", true);
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.EditUser.Click += new EventHandler(this.EditUser_Click);
            this.EditForum.Click += new EventHandler(this.EditForum_Click);
            this.EditUserGroup.Click += new EventHandler(this.EditUserGroup_Click);
            this.UpdateCache.Click += new EventHandler(this.UpdateCache_Click);
            this.CreateTemplate.Click += new EventHandler(this.CreateTemplate_Click);
            this.UpdateForumStatistics.Click += new EventHandler(this.UpdateForumStatistics_Click);
            this.UpdateTaoSiteMap.Click += new EventHandler(UpdateTaoSiteMap_Click);
            //装入有效的模板信息项
            foreach (DataRow dr in AdminTemplates.GetAllTemplateList(Utils.GetMapPath(@"..\..\templates\")).Rows)
            {
                if (dr["valid"].ToString() == "1")
                {
                    Templatepath.Items.Add(new ListItem(dr["tp_name"].ToString(), dr["tp_directory"].ToString()));
                }
            }
            Username.AddAttributes("onkeydown", "if(event.keyCode==13) return(document.forms(0).EditUser.focus());");
            Usergroupid.AddTableData(UserGroups.GetUserGroupForDataTable(), "ug_name", "ug_id");
        }

        #endregion

    }
}
