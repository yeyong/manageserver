using System;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI;

using SAS.Common;
using SAS.Logic;
using SAS.Control;
using SAS.Config;
using SAS.Entity;

namespace SAS.ManageWeb.ManagePage
{
    public partial class global_edituser : SAS.Web.UI.AdminPage
    {
        public UserInfo userInfo = new UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!AllowEditUser(this.userid, SASRequest.GetInt("uid", -1)))
                {
                    Response.Write("<script>alert('非创始人身份不能修改其它管理员的信息!');window.location.href='global_usergrid.aspx';</script>");
                    Response.End();
                    return;
                }
                IsEditUserName.Attributes.Add("onclick", "document.getElementById('" + userName.ClientID + "').disabled = !document.getElementById('" + IsEditUserName.ClientID + "').checked;");
            }
        }

        private bool AllowEditUser(int managerUId, int targetUId)
        {
            #region 是否可以编辑用户
            int managerGroupId = Users.GetUserInfo(managerUId).Ps_ug_id;
            if (Users.GetUserInfo(managerUId).Ps_pg_id == 0)
            {
                return false;
            }
            int targetGroupId = Users.GetUserInfo(targetUId).Ps_ug_id;
            int founderUId = BaseConfigs.GetBaseConfig().Founderuid;
            if (managerUId == targetUId)    //可自身修改
                return true;
            else if (managerUId == founderUId)  //创始人可修改
                return true;
            else if (managerGroupId == targetGroupId)   //管理组相同的不能修改
                return false;
            else
                return true;
            #endregion
        }

        public bool AllowEditUserInfo(int uid, bool redirect)
        {
            #region 是否允许编辑用户信息

            if ((BaseConfigs.GetBaseConfig().Founderuid == uid) && (uid == this.userid))
                return true;
            if (BaseConfigs.GetBaseConfig().Founderuid != uid) //当要编辑的用户信息不是创建人的信息时
                return true;
            if (redirect)
            {
                base.RegisterStartupScript("", "<script>alert('您要编辑信息是论坛创始人信息,请您以创始人身份登陆后台才能修改!');</script>");
            }
            return false;

            #endregion
        }

        ////public bool IsValidScoreName(int scoreid)
        ////{
        ////    #region 是否是有效的积分名称

        ////    bool isvalid = false;

        ////    foreach (DataRow dr in Scoresets.GetScoreSet().Rows)
        ////    {
        ////        if ((dr["id"].ToString() != "1") && (dr["id"].ToString() != "2"))
        ////        {
        ////            if (dr[scoreid + 1].ToString().Trim() != "0")
        ////            {
        ////                isvalid = true;
        ////                break;
        ////            }
        ////        }
        ////    }
        ////    return isvalid;

        ////    #endregion
        ////}

        ////public void LoadScoreInf(string fid, string fieldname)
        ////{
        ////    #region 加载积分信息

        ////    DataRow dr = Scoresets.GetScoreSet().Rows[0];
        ////    if (dr[2].ToString().Trim() != "")
        ////    {
        ////        extcredits1name.Text = dr[2].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(1))
        ////        {
        ////            extcredits1.Enabled = false;
        ////        }
        ////    }

        ////    if (dr[3].ToString().Trim() != "")
        ////    {
        ////        extcredits2name.Text = dr[3].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(2))
        ////        {
        ////            extcredits2.Enabled = false;
        ////        }
        ////    }


        ////    if (dr[4].ToString().Trim() != "")
        ////    {
        ////        extcredits3name.Text = dr[4].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(3))
        ////        {
        ////            extcredits3.Enabled = false;
        ////        }
        ////    }


        ////    if (dr[5].ToString().Trim() != "")
        ////    {
        ////        extcredits4name.Text = dr[5].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(4))
        ////        {
        ////            extcredits4.Enabled = false;
        ////        }
        ////    }


        ////    if (dr[6].ToString().Trim() != "")
        ////    {
        ////        extcredits5name.Text = dr[6].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(5))
        ////        {
        ////            extcredits5.Enabled = false;
        ////        }
        ////    }


        ////    if (dr[7].ToString().Trim() != "")
        ////    {
        ////        extcredits6name.Text = dr[7].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(6))
        ////        {
        ////            extcredits6.Enabled = false;
        ////        }
        ////    }


        ////    if (dr[8].ToString().Trim() != "")
        ////    {
        ////        extcredits7name.Text = dr[8].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(7))
        ////        {
        ////            extcredits7.Enabled = false;
        ////        }
        ////    }


        ////    if (dr[9].ToString().Trim() != "")
        ////    {
        ////        extcredits8name.Text = dr[9].ToString().Trim();
        ////    }
        ////    else
        ////    {
        ////        if (!IsValidScoreName(8))
        ////        {
        ////            extcredits8.Enabled = false;
        ////        }
        ////    }

        ////    #endregion
        ////}

        public void LoadCurrentUserInfo(int uid)
        {
            #region 加载相关信息

            userInfo = AdminUsers.GetUserInfo(uid);

            ViewState["username"] = userInfo.Ps_name;
            userName.Text = userInfo.Ps_name;

            //只有在当前用户为等待验证用户, 且系统论坛设置为1时才会显示重发EMAIL按钮
            if ((userInfo.Ps_ug_id == 8) && (config.Regverify == 1)) ReSendEmail.Visible = true;
            else ReSendEmail.Visible = false;

            nickname.Text = userInfo.Ps_nickName;
            ////accessmasks.SelectedValue = userInfo..ToString();
            bday.Text = userInfo.Pd_birthday.Trim();
            credits.Text = userInfo.Ps_credits.ToString();
            ////digestposts.Text = userInfo.Digestposts.ToString();
            email.Text = userInfo.Ps_email.Trim();
            gender.SelectedValue = userInfo.Ps_gender.ToString();
            ////groupexpiry.Text = userInfo.Groupexpiry.ToString();

            if (userInfo.Ps_ug_id.ToString() == "")
            {
                groupid.SelectedValue = "0";
            }
            else
            {
                try
                {
                    groupid.SelectedValue = userInfo.Ps_ug_id.ToString();
                }
                catch
                {
                    ////groupid.SelectedValue = UserCredits.GetCreditsUserGroupId(userInfo.Credits).Groupid.ToString();
                }
            }

            if (uid == BaseConfigs.GetFounderUid)
            {
                groupid.Enabled = false;
            }

            if (userInfo.Ps_ug_id == 4)
            {
                StopTalk.Text = "取消禁言";
                StopTalk.HintInfo = "取消禁言将会把当前用户所在的 \\'系统禁言\\' 组进行系统调整成为非禁言组";
            }

            ViewState["Groupid"] = userInfo.Ps_ug_id.ToString();

            invisible.SelectedValue = userInfo.Ps_invisible.ToString();
            joindate.Text = userInfo.Ps_createDate.ToString();
            lastactivity.Text = userInfo.Ps_lastactivity.ToString();
            lastip.Text = userInfo.Ps_loginIP.Trim();
            lastpost.Text = userInfo.Ps_lastChangePass.ToString();
            lastvisit.Text = userInfo.Ps_lastLogin;
            newpm.SelectedValue = userInfo.Ps_newpm.ToString();
            switch (userInfo.Ps_newsletter)
            {
                case ReceivePMSettingType.ReceiveNone:
                    SetNewsLetter(false, false, false);
                    break;
                case ReceivePMSettingType.ReceiveSystemPM:
                    SetNewsLetter(true, false, false);
                    break;
                case ReceivePMSettingType.ReceiveUserPM:
                    SetNewsLetter(false, true, false);
                    break;
                case ReceivePMSettingType.ReceiveAllPM:
                    SetNewsLetter(true, true, false);
                    break;
                case ReceivePMSettingType.ReceiveSystemPMWithHint:
                    SetNewsLetter(true, false, true);
                    break;
                case ReceivePMSettingType.ReceiveUserPMWithHint:
                    SetNewsLetter(false, true, true);
                    break;
                default:
                    SetNewsLetter(true, true, true);
                    break;
            }
            oltime.Text = userInfo.Ps_onlinetime.ToString();
            pageviews.Text = userInfo.Ps_pageviews.ToString();
            pmsound.Text = userInfo.Ps_bdSound.ToString();
            ////posts.Text = userInfo.Posts.ToString();
            ////ppp.Text = userInfo.Ppp.ToString();
            regip.Text = userInfo.Ps_regIP.Trim();

            showemail.SelectedValue = userInfo.Ps_isEmail.ToString();
            sigstatus.SelectedValue = userInfo.Ps_issign.ToString();

            if ((userInfo.Ps_tempID.ToString() != "") && (userInfo.Ps_tempID.ToString() != "0"))
            {
                templateid.SelectedValue = userInfo.Ps_tempID.ToString();
            }

            ////tpp.Text = userInfo.Tpp.ToString();

            ////extcredits1.Text = userInfo.Extcredits1.ToString();
            ////extcredits2.Text = userInfo.Extcredits2.ToString();
            ////extcredits3.Text = userInfo.Extcredits3.ToString();
            ////extcredits4.Text = userInfo.Extcredits4.ToString();
            ////extcredits5.Text = userInfo.Extcredits5.ToString();
            ////extcredits6.Text = userInfo.Extcredits6.ToString();
            ////extcredits7.Text = userInfo.Extcredits7.ToString();
            ////extcredits8.Text = userInfo.Extcredits8.ToString();


            //用户扩展信息
            website.Text = userInfo.Pd_website;
            ////icq.Text = userInfo.Icq;
            qq.Text = userInfo.Pd_QQ;
            yahoo.Text = userInfo.Pd_Yahoo;
            msn.Text = userInfo.Pd_MSN;
            skype.Text = userInfo.Pd_Skype;
            location.Text = userInfo.Pd_address_1;
            ////customstatus.Text = userInfo.Customstatus;
            //avatar.Text = userInfo.Avatar;
            //avatarheight.Text = userInfo.Avatarheight.ToString();
            //avatarwidth.Text = userInfo.Avatarwidth.ToString();
            bio.Text = userInfo.Pd_bio;
            signature.Text = userInfo.Pd_sign;
            realname.Text = userInfo.Pd_name;
            idcard.Text = userInfo.Pd_idcard;
            mobile.Text = userInfo.Pd_mobile;
            phone.Text = userInfo.Pd_phone;

            givenusername.Text = userInfo.Ps_name;

            ////if (userInfo.Medals.Trim() == "")
            ////{
            ////    userInfo.Medals = "0";
            ////}

            ////string begivenmedals = "," + userInfo.Medals + ",";
            ////DataTable dt = Medals.GetAvailableMedal();

            ////if (dt != null)
            ////{
            ////    DataColumn dc = new DataColumn();
            ////    dc.ColumnName = "isgiven";
            ////    dc.DataType = Type.GetType("System.Boolean");
            ////    dc.DefaultValue = false;
            ////    dc.AllowDBNull = false;
            ////    dt.Columns.Add(dc);

            ////    foreach (DataRow dr in dt.Rows)
            ////    {
            ////        if (begivenmedals.IndexOf("," + dr["medalid"].ToString() + ",") >= 0)
            ////        {
            ////            dr["isgiven"] = true;
            ////        }
            ////    }
            ////    medalslist.DataSource = dt;
            ////    medalslist.DataBind();
            ////}

            #endregion
        }

        private void SetNewsLetter(bool item1, bool item2, bool item3)
        {
            newsletter.Items[0].Selected = item2;
            newsletter.Items[1].Selected = item3;

            if (!item2)
            {
                newsletter.Items[1].Selected = false;
                newsletter.Items[1].Enabled = false;
            }
        }

        private int GetNewsLetter()
        {
            int item2 = 0;
            int item3 = 0;

            if (newsletter.Items[0].Selected)
            {
                item2 = 2;
            }
            if (newsletter.Items[1].Selected)
            {
                item3 = 4;
            }

            return item2 | item3;
        }

        private void IsEditUserName_CheckedChanged(object sender, EventArgs e)
        {
            #region 是否可以编辑用户名
            if (IsEditUserName.Checked)
            {
                userName.Enabled = true;
            }
            else
            {
                userName.Enabled = false;
            }
            #endregion
        }

        public string BeGivenMedal(string isgiven, string medalid)
        {
            #region 勋章的显示方式

            if (isgiven == "True")
            {
                return "<INPUT id=\"medalid\"  type=\"checkbox\" value=\"" + medalid + "\"  name=\"medalid\" checked>";
            }
            else
            {
                return "<INPUT id=\"medalid\"  type=\"checkbox\" value=\"" + medalid + "\"  name=\"medalid\">";
            }

            #endregion
        }


        ////private void GivenMedal_Click(object sender, EventArgs e)
        ////{
        ////    #region 给予勋章

        ////    if (this.CheckCookie())
        ////    {
        ////        int uid = SASRequest.GetInt("uid", -1);
        ////        GivenUserMedal(uid);

        ////        if (SASRequest.GetString("codition") == "")
        ////        {
        ////            Session["codition"] = null;
        ////        }
        ////        else
        ////        {
        ////            Session["codition"] = SASRequest.GetString("codition").Replace("^", "'");
        ////        }

        ////        base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + uid + "&condition=" + SASRequest.GetString("condition") + "';");
        ////    }

        ////    #endregion
        ////}

        ////private void GivenUserMedal(int uid)
        ////{
        ////    Users.UpdateMedals(uid, SASRequest.GetString("medalid"), userid, username, SASRequest.GetIP(), reason.Text.Trim());
        ////}

        ////private void ResetUserDigestPost_Click(object sender, EventArgs e)
        ////{
        ////    #region 重设用户精华帖

        ////    if (this.CheckCookie())
        ////    {
        ////        AdminForumStats.ReSetUserDigestPosts(SASRequest.GetInt("uid", -1), SASRequest.GetInt("uid", -1));
        ////        base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + userInfo.Uid + "&condition=" + SASRequest.GetString("condition") + "';");
        ////    }

        ////    #endregion
        ////}


        ////private void ResetUserPost_Click(object sender, EventArgs e)
        ////{
        ////    #region 重设用户发帖

        ////    if (this.CheckCookie())
        ////    {
        ////        AdminForumStats.ReSetUserPosts(SASRequest.GetInt("uid", -1), SASRequest.GetInt("uid", -1));
        ////        base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + userInfo.Uid + "&condition=" + SASRequest.GetString("condition") + "';");
        ////    }

        ////    #endregion
        ////}


        private void ResetPassWord_Click(object sender, EventArgs e)
        {
            #region 重设用户密码

            if (this.CheckCookie())
            {
                if (!AllowEditUserInfo(SASRequest.GetInt("uid", -1), true)) return;

                Response.Redirect("global_resetpassword.aspx?uid=" + SASRequest.GetString("uid"));
            }

            #endregion
        }


        private void StopTalk_Click(object sender, EventArgs e)
        {
            #region 设置禁言

            if (this.CheckCookie())
            {
                userInfo = AdminUsers.GetUserInfo(SASRequest.GetInt("uid", -1));

                if (!AllowEditUserInfo(SASRequest.GetInt("uid", -1), true)) return;

                if (ViewState["Groupid"].ToString() != "4") //当用户不是系统禁言组时
                {
                    if (userInfo.Ps_id > 1) //判断是不是当前uid是不是系统初始化时生成的uid
                    {
                        ////if (AlbumPluginProvider.GetInstance() != null)
                        ////    AlbumPluginProvider.GetInstance().Ban(userInfo.Uid);
                        ////if (SpacePluginProvider.GetInstance() != null)
                        ////    SpacePluginProvider.GetInstance().Ban(userInfo.Uid);
                        Users.UpdateUserToStopTalkGroup(userInfo.Ps_id.ToString());
                        base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + userInfo.Ps_id + "&condition=" + SASRequest.GetString("condition") + "';");
                    }
                    else
                    {
                        base.RegisterStartupScript("", "<script>alert('操作失败,你要禁言的用户是系统初始化时的用户,因此不能操作!');window.location.href='global_edituser.aspx?uid=" + userInfo.Ps_id + "&condition=" + SASRequest.GetString("condition") + "';</script>");
                    }
                }
                else
                {
                    if (UserCredits.GetCreditsUserGroupId(0) != null)
                    {
                        int tmpGroupID = UserCredits.GetCreditsUserGroupId(userInfo.Ps_credits).ug_id;
                        Users.UpdateUserGroup(userInfo.Ps_id, tmpGroupID);
                        base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + userInfo.Ps_id + "&condition=" + SASRequest.GetString("condition") + "';");
                    }
                    else
                    {
                        base.RegisterStartupScript("", "<script>alert('操作失败,系统未能找到合适的用户组来调整当前用户所处的组!');window.location.href='global_edituser.aspx?uid=" + userInfo.Ps_id + "&condition=" + SASRequest.GetString("condition") + "';</script>");
                    }
                }
                OnlineUsers.DeleteUserByUid(userInfo.Ps_id);
            }

            #endregion
        }


        private void DelPosts_Click(object sender, EventArgs e)
        {
            #region 删除用户帖

            if (this.CheckCookie())
            {
                int uid = SASRequest.GetInt("uid", -1);

                if (!AllowEditUserInfo(uid, true)) return;

                //清除用户所发的帖子
                ////Posts.ClearPosts(uid);
                //foreach (DataRow dr in Posts.GetAllPostTableName().Rows)
                //{
                //    if (dr["id"].ToString() != "")
                //    {
                //        Posts.DeletePostByPosterid(int.Parse(dr["id"].ToString()), uid);
                //    }
                //}
                //Topics.DeleteTopicByPosterid(uid);
                //Users.ClearPosts(uid);
                base.RegisterStartupScript("", "<script>alert('请到 论坛维护->论坛数据维护->重建指定主题区间帖数 对出现因为该操作产生\"读取信息失败\"的主题进行修复 ')</script>");
                base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + uid + "&condition=" + SASRequest.GetString("condition") + "';");
            }

            #endregion
        }

        private void ReSendEmail_Click(object sender, EventArgs e)
        {
            #region 发送EMAIL

            string authstr = LogicUtils.CreateAuthStr(20);
            Emails.SASSmtpMail(userName.Text, email.Text, "", authstr);
            string uid = SASRequest.GetString("uid");
            //DbHelper.ExecuteNonQuery("UPDATE [" + BaseConfigs.GetTablePrefix + "userfields] SET [Authstr]='" + authstr + "' , [Authtime]='" + DateTime.Now.ToString() + "' ,[Authflag]=1  WHERE [uid]=" + uid);
            Users.UpdateEmailValidateInfo(authstr, DateTime.Now, int.Parse(uid));
            base.RegisterStartupScript("PAGE", "window.location.href='global_edituser.aspx?uid=" + uid + "&condition=" + SASRequest.GetString("condition") + "';");

            #endregion
        }

        private void SaveUserInfo_Click(object sender, EventArgs e)
        {
            #region 保存用户信息

            if (this.CheckCookie())
            {
                int uid = SASRequest.GetInt("uid", -1);
                string errorInfo = "";

                if (!AllowEditUserInfo(uid, true)) return;

                if (userName.Text != ViewState["username"].ToString())
                {
                    if (AdminUsers.GetUserId(userName.Text) > 0)
                    {
                        base.RegisterStartupScript("", "<script>alert('您所输入的用户名已被使用过, 请输入其他的用户名!');</script>");
                        return;
                    }
                }

                if (userName.Text == "")
                {
                    base.RegisterStartupScript("", "<script>alert('用户名不能为空!');</script>");
                    return;
                }

                if (groupid.SelectedValue == "0")
                {
                    base.RegisterStartupScript("", "<script>alert('您未选中任何用户组!');</script>");
                    return;
                }

                userInfo = AdminUsers.GetUserInfo(uid);
                userInfo.Ps_name = userName.Text;
                userInfo.Ps_nickName = nickname.Text;
                ////userInfo.Accessmasks = Convert.ToInt32(accessmasks.SelectedValue);

                //当用户组发生变化时则相应更新用户的管理组字段
                if (userInfo.Ps_ug_id.ToString() != groupid.SelectedValue)
                {
                    userInfo.Ps_pg_id = UserGroups.GetUserGroupInfo(int.Parse(groupid.SelectedValue)).ug_pg_id;
                }

                //userInfo.Avatarshowid = 0;

                if ((bday.Text == "0000-00-00") || (bday.Text == "0000-0-0") | (bday.Text.Trim() == ""))
                {
                    userInfo.Pd_birthday = "";
                }
                else
                {
                    if (!Utils.IsDateString(bday.Text.Trim()))
                    {
                        base.RegisterStartupScript("", "<script>alert('用户生日不是有效的日期型数据!');</script>");
                        return;
                    }
                    else
                    {
                        userInfo.Pd_birthday = bday.Text;
                    }
                }

                if (Utils.IsNumeric(credits.Text.Replace("-", "")))
                {
                    userInfo.Ps_credits = Convert.ToInt32(credits.Text);
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('用户的积分不能为空或大于9位 !');</script>");
                    return;
                }

                if (!Users.ValidateEmail(email.Text, uid))
                {
                    base.RegisterStartupScript("", "<script>alert('当前用户的邮箱地址已被使用过, 请输入其他的邮箱!');</script>");
                    return;
                }

                userInfo.Ps_email = email.Text;
                userInfo.Ps_gender = Convert.ToInt32(gender.SelectedValue);
                ////userInfo.Groupexpiry = Convert.ToInt32(groupexpiry.Text);
                ////userInfo.Extgroupids = extgroupids.GetSelectString(",");

                if ((groupid.SelectedValue != "1") && (userInfo.Ps_id == 1))
                {
                    base.RegisterStartupScript("", "<script>alert('初始化系统管理员的所属用户组设置不能修改为其它组!');window.location.href='global_edituser.aspx?uid=" + SASRequest.GetString("uid") + "';</script>");
                    return;
                }

                userInfo.Ps_ug_id = Convert.ToInt32(groupid.SelectedValue);
                userInfo.Ps_invisible = Convert.ToInt32(invisible.SelectedValue);
                userInfo.Ps_createDate = joindate.Text;
                userInfo.Ps_lastactivity = lastactivity.Text;
                userInfo.Ps_loginIP = lastip.Text;
                ////userInfo.Lastpost = lastpost.Text;
                userInfo.Ps_lastLogin = lastvisit.Text;
                userInfo.Ps_newpm = Convert.ToInt32(newpm.SelectedValue);
                userInfo.Ps_newsletter = (ReceivePMSettingType)GetNewsLetter();
                userInfo.Ps_onlinetime = Convert.ToInt32(oltime.Text);
                userInfo.Ps_pageviews = Convert.ToInt32(pageviews.Text);
                userInfo.Ps_bdSound = Convert.ToInt32(pmsound.Text);
                ////userInfo.Posts = Convert.ToInt32(posts.Text);
                ////userInfo.Ppp = Convert.ToInt32(ppp.Text);
                userInfo.Ps_regIP = regip.Text;
                ////userInfo.Digestposts = Convert.ToInt32(digestposts.Text);

                if (secques.SelectedValue == "1") userInfo.Ps_secques = ""; //清空安全码

                userInfo.Ps_isEmail = Convert.ToInt32(showemail.SelectedValue);
                userInfo.Ps_issign = Convert.ToInt32(sigstatus.SelectedValue);
                userInfo.Ps_tempID = Convert.ToInt32(templateid.SelectedValue);
                ////userInfo.Tpp = Convert.ToInt32(tpp.Text);


                ////if (Utils.IsNumeric(extcredits1.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits1 = float.Parse(extcredits1.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits2.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits2 = float.Parse(extcredits2.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits3.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits3 = float.Parse(extcredits3.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits4.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits4 = float.Parse(extcredits4.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits5.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits5 = float.Parse(extcredits5.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits6.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits6 = float.Parse(extcredits6.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits7.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits7 = float.Parse(extcredits7.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}

                ////if (Utils.IsNumeric(extcredits8.Text.Replace("-", "")))
                ////{
                ////    userInfo.Extcredits8 = float.Parse(extcredits8.Text);
                ////}
                ////else
                ////{
                ////    base.RegisterStartupScript("", "<script>alert('用户扩展积分不能为空或大于7位 !');</script>");
                ////    return;
                ////}


                //用户扩展信息
                userInfo.Pd_website = website.Text;
                ////userInfo.Icq = icq.Text;
                userInfo.Pd_QQ = qq.Text;
                userInfo.Pd_Yahoo = yahoo.Text;
                userInfo.Pd_MSN = msn.Text;
                userInfo.Pd_Skype = skype.Text;
                userInfo.Pd_address_1 = location.Text;
                ////userInfo.Customstatus = customstatus.Text;
                //userInfo.Avatar = avatar.Text;
                //userInfo.Avatarheight = Convert.ToInt32(avatarheight.Text);
                //userInfo.Avatarwidth = Convert.ToInt32(avatarwidth.Text);
                userInfo.Pd_bio = bio.Text;
                if (signature.Text.Length > UserGroups.GetUserGroupInfo(userInfo.Ps_ug_id).ug_maxsigsize)
                {
                    errorInfo = "更新的签名长度超过 " + UserGroups.GetUserGroupInfo(userInfo.Ps_ug_id).ug_maxsigsize + " 字符的限制，未能更新。";
                }
                else
                {
                    userInfo.Pd_sign = signature.Text;
                    //签名UBB转换HTML
                    ////PostpramsInfo _postpramsinfo = new PostpramsInfo();
                    ////_postpramsinfo.Showimages = UserGroups.GetUserGroupInfo(userInfo.Groupid).Allowsigimgcode;
                    ////_postpramsinfo.Sdetail = signature.Text;
                    ////userInfo.Sightml = UBB.UBBToHTML(_postpramsinfo);
                }

                userInfo.Pd_name = realname.Text;
                userInfo.Pd_idcard = idcard.Text;
                userInfo.Pd_mobile = mobile.Text;
                userInfo.Pd_phone = phone.Text;
                ////userInfo.Medals = SASRequest.GetString("medalid");

                if (IsEditUserName.Checked)
                {
                    AdminUsers.UserNameChange(userInfo, ViewState["username"].ToString());
                }

                if (AdminUsers.UpdateUserAllInfo(userInfo))
                {
                    OnlineUsers.DeleteUserByUid(userInfo.Ps_id);    //移除该用户的在线信息，使之重建在线表信息
                    ////if (ViewState["Groupid"].ToString() != userInfo.Groupid.ToString())
                    ////{
                    ////    if (userInfo.Groupid == 4)
                    ////    {
                    ////        if (AlbumPluginProvider.GetInstance() != null)
                    ////        {
                    ////            AlbumPluginProvider.GetInstance().Ban(userInfo.Uid);
                    ////        }
                    ////        if (SpacePluginProvider.GetInstance() != null)
                    ////        {
                    ////            SpacePluginProvider.GetInstance().Ban(userInfo.Uid);
                    ////        }
                    ////    }
                    ////    else
                    ////    {
                    ////        if (AlbumPluginProvider.GetInstance() != null)
                    ////        {
                    ////            AlbumPluginProvider.GetInstance().UnBan(userInfo.Uid);
                    ////        }
                    ////        if (SpacePluginProvider.GetInstance() != null)
                    ////        {
                    ////            SpacePluginProvider.GetInstance().UnBan(userInfo.Uid);
                    ////        }
                    ////    }
                    ////}
                    if (userName.Text != ViewState["username"].ToString())
                    {
                        AdminUsers.UserNameChange(userInfo, ViewState["username"].ToString());
                    }
                    //删除头像
                    if (delavart.Checked)
                        Avatars.DeleteAvatar(userInfo.Ps_id.ToString());
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台编辑用户", "用户名:" + userName.Text);
                    if (errorInfo == "")
                    {
                        base.RegisterStartupScript("PAGE", "window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';");
                    }
                    else
                    {
                        base.RegisterStartupScript("PAGE", "alert('" + errorInfo + "');window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';");
                    }
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('操作失败');window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';</script>");
                }
            }

            #endregion
        }

        private void DelUserInfo_Click(object sender, EventArgs e)
        {
            #region 删除指定用户信息

            if (this.CheckCookie())
            {
                int uid = SASRequest.GetInt("uid", -1);

                if (!AllowEditUserInfo(uid, true)) return;

                if (AllowDeleteUser(this.userid, uid))
                {
                    bool delpost = !(deltype.SelectedValue.IndexOf("1") >= 0);
                    bool delpms = !(deltype.SelectedValue.IndexOf("2") >= 0);

                    ////if (SpacePluginProvider.GetInstance() != null)
                    ////{
                    ////    SpacePluginProvider.GetInstance().Delete(uid);
                    ////}

                    ////if (AlbumPluginProvider.GetInstance() != null)
                    ////{
                    ////    AlbumPluginProvider.GetInstance().Delete(uid);
                    ////}
                    if (AdminUsers.DelUserAllInf(uid, delpost, delpms))
                    {
                        //删除该用户头像
                        Avatars.DeleteAvatar(uid.ToString());
                        ////AdminUsers.UpdateForumsFieldModerators(userName.Text);

                        AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "后台删除用户", "用户名:" + userName.Text);
                        base.RegisterStartupScript("PAGE", "window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';");
                    }
                    else
                    {
                        base.RegisterStartupScript("", "<script>alert('操作失败');window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';</script>");
                    }
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('操作失败,你要删除的用户是创始人用户或是其它管理员,因此不能删除!');window.location.href='global_usergrid.aspx?condition=" + SASRequest.GetString("condition") + "';</script>");
                }
            }

            #endregion
        }

        private bool AllowDeleteUser(int managerUId, int byDeleterUId)
        {
            #region 判断将要删除的用户是否是创始人
            int managerGroupId = Users.GetUserInfo(managerUId).Ps_ug_id;
            int byDeleterGruopid = Users.GetUserInfo(byDeleterUId).Ps_ug_id;
            int founderUId = BaseConfigs.GetBaseConfig().Founderuid;
            if (byDeleterUId == founderUId) //判断被删除人是否为创始人
            {
                return false;
            }
            else if (managerUId != founderUId && managerGroupId == byDeleterGruopid)    //判断被删除人是否为相同组，即是否都是管理员，管理员不能相互删除
            {
                return false;
            }
            else
            {
                return true;
            }
            #endregion
        }

        private void CalculatorScore_Click(object sender, EventArgs e)
        {
            #region 计算积分
            if (this.CheckCookie())
            {
                ////credits.Text = UserCredits.GetUserCreditsByUid(SASRequest.GetInt("uid", -1)).ToString();
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
            this.StopTalk.Click += new EventHandler(this.StopTalk_Click);
            this.DelPosts.Click += new EventHandler(this.DelPosts_Click);
            this.SaveUserInfo.Click += new EventHandler(this.SaveUserInfo_Click);
            this.ResetPassWord.Click += new EventHandler(this.ResetPassWord_Click);
            this.IsEditUserName.CheckedChanged += new EventHandler(this.IsEditUserName_CheckedChanged);

            this.DelUserInfo.Click += new EventHandler(this.DelUserInfo_Click);
            this.ReSendEmail.Click += new EventHandler(this.ReSendEmail_Click);
            this.CalculatorScore.Click += new EventHandler(this.CalculatorScore_Click);
            ////this.ResetUserDigestPost.Click += new EventHandler(this.ResetUserDigestPost_Click);
            ////this.ResetUserPost.Click += new EventHandler(this.ResetUserPost_Click);

            ////this.GivenMedal.Click += new EventHandler(this.GivenMedal_Click);

            foreach (UserGroupInfo userGroupInfo in UserGroups.GetUserGroupList())
            {
                groupid.Items.Add(new ListItem(userGroupInfo.ug_name, userGroupInfo.ug_id.ToString()));
                extgroupids.Items.Add(new ListItem(userGroupInfo.ug_name, userGroupInfo.ug_id.ToString()));
            }
            templateid.AddTableData(Templates.GetValidTemplateList(), "tp_name", "tp_id");
            templateid.Items[0].Text = "默认";
            TabControl1.InitTabPage();
            if (SASRequest.GetString("uid") == "")
            {
                Response.Redirect("global_usergrid.aspx");
                return;
            }
            LoadCurrentUserInfo(SASRequest.GetInt("uid", -1));
            ////LoadScoreInf(SASRequest.GetString("uid"), SASRequest.GetString("fieldname"));
        }

        #endregion
    }
}
