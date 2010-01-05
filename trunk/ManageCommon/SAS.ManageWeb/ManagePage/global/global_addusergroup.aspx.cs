using System;
using System.Data;
using System.Web.UI;
using System.Collections;

using SAS.Logic;
using SAS.Common;
using SAS.Config;
using SAS.Common.Generic;
using SAS.Entity;
using SAS.Plugin.Album;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 添加积分用户组
    /// </summary>
    public partial class global_addusergroup : AdminPage
    {
        protected bool haveAlbum;
        protected bool haveSpace;

        protected void Page_Load(object sender, EventArgs e)
        {
            haveAlbum = AlbumPluginProvider.GetInstance() != null;
            //haveSpace = SpacePluginProvider.GetInstance() != null;
            if (!Page.IsPostBack)
            {
                usergrouppowersetting.Bind();
                if (SASRequest.GetString("groupid") != "")
                {
                    SetGroupRights(SASRequest.GetInt("groupid", 0));
                }
            }
        }

        public void SetGroupRights(int groupid)
        {
            #region 设置组权限相关信息
            UserGroupInfo userGroupInfo = UserGroups.GetUserGroupInfo(groupid);
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
            #endregion
        }

        private void AddUserGroupInf_Click(object sender, EventArgs e)
        {
            #region 插入相关组信息数据

            if (this.CheckCookie())
            {
                Hashtable ht = new Hashtable();
                ht.Add("附件最大尺寸", maxattachsize.Text);
                ht.Add("每天最大附件总尺寸", maxsizeperday.Text);
                ht.Add("个人空间附件总尺寸", maxspaceattachsize.Text);
                ht.Add("相册空间总尺寸", maxspacephotosize.Text);
                foreach (DictionaryEntry de in ht)
                {
                    if (!Utils.IsInt(de.Value.ToString()))
                    {
                        base.RegisterStartupScript("", "<script>alert('输入错误," + de.Key.ToString() + "只能是0或者正整数');window.location.href='global_editusergroup.aspx';</script>");
                        return;
                    }
                }
                UserGroupInfo userGroupInfo = new UserGroupInfo();
                userGroupInfo.ug_isSystem = 0;
                //userGroupInfo.Type = 0;
                userGroupInfo.ug_readaccess = Convert.ToInt32(readaccess.Text == "" ? "0" : readaccess.Text);
                userGroupInfo.ug_pg_id = 0;
                userGroupInfo.ug_name = groupTitle.Text;
                userGroupInfo.ug_scorehight = Convert.ToInt32(creditshigher.Text);
                userGroupInfo.ug_scorelow = Convert.ToInt32(creditslower.Text);
                usergrouppowersetting.GetSetting(ref userGroupInfo);
                if (userGroupInfo.ug_scorehight >= userGroupInfo.ug_scorelow)
                {
                    base.RegisterStartupScript("", "<script>alert('操作失败, 积分下限必须小于积分上限');</script>");
                    return;
                }
                //if (userGroupInfo.Allowbonus == 1 && (userGroupInfo.Minbonusprice >= userGroupInfo.Maxbonusprice))
                //{
                //    base.RegisterStartupScript("", "<script>alert('操作失败, 最低悬赏价格必须小于最高悬赏价格');</script>");
                //    return;
                //}
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
                //userGroupInfo.Raterange = "";

                if (AdminUserGroups.AddUserGroupInfo(userGroupInfo))
                {
                    SAS.Cache.SASCache.GetCacheService().RemoveObject("/SAS/UserGroupList");
                    UserGroups.GetUserGroupList();

                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台添加用户组", "组名:" + groupTitle.Text);

                    base.RegisterStartupScript("PAGE", "window.location.href='global_usergroupgrid.aspx';");
                }
                else
                {
                    if (AdminUserGroups.opresult != "")
                    {
                        base.RegisterStartupScript("", "<script>alert('操作失败,原因:" + AdminUserGroups.opresult + "');window.location.href='global_usergroupgrid.aspx';</script>");
                    }
                    else
                    {
                        base.RegisterStartupScript("", "<script>alert('操作失败');window.location.href='global_usergroupgrid.aspx';</script>");
                    }
                }
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
            this.AddUserGroupInf.Click += new EventHandler(this.AddUserGroupInf_Click);

            DataTable dt = Attachments.GetAttachmentType();
            attachextensions.AddTableData(dt);
        }

        #endregion
    }
}
