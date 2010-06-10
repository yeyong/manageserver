using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 评论数据操作
    /// </summary>
    public class Comments
    {
        /// <summary>
        /// 根据企业ID获取评论数量
        /// </summary>
        /// <param name="qyid"></param>
        /// <returns></returns>
        public static int GetCommentCountByQyID(int qyid)
        {
            return SAS.Data.DatabaseProvider.GetInstance().GetCommentCountByQyID(qyid);
        }
    }
}
