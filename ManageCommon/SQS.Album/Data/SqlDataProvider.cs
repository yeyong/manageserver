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

namespace SAS.Album.Data
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

        private DbParameter[] GetDateSpanParms(string startdate, string enddate)
        {
            DbParameter[] parms = new DbParameter[2];
            if (startdate != "")
            {
                parms[0] = DbHelper.MakeInParam("@startdate", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(startdate));
            }
            if (enddate != "")
            {
                parms[1] = DbHelper.MakeInParam("@enddate", (DbType)SqlDbType.DateTime, 8, DateTime.Parse(enddate).AddDays(1));
            }
            return parms;
        }

        #region	相册 操作类

        public int AddSpacePhoto(PhotoInfo photoinfo)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4,photoinfo.Userid),
                    DbHelper.MakeInParam("@username", (DbType)SqlDbType.NChar, 20, photoinfo.Username),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 20,photoinfo.Title),
					DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4,photoinfo.Albumid),
					DbHelper.MakeInParam("@filename", (DbType)SqlDbType.NVarChar, 255,photoinfo.Filename),
					DbHelper.MakeInParam("@attachment", (DbType)SqlDbType.NVarChar, 255,photoinfo.Attachment),
					DbHelper.MakeInParam("@filesize", (DbType)SqlDbType.Int, 4,photoinfo.Filesize),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NVarChar, 200,photoinfo.Description),
                    DbHelper.MakeInParam("@isattachment",(DbType)SqlDbType.Int,4,photoinfo.IsAttachment),
                    DbHelper.MakeInParam("@commentstatus", (DbType)SqlDbType.TinyInt, 1, (byte)photoinfo.Commentstatus),
                    DbHelper.MakeInParam("@tagstatus", (DbType)SqlDbType.TinyInt, 1, (byte)photoinfo.Tagstatus)
				};
            string commandText = String.Format("INSERT INTO [{0}photos] ([userid], [username], [title], [albumid], [filename], [attachment], [filesize], [description],[isattachment],[commentstatus], [tagstatus]) VALUES ( @userid, @username, @title, @albumid, @filename, @attachment, @filesize, @description,@isattachment, @commentstatus, @tagstatus);SELECT SCOPE_IDENTITY()", BaseConfigs.GetTablePrefix);
            //向关联表中插入相关数据
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parms));
        }

        public void AddAlbumCategory(AlbumCategoryInfo aci)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 50, aci.Title),
                                        DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 300, aci.Description),
                                        DbHelper.MakeInParam("@displayorder", (DbType)SqlDbType.Int, 4, aci.Displayorder)
                                };

            string commandText = string.Format(@"INSERT INTO [{0}albumcategories]([title], [description], [albumcount], [displayorder]) VALUES(@title, @description, 0, @displayorder)", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);
        }

        public IDataReader GetSpaceAlbumById(int albumId)
        {
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [albumid]=" + albumId);
        }

        public int GetSpacePhotoCountByAlbumId(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(1) FROM [{0}photos] WHERE [albumid]=@albumid", BaseConfigs.GetTablePrefix), parm), 0);
        }

        public DataTable GetPhotosByAlbumid(int albumid)
        {
            DbParameter parm = DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, albumid);
            return DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT [photoid], [userid], [username], [title], [filename] FROM [{0}photos] WHERE [albumid]=@albumid", BaseConfigs.GetTablePrefix), parm).Tables[0];
        }

        public SAS.Common.Generic.List<AlbumCategoryInfo> GetAlbumCategory()
        {
            string commandText = string.Format("SELECT * FROM [{0}albumcategories] ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);

            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, commandText);
            SAS.Common.Generic.List<AlbumCategoryInfo> acic = new SAS.Common.Generic.List<AlbumCategoryInfo>();
            while (reader.Read())
            {
                AlbumCategoryInfo aci = new AlbumCategoryInfo();
                aci.Albumcateid = TypeConverter.ObjectToInt(reader["albumcateid"], 0);
                aci.Albumcount = TypeConverter.ObjectToInt(reader["albumcount"], 0);
                aci.Description = reader["description"].ToString();
                aci.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"], 0);
                aci.Title = reader["title"].ToString();
                acic.Add(aci);
            }
            reader.Close();
            return acic;
        }

        public int GetPhotoSizeByUserid(int userid)
        {
            string commandText = string.Format("SELECT ISNULL(SUM(filesize), 0) AS [filesize] FROM [{0}photos] WHERE userid={1}", BaseConfigs.GetTablePrefix, userid);
            return (int)DbHelper.ExecuteScalar(CommandType.Text, commandText);
        }

        public bool SaveSpaceAlbum(AlbumInfo spaceAlbum)
        {
            DbParameter[] parms = 
				{
					DbHelper.MakeInParam("@albumid", (DbType)SqlDbType.Int, 4, spaceAlbum.Albumid),
					DbHelper.MakeInParam("@albumcateid", (DbType)SqlDbType.Int, 4, spaceAlbum.Albumcateid),
					DbHelper.MakeInParam("@title", (DbType)SqlDbType.NChar, 50,spaceAlbum.Title),
					DbHelper.MakeInParam("@description", (DbType)SqlDbType.NChar, 200,spaceAlbum.Description),
					DbHelper.MakeInParam("@password", (DbType)SqlDbType.NChar, 50,spaceAlbum.Password),
					DbHelper.MakeInParam("@imgcount", (DbType)SqlDbType.Int, 4,spaceAlbum.Imgcount),
					DbHelper.MakeInParam("@logo", (DbType)SqlDbType.NChar, 255, spaceAlbum.Logo),
					DbHelper.MakeInParam("@type", (DbType)SqlDbType.Int, 8,spaceAlbum.Type)
				};
            string commandText = String.Format("UPDATE [{0}albums] SET [albumcateid] = @albumcateid, [title] = @title, [description] = @description, [password] = @password, [imgcount] = @imgcount, [logo] = @logo, [altype] = @type WHERE [albumid] = @albumid", BaseConfigs.GetTablePrefix);

            DbHelper.ExecuteNonQuery(CommandType.Text, commandText, parms);

            return true;
        }

        public IDataReader GetFocusPhotoList(int type, int focusphotocount, int validDays)
        {
            return GetFocusPhotoList(type, focusphotocount, validDays, -1);
        }

        public IDataReader GetFocusPhotoList(int type, int focusphotocount, int validDays, int uid)
        {
            DbParameter[] parms = {
                                    DbHelper.MakeInParam("@validDays", (DbType)SqlDbType.Int, 4, validDays),
                                    DbHelper.MakeInParam("@uid", (DbType)SqlDbType.Int, 4, uid)
                               };
            string commandText = string.Format("SELECT TOP {0} [p].* FROM [{1}photos] [p],[{1}albums] [a] WHERE DATEDIFF(d, [postdate], getdate()) < @validDays AND [a].[albumid] = [p].[albumid] AND [a].[altype]=0{2}",
                                        focusphotocount, BaseConfigs.GetTablePrefix, uid > 1 ? " AND [p].[userid] =@uid" : string.Empty);
            switch (type)
            {
                case 0:
                    commandText += " ORDER BY [p].[views] DESC";
                    break;
                case 1:
                    commandText += " ORDER BY [p].[comments] DESC";
                    break;
                case 2:
                    commandText += " ORDER BY [p].[postdate] DESC";
                    break;
                default:
                    commandText += " ORDER BY [p].[views] DESC";
                    break;
            }
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }

        #endregion

        public DataTable GetSearchAlbumList(int pagesize, string albumids)
        {
            string commandText = string.Format("SELECT TOP {1} [{0}albums].[albumid], [{0}albums].[title], [{0}albums].[username], [{0}albums].[userid], [{0}albums].[createdatetime], [{0}albums].[imgcount], [{0}albums].[views], [{0}albumcategories].[albumcateid],[{0}albumcategories].[title] AS [categorytitle] FROM [{0}albums] LEFT JOIN [{0}albumcategories] ON [{0}albumcategories].[albumcateid] = [{0}albums].[albumcateid] WHERE [{0}albums].[albumid] IN({2}) ORDER BY CHARINDEX(CONVERT(VARCHAR(8),[{0}albums].[albumid]),'{2}')", BaseConfigs.GetTablePrefix, pagesize, albumids);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        public DataTable GetSpaceAlbumByUserId(int userid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "albums] WHERE [userid]=" + userid).Tables[0];
        }

        public IDataReader GetAlbumListByCondition(int type, int focusphotocount, int vaildDays)
        {
            DbParameter parm = DbHelper.MakeInParam("@vailddays", (DbType)SqlDbType.Int, 4, vaildDays);
            string commandText = string.Format("SELECT TOP {0} * FROM [{1}albums] WHERE DATEDIFF(d, [createdatetime], getdate()) < @vailddays AND [imgcount]>0 AND [altype]=0", focusphotocount, BaseConfigs.GetTablePrefix);

            switch (type)
            {
                case 0:
                    commandText += " ORDER BY [createdatetime] DESC";
                    break;
                case 1:
                    commandText += " ORDER BY [views] DESC";
                    break;
                case 2:
                    commandText += " ORDER BY [imgcount] DESC";
                    break;
                default:
                    commandText += " ORDER BY [createdatetime] DESC";
                    break;
            }
            return DbHelper.ExecuteReader(CommandType.Text, commandText, parm);
        }

        public IDataReader GetHotTagsListForPhoto(int count)
        {
            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT TOP {0} * FROM [{1}tags] WHERE [pcount] > 0 ORDER BY [pcount] DESC,[orderid]", count, BaseConfigs.GetTablePrefix));
        }

        public IDataReader GetTagsListByPhotoId(int photoid)
        {
            DbParameter parm = DbHelper.MakeInParam("@photoid", (DbType)SqlDbType.Int, 4, photoid);

            return DbHelper.ExecuteReader(CommandType.Text, string.Format("SELECT [{0}tags].* FROM [{0}tags], [{0}phototags] WHERE [{0}phototags].[tagid] = [{0}tags].[tagid] AND [{0}phototags].[photoid] = @photoid ORDER BY [orderid]", BaseConfigs.GetTablePrefix), parm);
        }

        public int GetPhotoCountWithSameTag(int tagid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid);

            string commandText = string.Format("SELECT COUNT(1) FROM [{0}phototags] AS [pt],[{0}photos] AS [p],[{0}albums] AS [a] WHERE [pt].[tagid] = @tagid AND [p].[photoid] = [pt].[photoid] AND [p].[albumid] = [a].[albumid] AND [a].[altype]=0", BaseConfigs.GetTablePrefix);

            return TypeConverter.ObjectToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, parm));
        }

        public IDataReader GetPhotosWithSameTag(int tagid, int pageid, int pagesize)
        {
            DbParameter[] parm = {
                                    DbHelper.MakeInParam("@tagid", (DbType)SqlDbType.Int, 4, tagid),
                                    DbHelper.MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, pageid),
                                    DbHelper.MakeInParam("@pagesize", (DbType)SqlDbType.Int, 4, pagesize)
                                 };
            string commandText = string.Format("{0}getphotolistbytag", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.StoredProcedure, commandText, parm);
        }

        public void DeleteAll(int userid)
        {
            DbParameter parm = DbHelper.MakeInParam("@userid", (DbType)SqlDbType.Int, 4, userid);

            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}photocomments] WHERE [userid]=@userid", BaseConfigs.GetTablePrefix), parm);
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}photos] WHERE [userid]=@userid", BaseConfigs.GetTablePrefix), parm);
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}albums] WHERE [userid]=@userid", BaseConfigs.GetTablePrefix), parm);
        }
    }
}
