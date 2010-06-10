using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 评论操作
    /// </summary>
    public class Comments
    {

        /// <summary>
        /// 根据企业ID获取评论数量
        /// </summary>
        public static int GetCommentCountByQyID(int qyid)
        {
            return SAS.Data.DataProvider.Comments.GetCommentCountByQyID(qyid);
        }
    }
}
