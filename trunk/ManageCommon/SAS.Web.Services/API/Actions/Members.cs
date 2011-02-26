using System;
using System.Text;
using System.Data;

using SAS.Common;
using SAS.Config;
using SAS.Logic;
using SAS.Entity;
using SAS.Plugin.Sirius;
using SAS.Common.Generic;
using Newtonsoft.Json;

namespace SAS.Web.Services.API.Actions
{
    /// <summary>
    /// 团队成员操作
    /// </summary>
    public class Members : ActionBase
    {
        SiriusPluginBase spb = SiriusPluginProvider.GetInstance();

        public string GetTeamActList()
        {
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return "";
            }

            //如果是桌面程序则需要验证用户身份
            if (this.App.ApplicationType == (int)ApplicationType.DESKTOP)
            {
                if (Uid < 1)
                {
                    ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
                    return "";
                }
            }

            if (CallId <= LastCallId)
            {
                ErrorCode = (int)ErrorType.API_EC_CALLID;
                return "";
            }

            if (!CheckRequiredParams("tid"))
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return "";
            }

            int tid = GetIntParam("tid", 1);
            List<SAS.Entity.TeamActInfo> actlist = spb.GetTeamActByTidWithCache(tid);
            TeamActGetListResponse tglr = new TeamActGetListResponse();
            List<TeamActInfos> alist = new List<TeamActInfos>();

            foreach (TeamActInfo tactinfo in actlist)
            {
                TeamActInfos tsinfo = new TeamActInfos();
                tsinfo.Actid = tactinfo.Id;
                tsinfo.Name = tactinfo.Name;
                tsinfo.Start = tactinfo.Start;
                tsinfo.End = tactinfo.End;
                tsinfo.Listpic = tactinfo.Img;
                tsinfo.Picbak = tactinfo.Imgbak;
                tsinfo.Shortdesc = tactinfo.Shortdesc;
                tsinfo.Teamid = tactinfo.Teamid;
                tsinfo.Piccollect = tactinfo.Piccollect;
                tsinfo.Atype = tactinfo.Atype;
                alist.Add(tsinfo);
            }

            tglr.Actnums = alist.Count;
            tglr.TeamactList = alist.ToArray();

            if (Format == FormatType.JSON)
            {
                return JavaScriptConvert.SerializeObject(tglr);
            }
            return SerializationHelper.Serialize(tglr);
        }

        public string GetMembers()
        {
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return "";
            }

            //如果是桌面程序则需要验证用户身份
            if (this.App.ApplicationType == (int)ApplicationType.DESKTOP)
            {
                if (Uid < 1)
                {
                    ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
                    return "";
                }
            }

            if (CallId <= LastCallId)
            {
                ErrorCode = (int)ErrorType.API_EC_CALLID;
                return "";
            }

            if (!CheckRequiredParams("tid"))
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return "";
            }

            int tid = GetIntParam("tid", 1);

            TeamInfo tinfo = spb.GetTeamByTeamID(tid);
            if (tinfo == null) return "";

            DataTable userlist = Users.GetMemberListByUserName(tinfo.TeamMember);
            MemberGetListResponse mglr = new MemberGetListResponse();
            List<MemberInfo> mlist = new List<MemberInfo>();

            foreach (DataRow dr in userlist.Rows)
            {
                MemberInfo minfo = new MemberInfo();
                minfo.Memberid = TypeConverter.StrToInt(dr["ps_id"].ToString(), 0);
                minfo.Name = dr["ps_name"].ToString();
                minfo.Nickname = dr["ps_nickName"].ToString();
                minfo.Job = dr["ps_company"].ToString();
                minfo.Level = TypeConverter.StrToInt(dr["ps_scores"].ToString(), 0);
                minfo.Light = TypeConverter.StrToInt(dr["tm_light"].ToString(), 0);
                minfo.QQ = dr["pd_QQ"].ToString();
                minfo.MSN = dr["pd_MSN"].ToString();
                minfo.EMail = dr["ps_email"].ToString();
                minfo.Home = dr["pd_website"].ToString();
                minfo.Figure = dr["tm_figure"].ToString();
                minfo.TeamAge = TypeConverter.StrToInt(dr["tm_teamage"].ToString(), 1);
                minfo.Birthday = dr["pd_birthday"].ToString();
                minfo.Location = dr["tm_location"].ToString();
                minfo.Gender = dr["ps_gender"].ToString();
                minfo.Sx = dr["tm_sx"].ToString();
                minfo.Xz = dr["tm_constellation"].ToString();
                minfo.Bio = dr["pd_bio"].ToString();
                minfo.Edu = dr["tm_education"].ToString();
                minfo.Profession = dr["tm_professional"].ToString();
                minfo.Specially = dr["tm_specialty"].ToString();
                minfo.Hobby = dr["tm_hobby"].ToString();
                minfo.Img = dr["tm_image"].ToString();
                minfo.Imgbak = dr["tm_imgbak"].ToString();
                minfo.Imglist = dr["tm_smallimg"].ToString();
                minfo.Sign = dr["tm_sign"].ToString();
                minfo.Selfdesc = dr["tm_selfdesc"].ToString();
                minfo.Selfenjoy = dr["tm_selfenjoy"].ToString();
                mlist.Add(minfo);
            }

            mglr.Mnums = mlist.Count;
            mglr.MemberList = mlist.ToArray();

            if (Format == FormatType.JSON)
            {
                return JavaScriptConvert.SerializeObject(mglr);
            }
            return SerializationHelper.Serialize(mglr);
        }
    }
}
