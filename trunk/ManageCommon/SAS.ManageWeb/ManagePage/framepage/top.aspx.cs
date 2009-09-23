using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Threading;
using System.Xml;

using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Common.XML;

namespace SAS.ManageWeb.ManagePage
{
    public partial class top : SAS.Web.UI.AdminPage
    {
        public StringBuilder sb = new StringBuilder();

        public int menucount = 0;

        public int olid;
        public string showmenuid;
        public string toptabmenuid;
        public string mainmenulist;
        public string defaulturl;
    }
}
