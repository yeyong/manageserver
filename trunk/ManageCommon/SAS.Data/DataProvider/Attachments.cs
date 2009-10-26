using System;
using System.Text;
using System.Data;
using System.IO;

using SAS.Entity;
using SAS.Config;
using SAS.Common;
using SAS.Common.Generic;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 附件数据操作类
    /// </summary>
    public class Attachments
    {
        /// <summary>
        /// 将系统设置的附件类型以DataTable的方式存入缓存
        /// </summary>
        /// <returns>系统设置的附件类型</returns>
        public static DataTable GetAttachmentType()
        {
            return DatabaseProvider.GetInstance().GetAttachmentType();
        }

        /// <summary>
        /// 获得上传附件文件的大小
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUploadFileSizeByUserId(int uid)
        {
            return DatabaseProvider.GetInstance().GetUploadFileSizeByUserId(uid);
        }

        ///// <summary>
        ///// 获取指定用户未使用的附件的JSON字符串
        ///// </summary>
        ///// <param name="userid">指定用户id</param>
        ///// <returns>JSON字符串</returns>
        public static string GetNoUsedAttachmentJson(int userid)
        {
            StringBuilder attachmentStringBuilder = new StringBuilder();
            attachmentStringBuilder.Append("[");
            IDataReader reader = DatabaseProvider.GetInstance().GetNoUsedAttachmentListByTid(userid);

            if (reader != null)
            {
                while (reader.Read())
                {
                    if (!Utils.StrIsNullOrEmpty(reader["aid"].ToString()))
                    {
                        attachmentStringBuilder.Append(string.Format("{{'aid' : {0}, 'attachment' : '{1}'}},",
                            reader["aid"].ToString().Trim(),
                            reader["attachment"].ToString().Trim()
                           ));
                    }
                }
                reader.Close();
            }
            if (attachmentStringBuilder.ToString().EndsWith(","))
            {
                attachmentStringBuilder.Remove(attachmentStringBuilder.Length - 1, 1);
            }
            attachmentStringBuilder.Append("]");

            return attachmentStringBuilder.ToString();
        }
    }
}
