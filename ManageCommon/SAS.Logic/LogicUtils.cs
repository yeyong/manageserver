using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing.Drawing2D;

using SAS.Common;
using SAS.Config;
using SAS.Entity;
using SAS.Data;

namespace SAS.Logic
{
    /// <summary>
    /// 站点工具类
    /// </summary>
    public class LogicUtils
    {
        /// <summary>
        /// 验证码生成的取值范围
        /// </summary>
        private static string[] verifycodeRange = { "1","2","3","4","5","6","7","8","9",
                                                    "a","b","c","d","e","f","g",
                                                    "h",    "j","k",    "m","n",
                                                        "p","q",    "r","s","t",
                                                    "u","v","w",    "x","y"
                                                  };
        /// <summary>
        /// 生成验证码所使用的随机数发生器
        /// </summary>
        private static Random verifycodeRandom = new Random();

        /// <summary>
        /// 禁用文字正则式对象
        /// </summary>
        private static Regex r_word;


        private static RegexOptions options = RegexOptions.IgnoreCase;

        public static Regex[] r = new Regex[4];

        static LogicUtils()
        {
            r[0] = new Regex(@"(\r\n)", options);
            r[1] = new Regex(@"(\n)", options);
            r[2] = new Regex(@"(\r)", options);
            r[3] = new Regex(@"(<br />)", options);
        }

        /// <summary>
        /// 返回论坛用户密码cookie明文
        /// </summary>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string GetCookiePassword(string key)
        {
            return DES.Decode(GetCookie("password"), key).Trim();
        }

        /// <summary>
        /// 返回论坛用户密码cookie明文
        /// </summary>
        /// <param name="password">密码密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string GetCookiePassword(string password, string key)
        {
            return DES.Decode(password, key);
        }


        /// <summary>
        /// 返回密码密文
        /// </summary>
        /// <param name="password">密码明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string SetCookiePassword(string password, string key)
        {
            return DES.Encode(password, key);
        }


        /// <summary>
        /// 返回用户安全问题答案的存储数据
        /// </summary>
        /// <param name="questionid">问题id</param>
        /// <param name="answer">答案</param>
        /// <returns></returns>
        public static string GetUserSecques(int questionid, string answer)
        {
            if (questionid > 0)
                return Utils.MD5(answer + Utils.MD5(questionid.ToString())).Substring(15, 8);

            return "";
        }

        #region Cookies

        /// <summary>
        /// 写论坛cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["sas"];
            if (cookie == null)
            {
                cookie = new HttpCookie("sas");
                cookie.Values[strName] = Utils.UrlEncode(strValue);
            }
            else
            {

                cookie.Values[strName] = Utils.UrlEncode(strValue);
                if (HttpContext.Current.Request.Cookies["sas"]["expires"] != null)
                {
                    int expires = Utils.StrToInt(HttpContext.Current.Request.Cookies["sas"]["expires"].ToString(), 0);
                    if (expires > 0)
                    {
                        cookie.Expires = DateTime.Now.AddMinutes(Utils.StrToInt(HttpContext.Current.Request.Cookies["sas"]["expires"].ToString(), 0));
                    }
                }
            }

            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写论坛登录用户的cookie
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="expires">cookie有效期</param>
        /// <param name="passwordkey">用户密码Key</param>
        /// <param name="templateid">用户当前要使用的界面风格</param>
        /// <param name="invisible">用户当前的登录模式(正常或隐身)</param>
        public static void WriteUserCookie(int uid, int expires, string passwordkey, int templateid, int invisible)
        {
            UserInfo userinfo = SAS.Data.DataProvider.Users.GetUserInfo(uid);
            WriteUserCookie(userinfo, expires, passwordkey, templateid, invisible);
        }

        /// <summary>
        /// 写论坛登录用户的cookie
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <param name="expires">cookie有效期</param>
        /// <param name="passwordkey">用户密码Key</param>
        /// <param name="templateid">用户当前要使用的界面风格</param>
        /// <param name="invisible">用户当前的登录模式(正常或隐身)</param>
        public static void WriteUserCookie(UserInfo userinfo, int expires, string passwordkey, int templateid, int invisible)
        {
            if (userinfo == null)
                return;

            HttpCookie cookie = new HttpCookie("sas");
            cookie.Values["userid"] = userinfo.Ps_id.ToString();
            cookie.Values["password"] = Utils.UrlEncode(SetCookiePassword(userinfo.Ps_password, passwordkey));
            if (Templates.GetTemplateItem(templateid) == null)
            {
                templateid = 0;

                foreach (string strTemplateid in Utils.SplitString(Templates.GetValidTemplateIDList(), ","))
                {
                    if (strTemplateid.Equals(userinfo.Ps_tempID.ToString()))
                    {
                        templateid = userinfo.Ps_tempID;
                        break;
                    }
                }
            }

            //cookie.Values["avatar"] = Utils.UrlEncode(userinfo.Avatar.ToString());
            //cookie.Values["tpp"] = userinfo.Tpp.ToString();
            //cookie.Values["ppp"] = userinfo.Ppp.ToString();
            cookie.Values["pmsound"] = userinfo.Ps_bdSound.ToString();
            if (invisible != 0 || invisible != 1)
            {
                invisible = userinfo.Ps_invisible;
            }
            cookie.Values["invisible"] = invisible.ToString();

            cookie.Values["referer"] = "index.aspx";
            cookie.Values["sigstatus"] = userinfo.Ps_issign.ToString();
            cookie.Values["expires"] = expires.ToString();
            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(expires);
            }
            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
            {
                cookie.Domain = cookieDomain;
            }

            HttpContext.Current.Response.AppendCookie(cookie);
            if (templateid > 0)
            {
                Utils.WriteCookie(Utils.GetTemplateCookieName(), templateid.ToString(), 999999);
            }
        }

        /// <summary>
        /// 写论坛登录用户的cookie
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="expires">cookie有效期</param>
        /// <param name="passwordkey">用户密码Key</param>
        public static void WriteUserCookie(int uid, int expires, string passwordkey)
        {
            WriteUserCookie(uid, expires, passwordkey, 0, -1);
        }

        /// <summary>
        /// 写论坛登录用户的cookie
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <param name="expires">cookie有效期</param>
        /// <param name="passwordkey">用户密码Key</param>
        public static void WriteUserCookie(UserInfo userinfo, int expires, string passwordkey)
        {
            WriteUserCookie(userinfo, expires, passwordkey, 0, -1);
        }

        /// <summary>
        /// 获得论坛cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <returns>值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["sas"] != null && HttpContext.Current.Request.Cookies["sas"][strName] != null)
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies["sas"][strName].ToString());

            return "";
        }


        /// <summary>
        /// 清除论坛登录用户的cookie
        /// </summary>
        public static void ClearUserCookie()
        {
            ClearUserCookie("sas");
        }

        public static void ClearUserCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            string cookieDomain = GeneralConfigs.GetConfig().CookieDomain.Trim();
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        #endregion

        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns>验证码</returns>
        public static string CreateAuthStr(int len)
        {
            int number;
            StringBuilder checkCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    checkCode.Append((char)('0' + (char)(number % 10)));
                else
                    checkCode.Append((char)('A' + (char)(number % 26)));
            }
            return checkCode.ToString();
        }

        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="OnlyNum">是否仅为数字</param>
        /// <returns>string</returns>
        public static string CreateAuthStr(int len, bool OnlyNum)
        {
            int number;
            StringBuilder checkCode = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                if (!OnlyNum)
                    number = verifycodeRandom.Next(0, verifycodeRange.Length);
                else
                    number = verifycodeRandom.Next(0, 10);

                checkCode.Append(verifycodeRange[number]);
            }
            return checkCode.ToString();
        }

        /// <summary>
        /// 创建主题缓存标志文件
        /// </summary>
        /// <returns>bool</returns>
        public static bool CreateTopicCacheInfoFile()
        {
            string infofilepath = Utils.GetMapPath(GetShowTopicCacheDir() + "/cacheinfo.config");
            try
            {
                using (FileStream fs = new FileStream(infofilepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?>");
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 向HTTP输出指定主题id的主题缓存文件, 其中某些时段会执行删除过期主题缓存文件
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="timeout">缓存文件的有效时间</param>
        /// <returns>bool</returns>
        public static bool ResponseTopicCacheFile(int tid, int timeout)
        {
            TimeSpan timeSpan;
            if ((System.DateTime.Now.Minute >= System.DateTime.Now.Day) && (System.DateTime.Now.Minute <= System.DateTime.Now.Day + 10))
            {
                string infofilepath = Utils.GetMapPath(GetShowTopicCacheDir() + "/cacheinfo.config");
                if (System.IO.File.Exists(infofilepath))
                {
                    timeSpan = (System.IO.File.GetCreationTime(infofilepath) - System.DateTime.Now);

                    if (Math.Abs(timeSpan.TotalHours) > 1)
                    {
                        CreateTopicCacheInfoFile();
                        DeleteTimeoutTopicCacheFiles(timeout);
                    }
                }
                else
                {
                    CreateTopicCacheInfoFile();
                }
            }

            string filepath = GetTopicCacheFilename(tid);
            if (System.IO.File.Exists(filepath))
            {
                timeSpan = (System.IO.File.GetCreationTime(filepath) - System.DateTime.Now);
                if (timeout > 0 && Math.Abs(timeSpan.TotalMinutes) > timeout)
                {
                    DeleteTopicCacheFile(tid);
                    return false;
                }
                System.Web.HttpContext.Current.Response.WriteFile(filepath);
                System.Web.HttpContext.Current.Response.End();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回主题缓存文件名
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <returns>string</returns>
        public static string GetTopicCacheFilename(int tid)
        {
            return Utils.GetMapPath(GetShowTopicCacheDir() + "/" + tid + ".sascache");
        }

        /// <summary>
        /// 删除所有过期的主题缓存文件
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>bool</returns>
        public static bool DeleteTimeoutTopicCacheFiles(int timeout)
        {
            try
            {
                DirectoryInfo dirinfo = new DirectoryInfo(Utils.GetMapPath(GetShowTopicCacheDir()));
                TimeSpan timeSpan;
                foreach (FileSystemInfo file in dirinfo.GetFiles())
                {
                    if (file != null && file.Extension.ToLower().Equals(".sascache"))
                    {
                        timeSpan = (System.IO.File.GetCreationTime(file.FullName) - System.DateTime.Now);
                        if (timeout > 0 && Math.Abs(timeSpan.TotalMinutes) > (timeout + 1))
                        {
                            try
                            {
                                file.Delete();
                            }
                            catch
                            { }
                        }
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建主题缓存文件
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="pagetext">缓存的字符串内容</param>
        /// <returns>bool</returns>
        public static bool CreateTopicCacheFile(int tid, string pagetext)
        {
            string filepath = GetTopicCacheFilename(tid);
            try
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Byte[] info = System.Text.Encoding.UTF8.GetBytes(pagetext);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定id的主题游客缓存
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <returns>bool</returns>
        public static bool DeleteTopicCacheFile(int tid)
        {
            string filepath = GetTopicCacheFilename(tid);
            try
            {
                System.IO.File.Delete(filepath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定id列表的主题游客缓存
        /// </summary>
        /// <param name="tidlist">主题id列表</param>
        /// <returns>int</returns>
        public static int DeleteTopicCacheFile(string tidlist)
        {
            int count = 0;
            string[] strNumber = Utils.SplitString(tidlist, ",");

            foreach (string tid in strNumber)
            {
                if (Utils.IsNumeric(tid) && DeleteTopicCacheFile(Int32.Parse(tid)))
                    count++;
            }
            return count;
        }


        /// <summary>
        /// 返回"查看主题"的页面缓存目录
        /// </summary>
        /// <returns>缓存目录</returns>
        public static string GetShowTopicCacheDir()
        {
            return GetCacheDir("showtopic");
        }

        /// <summary>
        /// 返回指定目录的页面缓存目录
        /// </summary>
        /// <param name="path">目录</param>
        /// <returns>缓存目录</returns>
        public static string GetCacheDir(string path)
        {
            path = path.Trim();
            StringBuilder dir = new StringBuilder();
            dir.Append(BaseConfigs.GetSitePath);
            dir.Append("cache/");
            dir.Append(path);
            string cachedir = dir.ToString();
            if (!Directory.Exists(Utils.GetMapPath(cachedir)))
            {
                Utils.CreateDir(Utils.GetMapPath(cachedir));
            }
            return cachedir;
        }

        /// <summary>
        /// 保存上传头像
        /// </summary>
        /// <param name="MaxAllowFileSize">最大允许的头像文件尺寸(单位:KB)</param>
        /// <returns>保存文件的相对路径</returns>
        public static string SaveRequestAvatarFile(int userid, int MaxAllowFileSize)
        {
            string filename = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            string fileextname = Path.GetExtension(filename).ToLower();
            string filetype = HttpContext.Current.Request.Files[0].ContentType.ToLower();

            // 判断 文件扩展名/文件大小/文件类型 是否符合要求
            if (Utils.InArray(fileextname, ".jpg,.gif,.png") && filetype.StartsWith("image"))
            {
                StringBuilder savedir = new StringBuilder(BaseConfigs.GetSitePath + "avatars/upload/");

                int t1 = (int)((double)userid / (double)10000);
                savedir.Append(t1);
                savedir.Append("/");
                int t2 = (int)((double)userid / (double)200);
                savedir.Append(t2);
                savedir.Append("/");
                if (!Directory.Exists(Utils.GetMapPath(savedir.ToString())))
                    Utils.CreateDir(Utils.GetMapPath(savedir.ToString()));

                string newfilename = savedir.ToString() + userid.ToString() + fileextname;

                if (HttpContext.Current.Request.Files[0].ContentLength <= MaxAllowFileSize)
                {
                    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".jpg");
                    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".gif");
                    File.Delete(Utils.GetMapPath(savedir.ToString()) + userid.ToString() + ".png");

                    HttpContext.Current.Request.Files[0].SaveAs(Utils.GetMapPath(newfilename));
                    return newfilename;
                }
            }
            return "";
        }

        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="watermarkFilename">水印文件名</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void AddImageSignPic(Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            Graphics g = Graphics.FromImage(img);
            //设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image watermark = new Bitmap(watermarkFilename);

            if (watermark.Height >= img.Height || watermark.Width >= img.Width)
                return;

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5F;
            if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
                transparency = (watermarkTransparency / 10.0F);


            float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
            }

            g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                    ici = codec;
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
                quality = 80;

            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
                img.Save(filename, ici, encoderParams);
            else
                img.Save(filename);

            g.Dispose();
            img.Dispose();
            watermark.Dispose();
            imageAttributes.Dispose();
        }


        /// <summary>
        /// 增加图片文字水印
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void AddImageSignText(Image img, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
        {
            Graphics g = Graphics.FromImage(img);
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);

            float xpos = 0;
            float ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (float)img.Width * (float).01;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 2:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = (float)img.Height * (float).01;
                    break;
                case 3:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 4:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 5:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 6:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 7:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 8:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 9:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
            }

            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                    ici = codec;
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
                quality = 80;

            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
                img.Save(filename, ici, encoderParams);
            else
                img.Save(filename);

            g.Dispose();
            img.Dispose();
        }

        /// <summary>
        /// 判断是否有上传的文件
        /// </summary>
        /// <returns>是否有上传的文件</returns>
        public static bool IsPostFile()
        {
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                if (HttpContext.Current.Request.Files[i].FileName != "")
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 返回星星图片html
        /// </summary>
        /// <param name="starcount">星星总数</param>
        /// <param name="starthreshold">星星阀值</param>
        /// <returns>星星图片html</returns>
        public static string GetStarImg(int starcount, int starthreshold)
        {
            StringBuilder sb = new StringBuilder();
            int len = starcount / (starthreshold * starthreshold);
            for (int i = 0; i < len; i++)
            {
                sb.Append("<img src=\"star_level3.gif\" />");
            }
            starcount = starcount - len * starthreshold * starthreshold;

            len = starcount / starthreshold;
            for (int i = 0; i < len; i++)
            {
                sb.Append("<img src=\"star_level2.gif\" />");
            }
            starcount = starcount - len * starthreshold;

            for (int i = 0; i < starcount; i++)
            {
                sb.Append("<img src=\"star_level1.gif\" />");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 替换原始字符串中的脏字词语
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns>替换后的结果</returns>
        public static string BanWordFilter(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            string[,] str = Caches.GetBanWordList();
            int count = str.Length / 2;
            for (int i = 0; i < count; i++)
            {
                if (str[i, 1] != "{BANNED}" && str[i, 1] != "{MOD}")
                {
                    sb = new StringBuilder().Append(
                                  Regex.Replace(sb.ToString(), str[i, 0].Replace("*", "\\*"),
                                        str[i, 1],
                                        Utils.GetRegexCompiledOptions()));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回当前页面是否是跨站提交
        /// </summary>
        /// <returns>当前页面是否是跨站提交</returns>
        public static bool IsCrossSitePost()
        {
            // 如果不是提交则为true
            if (!SASRequest.IsPost())
                return true;

            return IsCrossSitePost(SASRequest.GetUrlReferrer(), SASRequest.GetHost());
        }

        /// <summary>
        /// 判断是否是跨站提交
        /// </summary>
        /// <param name="urlReferrer">上个页面地址</param>
        /// <param name="host">论坛url</param>
        /// <returns>bool</returns>
        public static bool IsCrossSitePost(string urlReferrer, string host)
        {
            if (urlReferrer.Length < 7)
                return true;

            Uri u = new Uri(urlReferrer);
            return u.Host != host;
        }

        /// <summary>
        /// 是否是过滤的用户名
        /// </summary>
        /// <param name="str"></param>
        /// <param name="stringarray"></param>
        /// <returns>bool</returns>
        public static bool IsBanUsername(string str, string stringarray)
        {
            if (Utils.StrIsNullOrEmpty(stringarray))
                return false;

            stringarray = Regex.Escape(stringarray).Replace(@"\*", @"[\s\S]*");
            Regex r;
            foreach (string strarray in Utils.SplitString(stringarray, "\\n"))
            {
                r = new Regex(string.Format("^{0}$", strarray), RegexOptions.IgnoreCase);
                if (r.IsMatch(str) && (!strarray.Trim().Equals("")))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得Assembly版本号
        /// </summary>
        /// <returns>string</returns>
        public static string GetAssemblyVersion()
        {
            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return string.Format("{0}.{1}.{2}", myFileVersion.FileMajorPart, myFileVersion.FileMinorPart, myFileVersion.FileBuildPart);
        }

        /// <summary>
        /// 帖子中是否包含[hide]...[/hide]
        /// </summary>
        /// <param name="str">帖子内容</param>
        /// <returns>是否包含</returns>
        public static bool IsHidePost(string str)
        {
            return (str.IndexOf("[hide]") >= 0) && (str.IndexOf("[/hide]") > 0);
        }

        /// <summary>
        /// 返回显示魔法表情flash层的xhtml字符串
        /// </summary>
        /// <param name="magic">魔法表情id</param>
        /// <returns>string</returns>
        public static string ShowTopicMagic(int magic)
        {
            if (magic <= 0)
                return "";

            return "\r\n<!-- DNT Magic (ID:" + magic.ToString() + ") -->\r\n<object width=\"400\" height=\"300\" id=\"dntmagic\" classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" align=\"middle\" style=\"position:absolute;z-index:111;display:none;\"> \r\n<param name=\"allowScriptAccess\" value=\"sameDomain\" />\r\n<param name=\"movie\" value=\"magic/swf/" + magic.ToString() + ".swf\" />\r\n<param name=\"loop\" value=\"false\" />\r\n<param name=\"menu\" value=\"false\" />\r\n<param name=\"quality\" value=\"high\" />\r\n<param name=\"scale\" value=\"noscale\" />\r\n<param name=\"salign\" value=\"lt\" />\r\n<param name=\"wmode\" value=\"transparent\" /><param name=\"PLAY\" value=\"false\" /> \r\n<embed src=\"magic/swf/" + magic.ToString() + ".swf\" width=\"400\" height=\"300\" loop=\"false\" align=\"middle\" menu=\"false\" quality=\"high\" scale=\"noscale\" salign=\"lt\" wmode=\"transparent\" play=\"false\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" /> \r\n</object>\r\n<script language=\"javascript\">\r\nfunction $(id){\r\n\treturn document.getElementById(id);\r\n}\r\nfunction playFlash(){\r\n\tdivElement = $('dntmagic');\r\n\tdivElement.style.display = '';\r\n\tdivElement.style.left = (document.documentElement.clientWidth /2 - parseInt(divElement.offsetWidth)/2)+\"px\";\r\n\tdivElement.style.top = (document.documentElement.clientHeight /2 - parseInt(divElement.offsetHeight)/2 )+\"px\";\r\n\tsetTimeout(\"hiddenFlash()\", 5000);\r\n}\r\n\r\nfunction hiddenFlash() {\r\n\t$('dntmagic').style.display = 'none';\r\n}\r\nplayFlash();\r\n</script>\r\n<!-- DNT Magic End -->\r\n";
        }

        /// <summary>
        /// 从cookie中获取转向页
        /// </summary>
        /// <returns>string</returns>
        public static string GetReUrl()
        {
            if (SASRequest.GetString("reurl").Trim() != "")
            {
                Utils.WriteCookie("reurl", SASRequest.GetString("reurl").Trim());
                return SASRequest.GetString("reurl").Trim();
            }
            else
            {
                if (Utils.GetCookie("reurl") == "")
                    return "index.aspx";
                else
                    return Utils.GetCookie("reurl");
            }
        }

        /// <summary>
        /// 是否为有效域
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(string host)
        {
            if (host.IndexOf(".") == -1)
                return false;

            return new Regex(@"^\d+$").IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }

        /// <summary>
        /// 更新路径url串中的扩展名
        /// </summary>
        /// <param name="pathlist">路径url串</param>
        /// <param name="extname">扩展名</param>
        /// <returns>string</returns>
        public static string UpdatePathListExtname(string pathlist, string extname)
        {
            return pathlist.Replace("{extname}", extname);
        }

        public static void CreateTextImage(string filename, string watermarkText, int quality, string fontname, int fontsize, Color fontcolor)
        {
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);

            Bitmap bmp = new Bitmap(100, 50);
            Graphics g = Graphics.FromImage(bmp);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);
            bmp = new Bitmap((int)crSize.Width, (int)crSize.Height);

            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, crSize.Width, crSize.Height);

            float xpos = 0;
            float ypos = 0;

            g.DrawString(watermarkText, drawFont, new SolidBrush(fontcolor), xpos, ypos);

            bmp.Save(filename, ImageFormat.Png);
            g.Dispose();
            bmp.Dispose();
        }		

    }
}
