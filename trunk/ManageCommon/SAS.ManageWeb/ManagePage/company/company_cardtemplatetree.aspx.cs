using System;
using System.IO;
using System.Text;
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
    public partial class company_cardtemplatetree : AdminPage
    {
        private string skinpath;
        private int templateCounter = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            skinpath = SASRequest.GetString("path");
            DeleteTemplateFile.Enabled = CreateTemplate.Enabled = !(TreeView1.SelectedIndex == -1);
        }

        private DataTable LoadTemplateFileDT()
        {
            #region 装入模板文件
            DataTable templateFileList = new DataTable("cardtemplatefilelist");

            templateFileList.Columns.Add("fullfilename", Type.GetType("System.String"));
            templateFileList.Columns.Add("filename", Type.GetType("System.String"));
            templateFileList.Columns.Add("id", Type.GetType("System.Int32"));
            templateFileList.Columns.Add("extension", Type.GetType("System.String"));
            templateFileList.Columns.Add("parentid", Type.GetType("System.String"));
            templateFileList.Columns.Add("filepath", Type.GetType("System.String"));
            templateFileList.Columns.Add("filedescription", Type.GetType("System.String"));

            string path = SASRequest.GetString("path");
            //先以默认模板目录创建文件列表
            CreateTemplateFileList(templateFileList, "default", false);
            if (path.ToLower() != "defalut")
            {
                CreateTemplateFileList(templateFileList, path, true);
            }
            foreach (DataRow dr in templateFileList.Rows)
            {
                foreach (DataRow childdr in templateFileList.Select("filename like '" + dr["filename"] + "_%%'"))
                {
                    if (dr["filename"].ToString() != childdr["filename"].ToString())
                    {
                        childdr["parentid"] = dr["id"].ToString();
                    }
                }
            }

            return templateFileList;
            #endregion
        }

        private void CreateTemplateFileList(DataTable templateFileList, string path, bool resetList)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("..\\..\\cardtemplate\\" + path));
            foreach (FileSystemInfo file in dirInfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    if (file.Name == "images")
                        continue;
                    if (file.Attributes == FileAttributes.Directory)
                    {
                        CreateTemplateFileList(templateFileList, path + "\\" + file.Name, resetList);
                    }
                    else
                    {
                        if (file.Name.ToLower().EndsWith(".swf") || file.Name.ToLower().EndsWith(".config") || file.Name.ToLower().EndsWith(".jpg"))
                        {
                            if (resetList)
                            {
                                DataRow[] drs = templateFileList.Select("filename='" + file.Name.Substring(0, file.Name.LastIndexOf(".")) + "'");
                                if (drs.Length != 0)
                                {
                                    drs[0]["filename"] = file.Name.Substring(0, file.Name.LastIndexOf("."));
                                    drs[0]["fullfilename"] = path + "\\" + drs[0]["filename"] + file.Extension.ToLower();
                                    drs[0]["extension"] = file.Extension.ToLower();
                                    drs[0]["filepath"] = path;
                                    continue;
                                }
                            }
                            DataRow dr = templateFileList.NewRow();
                            dr["id"] = templateCounter;
                            dr["filename"] = file.Name.Substring(0, file.Name.LastIndexOf("."));
                            dr["fullfilename"] = path + "\\" + dr["filename"] + file.Extension.ToLower();
                            dr["extension"] = file.Extension.ToLower();
                            dr["parentid"] = "0";
                            dr["filepath"] = path;
                            dr["filedescription"] = "";
                            templateFileList.Rows.Add(dr);
                            templateCounter++;
                        }
                    }
                }
            }
        }

        private DataTable LoadOtherFileDT()
        {
            #region 装入其它文件
            DataTable otherfilelist = new DataTable("otherfilelist");

            otherfilelist.Columns.Add("id", Type.GetType("System.Int32"));
            otherfilelist.Columns.Add("filename", Type.GetType("System.String"));
            otherfilelist.Columns.Add("extension", Type.GetType("System.String"));
            otherfilelist.Columns.Add("parentid", Type.GetType("System.String"));
            otherfilelist.Columns.Add("filepath", Type.GetType("System.String"));
            otherfilelist.Columns.Add("filedescription", Type.GetType("System.String"));

            string path = SASRequest.GetString("path");
            DirectoryInfo dirinfo = new DirectoryInfo(Server.MapPath("../../cardtemplate/" + path));
            int i = 1;
            string extname;
            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    extname = file.Extension.ToLower();
                    if (extname.IndexOf("js") > 0 || extname.IndexOf("css") > 0 || extname.IndexOf("xml") > 0 || extname.IndexOf(".html") > 0)
                    {
                        DataRow dr = otherfilelist.NewRow();

                        dr["id"] = i;
                        dr["filename"] = file.Name.Substring(0, file.Name.LastIndexOf("."));
                        dr["extension"] = file.Extension.ToLower();
                        dr["parentid"] = "0";
                        dr["filepath"] = path;
                        dr["filedescription"] = "";
                        otherfilelist.Rows.Add(dr);
                        i++;
                    }
                }
            }

            foreach (DataRow dr in otherfilelist.Rows)
            {
                foreach (DataRow childdr in otherfilelist.Select("filename like '" + dr["filename"] + "_%%'"))
                {
                    if (dr["filename"].ToString() != childdr["filename"].ToString())
                    {
                        childdr["parentid"] = dr["id"].ToString();
                    }
                }
            }

            foreach (DataRow dr in otherfilelist.Rows)
            {
                //string imgstr = "";
                //if (dr["extension"].ToString().IndexOf("js") > 0) imgstr = "../images/js.gif";
                //if (dr["extension"].ToString().IndexOf("xml") > 0) imgstr = "../images/xml.gif";
                //if (dr["extension"].ToString().IndexOf("css") > 0) imgstr = "../images/css.gif";
                //if (dr["extension"].ToString().IndexOf("aspx") > 0) imgstr = "../images/aspx.gif";
                //if (dr["extension"].ToString().IndexOf("ascx") > 0) imgstr = "../images/ascx.gif";
                string ext = dr["extension"].ToString().Substring(1);
                if (ext == "jpg") dr["filename"] = "<img src=\"" + "../../cardtemplate/" + path + "/" + dr["filename"].ToString() + dr["extension"].ToString() + "\"/>";
                else dr["filename"] = "<img src=\"../images/" + ext + ".gif\" border=\"0\"> <a href=\"company_cardtemplateedit.aspx?path=" + dr["filepath"].ToString().Replace(" ", "%20") + "&filename=" + dr["filename"] + dr["extension"] + "&templateid=" + SASRequest.GetString("templateid") + "&templatename=" + SASRequest.GetString("templatename").Replace(" ", "%20") + "\" title=\"" + ext + "文件\">" + dr["filename"].ToString().Trim() + "</a>";
            }

            return otherfilelist;
            #endregion
        }

        protected void AddTemplateFile_Click(object sender, EventArgs e)
        {
            #region 增加模板文件
            if (CheckCookie())
            {
                fileurl.UpdateFile();
                RegisterStartupScript("PAGE", "window.location.href='company_cardtemplatetree.aspx?templateid=" + Request.Params["templateid"] + "&path=" + Request.Params["path"] + "&templatename=" + Request.Params["templatename"] + "';");
            }
            #endregion
        }

        protected void DeleteTemplateFile_Click(object sender, EventArgs e)
        {
            #region 删除模板文件
            if (CheckCookie())
            {
                string templatepathlist = TreeView1.GetSelectString(",");

                if (templatepathlist == "")
                {
                    RegisterStartupScript("", "<script>alert('您未选中任何模板');</script>");
                    return;
                }
                try
                {
                    foreach (string templatepath in templatepathlist.Split(','))
                    {
                        DeleteFile(templatepath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    RegisterStartupScript("", "<script>alert('您的目录设置了权限导致无法在此删除此文件');</script>");
                    return;
                }
                RegisterStartupScript("PAGE", "window.location.href='company_cardtemplatetree.aspx?templateid=" + Request.Params["templateid"] + "&path=" + Request.Params["path"] + "&templatename=" + Request.Params["templatename"] + "';");
            }
            #endregion
        }

        public bool DeleteFile(string filename)
        {
            #region 删除文件
            if (Utils.FileExists(Utils.GetMapPath(@"..\..\cardtemplate\" + filename)))
            {
                File.Delete(Utils.GetMapPath(@"..\..\cardtemplate\" + filename));
                return true;
            }
            return false;
            #endregion
        }

        protected void CreateTemplate_Click(object sender, EventArgs e)
        {
            #region 建立文件
            if (CheckCookie())
            {
                string templatePathList = TreeView1.GetSelectString(",");   //取得勾选文件列表

                if (templatePathList == "")
                {
                    RegisterStartupScript("", "<script>alert('您未选中任何文件');</script>");
                    return;
                }
                //if (SASRequest.GetString("chkall") == "" && templatePathList.Contains("_"))   //非全部生成
                //{
                //    templatePathList = RemadeTemplatePathList(templatePathList);
                //}
                int templateId = SASRequest.GetInt("templateid", 1);
                int updateCount = 0;
                string[] parmslist = new string[4];
                //ForumPageTemplate forumPageTemplate = new ForumPageTemplate();

                foreach (string templatePath in templatePathList.Split(','))
                {
                    string templateFileName = Path.GetFileName(templatePath).ToLower();//tempstr[tempstr.Length - 1];
                    string templateExtName = Path.GetExtension(templateFileName); //tempstr = templateName.Split('.');
                    if (templateExtName.Equals(".jpg"))
                    {
                        parmslist[0] = templateFileName;
                        updateCount++;
                    }
                    if (templateExtName.Equals(".swf"))
                    {
                        parmslist[1] = templateFileName;
                        updateCount++;
                    }
                }
                AdminTemplates.UpdateCardTemplate(templateId, string.Join("|", parmslist), userid, username, usergroupid, grouptitle, ip);
                RegisterStartupScript("PAGETemplate", "共" + updateCount + " 组名片已更新");
            }
            #endregion
        }
        ///// <summary>
        ///// 重新生成包含有头文件的文件列表
        ///// </summary>
        ///// <param name="templatePathList">已经选择的文件列表</param>
        ///// <returns>返回处理完毕的文件列表</returns>
        //private string RemadeTemplatePathList(string templatePathList)
        //{
        //    #region 生成头文件的列表文件
        //    if (!templatePathList.Contains("\\_"))  //列表中如果没有头文件则直接返回
        //    {
        //        return templatePathList;
        //    }
        //    StringBuilder result = new StringBuilder();
        //    foreach (string templatePath in templatePathList.Split(','))
        //    {
        //        string templateFileName = Path.GetFileName(templatePath).ToLower();
        //        string templateMainFileName = Path.GetFileNameWithoutExtension(templateFileName).ToLower();
        //        string templateExtName = Path.GetExtension(templateFileName).ToLower();
        //        if (!(templateExtName.Equals(".htm") || templateExtName.Equals(".config"))) continue; //非模板文件继续
        //        if (!templateFileName.StartsWith("_"))   //非头文件继续处理下个文件
        //        {
        //            if (!result.ToString().Contains(templateFileName))
        //            {
        //                result.Append(templatePath + ",");
        //            }
        //            continue;
        //        }
        //        string[] defaultFiles = Directory.GetFiles(Utils.GetMapPath("../../cardtemplate/default"));
        //        string[] findFiles = Directory.GetFiles(Utils.GetMapPath("../../cardtemplate/" + skinpath));
        //        string findContent = "<%template " + templateMainFileName + "%>";
        //        foreach (string fullFileName in defaultFiles)
        //        {
        //            string file = Path.GetFileName(fullFileName);
        //            if (file.StartsWith("_") || !(Path.GetExtension(file) == ".htm" || Path.GetExtension(file) == ".config")) continue; //如果是头文件则不处理
        //            using (StreamReader objReader = new StreamReader(Utils.GetMapPath("../../templates/default/" + file), Encoding.UTF8))
        //            {
        //                if (objReader.ReadToEnd().IndexOf(findContent) != -1)  //找到包含该头文件的文件
        //                {
        //                    if (!result.ToString().Contains(file))
        //                    {
        //                        if (File.Exists(Utils.GetMapPath("../../templates/" + skinpath + "/" + file)))
        //                        {
        //                            result.Append(skinpath + "\\" + file + ",");
        //                        }
        //                        else
        //                        {
        //                            result.Append("default\\" + file + ",");
        //                        }
        //                    }
        //                }
        //                objReader.Close();
        //            }
        //        }
        //    }
        //    return result.ToString().TrimEnd(',');
        //    #endregion
        //}

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            addtempfile.Click += new EventHandler(AddTemplateFile_Click);
            DataTable dt = LoadTemplateFileDT();
            string templateid = SASRequest.GetString("templateid");
            string templatename = SASRequest.GetString("templatename").Replace(" ", "%20");
            foreach (DataRow dr in dt.Rows)
            {
                string ext = dr["extension"].ToString().Substring(1);
                if (ext.Equals("jpg"))
                {
                    dr["filename"] = "<img src=\"" + "../../cardtemplate/" + SASRequest.GetString("path") + "/" + dr["filename"].ToString() + dr["extension"].ToString() + "\"/>";
                }
                else
                {
                    dr["filename"] = String.Format("<img src=../images/{0}.gif border=\"0\"  style=\"position:relative;top:5 px;height:16 px\"> {1} "
                    + "<a href=\"company_cardtemplateedit.aspx?path={2}&filename={1}{3}&templateid={4}&templatename={5}\" title=\"编辑{1}.{0}模板文件\"><img src='../images/editfile.gif' border='0'/></a>",
                        ext, dr["filename"].ToString().Trim(), dr["filepath"].ToString().Replace(" ", "%20"), dr["extension"].ToString().Trim(),
                        templateid, templatename);
                }
                
            }
            TreeView1.AddTableData(dt);
            for (int i = 0; i < TreeView1.Items.Count; i++)
            {
                TreeView1.Items[i].Attributes.Add("onclick", "checkedEnabledButton1(form,'TabControl1:tabPage22:CreateTemplate','TabControl1:tabPage22:DeleteTemplateFile')");
                TreeView1.Items[i].Attributes.Add("value", TreeView1.Items[i].Value);
            }

            TreeView2.DataSource = LoadOtherFileDT();
            TreeView2.DataBind();

            fileurl.UpFilePath = Utils.GetMapPath("../../cardtemplate/" + SASRequest.GetString("path") + "/");
        }

        #endregion
    }
}
