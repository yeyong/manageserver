﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SAS.Plugin.Album;
using SAS.Album.Data;
using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;

namespace SAS.Album
{
    public class AlbumPlugin : AlbumPluginBase
    {
        public override string PHOTO_HOT_TAG_CACHE_FILENAME
        {
            get { return AlbumTags.PHOTO_HOT_TAG_CACHE_FILENAME; }
        }

        public override void WritePhotoTagsCacheFile(int photoid)
        {
            AlbumTags.WritePhotoTagsCacheFile(photoid);
        }

        public override void WriteHotTagsListForPhotoJSONPCacheFile(int count)
        {
            AlbumTags.WriteHotTagsListForPhotoJSONPCacheFile(count);
        }

        public override SAS.Entity.AlbumInfo GetAlbumInfo(int albumid)
        {
            return DTOProvider.GetAlbumInfo(albumid);
        }

        public override void CreateAlbumJsonData(int albumid)
        {
            Albums.CreateAlbumJsonData(albumid);
        }

        public override string GetAlbumJsonData(int albumid)
        {
            return Albums.GetAlbumJsonData(albumid);
        }

        public override int GetPhotoCountWithSameTag(int tagid)
        {
            return Albums.GetPhotoCountWithSameTag(tagid);
        }

        public override SAS.Common.Generic.List<SAS.Entity.PhotoInfo> GetPhotosWithSameTag(int tagid, int pageid, int tpp)
        {
            return Albums.GetPhotosWithSameTag(tagid, pageid, tpp);
        }

        protected override string OnAttachCreated(SAS.Entity.AttachmentInfo[] attachs, int usergroupid, int userid, string username)
        {
            if (attachs == null)
            {
                return "";
            }
            string[] albumsid = SASRequest.GetString("albums") == "" ? null : SASRequest.GetString("albums").Split(',');
            if (albumsid == null)
                return "";
            int maxphotosize = UserGroups.GetUserGroupInfo(usergroupid).ug_maxspacephotosize;
            int currentphotisize = DbProvider.GetInstance().GetPhotoSizeByUserid(userid);

            for (int i = 0; i < attachs.Length; i++)
            {
                if (attachs[i].Filename != "" && (attachs[i].Filetype == "image/pjpeg") || (attachs[i].Filetype == "image/gif") || (attachs[i].Filetype == "image/x-png"))
                {
                    //空间查检
                    string aid = albumsid[i + 1];
                    if (aid != "0")
                    {
                        if ((maxphotosize - currentphotisize - (int)attachs[i].Filesize) > 0)
                        {
                            string filename = Utils.GetMapPath(BaseConfigs.GetSitePath + "upload/" + attachs[i].Filename.Replace('\\', '/'));
                            string extension = Path.GetExtension(filename);
                            Common.Thumbnail.MakeThumbnailImage(filename, filename.Replace(extension, "_thumbnail" + extension), 150, 150);
                            Common.Thumbnail.MakeSquareImage(filename, filename.Replace(extension, "_square" + extension), 100);
                            PhotoInfo photoinfo = new PhotoInfo();
                            photoinfo.Filename = "upload/" + attachs[i].Filename.Replace('\\', '/');
                            photoinfo.Attachment = attachs[i].Attachment;
                            photoinfo.Filesize = (int)attachs[i].Filesize;
                            photoinfo.Title = attachs[i].Attachment.Remove(attachs[i].Attachment.IndexOf("."));
                            photoinfo.Description = attachs[i].Description;
                            photoinfo.Albumid = int.Parse(aid);
                            photoinfo.Userid = userid;
                            photoinfo.Username = username;
                            photoinfo.Views = 0;
                            photoinfo.Commentstatus = 0;
                            photoinfo.Tagstatus = 0;
                            photoinfo.Comments = 0;
                            photoinfo.IsAttachment = 1;
                            DbProvider.GetInstance().AddSpacePhoto(photoinfo);
                            AlbumInfo albumInfo = DTOProvider.GetAlbumInfo(Convert.ToInt32(aid));
                            albumInfo.Imgcount = DbProvider.GetInstance().GetSpacePhotoCountByAlbumId(int.Parse(aid));
                            DbProvider.GetInstance().SaveSpaceAlbum(albumInfo);
                            currentphotisize += (int)attachs[i].Filesize;
                        }
                        else
                        {
                            return "相册空间不足,可能有图片未能加入相册";
                        }
                    }
                }
            }
            return "";
        }

        public override System.Data.IDataReader GetFocusPhotoList(int type, int focusphotocount, int vaildDays)
        {
            return DbProvider.GetInstance().GetFocusPhotoList(type, focusphotocount, vaildDays);
        }

        public override System.Data.IDataReader GetAlbumListByCondition(int type, int focusphotocount, int vaildDays)
        {
            return DbProvider.GetInstance().GetAlbumListByCondition(type, focusphotocount, vaildDays);
        }

        public override SAS.Entity.PhotoInfo GetPhotoEntity(System.Data.IDataReader reader)
        {
            return DTOProvider.GetPhotoEntity(reader);
        }

        public override SAS.Entity.AlbumInfo GetAlbumEntity(System.Data.IDataReader reader)
        {
            return DTOProvider.GetAlbumEntity(reader);
        }

        public override int GetPhotoSizeByUserid(int userid)
        {
            return DbProvider.GetInstance().GetPhotoSizeByUserid(userid);
        }

        public override System.Data.DataTable GetSpaceAlbumByUserId(int userid)
        {
            return DbProvider.GetInstance().GetSpaceAlbumByUserId(userid);
        }

        public override SAS.Common.Generic.List<AlbumCategoryInfo> GetAlbumCategory()
        {
            return DbProvider.GetInstance().GetAlbumCategory();
        }

        public override string GetThumbnailImage(string filename)
        {
            return Globals.GetThumbnailImage(filename);
        }

        protected override System.Data.DataTable GetSearchResult(int pagesize, string idstr)
        {
            return DbProvider.GetInstance().GetSearchAlbumList(pagesize, idstr);
        }

        protected override string GetFeedXML(int ttl)
        {
            return AlbumFeeds.GetPhotoRss(ttl);
        }

        protected override string GetFeedXML(int ttl, int uid)
        {
            return AlbumFeeds.GetPhotoRss(ttl, uid);
        }

        protected override void OnUserDeleted(int userid)
        {
            Albums.DeleteAlbums(userid);
        }
    }
}