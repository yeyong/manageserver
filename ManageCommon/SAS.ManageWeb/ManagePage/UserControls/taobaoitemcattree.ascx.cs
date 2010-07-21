using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Entity.Domain;
using SAS.Config;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobaoitemcattree : System.Web.UI.UserControl
    {
        #region 图片加载变量

        private string T_rootpic = "<img src=../images/lines/tplus.gif align=absmiddle>";
        private string L_rootpic = "<img src=../images/lines/lplus.gif align=absmiddle>";
        private string L_TOP_rootpic = "<img src=../images/lines/rplus.gif align=absmiddle>";
        private string I_rootpic = "<img src=../images/lines/dashplus.gif align=absmiddle>";

        private string T_nodepic = "<img src=../images/lines/tminus.gif align=absmiddle>";
        private string L_nodepic = "<img src=../images/lines/lminus.gif align=absmiddle>";
        private string I_nodepic = "<img src=../images/lines/i.gif align=absmiddle>";
        private string No_nodepic = "<img src=../images/lines/noexpand.gif align=absmiddle>";

        #endregion

        private int noPicCount = 0;
        public bool WithCheckBox = true;

        public string PageName = "forumbatchset";
        private string SelectForumStr = "";

        private TaoBaoPluginBase taobaos = TaoBaoPluginProvider.GetInstance();

        public StringBuilder sb = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (taobaos != null)
            {
                LoadActivityTree();
            }
        }

        public void LoadActivityTree()
        {
            //读取论坛版块树
            System.Collections.Generic.List<ItemCat> itemcatlist = taobaos.GetItemCatCache(0);

            sb.Append("<table border=\"0\"  width=\"100%\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\">");

            int cid = SASRequest.GetInt("cid", 0);
            CategoryInfo ad_dt = taobaos.GetCategoryInfo(cid);

            if (ad_dt != null && !string.IsNullOrEmpty(ad_dt.Cg_relateclass))
            {
                this.SelectForumStr = "," + ad_dt.Cg_relateclass + ",";
            }

            System.Collections.Generic.List<ItemCat> itemcatlist1 = itemcatlist.FindAll(new Predicate<ItemCat>(delegate(ItemCat iteminfo) { return iteminfo.ParentCid == 0; }));
            int n = 0;
            int atlength = itemcatlist1.Count;
            foreach (ItemCat itemcatinfo in itemcatlist1)
            {
                long s_value = itemcatinfo.Cid;
                string s_text = itemcatinfo.Name;
                string mystr = "";
                string currentnodestr = "";
                if ((n >= 0) && (n < (atlength - 1)))
                {
                    mystr += T_rootpic;
                    currentnodestr = I_nodepic;
                }
                else
                {
                    mystr += L_rootpic;
                    currentnodestr = No_nodepic;
                }

                if (this.SelectForumStr.IndexOf("," + s_value.ToString().Trim() + "|" + s_text.Trim() + ",") >= 0)
                {
                    sb.Append("<tr><td class=treetd> " + mystr + " <img src=../images/folder.gif class=treeimg > <input class=\"input1\" type=checkbox id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\" value=\"" + s_value.ToString().Trim() + "|" + s_text.Trim() + "\"  checked> " + s_text.ToString().Trim() + "</td></tr>");
                }
                else
                {
                    sb.Append("<tr><td class=treetd> " + mystr + " <img src=../images/folder.gif class=treeimg > <input class=\"input1\" type=checkbox id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\" value=\"" + s_value.ToString().Trim() + "|" + s_text.Trim() + "\" > " + s_text.ToString().Trim() + "</td></tr>");
                }

                if (itemcatinfo.IsParent)
                {
                    System.Collections.Generic.List<ItemCat> itemcatlist2 = taobaos.GetItemCatCache(itemcatinfo.Cid);
                    AddAdsTree(itemcatlist2, currentnodestr);
                }
                n++;
            }

            sb.Append("</table>");

            TreeContent.Text = sb.ToString();
        }

        private void AddAdsTree(System.Collections.Generic.List<ItemCat> subitemlist, string currentnodestr)
        {
            for (int n = 0; n < subitemlist.Count; n++)
            {
                string mystr = "";
                mystr += currentnodestr;
                string temp = currentnodestr;

                if ((n >= 0) && (n < (subitemlist.Count - 1)))
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

                if ((this.SelectForumStr.IndexOf("," + subitemlist[n].Cid.ToString().Trim() + "|" + subitemlist[n].Name.ToString().Trim() + ",") >= 0) && (this.SelectForumStr.IndexOf("全部") < 0))
                {
                    sb.Append("<tr><td class=treetd> " + mystr + " <img src=../images/folder.gif class=treeimg > <input class=\"input1\" type=checkbox id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\" value=\"" + subitemlist[n].Cid.ToString().Trim() + "|" + subitemlist[n].Name.ToString().Trim() + "\"  checked> " + subitemlist[n].Name.ToString().Trim() + "</td></tr>");
                }
                else
                {
                    sb.Append("<tr><td class=treetd> " + mystr + " <img src=../images/folder.gif class=treeimg > <input class=\"input1\" type=checkbox id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\" value=\"" + subitemlist[n].Cid.ToString().Trim() + "|" + subitemlist[n].Name.ToString().Trim() + "\" > " + subitemlist[n].Name.ToString().Trim() + "</td></tr>");
                }
            }
        }

        private string _hintTitle = "";
        public string HintTitle
        {
            get { return _hintTitle; }
            set { _hintTitle = value; }
        }

        private string _hintInfo = "";
        public string HintInfo
        {
            get { return _hintInfo; }
            set { _hintInfo = value; }
        }

        private int _hintLeftOffSet = 0;
        public int HintLeftOffSet
        {
            get { return _hintLeftOffSet; }
            set { _hintLeftOffSet = value; }
        }

        private int _hintTopOffSet = 0;
        public int HintTopOffSet
        {
            get { return _hintTopOffSet; }
            set { _hintTopOffSet = value; }
        }

        private string _hintShowType = "up";//或"down"
        public string HintShowType
        {
            get { return _hintShowType; }
            set { _hintShowType = value; }
        }

        private int _hintHeight = 30;
        public int HintHeight
        {
            get { return _hintHeight; }
            set { _hintHeight = value; }
        }
    }
}