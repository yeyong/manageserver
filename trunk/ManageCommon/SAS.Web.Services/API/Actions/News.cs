using System;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Common.Generic;
using Newtonsoft.Json;

namespace SAS.Web.Services.API.Actions
{
    /// <summary>
    /// 资讯API操作
    /// </summary>
    public class News : ActionBase
    {

        /// <summary>
        /// 新闻资讯信息集合
        /// </summary>
        /// <returns></returns>
        public string GetNewsList()
        {
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return "";
            }

            //如果是桌面程序则需要验证用户身份
            if (this.App.ApplicationType == (int)ApplicationType.DESKTOP)
            {
                if (Uid < 1)
                {
                    ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
                    return "";
                }
            }

            if (CallId <= LastCallId)
            {
                ErrorCode = (int)ErrorType.API_EC_CALLID;
                return "";
            }

            List<SAS.Entity.NewsContent> newslist = new List<SAS.Entity.NewsContent>();
            newslist = SAS.Logic.News.GetShangJiNews();

            NewsGetListResponse ngr = new NewsGetListResponse();
            List<NewsInfo> nlist = new List<NewsInfo>();

            foreach (SAS.Entity.NewsContent newsc in newslist)
            {
                NewsInfo ninfo = new NewsInfo();
                ninfo.Nid = newsc.ID;
                ninfo.NewsID = newsc.NewsID;
                ninfo.NewsTitle = newsc.NewsTitle;
                ninfo.NewsPic = newsc.NewsSPic;
                ninfo.NewsUrl = newsc.NewsUrl;
                nlist.Add(ninfo);
            }

            ngr.Anums = nlist.Count;
            ngr.NewsList = nlist.ToArray();

            if (Format == FormatType.JSON)
            {
                return JavaScriptConvert.SerializeObject(ngr);
            }
            return SerializationHelper.Serialize(ngr);
        }
    }
}
