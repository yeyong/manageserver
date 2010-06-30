using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;

using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    public partial class activetree : System.Web.UI.UserControl
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


        public StringBuilder sb = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadActivityTree();
        }

        public void LoadActivityTree()
        {
            //读取论坛版块树
            DataTable dt = AdminActivities.GetEnableActivities();

            if (dt.Rows.Count == 0) Server.Transfer("../global/global_addactivity.aspx"); //如果版块表中没有任何版块, 则跳转到"添加第一个版块"页面. 

            ViewState["dt"] = dt;

            sb.Append("<table border=\"0\"  width=\"100%\" align=\"center\" cellspacing=\"0\" cellpadding=\"0\">");


            int advid = SASRequest.GetInt("id", 0);
            AnnouncementInfo ad_dt = Announcements.GetAnnouncement(advid);

            if (ad_dt != null && !string.IsNullOrEmpty(ad_dt.Relateactive))
            {
                this.SelectForumStr = "," + ad_dt.Relateactive + ",";
            }

            ActivityType ate = new ActivityType();
            int n = 0;
            int atlength = Enum.GetNames(ate.GetType()).Length;
            foreach (string atname in Enum.GetNames(ate.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ate.GetType(), atname));
                string s_text = EnumCatch.GetActivityType(s_value);
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
                sb.Append("<tr><td class=treetd> " + mystr + "<img src=../images/folders.gif class=treeimg > " + s_text + "</td></tr>");
                AddAdsTree(dt.Select("[atype] = " + s_value), currentnodestr);
                n++;
            }

            sb.Append("</table>");

            TreeContent.Text = sb.ToString();
        }

        private void AddAdsTree(DataRow[] drs, string currentnodestr)
        {
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

                if ((this.SelectForumStr.IndexOf("," + drs[n]["id"].ToString().Trim() + ",") >= 0) && (this.SelectForumStr.IndexOf("全部") < 0))
                {
                    sb.Append("<tr><td class=treetd> " + mystr + " <img src=../images/folder.gif class=treeimg > <input class=\"input1\" type=checkbox id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\" value=\"" + drs[n]["id"].ToString().Trim() + "\"  checked> " + drs[n]["atitle"].ToString().Trim() + "</td></tr>");
                }
                else
                {
                    sb.Append("<tr><td class=treetd> " + mystr + " <img src=../images/folder.gif class=treeimg > <input class=\"input1\" type=checkbox id=\"" + this.ClientID + "\" name=\"" + this.ClientID + "\" value=\"" + drs[n]["id"].ToString().Trim() + "\" > " + drs[n]["atitle"].ToString().Trim() + "</td></tr>");
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