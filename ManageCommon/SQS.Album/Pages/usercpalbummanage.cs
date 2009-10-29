using System;
using System.Data;
using System.IO;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Logic;
using SAS.Entity;
using SAS.Config;
using SAS.Album.Data;
using SAS.Album.Config;

namespace SAS.Album.Pages
{
    /// <summary>
    /// 个人相册管理
    /// </summary>
    public class usercpalbummanage : BasePage
    {
        #region 页面变量
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid = SASRequest.GetInt("page", 1);
        /// <summary>
        /// 相册总数
        /// </summary>
        public int albumcount;
        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = "";
        /// <summary>
        /// 相册列表
        /// </summary>
        public DataTable albumInfoArray = new DataTable();
        /// <summary>
        /// 相册Id
        /// </summary>
        public int albumid = 0;
        /// <summary>
        /// 相册分类Id
        /// </summary>
        public int albumcateid = 0;
        /// <summary>
        /// 模式, 用于显示当前是新建还是编辑
        /// </summary>
        public string mod = "新增相册";
        /// <summary>
        /// 相册标题
        /// </summary>
        public string title = "";
        /// <summary>
        /// 相册描述
        /// </summary>
        public string description = "";
        /// <summary>
        /// 相册类型,1=公开相册,0=私人相册
        /// </summary>
        public int type = 0;
        /// <summary>
        /// 相册密码
        /// </summary>
        public string albumpassword = "";
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();
        /// <summary>
        /// 是否允许新建相册
        /// </summary>
        public bool allownewalbum;
        /// <summary>
        /// 相册分类列表
        /// </summary>
        public List<AlbumCategoryInfo> albumcates = DTOProvider.GetAlbumCategory();
        private readonly string defaultLogo = BaseConfigs.GetSitePath + "images/album_logo.png";
        private int pagesize = 15;

        #endregion

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }

            user = Users.GetUserInfo(userid);

            if (config.Enablealbum != 1)
            {
                AddErrLine("相册功能已被关闭");
                return;
            }

            if (SASRequest.IsPost())
            {
                if (LogicUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                if (SASRequest.GetFormString("albumcate") == "")
                {
                    AddErrLine("请选择相册分类");
                    return;
                }
                if (SASRequest.GetFormString("albumtitle") == "")
                {
                    AddErrLine("相册名称不能为空");
                    return;
                }
                if (SASRequest.GetFormInt("type", 0) == 1 && SASRequest.GetFormString("password") == string.Empty)
                {
                    AddErrLine("私人相册密码不能为空");
                    return;
                }

                ModifyAlbumInfo();
                return;
            }

            if (SASRequest.GetString("mod") == "delete")
            {
                DeleteAlbumInfo();
                ispost = true;
                return;
            }
            if (SASRequest.GetString("mod") == "edit")
                LoadAlbumInfo();

            if (SASRequest.GetString("albumid") != "")
                albumid = SASRequest.GetInt("albumid", 0);

            //获取主题总数
            albumcount = Data.DbProvider.GetInstance().GetSpaceAlbumsCount(userid);
            //获取总页数
            pagecount = albumcount % pagesize == 0 ? albumcount / pagesize : albumcount / pagesize + 1;
            if (pagecount == 0)
                pagecount = 1;

            //修正请求页数中可能的错误
            if (pageid < 1)
                pageid = 1;
            if (pageid > pagecount)
                pageid = pagecount;

            //获取相册分页记录数并显示
            albumInfoArray = Data.DbProvider.GetInstance().SpaceAlbumsList(pagesize, pageid, userid);
            foreach (DataRow singleAlbumInfo in albumInfoArray.Rows)
            {
                if (singleAlbumInfo["Logo"].ToString().IndexOf("http") < 0)
                {
                    if (singleAlbumInfo["Logo"].ToString().Trim() == "")
                        singleAlbumInfo["Logo"] = defaultLogo;
                    else
                        singleAlbumInfo["Logo"] = IsExistsLog(singleAlbumInfo["Logo"].ToString());
                }

                singleAlbumInfo["Title"] = singleAlbumInfo["Title"].ToString().Trim().Replace("\"", "&quot;").Replace("'", "&#39;");
            }
            pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "usercpspacemanagealbum.aspx", 8);
            allownewalbum = int.Parse(AlbumConfigs.GetConfig().MaxAlbumCount) - albumcount > 0;
            if (SASRequest.GetString("mod") == "edit")
                allownewalbum = true;
        }

        private string IsExistsLog(string logo)
        {
            logo = logo.Trim();
            string thumbnailImage = BaseConfigs.GetSitePath + logo;
            if (File.Exists(Utils.GetMapPath(thumbnailImage)))
                return thumbnailImage;
            else
                return defaultLogo;
        }

        private void LoadAlbumInfo()
        {
            mod = "修改相册";
            AlbumInfo albumInfo = DTOProvider.GetAlbumInfo(SASRequest.GetInt("albumid", 0));
            if (albumInfo.Userid != userid)
            {
                AddErrLine("您所编辑的相册不存在");
                return;
            }
            title = albumInfo.Title;
            albumcateid = albumInfo.Albumcateid;
            description = albumInfo.Description;
            type = albumInfo.Type;
            albumpassword = albumInfo.Password;
        }

        private void ModifyAlbumInfo()
        {
            string errorinfo = "";
            string id = SASRequest.GetFormString("albumid");
            if (id == "0")
            {
                AlbumInfo albumInfo = new AlbumInfo();
                albumInfo.Userid = userid;
                albumInfo.Username = username;
                albumInfo.Albumcateid = SASRequest.GetFormInt("albumcate", 0);
                albumInfo.Title = SASRequest.GetFormString("albumtitle");
                albumInfo.Description = SASRequest.GetFormString("albumdescription");
                albumInfo.Password = SASRequest.GetFormString("password");
                albumInfo.Type = SASRequest.GetFormInt("type", 0);
                Data.DbProvider.GetInstance().AddSpaceAlbum(albumInfo);
            }
            else
            {
                AlbumInfo albumInfo = DTOProvider.GetAlbumInfo(Convert.ToInt32(id));
                if (albumInfo.Userid != userid)
                {
                    AddErrLine("您所编辑的相册不存在");
                    return;
                }
                albumInfo.Title = SASRequest.GetFormString("albumtitle");
                albumInfo.Albumcateid = SASRequest.GetFormInt("albumcate", 0);
                albumInfo.Description = SASRequest.GetFormString("albumdescription");
                albumInfo.Password = SASRequest.GetFormString("password");
                albumInfo.Type = SASRequest.GetFormInt("type", 0);
                Data.DbProvider.GetInstance().SaveSpaceAlbum(albumInfo);
            }
            if (errorinfo == "")
            {
                SetUrl(string.Format("usercpspacemanagealbum.aspx?page={0}", SASRequest.GetInt("page", 1)));
                SetMetaRefresh();
                SetShowBackLink(true);
                if (id == "0")
                    AddMsgLine("相册增加成功");
                else
                    AddMsgLine("相册修改成功");
                return;
            }
            else
            {
                AddErrLine(errorinfo);
                return;
            }
        }

        private void DeleteAlbumInfo()
        {
            int albumid = SASRequest.GetInt("albumid", 0);
            if (DTOProvider.GetAlbumInfo(albumid).Userid != userid)
            {
                AddErrLine("您所删除的相册不存在");
                return;
            }
            string errorinfo = "";
            List<PhotoInfo> _spacephotoinfoarray = DTOProvider.GetSpacePhotosInfo(DbProvider.GetInstance().GetSpacePhotoByAlbumID(albumid));
            string photoidList = "";
            if (_spacephotoinfoarray != null)
            {
                foreach (PhotoInfo _s in _spacephotoinfoarray)
                {
                    if (_s.Userid == userid)
                        photoidList += _s.Photoid + ",";
                }
            }
            if (photoidList != "")
            {
                photoidList = photoidList.Substring(0, photoidList.Length - 1);
                Data.DbProvider.GetInstance().DeleteSpacePhotoByIDList(photoidList, albumid, userid);
            }
            Data.DbProvider.GetInstance().DeleteSpaceAlbum(albumid, userid);
            if (errorinfo == "")
            {
                SetUrl("usercpspacemanagealbum.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);
                AddMsgLine("删除成功");
                return;
            }
            else
            {
                AddErrLine(errorinfo);
                return;
            }
        }
    }
}
