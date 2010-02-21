using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Logic;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    public partial class company_cataloggrid : AdminPage
    {
        #region 图标信息变量声明

        private string T_rootpic = "<img src=../images/lines/tplus.gif align=absmiddle>";
        private string L_rootpic = "<img src=../images/lines/lplus.gif align=absmiddle>";
        private string L_TOP_rootpic = "<img src=../images/lines/rplus.gif align=absmiddle>";
        private string I_rootpic = "<img src=../images/lines/dashplus.gif align=absmiddle>";
        private string T_nodepic = "<img src=../images/lines/tminus.gif align=absmiddle>";
        private string L_nodepic = "<img src=../images/lines/lminus.gif align=absmiddle>";
        private string I_nodepic = "<img src=../images/lines/i.gif align=absmiddle>";
        private string No_nodepic = "<img src=../images/lines/noexpand.gif align=absmiddle>";

        #endregion

        public string str = "";
        public int noPicCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (SASRequest.GetString("currentfid") != "")
            //    MoveCategory();

            if (SASRequest.GetString("method") == "new")
                NewChildCategory();

            //if (SASRequest.GetString("method") == "edit")
            //    EditCategory();

            if (SASRequest.GetString("method") == "newrootnode")
                NewRootCategory();

            //if (SASRequest.GetString("method") == "del")
            //    DeleteNode();

            //if (SASRequest.GetString("method") == "updateall")
            //    UpdateAll();

            //if (SASRequest.GetString("method") == "update")
            //    UpdateCategoryGoodsCount();

            LoadCategoryTree();
        }

        private void LoadCategoryTree()
        {
            DataTable dt = AdminCatalogies.GetAllCatalog();
            ViewState["dt"] = dt;

            if (dt.Rows.Count == 0)
            {
                str = "<script type=\"text/javascript\">\r\n  obj = [];\r\n newtree = new tree(\"newtree\",obj,\"reSetTree\");</script>";
            }
            else
            {
                AddTree(0, dt.Select("[sort]=0 AND [parentid]=0"), "");
                str = "<script type=\"text/javascript\">\r\n  obj = [" + str;
                str = str.Substring(0, str.Length - 3);
                str += "];\r\n newtree = new tree(\"newtree\",obj,\"reSetTree\");";
                str += "</script>";
            }
            ShowTreeLabel.Text = str;
        }

        private void AddTree(int layer, DataRow[] drs, string currentnodestr)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (layer == 0)
            {
                #region 作为根分类

                for (int n = 0; n < drs.Length; n++)
                {
                    string mystr = "";
                    if (drs.Length == 1)
                    {
                        mystr += I_rootpic; //
                        currentnodestr = No_nodepic;
                    }
                    else
                    {
                        if (n == 0)
                        {
                            mystr += L_TOP_rootpic; //
                            currentnodestr = I_nodepic;
                        }
                        else
                        {
                            if ((n > 0) && (n < (drs.Length - 1)))
                            {
                                mystr += T_rootpic; //
                                currentnodestr = I_nodepic;
                            }
                            else
                            {
                                mystr += L_rootpic;
                                currentnodestr = No_nodepic;
                            }
                        }
                    }

                    str += "{fid:" + drs[n]["id"] + ",name:\"" +
                           Utils.HtmlEncode(drs[n]["categoryname"].ToString().Trim().Replace("\\", "\\\\\\\\").Replace("'", "\\\\'")) + "\",subject:\" " +
                           mystr + " <img src=../images/folders.gif align=\\\"absmiddle\\\" >";
                    if (drs[n]["fid"].ToString() != "0" && int.Parse(drs[n]["sort"].ToString()) < 2)
                        str += "<input type=\\\"checkbox\\\" name=\\\"ishighlevel\\\" value=\\\"" + drs[n]["id"] + "\\\">";
                    str += Utils.HtmlEncode(drs[n]["name"].ToString().Trim().Replace("\\", "\\\\ ")) + "\",linetitle:\"" +
                           mystr + "\",parentidlist:0,layer:" + drs[n]["layer"] + ",subforumcount:" +
                           (Convert.ToBoolean(drs[n]["haschild"].ToString()) ? 1 : 0) + ",addfid:" + drs[n]["fid"] +
                           ",editfid:" + (drs[n]["fid"].ToString() == "0" ? GetParentFid(drs[n]["parentid"].ToString(), dt) : drs[n]["fid"].ToString()) +
                           ",cfid:" + drs[n]["fid"] + "},\r\n";
                    if (Convert.ToBoolean(drs[n]["haschild"].ToString()))
                    {
                        int mylayer = Convert.ToInt32(drs[n]["sort"].ToString());
                        string selectstr = "sort=" + (++mylayer) + " AND parentid=" + drs[n]["id"];
                        AddTree(mylayer, dt.Select(selectstr), currentnodestr);
                    }
                }

                #endregion
            }
            else
            {
                #region 作为子分类

                for (int n = 0; n < drs.Length; n++)
                {
                    string mystr = "";
                    mystr += currentnodestr;
                    string temp = currentnodestr;

                    if ((n >= 0) && (n < (drs.Length - 1)))
                    {
                        mystr += T_nodepic; //
                        temp += I_nodepic;
                    }
                    else
                    {
                        mystr += L_nodepic;
                        noPicCount++;
                        temp += No_nodepic;
                    }

                    str += "{fid:" + drs[n]["id"] + ",name:\"" +
                          Utils.HtmlEncode(drs[n]["name"].ToString().Trim().Replace("\\", "\\\\\\\\").Replace("'", "\\\\'")) + "\",subject:\" " +
                          mystr + " <img src=../images/folder.gif align=\\\"absmiddle\\\" >";
                    if (drs[n]["fid"].ToString() != "0" && int.Parse(drs[n]["sort"].ToString()) < 2)
                        str += "<input type=\\\"checkbox\\\" name=\\\"ishighlevel\\\" value=\\\"" + drs[n]["categoryid"] + "\\\">";
                    str += Utils.HtmlEncode(drs[n]["categoryname"].ToString().Trim().Replace("\\", "\\\\ ")) + "\",linetitle:\"" +
                          mystr + "\",parentidlist:\"" + drs[n]["parentidlist"].ToString().Trim() + "\",layer:" +
                          drs[n]["sort"] + ",subforumcount:" +
                          (Convert.ToBoolean(drs[n]["haschild"].ToString()) ? 1 : 0) + ",addfid:" + drs[n]["fid"] +
                          ",editfid:" + (drs[n]["sort"].ToString() == "1" ? GetParentFid(drs[n]["parentid"].ToString(), dt) : drs[n]["fid"].ToString()) +
                          ",cfid:" + drs[n]["fid"] + "},\r\n";
                    if (Convert.ToBoolean(drs[n]["haschild"].ToString()))
                    {
                        int mylayer = Convert.ToInt32(drs[n]["sort"].ToString());
                        string selectstr = "[sort]=" + (++mylayer) + " AND parentid=" + drs[n]["id"];
                        AddTree(mylayer, dt.Select(selectstr), temp);
                    }
                }

                #endregion
            }
        }

        private void NewRootCategory()
        {
            CatalogInfo gc = new CatalogInfo();
            gc.name = Utils.HtmlEncode(SASRequest.GetString("categoryname").Trim());
            gc.parentid = 0;
            gc.sort = 0;
            gc.parentlist = "0";
            gc.companycount = 0;
            gc.haschild = 0;
            int newcategoryid = GoodsCategories.CreateGoodsCategory(gc);
            gc.Categoryid = newcategoryid;
            gc.Pathlist = string.Format("<a href=\"showgoodslist-{0}.aspx\">{1}</a>", newcategoryid, gc.Categoryname);
            GoodsCategories.UpdateGoodsCategory(gc);
            SetForumsTrade(gc.Fid);
            OpenMall(fid);
            ResetStatus();
            this.RegisterStartupScript("PAGE", "window.location='mall_goodscategoriesmanage.aspx';");
        }

        private string GetParentFid(string parentid, DataTable dt)
        {
            string fid = "0";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["id"].ToString() == parentid)
                    fid = dr["fid"].ToString();
            }
            return fid;
        }

        #region 把VIEWSTATE写入容器

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            base.SASLogicSavePageState(viewState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            return base.DiscuzForumLoadPageState();
        }

        #endregion

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            //this.Load += new EventHandler(this.Page_Load);

        }
        #endregion
    }
}
