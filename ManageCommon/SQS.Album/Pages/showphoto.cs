using System;
using System.Web;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;
using SAS.Album.Data;

namespace SAS.Album.Pages
{    
    /// <summary>
    /// 查看图片页面
    /// </summary>
    public class showphoto : BasePage
    {
        #region 页面变量
        /// <summary>
        /// 图片所属相册
        /// </summary>
        public AlbumInfo album;
        /// <summary>
        /// 所属相册分类
        /// </summary>
        public AlbumCategoryInfo albumcategory;
        /// <summary>
        /// 图片Id
        /// </summary>
        public int photoid = SASRequest.GetInt("photoid", 0);
        /// <summary>
        /// 图片信息
        /// </summary>
        public PhotoInfo photo;
        /// <summary>
        /// 查看图片方式,0表示查看当前Id图片,1表示查看当前图片的上一张图片,2表示查看当前图片的下一张图片
        /// </summary>
        public byte mode = 0;

        /// <summary>
        /// 是否能够发表评论
        /// </summary>
        public bool commentable = true;
        /// <summary>
        /// 是否能够编辑
        /// </summary>
        public bool editable = false;
        /// <summary>
        /// 相册弹出导航菜单HTML代码
        /// </summary>
        public string navhomemenu = Albums.GetPhotoListMenuDivCache();
        /// <summary>
        /// json数据文件名称
        /// </summary>
        public string jsonfilename;
        /// <summary>
        /// 相册RSS UrlRewrite
        /// </summary>
        private string photorssurl = "";
        #endregion

        protected override void ShowPage()
        {
            if (config.Enablealbum != 1)
            {
                AddErrLine("相册功能已被关闭");
                return;
            }
            
            string go = SASRequest.GetString("go");
            switch (go)
            {
                case "prev":
                    mode = 1;
                    break;
                case "next":
                    mode = 2;
                    break;
                default:
                    mode = 0;
                    break;
            }

            if (photoid < 1)
            {
                AddErrLine("指定的图片不存在");
                return;
            }

            photo = DTOProvider.GetPhotoInfo(photoid, 0, 0);
            if (photo == null)
            {
                AddErrLine("指定的图片不存在");
                return;
            }

            album = DTOProvider.GetAlbumInfo(photo.Albumid);
            if (album == null)
            {
                AddErrLine("指定的相册不存在");
                return;
            }

            if (mode != 0)
            {
                photo = DTOProvider.GetPhotoInfo(photoid, photo.Albumid, mode);
                if (photo == null)
                {
                    AddErrLine("指定的图片不存在");
                    return;
                }
            }

            if (config.Rssstatus == 1)
            {
                if (GeneralConfigs.GetConfig().Aspxrewrite == 1)
                    photorssurl = string.Format("photorss-{0}{1}", photo.Userid, GeneralConfigs.GetConfig().Extname);
                else
                    photorssurl = string.Format("rss.aspx?uid={0}&type=photo", photo.Userid);

                AddLinkRss(string.Format("tools/{0}", photorssurl), "最新图片");
            }

            pagetitle = photo.Title;
        }
    }
}
