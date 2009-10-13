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
        public const string USER_GROUPS = "[ug_id],[ug_name],[ug_scorehight],[ug_scorelow],[ug_logo],[ug_readaccess],[ug_allowvisit],[ug_allowcommunity],[ug_allowdown],[ug_allowup],[ug_allowsearch],[ug_allowavatar],[ug_allowshop],[ug_allowinvisible],[ug_maxattachsize],[ug_maxsizeperday],[ug_maxsigsize],[ug_attachextensions],[ug_maxspaceattachsize],[ug_maxspacephotosize],[ug_pg_id],[ug_color],[ug_isSystem]";

        /// <summary>
        /// 用户信息表字段
        /// </summary>
        public const string USERS = "[ps_id],[ps_en_id],[ps_name],[ps_nickName],[ps_gender],[ps_password],[ps_pay_pass],[ps_init],[ps_company],[ps_question],[ps_answer],[ps_isLock],[ps_createDate],[ps_lastLogin],[ps_lastChangePass],[ps_lockDate],[ps_email],[ps_prev_email],[ps_regIP],[ps_loginIP],[ps_star],[ps_credits],[ps_scores],[ps_pg_id],[ps_ug_id],[ps_tempID],[ps_isEmail],[ps_bdSound],[ps_status],[ps_onlinetime],[ps_isDetail],[ps_isCreater],[ps_creater],[ps_lastactivity],[ps_secques],[ps_pageviews],[ps_issign],[ps_newsletter],[ps_invisible],[ps_newMess],[ps_newpm],[ps_salt]";

        /// <summary>
        /// 全用户信息字段
        /// </summary>
        public const string USERS_JOIN_FIELDS = "[u].[ps_id],[u].[ps_en_id],[u].[ps_name],[u].[ps_nickName],[u].[ps_gender],[u].[ps_password],[u].[ps_pay_pass],[u].[ps_init],[u].[ps_company],[u].[ps_question],[u].[ps_answer],[u].[ps_isLock],[u].[ps_createDate],[u].[ps_lastLogin],[u].[ps_lastChangePass],[u].[ps_lockDate],[u].[ps_email],[u].[ps_prev_email],[u].[ps_regIP],[u].[ps_loginIP],[u].[ps_star],[u].[ps_credits],[u].[ps_scores],[u].[ps_pg_id],[u].[ps_ug_id],[u].[ps_tempID],[u].[ps_isEmail],[u].[ps_bdSound],[u].[ps_status],[u].[ps_onlinetime],[u].[ps_isDetail],[u].[ps_isCreater],[u].[ps_creater],[u].[ps_lastactivity],[u].[ps_secques],[u].[ps_pageviews],[u].[ps_issign],[u].[ps_newsletter],[u].[ps_invisible],[u].[ps_newMess],[u].[ps_newpm],[u].[ps_salt],[uf].[pd_name],[uf].[pd_birthday],[uf].[pd_MSN],[uf].[pd_QQ],[uf].[pd_Skype],[uf].[pd_Yahoo],[uf].[pd_sign],[uf].[pd_logo],[uf].[pd_phone],[uf].[pd_mobile],[uf].[pd_website],[uf].[pd_ai_id_1],[uf].[pd_address_1],[uf].[pd_ai_id_2],[uf].[pd_address_2],[uf].[pd_ai_id_3],[uf].[pd_address_3],[uf].[pd_ai_id_temp],[uf].[pd_address_temp],[uf].[pd_authstr],[uf].[pd_authtime],[uf].[pd_authflag],[uf].[pd_idcard],[uf].[pd_bio]";

        /// <summary>
        /// 模板信息表字段
        /// </summary>
        public const string TEMPLATES = "[tp_id],[tp_directory],[tp_name],[tp_author],[tp_createdate],[tp_ver],[tp_fordntver],[tp_copyright]";

        /// <summary>
        /// 短消息信息表字段
        /// </summary>
        public const string PMS = "[pmid],[msgfrom],[msgfromid],[msgto],[msgtoid],[folder],[new],[subject],[postdatetime],[message]";

        /// <summary>
        /// 禁用IP列表字段
        /// </summary>
        public const string BANNED = "[bdid],[ip1],[ip2],[ip3],[ip4],[admin],[dateline],[expiration]";

        /// <summary>
        /// 菜单列表字段
        /// </summary>
        public const string NAVS = "[id],[parentid],[name],[title],[url],[target],[navstype],[available],[displayorder],[lightstyle],[level]";

        /// <summary>
        /// 广告表字段
        /// </summary>
        public const string ADVERTISEMENTS = "[advid],[adavailable],[adtype],[addisplayorder],[title],[targets],[starttime],[endtime],[code],[parameters]";

        /// <summary>
        /// 管理员日志表字段
        /// </summary>
        public const string ADMIN_VISIT_LOG = "[av_id],[av_ps_id],[av_ps_name],[av_ug_id],[av_ug_name],[av_ip],[av_postdatetime],[av_actions],[av_others]";

        /// <summary>
        /// 数据表分表字段
        /// </summary>
        public const string TABLE_LIST = "[id],[createdatetime],[description],[mintid],[maxtid]";

        /// <summary>
        /// 脏词列表
        /// </summary>
        public const string WORDS = "[id],[admin],[find],[replacement]";
       
    }
}
