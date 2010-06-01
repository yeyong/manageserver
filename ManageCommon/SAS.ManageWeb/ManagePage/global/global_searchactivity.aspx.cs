using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Control;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Common;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_searchactivity : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                postdatetimeStart.SelectedDate = DateTime.Now.AddDays(-30);
                postdatetimeEnd.SelectedDate = DateTime.Now;
            }
            ActivityType ate = new ActivityType();
            typeid.Items.Clear();
            typeid.Items.Add(new ListItem("全部", "0"));
            foreach (string atname in Enum.GetNames(ate.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ate.GetType(), atname));
                string s_text = EnumCatch.GetActivityType(s_value);
                typeid.Items.Add(new ListItem(s_text, s_value.ToString()));
            }
        }

        private void SaveSearchCondition_Click(object sender, EventArgs e)
        {
            #region 生成查询条件

            if (this.CheckCookie())
            {
                //TODO:条件，先各个

                string sqlstring = Activities.GetActivitiesSearchConditions(TypeConverter.StrToInt(typeid.SelectedValue, 0), title.Text, keyword.Text, postdatetimeStart.SelectedDate, postdatetimeEnd.SelectedDate, TypeConverter.StrToInt(status.SelectedValue, 0));

                Session["topicswhere"] = sqlstring;
                Response.Redirect("global_activitygrid.aspx");
            }

            #endregion
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
            this.SaveSearchCondition.Click += new EventHandler(this.SaveSearchCondition_Click);
        }

        #endregion
    }
}
