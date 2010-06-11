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
        /// 新增评论
        /// </summary>
        /// <param name="cif"></param>
        /// <returns></returns>
        public static int CreateCommentInfo(CommentInfo cif)
        {
            return DatabaseProvider.GetInstance().CreateCommentInfo(cif);
        }
        /// <summary>
        /// 根据企业ID获取评论数量
        /// </summary>
        /// <param name="qyid"></param>
        /// <returns></returns>
        public static int GetCommentCountByQyID(int qyid)
        {
            return DatabaseProvider.GetInstance().GetCommentCountByQyID(qyid);
        }
        /// <summary>
        /// 根据企业ID获取企业信息集合
        /// </summary>
        public static DataTable GetCommentListByQyID(int qyid)
        {
            return DatabaseProvider.GetInstance().GetCommentListByQyID(qyid);
        }
        /// <summary>
        /// 根据企业ID获取企业信息集合
        /// </summary>
        public static DataTable GetCommentListPageByQyID(int qyid, int pageSize, int pageIndex)
        {
            return DatabaseProvider.GetInstance().GetCommentListPageByQyID(qyid, pageSize, pageIndex);
        }
    }
}
