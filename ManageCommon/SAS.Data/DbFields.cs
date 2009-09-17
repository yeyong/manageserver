using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Data
{
    public class DbFields
    {
        /// <summary>
        /// 在线用户表字段
        /// </summary>
        public const string ONLINE = "[ol_id],[ol_ps_id],[ol_ip],[ol_name],[ol_nickName],[ol_password],[ol_ug_id],[ol_img],[ol_pg_id],[ol_invisible],[ol_action],[ol_lastactivity],[ol_lastpostpmtime],[ol_lastsearchtime],[ol_lastupdatetime],[ol_pm_id],[ol_pm_name],[ol_verifycode],[ol_newpms],[ol_newnotices]";

        /// <summary>
        /// 统计信息表字段
        /// </summary>
        public const string STATISTICS = "[st_usercount],[st_lastuser],[st_lastuserid],[st_highestonlineusercount],[st_highestonlineusertime],[st_productcount],[st_productreleasecount],[st_adcount],[st_newscount]";

        /// <summary>
        /// 用户组信息表字段
        /// </summary>
        public const string USER_GROUPS = "[ug_id],[ug_name],[ug_scorehight],[ug_scorelow],[ug_logo],[ug_readaccess],[ug_allowvisit],[ug_allowcommunity],[ug_allowdown],[ug_allowup],[ug_allowsearch],[ug_allowavatar],[ug_allowshop],[ug_allowinvisible],[ug_maxattachsize],[ug_maxsizeperday],[ug_attachextensions],[ug_maxspaceattachsize],[ug_maxspacephotosize],[ug_pg_id],[ug_color],[ug_isSystem]";

        /// <summary>
        /// 用户信息b表字段
        /// </summary>
        public const string USERS = "[ps_id],[ps_en_id],[ps_name],[ps_nickName],[ps_gender],[ps_password],[ps_pay_pass],[ps_init],[ps_company],[ps_question],[ps_answer],[ps_isLock],[ps_createDate],[ps_lastLogin],[ps_lastChangePass],[ps_lockDate],[ps_email],[ps_prev_email],[ps_regIP],[ps_loginIP],[ps_star],[ps_credits],[ps_scores],[ps_pg_id],[ps_ug_id],[ps_tempID],[ps_isEmail],[ps_bdSound],[ps_status],[ps_onlinetime],[ps_isDetail],[ps_isCreater],[ps_creater],[ps_lastactivity],[ps_secques],[ps_pageviews],[ps_issign],[ps_newsletter],[ps_invisible],[ps_newMess],[ps_newpm],[ps_salt]";

       
    }
}
