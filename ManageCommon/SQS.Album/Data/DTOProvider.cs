using System;
using System.Collections;
using System.Data;
using System.Text;

using SAS.Cache;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Album.Data
{
    public class DTOProvider
    {
        public static AlbumInfo GetAlbumInfo(int aid)
        {
            IDataReader reader = DbProvider.GetInstance().GetSpaceAlbumById(aid);
            if (reader.Read())
            {
                AlbumInfo albumsinfo = GetAlbumEntity(reader);
                reader.Close();
                return albumsinfo;
            }
            else
            {
                reader.Close();
                return null;
            }
        }

        public static AlbumInfo GetAlbumEntity(IDataReader reader)
        {
            AlbumInfo album = new AlbumInfo();

            album.Albumid = TypeConverter.ObjectToInt(reader["albumid"]);
            album.Userid = TypeConverter.ObjectToInt(reader["userid"]);
            album.Username = reader["username"].ToString();
            album.Title = reader["title"].ToString();
            album.Description = reader["description"].ToString();
            album.Logo = reader["logo"].ToString();
            album.Password = reader["password"].ToString();
            album.Imgcount = TypeConverter.ObjectToInt(reader["imgcount"]);
            album.Views = TypeConverter.ObjectToInt(reader["views"]);
            album.Type = TypeConverter.ObjectToInt(reader["altype"]);
            album.Createdatetime = reader["createdatetime"].ToString();
            album.Albumcateid = TypeConverter.ObjectToInt(reader["albumcateid"]);
            album.pl_status = TypeConverter.ObjectToInt(reader["pl_status"]);

            return album;
        }

        public static PhotoInfo GetPhotoEntity(IDataReader reader)
        {
            PhotoInfo photoinfo = new PhotoInfo();
            photoinfo.Photoid = TypeConverter.ObjectToInt(reader["photoid"]);
            photoinfo.Filename = reader["filename"].ToString();
            photoinfo.Attachment = reader["attachment"].ToString();
            photoinfo.Filesize = TypeConverter.ObjectToInt(reader["filesize"]);
            photoinfo.Description = reader["description"].ToString();
            photoinfo.Postdate = reader["postdate"].ToString();
            photoinfo.Albumid = TypeConverter.ObjectToInt(reader["albumid"]);
            photoinfo.Userid = TypeConverter.ObjectToInt(reader["userid"]);
            photoinfo.Title = reader["title"].ToString();
            photoinfo.Views = TypeConverter.ObjectToInt(reader["views"]);
            photoinfo.Commentstatus = (PhotoStatus)TypeConverter.ObjectToInt(reader["commentstatus"]);
            photoinfo.Tagstatus = (PhotoStatus)TypeConverter.ObjectToInt(reader["tagstatus"]);
            photoinfo.Comments = TypeConverter.ObjectToInt(reader["comments"]);
            photoinfo.Username = reader["username"].ToString();

            return photoinfo;
        }

        public static SAS.Common.Generic.List<AlbumCategoryInfo> GetAlbumCategory()
        {
            SASCache cache = SASCache.GetCacheService();
            SAS.Common.Generic.List<AlbumCategoryInfo> acic = cache.RetrieveObject("/Space/AlbumCategory") as SAS.Common.Generic.List<AlbumCategoryInfo>;

            if (acic == null)
            {
                acic = new SAS.Common.Generic.List<AlbumCategoryInfo>();
                acic = Data.DbProvider.GetInstance().GetAlbumCategory();
                cache.AddObject("/Space/AlbumCategory", (ICollection)acic);
            }
            return acic;
        }

        public static SAS.Common.Generic.List<PhotoInfo> GetSpacePhotosInfo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return new SAS.Common.Generic.List<PhotoInfo>();

            SAS.Common.Generic.List<PhotoInfo> photosinfoarray = new SAS.Common.Generic.List<PhotoInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PhotoInfo photo = new PhotoInfo();
                photo.Photoid = TypeConverter.ObjectToInt(dt.Rows[i]["photoid"]);
                photo.Filename = dt.Rows[i]["filename"].ToString();
                photo.Attachment = dt.Rows[i]["attachment"].ToString();
                photo.Filesize = TypeConverter.ObjectToInt(dt.Rows[i]["filesize"]);
                photo.Description = dt.Rows[i]["description"].ToString();
                photo.Postdate = dt.Rows[i]["postdate"].ToString();
                photo.Albumid = TypeConverter.ObjectToInt(dt.Rows[i]["albumid"]);
                photo.Userid = TypeConverter.ObjectToInt(dt.Rows[i]["userid"]);
                photo.Title = dt.Rows[i]["title"].ToString();
                photo.Views = TypeConverter.ObjectToInt(dt.Rows[i]["views"]);
                photo.Commentstatus = (PhotoStatus)TypeConverter.ObjectToInt(dt.Rows[i]["commentstatus"]);
                photo.Tagstatus = (PhotoStatus)TypeConverter.ObjectToInt(dt.Rows[i]["tagstatus"]);
                photo.Comments = TypeConverter.ObjectToInt(dt.Rows[i]["comments"]);

                photosinfoarray.Add(photo);
            }
            dt.Dispose();
            return photosinfoarray;
        }
    }
}
