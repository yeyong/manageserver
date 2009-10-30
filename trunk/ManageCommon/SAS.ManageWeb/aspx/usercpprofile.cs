using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

using SAS.Common;
using SAS.Logic;
using SAS.Web.UI;
using SAS.Entity;
using SAS.Config;

namespace SAS.ManageWeb
{
    /// <summary>
    /// 更新用户档案页面
    /// </summary>
    public class usercpprofile : UserCpPage
    {
        public string avatarFlashParam = "";
        public string avatarImage = "";

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (!IsLogin()) return;

            if (SASRequest.IsPost())
            {
                if (LogicUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                ValidateInfo();

                if (IsErr()) return;

                if (page_err == 0)
                {
                    UserInfo userInfo = new UserInfo();
                    userInfo.Ps_id = userid;
                    userInfo.Ps_name = Utils.HtmlEncode(LogicUtils.BanWordFilter(SASRequest.GetString("nickname")));
                    userInfo.Ps_gender = SASRequest.GetInt("gender", 0);
                    userInfo.Pd_name = SASRequest.GetString("realname");
                    userInfo.pd_idcard = SASRequest.GetString("idcard");
                    userInfo.Pd_mobile = SASRequest.GetString("mobile");
                    userInfo.Pd_phone = SASRequest.GetString("phone");
                    userInfo.Ps_email = SASRequest.GetString("email").Trim().ToLower();
                    userInfo.Pd_birthday = Utils.HtmlEncode(SASRequest.GetString("bday"));
                    userInfo.Ps_isEmail = SASRequest.GetInt("showemail", 1);

                    if (SASRequest.GetString("website").IndexOf(".") > -1 && !SASRequest.GetString("website").ToLower().StartsWith("http"))
                        userInfo.Pd_website = Utils.HtmlEncode("http://" + SASRequest.GetString("website"));
                    else
                        userInfo.Pd_website = Utils.HtmlEncode(SASRequest.GetString("website"));

                    //userInfo.Icq = Utils.HtmlEncode(SASRequest.GetString("icq"));
                    userInfo.Pd_QQ = Utils.HtmlEncode(SASRequest.GetString("qq"));
                    userInfo.Pd_Yahoo = Utils.HtmlEncode(SASRequest.GetString("yahoo"));
                    userInfo.Pd_MSN = Utils.HtmlEncode(SASRequest.GetString("msn"));
                    userInfo.Pd_Skype = Utils.HtmlEncode(SASRequest.GetString("skype"));
                    userInfo.Pd_address_1 = Utils.HtmlEncode(SASRequest.GetString("location"));
                    userInfo.pd_bio = Utils.HtmlEncode(LogicUtils.BanWordFilter(SASRequest.GetString("bio")));

                    PostpramsInfo postPramsInfo = new PostpramsInfo();
                    postPramsInfo.Usergroupid = usergroupid;
                    postPramsInfo.Attachimgpost = config.Attachimgpost;
                    postPramsInfo.Showattachmentpath = config.Showattachmentpath;
                    postPramsInfo.Hide = 0;
                    postPramsInfo.Price = 0;
                    //获取提交的内容并进行脏字和Html处理
                    postPramsInfo.Sdetail = Utils.HtmlEncode(LogicUtils.BanWordFilter(SASRequest.GetString("signature"))); ;
                    postPramsInfo.Smileyoff = 1;
                    postPramsInfo.Bbcodeoff = 1 - usergroupinfo.Allowsigbbcode;
                    postPramsInfo.Parseurloff = 1;
                    postPramsInfo.Showimages = usergroupinfo.Allowsigimgcode;
                    postPramsInfo.Allowhtml = 0;
                    postPramsInfo.Signature = 1;
                    postPramsInfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
                    postPramsInfo.Customeditorbuttoninfo = null;
                    postPramsInfo.Smiliesmax = config.Smiliesmax;
                    postPramsInfo.Signature = 1;

                    userInfo.Sightml = UBB.UBBToHTML(postPramsInfo);
                    if (userInfo.Sightml.Length >= 1000)
                    {
                        AddErrLine("您的签名转换后超出系统最大长度， 请返回修改");
                        return;
                    }

                    userInfo.Signature = postPramsInfo.Sdetail;
                    userInfo.Sigstatus = SASRequest.GetInt("sigstatus", 0) != 0 ? 1 : 0;

                    Users.UpdateUserProfile(userInfo);
                    OnlineUsers.DeleteUserByUid(userid);    //删除在线表中的信息，使之重建该用户在线表信息
                    SetUrl("usercpprofile.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(true);
                    AddMsgLine("修改个人档案完毕");
                }
            }
            else
            {
                UserInfo userInfo = Users.GetUserInfo(userid);//olid
                avatarFlashParam = Utils.GetRootUrl(BaseConfigs.GetForumPath) + "images/common/camera.swf?nt=1&inajax=1&appid=" +
                    Utils.MD5(userInfo.Username + userInfo.Password + userInfo.Uid + olid) + "&input=" +
                    DES.Encode(userid + "," + olid, config.Passwordkey) + "&ucapi=" + Utils.UrlEncode(Utils.GetRootUrl(BaseConfigs.GetForumPath) +
                    "tools/ajax.aspx");
                avatarImage = Avatars.GetAvatarUrl(userid);
            }
        }

        /// <summary>
        /// 验证提交信息
        /// </summary>
        public void ValidateInfo()
        {
            //实名验证
            if (config.Realnamesystem == 1)
            {
                if (SASRequest.GetString("realname").Trim() == "")
                    AddErrLine("真实姓名不能为空");
                else if (SASRequest.GetString("realname").Trim().Length > 10)
                    AddErrLine("真实姓名不能大于10个字符");

                if (SASRequest.GetString("idcard").Trim() == "")
                    AddErrLine("身份证号码不能为空");
                else if (SASRequest.GetString("idcard").Trim().Length > 20)
                    AddErrLine("身份证号码不能大于20个字符");

                if (SASRequest.GetString("mobile").Trim() == "" && SASRequest.GetString("phone").Trim() == "")
                    AddErrLine("移动电话号码和是固定电话号码必须填写其中一项");

                if (SASRequest.GetString("mobile").Trim().Length > 20)
                    AddErrLine("移动电话号码不能大于20个字符");

                if (SASRequest.GetString("phone").Trim().Length > 20)
                    AddErrLine("固定电话号码不能大于20个字符");
            }

            if (SASRequest.GetString("idcard").Trim() != "" && !Regex.IsMatch(SASRequest.GetString("idcard").Trim(), @"^[\x20-\x80]+$"))
                AddErrLine("身份证号码中含有非法字符");

            if (SASRequest.GetString("mobile").Trim() != "" && !Regex.IsMatch(SASRequest.GetString("mobile").Trim(), @"^[\d|-]+$"))
                AddErrLine("移动电话号码中含有非法字符");

            if (SASRequest.GetString("phone").Trim() != "" && !Regex.IsMatch(SASRequest.GetString("phone").Trim(), @"^[\d|-]+$"))
                AddErrLine("固定电话号码中含有非法字符");

            string email = SASRequest.GetString("email").Trim().ToLower();
            if (Utils.StrIsNullOrEmpty(email))
            {
                AddErrLine("Email不能为空");
                return;
            }
            else if (!Utils.IsValidEmail(email))
            {
                AddErrLine("Email格式不正确");
                return;
            }
            else
            {
                if (!Users.ValidateEmail(email, userid))
                {
                    AddErrLine("Email: \"" + email + "\" 已经被其它用户注册使用");
                    return;
                }
                // 允许名单规则优先于禁止名单规则
                if (!Utils.StrIsNullOrEmpty(config.Accessemail) && !Utils.InArray(Utils.GetEmailHostName(email), config.Accessemail.Replace("\r\n", "\n"), "\n")) // 如果email后缀 不属于 允许名单
                {
                    AddErrLine("Email: \"" + email + "\" 不在本论坛允许范围之类, 本论坛只允许用户使用这些Email地址注册: " + config.Accessemail.Replace("\n", ",&nbsp;"));
                    return;
                }
                else if (!Utils.StrIsNullOrEmpty(config.Censoremail) && Utils.InArray(Utils.GetEmailHostName(email), config.Censoremail.Replace("\r\n", "\n"), "\n")) // 如果email后缀 属于 禁止名单
                {
                    AddErrLine("Email: \"" + email + "\" 不允许在本论坛使用, 本论坛不允许用户使用的Email地址包括: " + config.Censoremail.Replace("\n", ",&nbsp;"));
                    return;
                }
                if (SASRequest.GetString("bio").Length > 500) //如果自我介绍超过500...
                {
                    AddErrLine("自我介绍不得超过500个字符");
                    return;
                }
                if (SASRequest.GetString("signature").Length > 500) //如果签名超过500...
                {
                    AddErrLine("签名不得超过500个字符");
                    return;
                }
            }
            if (!Utils.StrIsNullOrEmpty(SASRequest.GetString("nickname")) && LogicUtils.IsBanUsername(SASRequest.GetString("nickname"), config.Censoruser))
            {
                AddErrLine("昵称 \"" + SASRequest.GetString("nickname") + "\" 不允许在本论坛使用");
                return;
            }
            if (SASRequest.GetString("signature").Length > usergroupinfo.Maxsigsize)
            {
                AddErrLine(string.Format("您的签名长度超过 {0} 字符的限制，请返回修改。", usergroupinfo.Maxsigsize));
                return;
            }
        }
    }
}
