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

    }
}
