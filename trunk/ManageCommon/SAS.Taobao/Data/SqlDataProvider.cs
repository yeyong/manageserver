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
                                    DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar,50, cinfo.Name),
		                            DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int,4, cinfo.Parentid),
		                            DbHelper.MakeInParam("@parentlist", (DbType)SqlDbType.NVarChar,50, cinfo.Parentlist),
		                            DbHelper.MakeInParam("@cg_img", (DbType)SqlDbType.VarChar,50, cinfo.Cg_img),
		                            DbHelper.MakeInParam("@sort", (DbType)SqlDbType.Int,4, cinfo.Sort),
		                            DbHelper.MakeInParam("@cg_prefix", (DbType)SqlDbType.VarChar,50, cinfo.Cg_prefix),
		                            DbHelper.MakeInParam("@cg_status", (DbType)SqlDbType.Int,4, cinfo.Cg_status),
		                            DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int,4, cinfo.Displayorder),
		                            DbHelper.MakeInParam("@haschild", (DbType)SqlDbType.Bit,1, cinfo.Haschild),
		                            DbHelper.MakeInParam("@cg_relatetype", (DbType)SqlDbType.Text,0, cinfo.Cg_relatetype),
		                            DbHelper.MakeInParam("@cg_relateclass", (DbType)SqlDbType.Text,0, cinfo.Cg_relateclass),
		                            DbHelper.MakeInParam("@cg_relatebrand", (DbType)SqlDbType.Text,0, cinfo.Cg_relatebrand),
		                            DbHelper.MakeInParam("@cg_desc", (DbType)SqlDbType.Text,0, cinfo.Cg_desc),
		                            DbHelper.MakeInParam("@cg_keyword", (DbType)SqlDbType.VarChar,200, cinfo.Cg_keyword),
		                            DbHelper.MakeInParam("@goodcount", (DbType)SqlDbType.Int,4, cinfo.Goodcount)
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
        /// 获取全部有效的商品类别信息
        /// </summary>
        public IDataReader GetVaildCategoryList()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}category] WHERE [cg_status] > 0 ORDER BY [displayorder]", DbFields.CATEGORY, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        public void UpdateCategoryInfo(CategoryInfo cinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@cid",(DbType)SqlDbType.Int,4,cinfo.Cid),
                                    DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar,50, cinfo.Name),
		                            DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.Int,4, cinfo.Parentid),
		                            DbHelper.MakeInParam("@parentlist", (DbType)SqlDbType.NVarChar,50, cinfo.Parentlist),
		                            DbHelper.MakeInParam("@cg_img", (DbType)SqlDbType.VarChar,50, cinfo.Cg_img),
		                            DbHelper.MakeInParam("@sort", (DbType)SqlDbType.Int,4, cinfo.Sort),
		                            DbHelper.MakeInParam("@cg_prefix", (DbType)SqlDbType.VarChar,50, cinfo.Cg_prefix),
		                            DbHelper.MakeInParam("@cg_status", (DbType)SqlDbType.Int,4, cinfo.Cg_status),
		                            DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int,4, cinfo.Displayorder),
		                            DbHelper.MakeInParam("@haschild", (DbType)SqlDbType.Bit,1, cinfo.Haschild),
		                            DbHelper.MakeInParam("@cg_relatetype", (DbType)SqlDbType.Text,0, cinfo.Cg_relatetype),
		                            DbHelper.MakeInParam("@cg_relateclass", (DbType)SqlDbType.Text,0, cinfo.Cg_relateclass),
		                            DbHelper.MakeInParam("@cg_relatebrand", (DbType)SqlDbType.Text,0, cinfo.Cg_relatebrand),
		                            DbHelper.MakeInParam("@cg_desc", (DbType)SqlDbType.Text,0, cinfo.Cg_desc),
		                            DbHelper.MakeInParam("@cg_keyword", (DbType)SqlDbType.VarChar,200, cinfo.Cg_keyword),
		                            DbHelper.MakeInParam("@goodcount", (DbType)SqlDbType.Int,4, cinfo.Goodcount)
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
                                    DbHelper.MakeInParam("@relatecategory", (DbType)SqlDbType.Int,4, cid),
		                            DbHelper.MakeInParam("@relatechanel", (DbType)SqlDbType.Int,4, chanelid),
		                            DbHelper.MakeInParam("@ctitle", (DbType)SqlDbType.NVarChar,200, rtitle),
		                            DbHelper.MakeInParam("@ccontent", (DbType)SqlDbType.Text,0, rcontent),
		                            DbHelper.MakeInParam("@ctype", (DbType)SqlDbType.Int,4, rtype)
                                  };
            string commandText = String.Format("INSERT INTO [{0}recommend] ([ctitle],[ctype],[relatechanel],[relatecategory],[ccontent]) VALUES (@ctitle,@ctype,@relatechanel,@relatecategory,@ccontent);SELECT SCOPE_IDENTITY() AS cid", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }
        /// <summary>
        /// 获取推荐信息
        /// </summary>
        public IDataReader GetRecommendInfo(int id)
        {
            DbParameter param = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id);
            string commandText = string.Format("SELECT {0} FROM [{1}recommend] WHERE [id]=@id",
                                                DbFields.RECOMMEND,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 获取推荐信息
        /// </summary>
        public DataTable GetRecommendList(string conditions)
        {
            string commandText = "";

            if (string.IsNullOrEmpty(conditions))
            {
                commandText = string.Format("SELECT {0},[{1}category].[name] FROM [{1}recommend] LEFT JOIN [{1}category] ON [relatecategory]=[cid]", DbFields.RECOMMEND, BaseConfigs.GetTablePrefix);
            }
            else
            {
                commandText = string.Format("SELECT {0},[{1}category].[name] FROM [{1}recommend] LEFT JOIN [{1}category] ON [relatecategory]=[cid] WHERE 1=1 {2}", DbFields.RECOMMEND, BaseConfigs.GetTablePrefix, conditions);
            }
            
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 获取全部推荐信息
        /// </summary>
        public IDataReader GetAllRecommendList(int ctype, int chanel, int classid)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}recommend] WHERE 1=1", DbFields.RECOMMEND, BaseConfigs.GetTablePrefix);

            if (ctype > 0) commandText += " AND [ctype] = " + ctype;
            if (chanel > 0) commandText += " AND [relatechanel] = " + chanel;
            if (classid > 0) commandText += " AND [relatecategory] = " + classid;

            return DbHelper.ExecuteReader(CommandType.Text, commandText);
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
        /// <summary>
        /// 更新推荐信息
        /// </summary>
        /// <param name="id">推荐ID</param>
        /// <param name="cid">相关类别</param>
        /// <param name="chanelid">相关频道</param>
        /// <param name="rtitle">推荐标题</param>
        /// <param name="rcontent">推荐内容</param>
        /// <param name="rtype">推荐类型（默认1，商品推荐；2，店铺推荐；3，活动推荐；4，店铺推荐）</param>
        public void UpdateRecommendInfo(int id, int cid, int chanelid, string rtitle, string rcontent, int rtype)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int,4, id),
                                    DbHelper.MakeInParam("@relatecategory", (DbType)SqlDbType.Int,4, cid),
		                            DbHelper.MakeInParam("@relatechanel", (DbType)SqlDbType.Int,4, chanelid),
		                            DbHelper.MakeInParam("@ctitle", (DbType)SqlDbType.NVarChar,200, rtitle),
		                            DbHelper.MakeInParam("@ccontent", (DbType)SqlDbType.Text,0, rcontent),
		                            DbHelper.MakeInParam("@ctype", (DbType)SqlDbType.Int,4, rtype)
                                  };
            string commandText = String.Format("Update [{0}recommend] SET [relatecategory] = @relatecategory,[relatechanel] = @relatechanel,[ctitle] = @ctitle,[ccontent] = @ccontent,[ctype] = @ctype WHERE [id]=@id ", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        #region 店铺收集
        /// <summary>
        /// 店铺信息收集
        /// </summary>
        /// <returns>返回1，更新店铺信息；返回2，增加店铺信息</returns>
        public int CollectionTaobaoShops(ShopDetailInfo sinfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("sid", (DbType)SqlDbType.BigInt,8,sinfo.sid),
				                    DbHelper.MakeInParam("user_id", (DbType)SqlDbType.BigInt,8,sinfo.user_id),
				                    DbHelper.MakeInParam("cid", (DbType)SqlDbType.BigInt,8,sinfo.cid),
				                    DbHelper.MakeInParam("nick", (DbType)SqlDbType.NVarChar,200,sinfo.nick),
				                    DbHelper.MakeInParam("title", (DbType)SqlDbType.NVarChar,500,sinfo.title),
				                    DbHelper.MakeInParam("item_score", (DbType)SqlDbType.NChar,10,sinfo.item_score),
				                    DbHelper.MakeInParam("service_score", (DbType)SqlDbType.NChar,10,sinfo.service_score),
				                    DbHelper.MakeInParam("delivery_score", (DbType)SqlDbType.NChar,10,sinfo.delivery_score),
				                    DbHelper.MakeInParam("shop_desc", (DbType)SqlDbType.Text,0,sinfo.shop_desc),
				                    DbHelper.MakeInParam("bulletin", (DbType)SqlDbType.NVarChar,200,sinfo.bulletin),
				                    DbHelper.MakeInParam("pic_path", (DbType)SqlDbType.VarChar,500,sinfo.pic_path),
				                    DbHelper.MakeInParam("created", (DbType)SqlDbType.DateTime,8,sinfo.created),
				                    DbHelper.MakeInParam("modified", (DbType)SqlDbType.DateTime,8,sinfo.modified),
				                    DbHelper.MakeInParam("promoted_type", (DbType)SqlDbType.VarChar,50,sinfo.promoted_type),
				                    DbHelper.MakeInParam("consumer_protection", (DbType)SqlDbType.Bit,1,sinfo.consumer_protection),
				                    DbHelper.MakeInParam("shop_status", (DbType)SqlDbType.VarChar,50,sinfo.shop_status),
				                    DbHelper.MakeInParam("shop_type", (DbType)SqlDbType.VarChar,20,sinfo.shop_type),
				                    DbHelper.MakeInParam("shop_level", (DbType)SqlDbType.Int,4,sinfo.shop_level),
				                    DbHelper.MakeInParam("@shop_score", (DbType)SqlDbType.Int,4,sinfo.shop_score),
				                    DbHelper.MakeInParam("@total_num", (DbType)SqlDbType.BigInt,8,sinfo.total_num),
				                    DbHelper.MakeInParam("@good_num", (DbType)SqlDbType.BigInt,8,sinfo.good_num),
				                    DbHelper.MakeInParam("@shop_country", (DbType)SqlDbType.NVarChar,50,sinfo.shop_country),
				                    DbHelper.MakeInParam("@shop_province", (DbType)SqlDbType.NVarChar,50,sinfo.shop_province),
				                    DbHelper.MakeInParam("@shop_city", (DbType)SqlDbType.NVarChar,50,sinfo.shop_city),
				                    DbHelper.MakeInParam("@shop_address", (DbType)SqlDbType.NVarChar,200,sinfo.shop_address),
				                    DbHelper.MakeInParam("@commission_rate", (DbType)SqlDbType.VarChar,10,sinfo.commission_rate),
				                    DbHelper.MakeInParam("@click_url", (DbType)SqlDbType.VarChar,500,sinfo.click_url)
                                  };
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.StoredProcedure, string.Format("{0}collectiontaobaoshop", BaseConfigs.GetTablePrefix), parms), 0);
        }
        /// <summary>
        /// 根据条件获取淘宝店铺分页信息
        /// </summary>
        public IDataReader GetTaoBaoShopListPage(string conditions, int pagesize, int pageindex, string ordercolumn, string ordertype)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageindex),
                                    DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,pagesize),
                                    DbHelper.MakeInParam("@ordercolumn",(DbType)SqlDbType.VarChar,20,ordercolumn),
                                    DbHelper.MakeInParam("@ordertype",(DbType)SqlDbType.VarChar,5,ordertype),
                                    DbHelper.MakeInParam("@conditions",(DbType)SqlDbType.NVarChar,2000,conditions)
                                  };
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, string.Format("{0}gettaobaoshoplistbypage", BaseConfigs.GetTablePrefix), parms);
        }
        /// <summary>
        /// 获取店铺搜索条件
        /// </summary>
        /// <param name="shoptitle">店铺标题名称</param>
        /// <param name="shopnick">卖家昵称</param>
        /// <param name="province">所在省份</param>
        /// <param name="city">所在市</param>
        /// <param name="startscore">最小评分</param>
        /// <param name="endstartscore">最大评分</param>
        /// <param name="startcredit">最小信誉值</param>
        /// <param name="endcredit">最大信誉值</param>
        /// <param name="startrate">最小佣金率</param>
        /// <param name="endrate">最大佣金率</param>
        public string GetTaoBaoShopCondition(string shoptitle, string shopnick, string province, string city, int startscore, int endscore, int startcredit, int endcredit, int startrate, int endrate)
        {
            string tableName = string.Format("{0}taobaoshop", BaseConfigs.GetTablePrefix);
            StringBuilder sqlBuilder = new StringBuilder("1=1");

            if (shoptitle != "")
            {
                sqlBuilder.AppendFormat(" AND [{1}].[title] like '%{0}%'", shoptitle, tableName);
            }

            if (shopnick != "")
            {
                sqlBuilder.AppendFormat(" AND [{1}].[nick] like '%{0}%'", shopnick, tableName);
            }

            if (province != "")
            {
                sqlBuilder.AppendFormat(" AND [{1}].[shop_province] like '%{0}%'", province, tableName);
            }

            if (city != "")
            {
                sqlBuilder.AppendFormat(" AND [{1}].[shop_city] like '%{0}%'", city, tableName);
            }

            if (startscore * endscore > 0 && startscore < endscore)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[shop_score] >= {0}", startscore, tableName);
                sqlBuilder.AppendFormat(" AND [{1}].[shop_score] <= {0}", endscore, tableName);
            }

            if (startcredit * endcredit > 0 && startcredit < endcredit)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[shop_score] >= {0}", startcredit, tableName);
                sqlBuilder.AppendFormat(" AND [{1}].[shop_score] <= {0}", endcredit, tableName);
            }

            if (startrate * endrate > 0 && startrate < endrate)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[shop_score] >= {0}", startrate, tableName);
                sqlBuilder.AppendFormat(" AND [{1}].[shop_score] <= {0}", endrate, tableName);
            }

            return sqlBuilder.ToString();
        }
        /// <summary>
        /// 根据条件获取淘宝店铺数量
        /// </summary>
        public int GetTaoBaoShopCount(string conditions)
        {
            string commandText = string.Format("SELECT COUNT(*) FROM [{0}taobaoshop] WHERE {1}", BaseConfigs.GetTablePrefix, conditions);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }
        /// <summary>
        /// 根据店铺ID集合获取店铺信息集合
        /// </summary>
        public IDataReader GetTaoBaoShopList(string ids)
        {
            string commandText = string.Format("SELECT {0} FROM [{1}taobaoshop] WHERE [sid] IN ({2}) ORDER BY [shop_level] DESC", DbFields.SHOPDETAIL, BaseConfigs.GetTablePrefix, ids);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 根据店铺掌柜昵称获取店铺信息
        /// </summary>
        public IDataReader GetTaoBaoShopInfo(string nick)
        {
            string commandText = string.Format("SELECT TOP 1 {0} FROM [{1}taobaoshop] WHERE [nick] = '{2}'", DbFields.SHOPDETAIL, BaseConfigs.GetTablePrefix, nick);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }
        /// <summary>
        /// 店铺商品变动
        /// </summary>
        public void UpdateShopProduct(long sid,string products)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@id",(DbType)SqlDbType.BigInt,8,sid),
			                        DbHelper.MakeInParam("@products", (DbType)SqlDbType.Text,0, products)
                                  };
            string commandText = String.Format("Update [{0}taobaoshop] SET [relategoods] = @products WHERE [sid]=@id ", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        #endregion

        #region 品牌操作
        /// <summary>
        /// 创建品牌
        /// </summary>
        public int CreateGoodsBrandInfo(GoodsBrandInfo ginfo)
        {
            DbParameter[] parms = {
			                        DbHelper.MakeInParam("@bname", (DbType)SqlDbType.NVarChar,100, ginfo.bname),
			                        DbHelper.MakeInParam("@spell", (DbType)SqlDbType.VarChar,100, ginfo.spell),
			                        DbHelper.MakeInParam("@website", (DbType)SqlDbType.VarChar,200, ginfo.website),
			                        DbHelper.MakeInParam("@bcompany", (DbType)SqlDbType.VarChar,200, ginfo.bcompany),
			                        DbHelper.MakeInParam("@order", (DbType)SqlDbType.Int,4, ginfo.order),
			                        DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NVarChar,100, ginfo.logo),
			                        DbHelper.MakeInParam("@img", (DbType)SqlDbType.VarChar,200, ginfo.img),
			                        DbHelper.MakeInParam("@keyword", (DbType)SqlDbType.VarChar,500, ginfo.keyword),
			                        DbHelper.MakeInParam("@shortdesc", (DbType)SqlDbType.VarChar,500, ginfo.shortdesc),
			                        DbHelper.MakeInParam("@detaildesc", (DbType)SqlDbType.Text,0, ginfo.detaildesc),
			                        DbHelper.MakeInParam("@status", (DbType)SqlDbType.Int,4, ginfo.status),
			                        DbHelper.MakeInParam("@relateclass", (DbType)SqlDbType.VarChar,200, ginfo.relateclass)
                                  };
            string commandText = String.Format("INSERT INTO [{0}goodsbrand] ([bname],[spell],[website],[bcompany],[order],[logo],[img],[keyword],[shortdesc],[detaildesc],[status],[relateclass]) VALUES (@bname,@spell,@website,@bcompany,@order,@logo,@img,@keyword,@shortdesc,@detaildesc,@status,@relateclass);SELECT SCOPE_IDENTITY() AS id", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms), -1);
        }
        /// <summary>
        /// 根据条件获取品牌数量
        /// </summary>
        public int GetGoodsBrandCountByCond(string condition)
        {
            string commandText = string.Format("SELECT COUNT(*) FROM [{0}goodsbrand] WHERE {1}", BaseConfigs.GetTablePrefix, condition);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText), 0);
        }
        /// <summary>
        /// 品牌信息分页
        /// </summary>
        public DataTable GetGoodsBrandByPage(string conditions, int pagesize, int pageindex)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@pageindex",(DbType)SqlDbType.Int,4,pageindex),
                                    DbHelper.MakeInParam("@pagesize",(DbType)SqlDbType.Int,4,pagesize),
                                    DbHelper.MakeInParam("@conditions",(DbType)SqlDbType.NVarChar,2000,conditions)
                                  };
            return DbHelper.ExecuteDataset(CommandType.StoredProcedure, string.Format("{0}getgoodsbrandlistbypage", BaseConfigs.GetTablePrefix), parms).Tables[0];
        }
        /// <summary>
        /// 返回品牌查询条件
        /// </summary>
        /// <param name="isspell">是否同时查询别名</param>
        /// <param name="brandname">品牌名称</param>
        /// <param name="relateclass">关联类别</param>
        /// <param name="brandstatus">状态</param>
        public string GetGoodsBrandSearchCondition(bool isspell, string brandname, string relateclass, int brandstatus)
        {
            string tableName = string.Format("{0}goodsbrand", BaseConfigs.GetTablePrefix);
            StringBuilder sqlBuilder = new StringBuilder("1=1");

            if (brandname != "")
            {
                if (!isspell)
                {
                    sqlBuilder.AppendFormat(" AND [{1}].[bname] like '%{0}%'", brandname, tableName);
                }
                else
                {
                    sqlBuilder.AppendFormat(" AND ([{1}].[bname] like '%{0}%' OR [{1}].[spell] like '%{0}%')", brandname, tableName);
                }
            }

            if (relateclass != "" && relateclass != "0")
            {
                sqlBuilder.AppendFormat(" AND [{1}].[relateclass] = '{0}'", relateclass, tableName);
            }

            if (brandstatus > -1)
            {
                sqlBuilder.AppendFormat(" AND [{1}].[status] = {0}", brandstatus, tableName);
            }

            return sqlBuilder.ToString();
        }
        /// <summary>
        /// 更改品牌状态
        /// </summary>
        public void UpdateGoodsBrandStatus(string idlist,int status)
        {
            string commandText = string.Format("UPDATE [{0}goodsbrand] SET [status] = {1} WHERE [id] IN ({2})", BaseConfigs.GetTablePrefix, status, idlist);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }
        /// <summary>
        /// 获取品牌信息
        /// </summary>
        public IDataReader GetGoodsBrandInfo(int id)
        {
            DbParameter param = DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id);
            string commandText = string.Format("SELECT {0} FROM [{1}goodsbrand] WHERE [id]=@id", DbFields.GOODSBRAND, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 根据ID集合获取品牌信息集合
        /// </summary>
        public DataTable GetGoodsBrandListByIds(string ids)
        {
            string commandText = string.Format("SELECT {0},[{1}category].[name] as cname FROM [{1}goodsbrand] LEFT JOIN [{1}category] ON [{1}goodsbrand].[relateclass] = [{1}category].[cid] WHERE [{1}goodsbrand].[id] IN ({2})", DbFields.GOODSBRAND, BaseConfigs.GetTablePrefix, ids);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }
        /// <summary>
        /// 根据类别获取品牌列表信息
        /// </summary>
        public IDataReader GetGoodsBrandListByClass(int classid)
        {
            DbParameter param = DbHelper.MakeInParam("@classid", (DbType)SqlDbType.Int, 4, classid);
            string commandText = string.Format("SELECT {0} FROM [{1}goodsbrand] WHERE [relateclass]=@classid AND [status] > 0 ORDER BY [order]", DbFields.GOODSBRAND, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText, param);
        }
        /// <summary>
        /// 更新品牌信息
        /// </summary>
        public void UpdateGoodsBrand(GoodsBrandInfo ginfo)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@id",(DbType)SqlDbType.Int,4,ginfo.id),
			                        DbHelper.MakeInParam("@bname", (DbType)SqlDbType.NVarChar,100, ginfo.bname),
			                        DbHelper.MakeInParam("@spell", (DbType)SqlDbType.VarChar,100, ginfo.spell),
			                        DbHelper.MakeInParam("@website", (DbType)SqlDbType.VarChar,200, ginfo.website),
			                        DbHelper.MakeInParam("@bcompany", (DbType)SqlDbType.VarChar,200, ginfo.bcompany),
			                        DbHelper.MakeInParam("@order", (DbType)SqlDbType.Int,4, ginfo.order),
			                        DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NVarChar,100, ginfo.logo),
			                        DbHelper.MakeInParam("@img", (DbType)SqlDbType.VarChar,200, ginfo.img),
			                        DbHelper.MakeInParam("@keyword", (DbType)SqlDbType.VarChar,500, ginfo.keyword),
			                        DbHelper.MakeInParam("@shortdesc", (DbType)SqlDbType.VarChar,500, ginfo.shortdesc),
			                        DbHelper.MakeInParam("@detaildesc", (DbType)SqlDbType.Text,0, ginfo.detaildesc),
			                        DbHelper.MakeInParam("@status", (DbType)SqlDbType.Int,4, ginfo.status),
			                        DbHelper.MakeInParam("@relateclass", (DbType)SqlDbType.VarChar,200, ginfo.relateclass)
                                  };
            string commandText = String.Format("Update [{0}goodsbrand] SET [bname] = @bname,[spell] = @spell,[website] = @website,[bcompany] = @bcompany,[order] = @order,[logo] = @logo,[img] = @img,[keyword] = @keyword,[shortdesc] = @shortdesc,[detaildesc] = @detaildesc,[status] = @status,[relateclass] = @relateclass WHERE [id]=@id ", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        #endregion
    }
}
