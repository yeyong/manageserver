using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Config;
using SAS.Entity;
using SAS.Common;
using SAS.Logic;
using SAS.Entity.Domain;
using SAS.Plugin.TaoBao;

namespace SAS.ManageWeb.ManagePage
{
    public partial class taobao_addtopicrecommend : AdminPage
    {
        private TaoBaoPluginBase tpb = TaoBaoPluginProvider.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
