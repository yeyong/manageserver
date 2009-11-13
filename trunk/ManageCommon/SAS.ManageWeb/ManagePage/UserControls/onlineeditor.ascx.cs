using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using SAS.Control;

namespace SAS.ManageWeb.ManagePage
{
    public partial class onlineeditor : System.Web.UI.UserControl
    {
        public string controlname;
        public int postminchars = 0;
        public int postmaxchars = 2000;

        public string Text
        {
            set { DataTextarea.InnerText = value; }
            get { return DataTextarea.InnerText.Replace("'", "''"); }
        }
    }
}