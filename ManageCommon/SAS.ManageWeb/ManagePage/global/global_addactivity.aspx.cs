using System;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 活动添加
    /// </summary>
    public partial class global_addactivity : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                postdatetimeStart.SelectedDate = DateTime.Now;
                postdatetimeEnd.SelectedDate = DateTime.Now.AddDays(30);
            }
        }

        private void AddActInfo_Click(object sender, EventArgs e)
        {
            #region 添加活动
            if (this.CheckCookie())
            {
                if (templatenew.Text == "")
                {
                    base.RegisterStartupScript("", "<script>alert('请添加活动页面内容，内容不可为空！');</script>");
                    return;
                }

                //获取生效与结束日期
                string starttimestr = postdatetimeStart.SelectedDate.ToString();
                string endtimestr = postdatetimeEnd.SelectedDate.ToString();
                //有发布时间限制的广告，则检查发布日期范围是否合法
                if (starttimestr.IndexOf("1900") < 0 && endtimestr.IndexOf("1900") < 0)
                {
                    if (Convert.ToDateTime(postdatetimeStart.SelectedDate.ToString()) >= Convert.ToDateTime(postdatetimeEnd.SelectedDate.ToString()))
                    {
                        base.RegisterStartupScript("", "<script>alert('开始时间应该早于结束时间');</script>");
                        return;
                    }
                }

                AdminActivities.CreateActivity(LoadActivityInfo());
                if (TypeConverter.StrToInt(typeid.SelectedValue, 0) == Convert.ToInt16(ActivityType.TaobaoActivity))
                {
                    SAS.Cache.WebCacheFactory.GetWebCache().Remove("/SAS/TaoActivities", true);
                }
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/Activity");
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "增加活动", "增加活动,标题为:" + act_title.Text);
                base.RegisterStartupScript("PAGE", "window.location.href='global_searchactivity.aspx';");
            }
            #endregion
        }

        /// <summary>
        /// 加载活动实体信息
        /// </summary>
        /// <returns></returns>
        private ActivityInfo LoadActivityInfo()
        {
            ActivityInfo aif = new ActivityInfo();
            aif.Atype = TypeConverter.StrToInt(typeid.SelectedValue, 0);
            aif.Atitle = Utils.RemoveHtml(act_title.Text);
            aif.Stylecode = act_style.Text;
            aif.Scriptcode = act_script.Text;
            aif.Desccode = templatenew.Text;
            aif.Begintime = postdatetimeStart.SelectedDate.ToString();
            aif.Endtime = postdatetimeEnd.SelectedDate.ToString();
            aif.Seotitle = Utils.RemoveHtml(seotitle.Text);
            aif.Seokeyword = Utils.RemoveHtml(seokeyword.Text);
            aif.Seodesc = Utils.RemoveHtml(seodesc.Text);
            aif.Enabled = TypeConverter.StrToInt(act_status.SelectedValue, 0);
            return aif;
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
            AddActInfo.Click += new EventHandler(AddActInfo_Click);

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

        #endregion
    }
}
