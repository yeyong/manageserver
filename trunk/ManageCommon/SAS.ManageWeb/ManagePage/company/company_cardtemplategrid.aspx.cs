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
    public partial class company_cardtemplategrid : AdminPage
    {
        public string path = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTemplateData();
                if (SASRequest.GetString("createtemplate") != "" && SASRequest.GetInt("cardtempid", 0) > 0)
                {
                    ResetCardTemplate(SASRequest.GetString("createtemplate"), SASRequest.GetInt("cardtempid", 0));
                    base.RegisterStartupScript("PAGE", "window.location.href='company_cardtemplategrid.aspx';");
                }
            }
        }

        private DataTable buildGridData()
        {
            return AdminTemplates.GetAllCardTemplateList(path);
        }

        public void LoadTemplateData()
        {
            #region 加载模板数据

            path = Utils.GetMapPath(@"..\..\cardtemplate\");

            string templatepath = "由于目录 : ";
            string templateidlist = "0";
            foreach (DataRow dr in buildGridData().Select("valid =1"))
            {
                DirectoryInfo dirinfo = new DirectoryInfo(path + dr["directory"].ToString() + "/");
                if (dr["directory"].ToString().ToLower() == "default")
                    continue;
                if (!dirinfo.Exists)
                {
                    templatepath += dr["directory"].ToString() + " ,";
                    templateidlist += "," + dr["id"].ToString();
                }
            }

            if ((templateidlist != "") && (templateidlist != "0"))
            {
                base.RegisterStartupScript("", "<script>alert('" + templatepath.Substring(0, templatepath.Length - 1) + "已被删除, 因此系统将自动更新名片模板列表!')</script>");
                AdminTemplates.DeleteCardTemplateItem(templateidlist);
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "从数据库中删除名片模板文件", "ID为:" + templateidlist);
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CardTemplateIDList");
                SAS.Logic.Templates.GetValidCardTemplateIDList();
            }

            DataGrid1.AllowCustomPaging = false;
            DataGrid1.DataSource = buildGridData();
            DataGrid1.DataBind();

            #endregion
        }

        private void ResetCardTemplate(string directorypath,int templateid)
        {
            #region 更新名片文件

            if (this.CheckCookie())
            {
                if (Directory.Exists(Server.MapPath("..\\..\\cardimg\\" + templateid)))
                {
                    try
                    {
                        Directory.Delete(Server.MapPath("..\\..\\cardimg\\" + templateid), true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        RegisterStartupScript("", "<script>alert('您的目录设置了权限导致无法在此删除此文件夹');</script>");
                        return;
                    }
                }
                else
                {
                    RegisterStartupScript("", "<script>alert('文件夹" + Server.MapPath("..\\..\\cardimg\\" + templateid) + "不存在" + templateid + "！');</script>");
                    return;
                }
            }
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CardTemplateList");
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CardTemplateIDList");
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
                        base.RegisterStartupScript("", "<script>alert('选中的模板中含有系统初始化模板,此次提交无法执行');window.location.href='company_cardtemplategrid.aspx'</script>");
                        return;
                    }

                    path = Utils.GetMapPath(@"..\..\cardtemplate\");
                    int maxdbtemplateid = TypeConverter.ObjectToInt(Templates.GetValidCardTemplateList().Compute("Max(id)", ""));

                    foreach (string templateid in templateidlist.Split(','))
                    {
                        if (Utils.StrToInt(templateid, 0) < maxdbtemplateid)
                        {
                            base.RegisterStartupScript("", "<script>alert('选中入库的模板中含有已入库的模板,此次提交无法执行');window.location.href='company_cardtemplategrid.aspx'</script>");
                            return;
                        }
                    }

                    foreach (DataRow dr in buildGridData().Select("[id] IN (" + templateidlist + ") AND [id] > " + maxdbtemplateid))
                    {
                        AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "模板文件入库", dr["name"].ToString().Trim());

                        try
                        {
                            Templates.CreateCardTemplate(dr["name"].ToString().Trim(),
                            dr["directory"].ToString().Trim(),
                            dr["copyright"].ToString().Trim(),
                            dr["author"].ToString().Trim(),
                            dr["createdate"].ToString().Trim(),
                            dr["ver"].ToString().Trim(),
                            dr["currentfile"].ToString().Trim());
                        }
                        catch(SASException ex)
                        {
                            base.RegisterStartupScript("", "<script>alert('无法更新数据库," + ex.Message + "');window.location.href='company_cardtemplategrid.aspx';</script>");
                        }

                        //CreateTemplateByDirectory(dr["directory"].ToString().Trim());
                    }

                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CardTemplateList");
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CardTemplateIDList");
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/UI/CardTemplateListBoxOptions");
                    base.RegisterStartupScript("PAGE", "window.location.href='company_cardtemplategrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何选项');window.location.href='company_cardtemplategrid.aspx';</script>");
                }
            }

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
                        base.RegisterStartupScript("", "<script>alert('选中的模板中含有系统初始化模板,此次提交无法执行');window.location.href='company_cardtemplategrid.aspx'</script>");
                        return;
                    }

                    int maxdbtemplateid = TypeConverter.ObjectToInt(Templates.GetValidCardTemplateList().Compute("Max(id)", ""));
                    foreach (string templateid in templateidlist.Split(','))
                    {
                        if (Utils.StrToInt(templateid, 0) > maxdbtemplateid)
                        {
                            base.RegisterStartupScript("", "<script>alert('选中出库的模板中含有已出库模板,此次提交无法执行');window.location.href='company_cardtemplategrid.aspx'</script>");
                            return;
                        }
                    }

                    AdminTemplates.RemoveCardTemplateInDB(templateidlist, userid, username, usergroupid, grouptitle, ip);
                    Response.Redirect("company_cardtemplategrid.aspx");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何选项'); window.location.href='company_cardtemplategrid.aspx';</script>");
                }
            }

            #endregion
        }

        public string CreateStr(string name, string path, string templateid)
        {
            string result = "<a href=company_cardtemplatetree.aspx?path=" + path.Trim().Replace(" ", "%20") + "&templateid=" + templateid.Trim() + "&templatename=" + name.Trim().Replace(" ", "%20") + ">管理</a>&nbsp;&nbsp;";
            result += "<a href=\"javascript:CreateTemplate('" + name + "','" + templateid + "')\">生成</a>";
            return result;
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
                        RegisterStartupScript("", "<script>alert('您选中的数据项中含有系统初始化模板,此次提交无法执行');window.location.href='company_cardtemplategrid.aspx'</script>");
                        return;
                    }
                    AdminTemplates.DeleteCardTemplate(templateidlist, userid, username, usergroupid, grouptitle, ip);
                    Response.Redirect("company_cardtemplategrid.aspx");
                }
                else
                {
                    RegisterStartupScript("", "<script>alert('您未选中任何选项'); window.location.href='company_cardtemplategrid.aspx';</script>");
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
            DataGrid1.TableHeaderName = "名片模板列表";
            DataGrid1.DataKeyField = "id";
            DataGrid1.AllowPaging = false;
            DataGrid1.AllowSorting = false;
            DataGrid1.ShowFooter = false;
        }

        #endregion
    }
}