using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SAS.Cache;
using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;

namespace SAS.Sirius.Admin
{
    /// <summary>
    /// add team
    /// </summary>
    public partial class addteam : AdminPage
    {
        protected string root = Utils.GetRootUrl(BaseConfigs.GetBaseConfig().Sitepath);

        public void InitInfo()
        {
            templateid.AddTableData(Templates.GetValidTemplateList(), "tp_name", "tp_id");
        }

        private void SubmitAddChild()
        {
            #region 保存新增团队信息
            if (this.CheckCookie())
            {
                if (name.Text.Trim() == "")
                {
                    base.RegisterStartupScript("", "<script>alert('团队名称不能为空');</script>");
                    return;
                }

                if (!Utils.IsSafeSqlString(name.Text.Trim()))
                {
                    base.RegisterStartupScript("", "<script>alert('您输入的团队名包含不安全的字符,因此无法提交!');window.location.href='sirius_addteam.aspx';</script>");
                    return;
                }

                if (PrivateMessages.SystemUserName == name.Text)
                {
                    base.RegisterStartupScript("", "<script>alert('您不能创建该团队名,因为它是系统保留的用户名,请您输入其它的团队名!');window.location.href='sirius_addteam.aspx';</script>");
                    return;
                }





            }
            #endregion
        }

        private void SubmitAdd_Click(object sender, EventArgs e)
        {            
            SubmitAddChild();
        }

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.TabControl1.InitTabPage();
            this.SubmitAdd.Click += new EventHandler(this.SubmitAdd_Click);
            InitInfo();
        }

        #endregion        
    }
}
