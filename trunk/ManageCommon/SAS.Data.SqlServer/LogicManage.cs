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
        #region 板块处理forum操作

        public int InsertForumsInf(ForumInfo forumInfo)
        {
            DbParameter[] parms = {
				DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.SmallInt, 2, forumInfo.Parentid),
				DbHelper.MakeInParam("@layer", (DbType)SqlDbType.Int, 4, forumInfo.Layer),
				DbHelper.MakeInParam("@pathlist", (DbType)SqlDbType.NChar, 3000, Utils.StrIsNullOrEmpty(forumInfo.Pathlist) ? " " : forumInfo.Pathlist),
				DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.NChar, 300, Utils.StrIsNullOrEmpty(forumInfo.Parentidlist) ? " " : forumInfo.Parentidlist),
				DbHelper.MakeInParam("@subforumcount", (DbType)SqlDbType.Int, 4, forumInfo.Subforumcount),
				DbHelper.MakeInParam("@name", (DbType)SqlDbType.NChar, 50, forumInfo.Name),
				DbHelper.MakeInParam("@status", (DbType)SqlDbType.Int, 4, forumInfo.Status),
                ////DbHelper.MakeInParam("@colcount", (DbType)SqlDbType.SmallInt, 4, forumInfo.Colcount),
				DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, forumInfo.Displayorder),
				DbHelper.MakeInParam("@templateid", (DbType)SqlDbType.SmallInt, 2, forumInfo.Templateid),
				DbHelper.MakeInParam("@allowsmilies", (DbType)SqlDbType.Int, 4, forumInfo.Allowsmilies),
				DbHelper.MakeInParam("@allowrss", (DbType)SqlDbType.Int, 6, forumInfo.Allowrss),
				DbHelper.MakeInParam("@allowhtml", (DbType)SqlDbType.Int, 4, forumInfo.Allowhtml),
				DbHelper.MakeInParam("@allowbbcode", (DbType)SqlDbType.Int, 4, forumInfo.Allowbbcode),
				DbHelper.MakeInParam("@allowimgcode", (DbType)SqlDbType.Int, 4, forumInfo.Allowimgcode),
                ////DbHelper.MakeInParam("@allowblog", (DbType)SqlDbType.Int, 4, forumInfo.Allowblog),
                ////DbHelper.MakeInParam("@istrade", (DbType)SqlDbType.Int, 4, forumInfo.Istrade),
				DbHelper.MakeInParam("@alloweditrules", (DbType)SqlDbType.Int, 4, forumInfo.Alloweditrules),
				DbHelper.MakeInParam("@allowthumbnail", (DbType)SqlDbType.Int, 4, forumInfo.Allowthumbnail),
                DbHelper.MakeInParam("@allowtag",(DbType)SqlDbType.Int,4,forumInfo.Allowtag),
				DbHelper.MakeInParam("@recyclebin", (DbType)SqlDbType.Int, 4, forumInfo.Recyclebin),
				DbHelper.MakeInParam("@modnewposts", (DbType)SqlDbType.Int, 4, forumInfo.Modnewposts),
				DbHelper.MakeInParam("@jammer", (DbType)SqlDbType.Int, 4, forumInfo.Jammer),
				DbHelper.MakeInParam("@disablewatermark", (DbType)SqlDbType.Int, 4, forumInfo.Disablewatermark),
				DbHelper.MakeInParam("@inheritedmod", (DbType)SqlDbType.Int, 4, forumInfo.Inheritedmod),
				DbHelper.MakeInParam("@autoclose", (DbType)SqlDbType.SmallInt, 2, forumInfo.Autoclose),                
                ////DbHelper.MakeInParam("@allowpostspecial",(DbType)SqlDbType.Int,4,forumInfo.Allowpostspecial),
                ////DbHelper.MakeInParam("@allowspecialonly",(DbType)SqlDbType.Int,4,forumInfo.Allowspecialonly),
			};
            string commandText = string.Format("INSERT INTO [{0}forums] ([parentid],[layer],[pathlist],[parentidlist],[subforumcount],[name],"
                                                + "[status],[displayorder],[templateid],[allowsmilies],[allowrss],[allowhtml],[allowbbcode],[allowimgcode],"
                                                + "[alloweditrules],[recyclebin],[modnewposts],[jammer],[disablewatermark],[inheritedmod],[autoclose],[allowthumbnail],"
                                                + "[allowtag]) VALUES (@parentid,@layer,@pathlist,@parentidlist,@subforumcount,@name,@status, @displayorder,"
                                                + "@templateid,@allowsmilies,@allowrss,@allowhtml,@allowbbcode,@allowimgcode,@alloweditrules,@recyclebin,"
                                                + "@modnewposts,@jammer,@disablewatermark,@inheritedmod,@autoclose,@allowthumbnail,@allowtag)",
                                                BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);

            int fid = GetMaxForumId();

            DbParameter[] prams1 = {
				DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, fid),
				DbHelper.MakeInParam("@description", (DbType)SqlDbType.NText, 0, forumInfo.Description),
				DbHelper.MakeInParam("@password", (DbType)SqlDbType.VarChar, 16, forumInfo.Password),
				DbHelper.MakeInParam("@icon", (DbType)SqlDbType.VarChar, 255, forumInfo.Icon),
                ////DbHelper.MakeInParam("@postcredits", (DbType)SqlDbType.VarChar, 255, forumInfo.Postcredits),
                ////DbHelper.MakeInParam("@replycredits", (DbType)SqlDbType.VarChar, 255, forumInfo.Replycredits),
				DbHelper.MakeInParam("@redirect", (DbType)SqlDbType.VarChar, 255, forumInfo.Redirect),
				DbHelper.MakeInParam("@attachextensions", (DbType)SqlDbType.VarChar, 255, forumInfo.Attachextensions),
				DbHelper.MakeInParam("@moderators", (DbType)SqlDbType.Text, 0, forumInfo.Moderators),
				DbHelper.MakeInParam("@rules", (DbType)SqlDbType.NText, 0, forumInfo.Rules),
				DbHelper.MakeInParam("@topictypes", (DbType)SqlDbType.Text, 0, forumInfo.Topictypes),
				DbHelper.MakeInParam("@viewperm", (DbType)SqlDbType.Text, 0, forumInfo.Viewperm),
				DbHelper.MakeInParam("@postperm", (DbType)SqlDbType.Text, 0, forumInfo.Postperm),
				DbHelper.MakeInParam("@replyperm", (DbType)SqlDbType.Text, 0, forumInfo.Replyperm),
				DbHelper.MakeInParam("@getattachperm", (DbType)SqlDbType.Text, 0, forumInfo.Getattachperm),
				DbHelper.MakeInParam("@postattachperm", (DbType)SqlDbType.Text, 0, forumInfo.Postattachperm),
				DbHelper.MakeInParam("@seokeywords", (DbType)SqlDbType.NVarChar, 500, forumInfo.Seokeywords),
                DbHelper.MakeInParam("@seodescription", (DbType)SqlDbType.NVarChar, 500, forumInfo.Seodescription),
                DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.NVarChar, 20, forumInfo.Rewritename)
			};
            commandText = string.Format("INSERT INTO [{0}forumfields] ([fid],[description],[password],[icon],[redirect],"
                                      + "[attachextensions],[moderators],[rules],[topictypes],[viewperm],[postperm],[replyperm],[getattachperm],[postattachperm],[seokeywords],[seodescription],[rewritename]) "
                                      + "VALUES (@fid,@description,@password,@icon,@redirect,@attachextensions,@moderators,@rules,@topictypes,@viewperm,"
                                      + "@postperm,@replyperm,@getattachperm,@postattachperm,@seokeywords,@seodescription,@rewritename)",
                                      BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteDataset(CommandType.Text, commandText, prams1);
            return fid;
        }

        public int GetMaxForumId()
        {
            string commandText = string.Format("SELECT ISNULL(MAX(fid), 0) FROM [{0}forums]", BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 更新版块和用户模板Id
        /// </summary>
        /// <param name="templateIdList">模板Id列表</param>
        public void UpdateForumAndUserTemplateId(string templateIdList)
        {
            string commandText = string.Format("UPDATE [{0}forums] SET [templateid]=0 WHERE [templateid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);

            commandText = string.Format("UPDATE [{0}personInfo] SET [ps_tempID]=0 WHERE [ps_tempID] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得全部版块列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetForumsTable()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}forums] AS [f] LEFT JOIN [{1}forumfields] AS [ff] ON [f].[fid]=[ff].[fid] ORDER BY [f].[displayorder]",
                                                DbFields.FORUMS_JOIN_FIELDS,
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得子版块列表
        /// </summary>
        /// <param name="fid">版块id</param>
        /// <returns></returns>
        public DataTable GetSubForumTable(int fid)
        {
            DbParameter[] parms = {
									   DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int,4, fid)
								   };
            return DbHelper.ExecuteDataset(CommandType.Text, GetSubForumSql(), parms).Tables[0];
        }

        private static string GetSubForumSql()
        {
            return string.Format("SELECT CASE WHEN DATEDIFF(n, [lastpost], GETDATE())<600 THEN 'new' ELSE 'old' END AS [havenew],[{0}forums].*, [{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] WHERE [parentid] = @fid AND [status] > 0 ORDER BY [displayorder]",
                                  BaseConfigs.GetTablePrefix);
        }

        /// <summary>
        /// 保存板块信息
        /// </summary>
        /// <param name="forumInfo">版块信息</param>
        public void SaveForumsInfo(ForumInfo forumInfo)
        {
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    DbParameter[] parms = {
                        DbHelper.MakeInParam("@parentid", (DbType)SqlDbType.SmallInt, 2, forumInfo.Parentid),
				        DbHelper.MakeInParam("@layer", (DbType)SqlDbType.Int, 4, forumInfo.Layer),
				        DbHelper.MakeInParam("@pathlist", (DbType)SqlDbType.NChar, 3000, Utils.StrIsNullOrEmpty(forumInfo.Pathlist) ? " " : forumInfo.Pathlist),
				        DbHelper.MakeInParam("@parentidlist", (DbType)SqlDbType.NChar, 300, Utils.StrIsNullOrEmpty(forumInfo.Parentidlist) ? " " : forumInfo.Parentidlist),
				        DbHelper.MakeInParam("@subforumcount", (DbType)SqlDbType.Int, 4, forumInfo.Subforumcount),
						DbHelper.MakeInParam("@name", (DbType)SqlDbType.NChar, 50, forumInfo.Name),
						DbHelper.MakeInParam("@status", (DbType)SqlDbType.Int, 4, forumInfo.Status),
						//DbHelper.MakeInParam("@colcount", (DbType)SqlDbType.SmallInt, 4, forumInfo.Colcount),
						DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, forumInfo.Displayorder),
						DbHelper.MakeInParam("@templateid", (DbType)SqlDbType.SmallInt, 2, forumInfo.Templateid),
						DbHelper.MakeInParam("@topics", (DbType)SqlDbType.Int, 4, forumInfo.Topics),
                        DbHelper.MakeInParam("@curtopics", (DbType)SqlDbType.Int, 4, forumInfo.CurrentTopics),
                        DbHelper.MakeInParam("@posts", (DbType)SqlDbType.Int, 4, forumInfo.Posts),
                        DbHelper.MakeInParam("@todayposts", (DbType)SqlDbType.Int, 4, forumInfo.Todayposts),
                        DbHelper.MakeInParam("@lasttid", (DbType)SqlDbType.Int, 4, forumInfo.Lasttid),
                        DbHelper.MakeInParam("@lasttitle", (DbType)SqlDbType.NChar, 60, forumInfo.Lasttitle),
                        DbHelper.MakeInParam("@lastpost", (DbType)SqlDbType.DateTime, 8, forumInfo.Lastpost),
                        DbHelper.MakeInParam("@lastposterid", (DbType)SqlDbType.Int, 4, forumInfo.Lastposterid),
                        DbHelper.MakeInParam("@lastposter", (DbType)SqlDbType.NChar, 20, forumInfo.Lastposter),
                        DbHelper.MakeInParam("@allowsmilies", (DbType)SqlDbType.Int, 4, forumInfo.Allowsmilies),
						DbHelper.MakeInParam("@allowrss", (DbType)SqlDbType.Int, 6, forumInfo.Allowrss),
						DbHelper.MakeInParam("@allowhtml", (DbType)SqlDbType.Int, 4, forumInfo.Allowhtml),
						DbHelper.MakeInParam("@allowbbcode", (DbType)SqlDbType.Int, 4, forumInfo.Allowbbcode),
						DbHelper.MakeInParam("@allowimgcode", (DbType)SqlDbType.Int, 4, forumInfo.Allowimgcode),
                        //DbHelper.MakeInParam("@allowblog", (DbType)SqlDbType.Int, 4, forumInfo.Allowblog),
                        //DbHelper.MakeInParam("@istrade", (DbType)SqlDbType.Int, 4, forumInfo.Istrade),
                        //DbHelper.MakeInParam("@allowpostspecial",(DbType)SqlDbType.Int,4,forumInfo.Allowpostspecial),
                        //DbHelper.MakeInParam("@allowspecialonly",(DbType)SqlDbType.Int,4,forumInfo.Allowspecialonly),
						DbHelper.MakeInParam("@alloweditrules", (DbType)SqlDbType.Int, 4, forumInfo.Alloweditrules),
						DbHelper.MakeInParam("@allowthumbnail", (DbType)SqlDbType.Int, 4, forumInfo.Allowthumbnail),
                        DbHelper.MakeInParam("@allowtag",(DbType)SqlDbType.Int,4,forumInfo.Allowtag),
						DbHelper.MakeInParam("@recyclebin", (DbType)SqlDbType.Int, 4, forumInfo.Recyclebin),
						DbHelper.MakeInParam("@modnewposts", (DbType)SqlDbType.Int, 4, forumInfo.Modnewposts),
						DbHelper.MakeInParam("@jammer", (DbType)SqlDbType.Int, 4, forumInfo.Jammer),
						DbHelper.MakeInParam("@disablewatermark", (DbType)SqlDbType.Int, 4, forumInfo.Disablewatermark),
						DbHelper.MakeInParam("@inheritedmod", (DbType)SqlDbType.Int, 4, forumInfo.Inheritedmod),
						DbHelper.MakeInParam("@autoclose", (DbType)SqlDbType.SmallInt, 2, forumInfo.Autoclose),
						DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, forumInfo.Fid)
					};
                    string commandText = string.Format("UPDATE [{0}forums] SET [parentid]=@parentid, [layer]=@layer, [pathlist]=@pathlist, "
                                                        + "[parentidlist]=@parentidlist, [subforumcount]=@subforumcount, [name]=@name, [status]=@status,"
                                                        + "[displayorder]=@displayorder,[templateid]=@templateid,[topics]=@topics,"
                                                        + "[curtopics]=@curtopics,[posts]=@posts,[todayposts]=@todayposts,[lasttid]=@lasttid,[lasttitle]=@lasttitle,"
                                                        + "[lastpost]=@lastpost,[lastposterid]=@lastposterid,[lastposter]=@lastposter,"
                                                        + "[allowsmilies]=@allowsmilies ,[allowrss]=@allowrss, [allowhtml]=@allowhtml, [allowbbcode]=@allowbbcode, [allowimgcode]=@allowimgcode, "
                                                        //+ "[allowblog]=@allowblog,[istrade]=@istrade,[allowpostspecial]=@allowpostspecial,[allowspecialonly]=@allowspecialonly,"
                                                        + "[alloweditrules]=@alloweditrules ,[allowthumbnail]=@allowthumbnail ,[allowtag]=@allowtag,"
                                                        + "[recyclebin]=@recyclebin, [modnewposts]=@modnewposts,[jammer]=@jammer,[disablewatermark]=@disablewatermark,[inheritedmod]=@inheritedmod,"
                                                        + "[autoclose]=@autoclose WHERE [fid]=@fid",
                                                        BaseConfigs.GetTablePrefix);
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, commandText, parms);

                    DbParameter[] prams1 = {
						DbHelper.MakeInParam("@password", (DbType)SqlDbType.NVarChar, 16, forumInfo.Password),
						DbHelper.MakeInParam("@icon", (DbType)SqlDbType.VarChar, 255, forumInfo.Icon),
                        //DbHelper.MakeInParam("@postcredits", (DbType)SqlDbType.VarChar, 255, forumInfo.Postcredits),
                        //DbHelper.MakeInParam("@replycredits", (DbType)SqlDbType.VarChar, 255, forumInfo.Replycredits),
						DbHelper.MakeInParam("@redirect", (DbType)SqlDbType.VarChar, 255, forumInfo.Redirect),
						DbHelper.MakeInParam("@attachextensions", (DbType)SqlDbType.VarChar, 255, forumInfo.Attachextensions),
						DbHelper.MakeInParam("@rules", (DbType)SqlDbType.NText, 0, forumInfo.Rules),
						DbHelper.MakeInParam("@topictypes", (DbType)SqlDbType.Text, 0, forumInfo.Topictypes),
						DbHelper.MakeInParam("@viewperm", (DbType)SqlDbType.Text, 0, forumInfo.Viewperm),
						DbHelper.MakeInParam("@postperm", (DbType)SqlDbType.Text, 0, forumInfo.Postperm),
						DbHelper.MakeInParam("@replyperm", (DbType)SqlDbType.Text, 0, forumInfo.Replyperm),
						DbHelper.MakeInParam("@getattachperm", (DbType)SqlDbType.Text, 0, forumInfo.Getattachperm),
						DbHelper.MakeInParam("@postattachperm", (DbType)SqlDbType.Text, 0, forumInfo.Postattachperm),
                        DbHelper.MakeInParam("@moderators", (DbType)SqlDbType.Text, 0, forumInfo.Moderators),
						DbHelper.MakeInParam("@description", (DbType)SqlDbType.NText, 0, forumInfo.Description),
                        DbHelper.MakeInParam("@applytopictype", (DbType)SqlDbType.TinyInt, 1, forumInfo.Applytopictype),
						DbHelper.MakeInParam("@postbytopictype", (DbType)SqlDbType.TinyInt, 1, forumInfo.Postbytopictype),
						DbHelper.MakeInParam("@viewbytopictype", (DbType)SqlDbType.TinyInt, 1, forumInfo.Viewbytopictype),
						DbHelper.MakeInParam("@topictypeprefix", (DbType)SqlDbType.TinyInt, 1, forumInfo.Topictypeprefix),
                        DbHelper.MakeInParam("@permuserlist", (DbType)SqlDbType.NText, 0, forumInfo.Permuserlist),
						DbHelper.MakeInParam("@seokeywords", (DbType)SqlDbType.NVarChar, 500, forumInfo.Seokeywords),
                        DbHelper.MakeInParam("@seodescription", (DbType)SqlDbType.NVarChar, 500, forumInfo.Seodescription),
                        DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.NVarChar, 20, forumInfo.Rewritename),
						DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, forumInfo.Fid)
					};
                    commandText = string.Format("UPDATE [{0}forumfields] SET [password]=@password,[icon]=@icon,"
                                                + "[redirect]=@redirect,[attachextensions]=@attachextensions,[rules]=@rules,[topictypes]=@topictypes,"
                                                + "[viewperm]=@viewperm,[postperm]=@postperm,[replyperm]=@replyperm,[getattachperm]=@getattachperm,[postattachperm]=@postattachperm,"
                                                + "[moderators]=@moderators,[description]=@description,[applytopictype]=@applytopictype,[postbytopictype]=@postbytopictype,"
                                                + "[viewbytopictype]=@viewbytopictype,[topictypeprefix]=@topictypeprefix,[permuserlist]=@permuserlist,[seokeywords]=@seokeywords,"
                                                + "[seodescription]=@seodescription,[rewritename]=@rewritename WHERE [fid]=@fid",
                                                BaseConfigs.GetTablePrefix);

                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, commandText, prams1);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            conn.Close();
        }

        /// <summary>
        /// 更新子版块数
        /// </summary>
        /// <param name="fid"></param>
        public void UpdateSubForumCount(int fid)
        {
            string commandText = string.Format("UPDATE [{0}forums] SET [subforumcount]=[subforumcount]+1  WHERE [fid]={1}",
                                                BaseConfigs.GetTablePrefix,
                                                fid);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新子版块数量
        /// </summary>
        /// <param name="subForumCount">子板块数</param>
        /// <param name="fid">板块ID</param>
        public void UpdateSubForumCount(int subForumCount, int fid)
        {
            DbParameter[] parms =
			{
                DbHelper.MakeInParam("@subforumcount", (DbType)SqlDbType.Int, 4, subForumCount),
                DbHelper.MakeInParam("@fid", (DbType)SqlDbType.Int, 4, fid)
			};
            string commandText = string.Format("UPDATE [{0}forums] SET [subforumcount]=@subforumcount WHERE [fid]=@fid", BaseConfigs.GetTablePrefix);
            DbHelper.ExecuteDataset(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 移动版块位置
        /// </summary>
        /// <param name="currentFid">当前板块ID</param>
        /// <param name="targetFid"></param>
        /// <param name="isAsChildNode"></param>
        /// <param name="extName"></param>
        public void MovingForumsPos(string currentFid, string targetFid, bool isAsChildNode, string extName)
        {
            SqlConnection conn = new SqlConnection(DbHelper.ConnectionString);
            conn.Open();

            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //取得当前论坛版块的信息
                    DataRow dr = DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("SELECT TOP 1 {0} FROM [{1}forums] WHERE [fid] = {2}", DbFields.FORUMS, BaseConfigs.GetTablePrefix, currentFid)).Tables[0].Rows[0];

                    //取得目标论坛版块的信息
                    DataRow targetdr = DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("SELECT TOP 1 {0}  FROM [{1}forums] WHERE [fid]={2}", DbFields.FORUMS, BaseConfigs.GetTablePrefix, targetFid)).Tables[0].Rows[0];

                    //当前论坛版块带子版块时
                    if (DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT TOP 1 FID FROM [{0}forums] WHERE [parentid]={1}", BaseConfigs.GetTablePrefix, currentFid)).Tables[0].Rows.Count > 0)
                    {
                        #region

                        string commandText = "";
                        if (isAsChildNode) //作为论坛子版块插入
                        {
                            //让位于当前论坛版块(分类)显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            commandText = string.Format("UPDATE [{0}forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={1}", BaseConfigs.GetTablePrefix, TypeConverter.ObjectToInt(targetdr["displayorder"]) + 1);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);

                            //更新当前论坛版块的相关信息
                            commandText = string.Format("UPDATE [{0}forums] SET [parentid]='{1}',[layer]={2},[pathlist]='{3}'+[pathlist],[parentidlist]=(CASE WHEN [parentidlist]='0' THEN '{4}' ELSE '{4},'+[parentidlist] END),[displayorder]='{5}' WHERE [fid]={6}",
                                BaseConfigs.GetTablePrefix, targetdr["fid"].ToString(), TypeConverter.ObjectToInt(targetdr["layer"]) + 1,
                                targetdr["pathlist"].ToString().Trim(), (targetdr["parentidlist"].ToString().Trim() == "0" ? "" : targetdr["parentidlist"].ToString().Trim() + ",") + targetdr["fid"],
                                TypeConverter.ObjectToInt(targetdr["displayorder"]) + 1, currentFid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);
                            DataRow afterUpdatedr = DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("SELECT TOP 1 {0} FROM [{1}forums] WHERE [fid] = {2}", DbFields.FORUMS, BaseConfigs.GetTablePrefix, currentFid)).Tables[0].Rows[0];
                            commandText = string.Format("UPDATE [{0}forums] SET [layer]={1},[pathlist]='{2}'+[pathlist],[parentidlist]='{3},'+[parentidlist] WHERE [parentid]={4}",
                                BaseConfigs.GetTablePrefix, TypeConverter.ObjectToInt(afterUpdatedr["layer"]) + 1,
                                afterUpdatedr["pathlist"].ToString().Trim(), afterUpdatedr["parentidlist"].ToString().Trim(), currentFid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);
                        }
                        else //作为同级论坛版块,在目标论坛版块之前插入
                        {
                            //让位于包括当前论坛版块显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            commandText = string.Format("UPDATE [{0}forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={1} OR [fid]={2}", BaseConfigs.GetTablePrefix, targetdr["displayorder"], targetdr["fid"]);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);

                            //更新当前论坛版块的相关信息
                            commandText = string.Format("UPDATE [{0}forums] SET [parentid]='{1}',[layer]={2},[parentidlist]={3},[displayorder]='{4}' WHERE [fid]={5}",
                                BaseConfigs.GetTablePrefix, targetdr["parentid"], targetdr["layer"], targetdr["parentidlist"], targetdr["displayorder"], currentFid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);
                            DataRow afterUpdatedr = DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("SELECT TOP 1 {0} FROM [{1}forums] WHERE [fid] = {2}", DbFields.FORUMS, BaseConfigs.GetTablePrefix, currentFid)).Tables[0].Rows[0];
                            commandText = string.Format("UPDATE [{0}forums] SET [layer]={1},[parentidlist]='{2}' WHERE [parentid]={3}",
                                BaseConfigs.GetTablePrefix, TypeConverter.ObjectToInt(afterUpdatedr["layer"]) + 1, (afterUpdatedr["parentidlist"].ToString().Trim() == "0" ? "" : afterUpdatedr["parentidlist"].ToString().Trim() + ",") + afterUpdatedr["fid"], currentFid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);
                        }

                        //更新由于上述操作所影响的版块数和帖子数
                        if (dr["topics"].ToString() != "0" && TypeConverter.ObjectToInt(dr["topics"]) > 0 && dr["posts"].ToString() != "0" && TypeConverter.ObjectToInt(dr["posts"]) > 0)
                        {
                            if (!Utils.StrIsNullOrEmpty(dr["parentidlist"].ToString()))
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [topics]=[topics]-{1},[posts]=[posts]-{2}  WHERE [fid] IN ({3})", BaseConfigs.GetTablePrefix, dr["topics"], dr["posts"], dr["parentidlist"].ToString().Trim()));

                            if (!Utils.StrIsNullOrEmpty(targetdr["parentidlist"].ToString()))
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [topics]=[topics]+{1},[posts]=[posts]+{2}  WHERE [fid] IN ({3})", BaseConfigs.GetTablePrefix, dr["topics"], dr["posts"], targetdr["parentidlist"].ToString().Trim()));
                        }

                        #endregion
                    }
                    else //当前论坛版块不带子版
                    {
                        #region

                        //设置旧的父一级的子论坛数
                        DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [subforumcount]=[subforumcount]-1 WHERE [fid]={1}", BaseConfigs.GetTablePrefix, dr["parentid"]));

                        //让位于当前节点显示顺序之后的节点全部减1 [起到删除节点的效果]
                        if (isAsChildNode) //作为子论坛版块插入
                        {
                            //更新相应的被影响的版块数和帖子数
                            if (dr["topics"].ToString() != "0" && TypeConverter.ObjectToInt(dr["topics"].ToString()) > 0 && dr["posts"].ToString() != "0" && TypeConverter.ObjectToInt(dr["posts"]) > 0)
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [topics]=[topics]-{1},[posts]=[posts]-{2} WHERE [fid] IN ({3})", BaseConfigs.GetTablePrefix, dr["topics"], dr["posts"], dr["parentidlist"]));
                                if (targetdr["parentidlist"].ToString().Trim() != "")
                                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [topics]=[topics]+{1},[posts]=[posts]+{2} WHERE [fid] IN ({3},{4})", BaseConfigs.GetTablePrefix, dr["topics"], dr["posts"], targetdr["parentidlist"], targetFid));
                            }

                            //让位于当前论坛版块显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            string commandText = string.Format(string.Format("UPDATE [{0}forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={1}", BaseConfigs.GetTablePrefix, TypeConverter.ObjectToInt(targetdr["displayorder"]) + 1));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);

                            //设置新的父一级的子论坛数
                            DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [subforumcount]=[subforumcount]+1 WHERE [fid]={1}", BaseConfigs.GetTablePrefix, targetFid));

                            string parentidlist = null;
                            if (targetdr["parentidlist"].ToString().Trim() == "0")
                                parentidlist = targetFid;
                            else
                                parentidlist = targetdr["parentidlist"].ToString().Trim() + "," + targetFid;

                            //更新当前论坛版块的相关信息
                            commandText = string.Format("UPDATE [{0}forums] SET [parentid]='{1}',[layer]='{2}',[pathlist]='{3}', [parentidlist]='{4}',[displayorder]='{5}' WHERE [fid]={6}",
                                                      BaseConfigs.GetTablePrefix,
                                                      targetdr["fid"].ToString(),
                                                      TypeConverter.ObjectToInt(targetdr["layer"]) + 1,
                                                      targetdr["pathlist"].ToString().Trim() + "<a href=\"showforum-" + currentFid + extName + "\">" + dr["name"].ToString().Trim().Replace("'", "''") + "</a>",
                                                      parentidlist,
                                                      TypeConverter.ObjectToInt(targetdr["displayorder"]) + 1,
                                                      currentFid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);

                        }
                        else //作为同级论坛版块,在目标论坛版块之前插入
                        {
                            //更新相应的被影响的版块数和帖子数
                            if (dr["topics"].ToString() != "0" && TypeConverter.ObjectToInt(dr["topics"]) > 0 && dr["posts"].ToString() != "0" && TypeConverter.ObjectToInt(dr["posts"]) > 0)
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [topics]=[topics]-{1},[posts]=[posts]-{2}  WHERE [fid] IN ({3})", BaseConfigs.GetTablePrefix, dr["topics"], dr["posts"], dr["parentidlist"]));
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format("UPDATE [{0}forums] SET [topics]=[topics]+{1},[posts]=[posts]+{2}  WHERE [fid] IN ({3})", BaseConfigs.GetTablePrefix, dr["topics"], dr["posts"], targetdr["parentidlist"]));
                            }

                            //让位于包括当前论坛版块显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            string commandText = string.Format("UPDATE [{0}forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={1} OR [fid]={2}",
                                                                BaseConfigs.GetTablePrefix,
                                                                TypeConverter.ObjectToInt(targetdr["displayorder"]) + 1,
                                                                targetdr["fid"]);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);

                            //设置新的父一级的子论坛数
                            DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("UPDATE [{0}forums]  SET [subforumcount]=[subforumcount]+1 WHERE [fid]={1}", BaseConfigs.GetTablePrefix, targetdr["parentid"]));
                            string parentpathlist = "";
                            DataTable dt = DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("SELECT TOP 1 [pathlist] FROM [{0}forums] WHERE [fid]={1}", BaseConfigs.GetTablePrefix, targetdr["parentid"])).Tables[0];
                            if (dt.Rows.Count > 0)
                                parentpathlist = DbHelper.ExecuteDataset(trans, CommandType.Text, string.Format("SELECT TOP 1 [pathlist] FROM [{0}forums] WHERE [fid]={1}", BaseConfigs.GetTablePrefix, targetdr["parentid"])).Tables[0].Rows[0][0].ToString().Trim();

                            //更新当前论坛版块的相关信息
                            commandText = string.Format("UPDATE [{0}forums]  SET [parentid]='{1}',[layer]='{2}',[pathlist]='{3}', [parentidlist]='{4}',[displayorder]='{5}' WHERE [fid]={6}",
                                                      BaseConfigs.GetTablePrefix,
                                                      targetdr["parentid"],
                                                      targetdr["layer"],
                                                      parentpathlist + "<a href=\"showforum-" + currentFid + extName + "\">" + dr["name"].ToString().Trim() + "</a>",
                                                      targetdr["parentidlist"].ToString().Trim(),
                                                      TypeConverter.ObjectToInt(targetdr["displayorder"]),
                                                      currentFid);
                            DbHelper.ExecuteDataset(trans, CommandType.Text, commandText);
                        }

                        #endregion
                    }
                    trans.Commit();
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                conn.Close();
            }
        }

        /// <summary>
        /// 更新版块显示顺序
        /// </summary>
        /// <param name="minDisplayOrder"></param>
        public void UpdateForumsDisplayOrder(int minDisplayOrder)
        {
            string commandText = string.Format("UPDATE [{0}forums] SET [displayorder]=[displayorder]+1  WHERE [displayorder]>{1}",
                                                BaseConfigs.GetTablePrefix,
                                                minDisplayOrder);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 检查rewritename是否存在或非法
        /// </summary>
        /// <param name="rewriteName"></param>
        /// <returns>如果存在或者非法的Rewritename则返回true,否则为false</returns>
        public bool CheckForumRewriteNameExists(string rewriteName)
        {
            DbParameter[] parms = { 
                                       DbHelper.MakeInParam("@rewritename", (DbType)SqlDbType.NVarChar, 20, rewriteName) 
                                  };
            string commandText = string.Format("SELECT COUNT(1) FROM [{0}forumfields] WHERE [rewritename]=@rewritename",
                                                BaseConfigs.GetTablePrefix);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms)) >= 1;
        }
        #endregion

        #region 专题分类处理TopicTypes

        /// <summary>
        /// 获取指定关键字的主题类型
        /// </summary>
        /// <param name="searthKeyWord"></param>
        /// <returns></returns>
        public DataTable GetTopicTypes(string searthKeyWord)
        {
            string commandText = string.Format("SELECT [typeid] AS id,[name],[displayorder],[description] FROM [{0}topictypes] {1} {2}",
                                        BaseConfigs.GetTablePrefix,
                                        !Utils.StrIsNullOrEmpty(searthKeyWord) ? " WHERE [name] LIKE '%" + searthKeyWord + "%' " : "",
                                        "ORDER BY [displayorder] ASC");
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }
        #endregion

        #region 友情链接frendlink操作
        /// <summary>
        /// 添加友情链接
        /// </summary>
        /// <param name="displayOrder">显示顺序</param>
        /// <param name="name">名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="note">备注</param>
        /// <param name="logo">Logo地址</param>
        /// <returns></returns>
        public int AddSASLink(int displayOrder, string name, string url, string note, string logo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 100, name),
                                        DbHelper.MakeInParam("@url", (DbType)SqlDbType.NVarChar, 100, url),
                                        DbHelper.MakeInParam("@note", (DbType)SqlDbType.NVarChar, 200, note),
                                        DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NVarChar, 100, logo)
                                    };
            string commandText = string.Format("INSERT INTO [{0}frendlink] ([displayorder], [name],[linkurl],[note],[logo]) VALUES (@displayorder,@name,@url,@note,@logo)",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 获得所有友情链接
        /// </summary>
        /// <returns></returns>
        public DataTable GetSASLinks()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}frendlink]", DbFields.FRIENDLINK, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(commandText).Tables[0];
        }

        /// <summary>
        /// 删除指定友情链接
        /// </summary>
        /// <param name="forumlinkid"></param>
        /// <returns></returns>
        public int DeleteSASLink(string forumLinkIdList)
        {
            string commandText = string.Format("DELETE FROM [{0}frendlink] WHERE [id] IN ({1})", BaseConfigs.GetTablePrefix, forumLinkIdList);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// 更新指定友情链接
        /// </summary>
        /// <param name="id">友情链接Id</param>
        /// <param name="displayOrder">显示顺序</param>
        /// <param name="name">名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="note">备注</param>
        /// <param name="logo">Logo地址</param>
        /// <returns></returns>
        public int UpdateSASLink(int id, int displayOrder, string name, string url, string note, string logo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id", (DbType)SqlDbType.Int, 4, id),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, displayOrder),
                                        DbHelper.MakeInParam("@name", (DbType)SqlDbType.NVarChar, 100, name),
                                        DbHelper.MakeInParam("@url", (DbType)SqlDbType.NVarChar, 100, url),
                                        DbHelper.MakeInParam("@note", (DbType)SqlDbType.NVarChar, 200, note),
                                        DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NVarChar, 100, logo)
                                    };
            string commandText = string.Format("UPDATE [{0}frendlink] SET [displayorder]=@displayorder,[name]=@name,[linkurl]=@url,[note]=@note,[logo]=@logo WHERE [id]=@id",
                                                BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }
        /// <summary>
        /// 获得所有友情链接
        /// </summary>
        public IDataReader GetAllLinks()
        {
            string commandText = string.Format("SELECT {0} FROM [{1}frendlink]", DbFields.FRIENDLINK, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text,commandText);
        }
        #endregion
    }
}
