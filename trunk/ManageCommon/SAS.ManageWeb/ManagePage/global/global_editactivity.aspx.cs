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
    public partial class global_editactivity : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (SASRequest.GetString("id") == "")
                {
                    Response.Redirect("global_activitygrid.aspx");
                }
                else
                {
                    LoadActivityInf(SASRequest.GetInt("id", -1));
                }
            }
        }

        public void LoadActivityInf(int id)
        {
            #region 加载活动信息

            ActivityInfo actInfo = Activities.GetActivityInfo(id);
            act_title.Text = actInfo.Atitle;
            postdatetimeStart.SelectedDate = Convert.ToDateTime(actInfo.Begintime);
            postdatetimeEnd.SelectedDate = Convert.ToDateTime(actInfo.Endtime);
            act_style.Text = actInfo.Stylecode;
            act_script.Text = actInfo.Scriptcode;
            templatenew.Text = actInfo.Desccode;
            seotitle.Text = actInfo.Seotitle;
            seokeyword.Text = actInfo.Seokeyword;
            seodesc.Text = actInfo.Seodesc;
            act_status.SelectedValue = actInfo.Enabled.ToString();

            ActivityType ate = new ActivityType();
            typeid.Items.Clear();
            typeid.Items.Add(new ListItem("全部", "0"));
            foreach (string atname in Enum.GetNames(ate.GetType()))
            {
                int s_value = Convert.ToInt16(Enum.Parse(ate.GetType(), atname));
                string s_text = EnumCatch.GetActivityType(s_value);
                typeid.Items.Add(new ListItem(s_text, s_value.ToString()));
            }
            typeid.SelectedValue = actInfo.Atype.ToString();

            #endregion
        }

        private void EditActInfo_Click(object sender, EventArgs e)
        {
            #region 更新公告相关信息

            if (this.CheckCookie())
            {
                ActivityInfo activityInfo = new ActivityInfo();
                activityInfo.Id = SASRequest.GetInt("id", 0);

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

                activityInfo.Atype = TypeConverter.StrToInt(typeid.SelectedValue, 0);
                activityInfo.Atitle = Utils.RemoveHtml(act_title.Text);
                activityInfo.Stylecode = act_style.Text;
                activityInfo.Scriptcode = act_script.Text;
                activityInfo.Desccode = templatenew.Text;
                activityInfo.Begintime = postdatetimeStart.SelectedDate.ToString();
                activityInfo.Endtime = postdatetimeEnd.SelectedDate.ToString();
                activityInfo.Seotitle = Utils.RemoveHtml(seotitle.Text);
                activityInfo.Seokeyword = Utils.RemoveHtml(seokeyword.Text);
                activityInfo.Seodesc = Utils.RemoveHtml(seodesc.Text);
                activityInfo.Enabled = TypeConverter.StrToInt(act_status.SelectedValue, 0);

                AdminActivities.UpdateActivityInfo(activityInfo);
                SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/Activity");
                //记录日志
                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "更新活动", "更新活动,标题为:" + act_title.Text);
                base.RegisterStartupScript("PAGE", "window.location.href='global_searchactivity.aspx';");
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
            EditActInfo.Click += new EventHandler(EditActInfo_Click);
        }

        #endregion
    }
}
