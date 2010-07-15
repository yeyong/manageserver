using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;

using SAS.Config;
using SAS.Data;
using SAS.Entity;
using SAS.Common;

namespace SAS.Taobao.Data
{
    public class DataProvider
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 创建商品类别
        /// </summary>
        public int CreateCategoryInfo(CategoryInfo cinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("name", (DbType)SqlDbType.NVarChar,50, cinfo.Name),
		                            DbHelper.MakeInParam("parentid", (DbType)SqlDbType.Int,4, cinfo.Parentid),
		                            DbHelper.MakeInParam("parentlist", (DbType)SqlDbType.NVarChar,50, cinfo.Parentlist),
		                            DbHelper.MakeInParam("cg_img", (DbType)SqlDbType.VarChar,50, cinfo.Cg_img),
		                            DbHelper.MakeInParam("sort", (DbType)SqlDbType.Int,4, cinfo.Sort),
		                            DbHelper.MakeInParam("cg_prefix", (DbType)SqlDbType.VarChar,50, cinfo.Cg_prefix),
		                            DbHelper.MakeInParam("cg_status", (DbType)SqlDbType.Int,4, cinfo.Cg_status),
		                            DbHelper.MakeInParam("displayorder", (DbType)SqlDbType.Int,4, cinfo.Displayorder),
		                            DbHelper.MakeInParam("haschild", (DbType)SqlDbType.Bit,1, cinfo.Haschild),
		                            DbHelper.MakeInParam("cg_relatetype", (DbType)SqlDbType.Text,0, cinfo.Cg_relatetype),
		                            DbHelper.MakeInParam("cg_relateclass", (DbType)SqlDbType.Text,0, cinfo.Cg_relateclass),
		                            DbHelper.MakeInParam("cg_relatebrand", (DbType)SqlDbType.Text,0, cinfo.Cg_relatebrand),
		                            DbHelper.MakeInParam("cg_desc", (DbType)SqlDbType.Text,0, cinfo.Cg_desc),
		                            DbHelper.MakeInParam("cg_keyword", (DbType)SqlDbType.VarChar,200, cinfo.Cg_keyword),
		                            DbHelper.MakeInParam("goodcount", (DbType)SqlDbType.Int,4, cinfo.Goodcount)
                                  };
            string commandText = String.Format("INSERT INTO [{0}category] ([name],[parentid],[parentlist],[cg_img],[sort],[cg_prefix],[cg_status],[displayorder],[haschild],[cg_relatetype],[cg_relateclass],[cg_relatebrand],[cg_desc],[cg_keyword],[goodcount]) VALUES (@name,@parentid,@parentlist,@cg_img,@sort,@cg_prefix,@cg_status,@displayorder,@haschild,@cg_relatetype,@cg_relateclass,@cg_relatebrand,@cg_desc,@cg_keyword,@goodcount);SELECT SCOPE_IDENTITY() AS cid", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }
        /// <summary>
        /// 获取商品类别
        /// </summary>
        public IDataReader GetCategoryInfo(int cid)
        {
            DbParameter param = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, cid);
            string commandText = string.Format("SELECT {0} FROM [{1}category] WHERE [cid]=@id",
                                                DbFields.CATEGORY,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 获取商品类别全部信息
        /// </summary>
        public DataTable GetAllCategoryList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}category]", DbFields.CATEGORY,  BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        public void UpdateCategoryInfo(CategoryInfo cinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("cid",(DbType)SqlDbType.Int,4,cinfo.Cid),
                                    DbHelper.MakeInParam("name", (DbType)SqlDbType.NVarChar,50, cinfo.Name),
		                            DbHelper.MakeInParam("parentid", (DbType)SqlDbType.Int,4, cinfo.Parentid),
		                            DbHelper.MakeInParam("parentlist", (DbType)SqlDbType.NVarChar,50, cinfo.Parentlist),
		                            DbHelper.MakeInParam("cg_img", (DbType)SqlDbType.VarChar,50, cinfo.Cg_img),
		                            DbHelper.MakeInParam("sort", (DbType)SqlDbType.Int,4, cinfo.Sort),
		                            DbHelper.MakeInParam("cg_prefix", (DbType)SqlDbType.VarChar,50, cinfo.Cg_prefix),
		                            DbHelper.MakeInParam("cg_status", (DbType)SqlDbType.Int,4, cinfo.Cg_status),
		                            DbHelper.MakeInParam("displayorder", (DbType)SqlDbType.Int,4, cinfo.Displayorder),
		                            DbHelper.MakeInParam("haschild", (DbType)SqlDbType.Bit,1, cinfo.Haschild),
		                            DbHelper.MakeInParam("cg_relatetype", (DbType)SqlDbType.Text,0, cinfo.Cg_relatetype),
		                            DbHelper.MakeInParam("cg_relateclass", (DbType)SqlDbType.Text,0, cinfo.Cg_relateclass),
		                            DbHelper.MakeInParam("cg_relatebrand", (DbType)SqlDbType.Text,0, cinfo.Cg_relatebrand),
		                            DbHelper.MakeInParam("cg_desc", (DbType)SqlDbType.Text,0, cinfo.Cg_desc),
		                            DbHelper.MakeInParam("cg_keyword", (DbType)SqlDbType.VarChar,200, cinfo.Cg_keyword),
		                            DbHelper.MakeInParam("goodcount", (DbType)SqlDbType.Int,4, cinfo.Goodcount)
                                  };
            string commandText = String.Format("Update [{0}category] SET [name] = @name,[parentid] = @parentid,[parentlist] = @parentlist,[cg_img] = @cg_img,[sort] = @sort,[cg_prefix] = @cg_prefix,[cg_status] = @cg_status,[displayorder] = @displayorder,[haschild] = @haschild,[cg_relatetype] = @cg_relatetype,[cg_relateclass] = @cg_relateclass,[cg_relatebrand] = @cg_relatebrand,[cg_desc] = @cg_desc,[cg_keyword] = @cg_keyword,[goodcount] = @goodcount WHERE [cid]=@cid ", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 创建推荐信息
        /// </summary>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public int CreateRecommendInfo(int cid, int chanelid, string rtitle, string rcontent, int rtype)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("relatecategory", (DbType)SqlDbType.Int,4, cid),
		                            DbHelper.MakeInParam("relatechanel", (DbType)SqlDbType.Int,4, chanelid),
		                            DbHelper.MakeInParam("ctitle", (DbType)SqlDbType.NVarChar,200, rtitle),
		                            DbHelper.MakeInParam("ccontent", (DbType)SqlDbType.Text,0, rcontent),
		                            DbHelper.MakeInParam("ctype", (DbType)SqlDbType.Int,4, rtype)
                                  };
            string commandText = String.Format("INSERT INTO [{0}recommend] ([ctitle],[ctype],[relatechanel],[relatecategory],[ccontent]) VALUES (@ctitle,@ctype,@relatechanel,@relatecategory,@ccontent);SELECT SCOPE_IDENTITY() AS cid", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }
        /// <summary>
        /// 获取推荐信息
        /// </summary>
        public DataTable GetRecommendList(string conditions)
        {
            string commandText = "";

            if (string.IsNullOrEmpty(conditions))
            {
                commandText = string.Format("SELECT {0},[{1}category].[name] FROM [{1}recommend] LEFT JOIN [{1}category] ON [ctype]=[cid]", DbFields.RECOMMEND, BaseConfigs.GetTablePrefix);
            }
            else
            {
                commandText = string.Format("SELECT {0},[{1}category].[name] FROM [{1}recommend] LEFT JOIN [{1}category] ON [ctype]=[cid] WHERE 1=1 {2}", DbFields.RECOMMEND, BaseConfigs.GetTablePrefix, conditions);
            }
            
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 设置推荐搜索条件
        /// </summary>
        public string GetRecommendCondition(bool islike, string rtitle, int rcategory, int rchanel, bool iscreatedate, string startcreate, string endcreate, bool isupdatedate, string startupdate, string endupdate)
        {
            string tableName = string.Format("{0}recommend", BaseConfigs.GetTablePrefix);
            StringBuilder sqlBuilder = new StringBuilder();

            if (islike)
            {
                if (!Utils.StrIsNullOrEmpty(rtitle)) sqlBuilder.AppendFormat(" AND [{1}].[ctitle] like '%{0}%'", RegEsc(rtitle), tableName);
            }
            else
            {
                if (!Utils.StrIsNullOrEmpty(rtitle)) sqlBuilder.AppendFormat(" AND [{1}].[ctitle] = '{0}'", RegEsc(rtitle), tableName);
            }

            if (rcategory > 0)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[relatecategory] = {0}", rcategory, tableName);
            }

            if (rchanel > 0)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[relatechanel] = {0}", rchanel, tableName);
            }

            if (iscreatedate)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[createdatetime] >= '{0}'", DateTime.Parse(startcreate).ToString("yyyy-MM-dd HH:mm:ss"), tableName);
                sqlBuilder.AppendFormat(" AND [{1}].[createdatetime] <= '{0}'", DateTime.Parse(endcreate).ToString("yyyy-MM-dd HH:mm:ss"), tableName);
            }

            if (isupdatedate)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[updatedatetime] >= '{0}'", DateTime.Parse(startupdate).ToString("yyyy-MM-dd HH:mm:ss"), tableName);
                sqlBuilder.AppendFormat(" AND [{1}].[updatedatetime] <= '{0}'", DateTime.Parse(endupdate).ToString("yyyy-MM-dd HH:mm:ss"), tableName);
            }

            return sqlBuilder.ToString();
        }
    }
}
