﻿using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Logic;
using SAS.Plugin.VerifyImage;

namespace SAS.Web.UI
{
    /// <summary>
    /// 验证码图片页面类
    /// </summary>
    public class VerifyImagePage : System.Web.UI.Page
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            string bgcolor = SASRequest.GetQueryString("bgcolor").Trim();
            int textcolor = SASRequest.GetQueryInt("textcolor", 1);
            string[] bgcolorArray = bgcolor.Split(',');

            Color bg = Color.White;
            if (bgcolorArray.Length == 1 && bgcolor != string.Empty)
                bg = Utils.ToColor(bgcolor);
            else if (bgcolorArray.Length == 3 && Utils.IsNumericArray(bgcolorArray))
                bg = Color.FromArgb(Utils.StrToInt(bgcolorArray[0], 255), Utils.StrToInt(bgcolorArray[1], 255), Utils.StrToInt(bgcolorArray[2], 255));

            VerifyImageInfo verifyimg = VerifyImageProvider.GetInstance(config.VerifyImageAssemly).GenerateImage(OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout).ol_verifycode, 120, 60, bg, textcolor);
            Bitmap image = verifyimg.Image;

            System.Web.HttpContext.Current.Response.ContentType = verifyimg.ContentType;
            image.Save(this.Response.OutputStream, verifyimg.ImageFormat);
        }
    }
}
