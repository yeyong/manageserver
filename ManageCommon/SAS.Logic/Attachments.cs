using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Plugin.Preview;

namespace SAS.Logic
{
    /// <summary>
    /// 附件操作类
    /// </summary>
    public class Attachments
    {
        private const string ATTACH_TIP_IMAGE = "<span style=\"position: absolute; display: none;\" onmouseover=\"showMenu(this.id, 0, 1)\" id=\"attach_{0}\"><img border=\"0\" src=\"{1}images/attachicons/attachimg.gif\" /></span>";
        private const string IMAGE_ATTACH = "{0}<img src=\"{1}\" onload=\"{8}attachimg(this, 'load');\" onmouseover=\"attachimginfo(this, 'attach_{2}', 1);attachimg(this, 'mouseover')\" onclick=\"zoom(this);\" onmouseout=\"attachimginfo(this, 'attach_{2}', 0, event)\" {7} /><div id=\"attach_{2}_menu\" style=\"display: none;\" class=\"t_attach\"><img border=\"0\" alt=\"\" class=\"absmiddle\" src=\"{3}images/attachicons/image.gif\" /><a target=\"_blank\" href=\"{1}\"><strong>{4}</strong></a>({5})<br/><div class=\"t_smallfont\">{6}</div></div>";
        private const string PAID_ATTACH = "{0}<strong>收费附件:{1}</strong>";

        /// <summary>
        /// 将系统设置的附件类型以DataTable的方式存入缓存
        /// </summary>
        /// <returns>系统设置的附件类型</returns>
        public static DataTable GetAttachmentType()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/SAS/ForumSetting/AttachmentType") as DataTable;
            if (dt == null)
            {
                dt = SAS.Data.DataProvider.Attachments.GetAttachmentType();

                cache.AddObject("/SAS/ForumSetting/AttachmentType", dt);
            }
            return dt;
        }

        /// <summary>
        /// 获得系统允许的附件类型和大小, 格式为: 扩展名,最大允许大小\r\n扩展名,最大允许大小\r\n.......
        /// </summary>
        /// <returns></returns>
        public static string GetAttachmentTypeArray(string filterexpression)
        {
            DataTable dt = GetAttachmentType();
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Select(filterexpression))
            {
                sb.Append(dr["extension"]);
                sb.Append(",");
                sb.Append(dr["maxsize"]);
                sb.Append("\r\n");
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 获得当前设置允许的附件类型
        /// </summary>
        /// <returns>逗号间隔的附件类型字符串</returns>
        public static string GetAttachmentTypeString(string filterexpression)
        {
            DataTable dt = GetAttachmentType();
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Select(filterexpression))
            {
                if (!Utils.StrIsNullOrEmpty(sb.ToString()))
                    sb.Append(",");

                sb.Append(dr["extension"]);
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 得到用户可以上传的文件类型
        /// </summary>
        /// <param name="usergroupinfo">当前用户所属用户组信息</param>
        /// <param name="forum">所在版块</param>
        /// <returns></returns>
        public static string GetAllowAttachmentType(UserGroupInfo usergroupinfo, ForumInfo forum)
        {
            //得到用户可以上传的文件类型
            StringBuilder sbAttachmentTypeSelect = new StringBuilder();
            if (!usergroupinfo.ug_attachextensions.Trim().Equals(""))
            {
                sbAttachmentTypeSelect.Append("[id] in (");
                sbAttachmentTypeSelect.Append(usergroupinfo.ug_attachextensions);
                sbAttachmentTypeSelect.Append(")");
            }

            if (!forum.Attachextensions.Equals(""))
            {
                if (sbAttachmentTypeSelect.Length > 0)
                    sbAttachmentTypeSelect.Append(" AND ");

                sbAttachmentTypeSelect.Append("[id] in (");
                sbAttachmentTypeSelect.Append(forum.Attachextensions);
                sbAttachmentTypeSelect.Append(")");
            }
            return sbAttachmentTypeSelect.ToString();
        }

        /// <summary>
        /// 获得上传附件文件的大小
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUploadFileSizeByuserid(int uid)
        {
            return SAS.Data.DataProvider.Attachments.GetUploadFileSizeByUserId(uid);
        }

        /// <summary>
        /// 获取指定用户未使用的附件的JSON字符串(posttopic模版页调用)
        /// </summary>
        /// <param name="userid">指定用户id</param>
        /// <returns>JSON字符串</returns>
        public static string GetNoUsedAttachmentJson(int userid)
        {
            return SAS.Data.DataProvider.Attachments.GetNoUsedAttachmentJson(userid);
        }

    }
}
