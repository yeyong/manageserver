using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Logic;
using SAS.Entity;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_categorygrid : AdminPage
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
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SASRequest.GetString("currentfid") != "")
                MoveCategory();

            LoadCategoryTree();
        }

        private void LoadCategoryTree()
        {
            DataTable dt = tpb.GetAllCategoryList();
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

                    str += "{fid:" + drs[n]["cid"] + ",name:\"" +
                           Utils.HtmlEncode(drs[n]["name"].ToString().Trim().Replace("\\", "\\\\\\\\").Replace("'", "\\\\'")) + "\",subject:\" " +
                           mystr + " <img src=../images/folders.gif align=\\\"absmiddle\\\" >";
                    if (int.Parse(drs[n]["sort"].ToString()) < 2) str += "<input type=\\\"checkbox\\\" name=\\\"ishighlevel\\\" value=\\\"" + drs[n]["cid"] + "\\\">";
                    str += Utils.HtmlEncode(drs[n]["name"].ToString().Trim().Replace("\\", "\\\\ ")) + "\",linetitle:\"" +
                           mystr + "\",parentidlist:0,layer:" + drs[n]["sort"] + ",subforumcount:" +
                           (Convert.ToBoolean(drs[n]["haschild"].ToString()) ? 1 : 0) + "},\r\n";
                    if (Convert.ToBoolean(drs[n]["haschild"].ToString()))
                    {
                        int mylayer = Convert.ToInt32(drs[n]["sort"].ToString());
                        string selectstr = "[sort]=" + (++mylayer) + " AND [parentid]=" + drs[n]["cid"];
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

                    str += "{fid:" + drs[n]["cid"] + ",name:\"" +
                          Utils.HtmlEncode(drs[n]["name"].ToString().Trim().Replace("\\", "\\\\\\\\").Replace("'", "\\\\'")) + "\",subject:\" " +
                          mystr + " <img src=../images/folder.gif align=\\\"absmiddle\\\" >";
                    if (int.Parse(drs[n]["sort"].ToString()) < 2)
                        str += "<input type=\\\"checkbox\\\" name=\\\"ishighlevel\\\" value=\\\"" + drs[n]["cid"] + "\\\">";
                    str += Utils.HtmlEncode(drs[n]["name"].ToString().Trim().Replace("\\", "\\\\ ")) + "\",linetitle:\"" +
                          mystr + "\",parentidlist:\"" + drs[n]["parentlist"].ToString().Trim() + "\",layer:" +
                          drs[n]["sort"] + ",subforumcount:" +
                          (Convert.ToBoolean(drs[n]["haschild"].ToString()) ? 1 : 0) + "},\r\n";
                    if (Convert.ToBoolean(drs[n]["haschild"].ToString()))
                    {
                        int mylayer = Convert.ToInt32(drs[n]["sort"].ToString());
                        string selectstr = "[sort]=" + (++mylayer) + " AND [parentid]=" + drs[n]["cid"];
                        AddTree(mylayer, dt.Select(selectstr), temp);
                    }
                }

                #endregion
            }
        }

        private void MoveCategory()
        {
            int currentfid = SASRequest.GetInt("currentfid", 0);
            int targetfid = SASRequest.GetInt("targetfid", 0);
            string isaschildnode = SASRequest.GetString("isaschildnode");
            CategoryInfo gc = tpb.GetCategoryInfo(currentfid);
            int oldparentid = gc.Parentid;
            CategoryInfo parentgc = tpb.GetCategoryInfo(targetfid);
            gc.Parentid = targetfid;
            gc.Parentlist = parentgc.Parentlist + "," + parentgc.Cid;
            gc.Sort = parentgc.Sort + 1;
            tpb.UpdateCategoryInfo(gc);
            parentgc.Haschild = 1;
            tpb.UpdateCategoryInfo(parentgc);
            if (gc.Haschild == 0)
                return;
            DataTable dt = tpb.GetAllCategoryList();
            MoveSubCategory(gc, dt);
            CategoryInfo oldparentgc = tpb.GetCategoryInfo(oldparentid);
            oldparentgc.Haschild = (dt.Select("parentid=" + oldparentid) == null ? 0 : 1);
            tpb.UpdateCategoryInfo(oldparentgc);
            ResetStatus();
            this.RegisterStartupScript("PAGE", "window.location='taobao_categorygrid.aspx';");
        }

        private void MoveSubCategory(CategoryInfo parentgc, DataTable dt)
        {
            DataRow[] datarow = dt.Select("parentid=" + parentgc.Cid.ToString());
            if (datarow.Length == 0)
                return;
            foreach (DataRow dr in datarow)
            {
                CategoryInfo gc = tpb.GetCategoryInfo(int.Parse(dr["cid"].ToString()));
                gc.Parentlist = parentgc.Parentlist + "," + parentgc.Cid;
                gc.Sort = parentgc.Sort + 1;
                tpb.UpdateCategoryInfo(gc);
                MoveSubCategory(gc, dt);
            }
        }

        private void ResetStatus()
        {
            //Catalogs.GetInstance.WriteJsonFile();
            SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/CategoryList");
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
