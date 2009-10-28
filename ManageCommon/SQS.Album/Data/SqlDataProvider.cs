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

        #endregion

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
