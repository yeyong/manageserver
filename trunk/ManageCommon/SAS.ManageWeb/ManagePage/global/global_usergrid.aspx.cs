﻿using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;

namespace SAS.ManageWeb.ManagePage
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public partial class global_usergrid : SAS.Web.UI.AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region 实始化控件

                joindateStart.SelectedDate = DateTime.Now.AddDays(-30);

                joindateEnd.SelectedDate = DateTime.Now;

                UserGroup.AddTableData(UserGroups.GetUserGroupForDataTable(), "pg_name", "pg_id");
                if ((SASRequest.GetString("username") != null) && (SASRequest.GetString("username") != ""))
                {
                    ViewState["condition"] = Users.GetUserListCondition(SASRequest.GetString("username"));
                    searchtable.Visible = false;
                    ResetSearchTable.Visible = true;
                }

                if (ViewState["condition"] != null)
                {
                    searchtable.Visible = false;
                    ResetSearchTable.Visible = true;
                }
                else
                {
                    if (SASRequest.GetString("condition") != "")
                    {
                        ViewState["condition"] = SASRequest.GetString("condition").Replace("~^", "'").Replace("~$", "%");
                        searchtable.Visible = false;
                        ResetSearchTable.Visible = true;
                    }
                }
                BindData();

                #endregion
            }
        }

        public void BindData()
        {
            #region 绑定数据

            DataGrid1.AllowCustomPaging = true;
            DataGrid1.VirtualItemCount = GetRecordCount();
            DataGrid1.DataSource = buildGridData();
            DataGrid1.DataBind();

            #endregion
        }

        private DataTable buildGridData()
        {
            #region 加载数据

            DataTable dt = new DataTable();
            if (ViewState["condition"] == null)
            {
                dt = Users.GetUserListByCurrentPage(DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1);
            }
            else
            {
                dt = Users.GetUserList(DataGrid1.PageSize, DataGrid1.CurrentPageIndex + 1, ViewState["condition"].ToString());
            }

            if ((dt.Rows.Count == 1) && (SASRequest.GetString("username") != null) && (SASRequest.GetString("username") != ""))
            {
                Response.Redirect("global_edituser.aspx?uid=" + dt.Rows[0][0].ToString());
            }
            return dt;

            #endregion
        }

        public void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            BindData();
        }

        private int GetRecordCount()
        {
            #region 获取记录数

            if (ViewState["condition"] == null)
            {
                return Users.GetUserCount("");
            }
            else
            {
                return Users.GetUserCount(ViewState["condition"].ToString());
            }

            #endregion
        }

        public void GoToPagerButton_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void StopTalk_Click(object sender, EventArgs e)
        {
            #region 禁言
            if (SASRequest.GetString("uid") != "")
            {
                string uidlist = "0" + SASRequest.GetString("uid");
                string[] uids = uidlist.Split(',');
                foreach (string uid in uids)
                {
                    if (!string.IsNullOrEmpty(uid))
                    {

                        Guid iuid = new Guid(uid);
                        //SpacePluginProvider.GetInstance().Ban(iuid);
                        //AlbumPluginProvider.GetInstance().Ban(iuid);
                        SAS.Logic.OnlineUsers.DeleteUserByUid(iuid);
                    }
                }
                SAS.Logic.Users.UpdateUserToStopTalkGroup(uidlist);
                base.RegisterStartupScript("PAGE", "window.location.href='global_usergrid.aspx';");
            }
            else
            {
                base.RegisterStartupScript("", "<script>alert('请选择相应的用户!');window.location.href='global_usergrid.aspx';</script>");
            }

            #endregion
        }

        private bool CheckSponser(string uid)
        {
            #region 检查创建人

            return !((BaseConfigs.GetBaseConfig().Founderuid == uid) && (!BaseConfigs.GetBaseConfig().Founderuid.Equals(this.userid.ToString())));

            #endregion
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            #region 删除相关用户

            if (this.CheckCookie())
            {
                string uidList = SASRequest.GetString("uid").Trim(',');
                if (uidList != "")
                {
                    bool delpost = deltype.SelectedValue.IndexOf("1") >= 0 ? false : true;
                    bool delpms = deltype.SelectedValue.IndexOf("2") >= 0 ? false : true;

                    foreach (string uid in uidList.Split(','))
                    {
                        if (uid != "")
                        {
                            if (CheckSponser(uid))
                            {
                                if (Convert.ToInt32(uid) > 1) //判断是不是当前Uid是不是系统初始化时生成的Uid
                                {
                                    int deluserid = Convert.ToInt32(uid);
                                    ////if (AlbumPluginProvider.GetInstance() != null)
                                    ////{
                                    ////    AlbumPluginProvider.GetInstance().Delete(deluserid);
                                    ////}
                                    ////if (SpacePluginProvider.GetInstance() != null)
                                    ////{
                                    ////    SpacePluginProvider.GetInstance().Delete(deluserid);
                                    ////}
                                    if (AdminUsers.DelUserAllInf(deluserid, delpost, delpms))
                                    {
                                        AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台删除用户", "用户名:批量用户删除");
                                        base.RegisterStartupScript("PAGE", "window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('请选择相应的用户!');window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';</script>");
                }
            }

            #endregion
        }


        private void Search_Click(object sender, EventArgs e)
        {
            #region 按指定条件查询用户数据

            if (this.CheckCookie())
            {
                string searchcondition = Users.GetUsersSearchCondition(islike.Checked,
                    ispostdatetime.Checked, Username.Text, nickname.Text, UserGroup.SelectedValue, email.Text, credits_start.Text,
                    credits_end.Text, lastip.Text, posts.Text, digestposts.Text, uid.Text, joindateStart.SelectedDate.ToString(),
                    joindateEnd.SelectedDate.AddDays(1).ToString());
                ViewState["condition"] = searchcondition;

                searchtable.Visible = false;
                ResetSearchTable.Visible = true;
                DataTable dt = Users.GetUsersByCondition(searchcondition);
                if (dt.Rows.Count == 1)
                {
                    Response.Redirect("global_edituser.aspx?uid=" + dt.Rows[0][0].ToString() + "&condition=" + ViewState["condition"].ToString().Replace("'", "~^").Replace("%", "~$"));
                }
                else
                {
                    DataGrid1.CurrentPageIndex = 0;
                    BindData();
                }
            }

            #endregion
        }

        private void ResetSearchTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("global_usergrid.aspx");
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Search.Click += new EventHandler(this.Search_Click);
            this.StopTalk.Click += new EventHandler(this.StopTalk_Click);
            this.DeleteUser.Click += new EventHandler(this.DeleteUser_Click);
            this.DataGrid1.GoToPagerButton.Click += new EventHandler(GoToPagerButton_Click);
            this.ResetSearchTable.Click += new EventHandler(this.ResetSearchTable_Click);

            DataGrid1.TableHeaderName = "用户列表";
            DataGrid1.DataKeyField = "uid";
            DataGrid1.AllowSorting = false;
            DataGrid1.ColumnSpan = 12;
        }

        #endregion
    }
}
