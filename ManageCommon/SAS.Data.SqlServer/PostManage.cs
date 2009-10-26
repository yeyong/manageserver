using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;

namespace SAS.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        #region 专题处理topic

        /// <summary>
        /// 更新主题浏览量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewcount">浏览量</param>
        /// <returns>成功返回1，否则返回0</returns>
        public int UpdateTopicViewCount(int tid, int viewCount)
        {
            DbParameter[] parms = {
										DbHelper.MakeInParam("@tid",(DbType)SqlDbType.Int,4,tid),	
										DbHelper.MakeInParam("@viewcount",(DbType)SqlDbType.Int,4,viewCount)			   
									};
            return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, string.Format("{0}updatetopicviewcount", BaseConfigs.GetTablePrefix), parms);
        }

        #endregion

        #region 附件处理attachment

        /// <summary>
        /// 获得系统设置的附件类型
        /// </summary>
        /// <returns>系统设置的附件类型</returns>
        public DataTable GetAttachmentType()
        {
            DataSet ds = DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT [id], [extension], [maxsize] FROM [{0}attachtypes]",
                                                 BaseConfigs.GetTablePrefix));
            return (ds != null) ? ds.Tables[0] : new DataTable();
        }

        /// <summary>
        /// 获得上传附件文件的大小
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int GetUploadFileSizeByUserId(int uid)
        {
            return TypeConverter.ObjectToInt(
                        DbHelper.ExecuteScalar(CommandType.StoredProcedure,
                                               string.Format("{0}gettodayuploadedfilesize", BaseConfigs.GetTablePrefix),
                                               DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)));
        }

        public IDataReader GetNoUsedAttachmentListByTid(int userId)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@uid",(DbType)SqlDbType.Int,4,userId)
                                  };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure,
                                          string.Format("{0}getnousedattachmentlistbytid",
                                          BaseConfigs.GetTablePrefix), parms);
        }

        #endregion
    }
}