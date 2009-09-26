﻿using System;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

using SAS.Common;
using SAS.Entity;
using SAS.Logic;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 模板列表
    /// </summary>
    public partial class global_templatesgrid : SAS.Web.UI.AdminPage
    {
        public string path = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTemplateData();
                if (SASRequest.GetString("createtemplate") != "")
                {
                    CreateTemplateByDirectory(SASRequest.GetString("createtemplate"));
                    base.RegisterStartupScript("PAGE", "window.location.href='global_templatesgrid.aspx';");
                }
            }
        }


        private DataTable buildGridData()
        {
            return AdminTemplates.GetAllTemplateList(path);
        }

        public void LoadTemplateData()
        {
            #region 加载模板数据

            path = Utils.GetMapPath(@"..\..\templates\");

            string templatepath = "由于目录 : ";
            string templateidlist = "0";
            foreach (DataRow dr in buildGridData().Select("valid =1"))
            {
                DirectoryInfo dirinfo = new DirectoryInfo(path + dr["tp_directory"].ToString() + "/");
                if (dr["tp_directory"].ToString().ToLower() == "default")
                    continue;
                if (!dirinfo.Exists)
                {
                    templatepath += dr["tp_directory"].ToString() + " ,";
                    templateidlist += "," + dr["tp_id"].ToString();
                }
            }

            if ((templateidlist != "") && (templateidlist != "0"))
            {
                base.RegisterStartupScript("", "<script>alert('" + templatepath.Substring(0, templatepath.Length - 1) + "已被删除, 因此系统将自动更新模板列表!')</script>");
                AdminTemplates.DeleteTemplateItem(templateidlist);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "从数据库中删除模板文件", "ID为:" + templateidlist);
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TemplateIDList");
                SAS.Logic.Templates.GetValidTemplateIDList();
            }

            DataGrid1.AllowCustomPaging = false;
            DataGrid1.DataSource = buildGridData();
            DataGrid1.DataBind();

            #endregion
        }


        private void DelRec_Click(object sender, EventArgs e)
        {
            #region 将指定的模板从数据库中删除

            if (this.CheckCookie())
            {
                string templateidlist = SASRequest.GetString("templateid");
                if (templateidlist != "")
                {
                    if (("," + templateidlist + ",").IndexOf(",1,") >= 0)
                    {
                        base.RegisterStartupScript("", "<script>alert('选中的模板中含有系统初始化模板,此次提交无法执行');window.location.href='global_templatesgrid.aspx'</script>");
                        return;
                    }

                    int maxdbtemplateid = TypeConverter.ObjectToInt(Templates.GetValidTemplateList().Compute("Max(tp_id)", ""));
                    foreach (string templateid in templateidlist.Split(','))
                    {
                        if (Utils.StrToInt(templateid, 0) > maxdbtemplateid)
                        {
                            base.RegisterStartupScript("", "<script>alert('选中出库的模板中含有已出库模板,此次提交无法执行');window.location.href='global_templatesgrid.aspx'</script>");
                            return;
                        }
                    }

                    AdminTemplates.RemoveTemplateInDB(templateidlist, userid, username, usergroupid, grouptitle, ip);
                    Response.Redirect("global_templatesgrid.aspx");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何选项'); window.location.href='global_templatesgrid.aspx';</script>");
                }
            }

            #endregion
        }

        public string DirectoryStr(string path)
        {
            #region 获取路径信息

            if (path.ToLower().IndexOf("http") >= 0)
            {
                return "<a href=" + path + ">点击浏览</a>";
            }
            else
            {
                if (path.ToLower().IndexOf("templates/default") >= 0)
                {
                    return "<a href=../../" + path + ".htm >点击浏览</a>";
                }
                else
                {
                    return "<a href=../../templates/default/" + path + ".htm >点击浏览</a>";
                }
            }

            #endregion
        }

        private void CreateTemplateByDirectory(string directorypath)
        {
            #region 生成模板

            if (this.CheckCookie())
            {

                SAS.Web.UI.Globals.BuildTemplate(directorypath);
            }

            #endregion
        }



        private void IntoDB_Click(object sender, EventArgs e)
        {
            #region 入库

            if (this.CheckCookie())
            {
                string templateidlist = SASRequest.GetString("templateid");
                if (templateidlist != "")
                {
                    if ((templateidlist == "1") || (templateidlist.IndexOf(",1,") >= 0))
                    {
                        base.RegisterStartupScript("", "<script>alert('选中的模板中含有系统初始化模板,此次提交无法执行');window.location.href='global_templatesgrid.aspx'</script>");
                        return;
                    }

                    path = Utils.GetMapPath(@"..\..\templates\");
                    int maxdbtemplateid = TypeConverter.ObjectToInt(Templates.GetValidTemplateList().Compute("Max(tp_id)", ""));

                    foreach (string templateid in templateidlist.Split(','))
                    {
                        if (Utils.StrToInt(templateid, 0) < maxdbtemplateid)
                        {
                            base.RegisterStartupScript("", "<script>alert('选中入库的模板中含有已入库的模板,此次提交无法执行');window.location.href='global_templatesgrid.aspx'</script>");
                            return;
                        }
                    }

                    foreach (DataRow dr in buildGridData().Select("tp_id IN (" + templateidlist + ") AND tp_id > " + maxdbtemplateid))
                    {
                        AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "模板文件入库", dr["tp_name"].ToString().Trim());

                        try
                        {
                            Templates.CreateTemplate(dr["tp_name"].ToString().Trim(),
                            dr["tp_directory"].ToString().Trim(),
                            dr["tp_copyright"].ToString().Trim(),
                            dr["tp_author"].ToString().Trim(),
                            dr["tp_createdate"].ToString().Trim(),
                            dr["tp_ver"].ToString().Trim(),
                            dr["tp_fordntver"].ToString().Trim());
                        }
                        catch
                        {
                            base.RegisterStartupScript("", "<script>alert('无法更新数据库');window.location.href='global_templatesgrid.aspx';</script>");
                        }

                        CreateTemplateByDirectory(dr["tp_directory"].ToString().Trim());
                    }

                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TemplateList");
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/TemplateIDList");
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/UI/TemplateListBoxOptions");
                    base.RegisterStartupScript("PAGE", "window.location.href='global_templatesgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何选项');window.location.href='global_templatesgrid.aspx';</script>");
                }
            }

            #endregion
        }


        public string CreateStr(string name, string path, string templateid)
        {
            string result = "<a href=global_templatetree.aspx?path=" + path.Trim().Replace(" ", "%20") + "&templateid=" + templateid.Trim() + "&templatename=" + name.Trim().Replace(" ", "%20") + ">管理</a>&nbsp;&nbsp;";
            result += "<a href=\"javascript:CreateTemplate('" + name + "')\">生成</a>";
            return result;
        }

        public string Valid(string valid)
        {
            #region 生成状态图标
            if (valid == "1")
            {
                return "<div align=center><img src=../images/state2.gif /></div>";
            }
            else
            {
                return "<div align=center><img src=../images/state3.gif /></div>";
            }
            #endregion
        }

        protected void DelTemplates_Click(object sender, EventArgs e)
        {
            #region 删除模板
            if (CheckCookie())
            {
                string templateidlist = SASRequest.GetString("templateid");
                if (templateidlist != "")
                {
                    if (("," + templateidlist + ",").IndexOf(",1,") >= 0)
                    {
                        RegisterStartupScript("", "<script>alert('您选中的数据项中含有系统初始化模板,此次提交无法执行');window.location.href='global_templatesgrid.aspx'</script>");
                        return;
                    }
                    AdminTemplates.DeleteTemplate(templateidlist, userid, username, usergroupid, grouptitle, ip);
                    Response.Redirect("global_templatesgrid.aspx");
                }
                else
                {
                    RegisterStartupScript("", "<script>alert('您未选中任何选项'); window.location.href='global_templatesgrid.aspx';</script>");
                }
            }
            #endregion
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.DelRec.Click += new EventHandler(this.DelRec_Click);
            this.IntoDB.Click += new EventHandler(this.IntoDB_Click);

            DataGrid1.SaveDSViewState = true;
            DataGrid1.TableHeaderName = "模板列表";
            DataGrid1.DataKeyField = "tp_id";
            DataGrid1.AllowPaging = false;
            DataGrid1.AllowSorting = false;
            DataGrid1.ShowFooter = false;
        }

        #endregion
    }
}
