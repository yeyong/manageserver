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
    }
}
