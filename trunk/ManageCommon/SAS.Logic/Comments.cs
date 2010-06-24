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
        /// 新增评论
        /// </summary>
        /// <param name="cif"></param>
        /// <returns></returns>
        public static int CreateComment(CommentInfo cif)
        {
            return SAS.Data.DataProvider.Comments.CreateCommentInfo(cif);
        }

        /// <summary>
        /// 批量删除评论
        /// </summary>
        /// <param name="commidlist"></param>
        public static int DelComments(string commidlist)
        {
            return SAS.Data.DataProvider.Comments.DelComments(commidlist);
        }

        /// <summary>
        /// 根据企业ID获取评论数量
        /// </summary>
        public static int GetCommentCountByQyID(int qyid)
        {
            return SAS.Data.DataProvider.Comments.GetCommentCountByQyID(qyid);
        }

        /// <summary>
        /// 根据企业ID获取企业信息集合
        /// </summary>
        /// <param name="qyid"></param>
        /// <returns></returns>
        public static DataTable GetCommentListByQyID(int qyid)
        {
            return SAS.Data.DataProvider.Comments.GetCommentListByQyID(qyid);
        }

        /// <summary>
        /// 根据企业ID获取企业信息集合
        /// </summary>
        public static DataTable GetCommentListByQyID(int qyid, int pageSize, int pageIndex)
        {
            return SAS.Data.DataProvider.Comments.GetCommentListPageByQyID(qyid, pageSize, pageIndex);
        }

        /// <summary>
        /// 获取json数据
        /// </summary>
        /// <param name="qyid"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public static StringBuilder GetCommentListJosn(int qyid, int pagesize, int pageindex)
        {
            return Utils.DataTableToJSON(GetCommentListByQyID(qyid, pagesize, pageindex));
        }

        /// <summary>
        /// 获取企业评分
        /// </summary>
        /// <param name="qyid"></param>
        /// <returns></returns>
        public static float GetCommentScored(int qyid)
        {
            return SAS.Data.DataProvider.Comments.GetCommentScored(qyid);
        }
        /// <summary>
        /// 获取企业评分Josn数据
        /// </summary>
        public static StringBuilder GetCommentScoredJosn(int qyid)
        {
            StringBuilder socredjosn = new StringBuilder();
            socredjosn.Append("[");
            socredjosn.Append(string.Format("{0}", GetCommentScored(qyid)));
            socredjosn.Append("]");
            return socredjosn;
        }
    }
}
