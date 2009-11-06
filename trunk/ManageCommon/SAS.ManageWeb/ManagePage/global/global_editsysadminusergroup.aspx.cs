using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Control;
using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;
using SAS.Plugin.Album;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 编辑系统管理组
    /// </summary>
    public partial class global_editsysadminusergroup : AdminPage
    {
        public UserGroupInfo userGroupInfo = new UserGroupInfo();
        protected SAS.Control.CheckBoxList admingroupright;
        protected SAS.Control.DropDownList allowstickthread;
        protected bool haveAlbum;
        protected bool haveSpace;

        protected void Page_Load(object sender, EventArgs e)
        {
            haveAlbum = AlbumPluginProvider.GetInstance() != null;
            haveSpace = false;
        }

        public void LoadUserGroupInf(int groupid)
        {
            #region 加载相关组信息

            userGroupInfo = AdminUserGroups.AdminGetUserGroupInfo(groupid);

            groupTitle.Text = Utils.RemoveFontTag(userGroupInfo.ug_name);
            creditshigher.Text = userGroupInfo.ug_scorehight.ToString();
            creditslower.Text = userGroupInfo.ug_scorelow.ToString();
            stars.Text = userGroupInfo.Stars.ToString();
            color.Text = userGroupInfo.ug_color;
            groupavatar.Text = userGroupInfo.ug_logo;
            readaccess.Text = userGroupInfo.ug_readaccess.ToString();
            //maxprice.Text = userGroupInfo.Maxprice.ToString();
            maxpmnum.Text = userGroupInfo.Maxpmnum.ToString();
            maxsigsize.Text = userGroupInfo.ug_maxsigsize.ToString();
            maxattachsize.Text = userGroupInfo.Ug_maxattachsize.ToString();
            maxsizeperday.Text = userGroupInfo.Ug_maxsizeperday.ToString();
            maxspaceattachsize.Text = userGroupInfo.ug_maxspaceattachsize.ToString();
            maxspacephotosize.Text = userGroupInfo.ug_maxspacephotosize.ToString();

            attachextensions.SetSelectByID(userGroupInfo.ug_attachextensions.Trim());

            if (groupid > 0 && groupid <= 3) radminid.Enabled = false;
            radminid.SelectedValue = userGroupInfo.ug_pg_id.ToString();

            usergrouppowersetting.Bind(userGroupInfo);

            if (radminid.SelectedValue == "1")
            {
                allowstickthread.Enabled = false;
                allowstickthread.SelectedValue = "3";
            }

            #endregion
        }


        public int BoolToInt(bool a)
        {
            return a ? 1 : 0;
        }


        public byte BoolToByte(bool a)
        {
            return (byte)(a ? 1 : 0);
        }


        private void UpdateUserGroupInf_Click(object sender, EventArgs e)
        {
            #region 更新系统管理组信息

            if (this.CheckCookie())
            {
                userGroupInfo = AdminUserGroups.AdminGetUserGroupInfo(SASRequest.GetInt("groupid", -1));
                userGroupInfo.ug_isSystem = 0;
                userGroupInfo.ug_readaccess = Convert.ToInt32(readaccess.Text);
                userGroupInfo.Allowviewstats = 0;
                userGroupInfo.Allownickname = 0;
                userGroupInfo.Ug_allowhtml = 0;
                userGroupInfo.ug_allowshop = 0;
                userGroupInfo.ug_allowinvisible = 0;
                userGroupInfo.Reasonpm = 0;

                if (radminid.SelectedValue == "0") //当未选取任何管理模板时
                {
                    SAS.Logic.AdminGroups.DeleteAdminGroupInfo((short)userGroupInfo.ug_id);
                    userGroupInfo.ug_pg_id = 0;
                }

                Users.UpdateUserAdminIdByGroupId(userGroupInfo.ug_pg_id, userGroupInfo.ug_id);
                userGroupInfo.ug_name = groupTitle.Text;
                userGroupInfo.ug_scorehight = Convert.ToInt32(creditshigher.Text);
                userGroupInfo.ug_scorelow = Convert.ToInt32(creditslower.Text);
                userGroupInfo.Stars = Convert.ToInt32(stars.Text);
                userGroupInfo.ug_color = color.Text;
                userGroupInfo.ug_logo = groupavatar.Text;
                //userGroupInfo.Maxprice = Convert.ToInt32(maxprice.Text);
                userGroupInfo.Maxpmnum = Convert.ToInt32(maxpmnum.Text);
                userGroupInfo.ug_maxsigsize = Convert.ToInt32(maxsigsize.Text);
                userGroupInfo.Ug_maxattachsize = Convert.ToInt32(maxattachsize.Text);
                userGroupInfo.Ug_maxsizeperday = Convert.ToInt32(maxsizeperday.Text);
                userGroupInfo.ug_maxspaceattachsize = Convert.ToInt32(maxspaceattachsize.Text);
                userGroupInfo.ug_maxspacephotosize = Convert.ToInt32(maxspacephotosize.Text);
                userGroupInfo.ug_attachextensions = attachextensions.GetSelectString(",");

                usergrouppowersetting.GetSetting(ref userGroupInfo);

                if (AdminUserGroups.UpdateUserGroupInfo(userGroupInfo))
                {
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/UserGroupList");
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/AdminGroupList");
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台更新系统组", "组ID:" + SASRequest.GetInt("groupid", -1));
                    base.RegisterStartupScript("PAGE", "window.location.href='global_sysadminusergroupgrid.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('操作失败');window.location.href='global_sysadminusergroupgrid.aspx';</script>");
                }
            }

            #endregion
        }


        private void radminid_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 绑定关联用户组信息
            UserGroupInfo radminUserGroupInfo = UserGroups.GetUserGroupInfo(int.Parse(radminid.SelectedValue));
            if (radminUserGroupInfo != null)
            {
                //设置管理组初始化信息
                //DataRow usergrouprights = usergrouprightstable.Rows[0];
                creditshigher.Text = radminUserGroupInfo.ug_scorehight.ToString();
                creditslower.Text = radminUserGroupInfo.ug_scorelow.ToString();
                stars.Text = radminUserGroupInfo.Stars.ToString();
                color.Text = radminUserGroupInfo.ug_color;
                groupavatar.Text = radminUserGroupInfo.ug_logo;
                readaccess.Text = radminUserGroupInfo.ug_readaccess.ToString();
                //maxprice.Text = radminUserGroupInfo.Maxprice.ToString();
                maxpmnum.Text = radminUserGroupInfo.Maxpmnum.ToString();
                maxsigsize.Text = radminUserGroupInfo.ug_maxsigsize.ToString();
                maxattachsize.Text = radminUserGroupInfo.Ug_maxattachsize.ToString();
                maxsizeperday.Text = radminUserGroupInfo.Ug_maxsizeperday.ToString();
                DataTable dt = Attachments.GetAttachmentType();
                attachextensions.AddTableData(dt, radminUserGroupInfo.ug_attachextensions);
            }

            AdminGroupInfo radminUserGroup = AdminGroups.GetAdminGroupInfo(int.Parse(radminid.SelectedValue));
            if (radminUserGroup != null)
            {
                //设置管理权限组初始化信息
                //DataRow dr = admingrouprights.Rows[0];
                admingroupright.SelectedIndex = -1;
                admingroupright.Items[0].Selected = radminUserGroup.Alloweditpost == 1;
                //admingroupright.Items[1].Selected = radminUserGroup.Alloweditpoll == 1;
                admingroupright.Items[2].Selected = radminUserGroup.Allowdelpost == 1;
                admingroupright.Items[3].Selected = radminUserGroup.Allowmassprune == 1;
                admingroupright.Items[4].Selected = radminUserGroup.Allowviewip == 1;
                admingroupright.Items[5].Selected = radminUserGroup.Allowedituser == 1;
                admingroupright.Items[6].Selected = radminUserGroup.Allowviewlog == 1;
                //admingroupright.Items[7].Selected = radminUserGroup.Disablepostctrl == 1;
            }
            #endregion
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.TabControl1.InitTabPage();
            this.UpdateUserGroupInf.Click += new EventHandler(this.UpdateUserGroupInf_Click);
            radminid.Items.Add(new ListItem("请选择     ", "0"));
            foreach (UserGroupInfo userGroupInfo in UserGroups.GetAdminUserGroup())
                radminid.Items.Add(new ListItem(userGroupInfo.ug_name, userGroupInfo.ug_id.ToString()));
            DataTable dt = Attachments.GetAttachmentType();
            attachextensions.AddTableData(dt);

            string groupid = SASRequest.GetString("groupid");
            if (groupid != "")
            {
                LoadUserGroupInf(SASRequest.GetInt("groupid", -1));
            }
            else
            {
                Response.Redirect("sysglobal_sysadminusergroupgrid.aspx");
            }

        }

        #endregion
    }
}
