using System;
using System.Data;
using System.Data.Common;
using System.Web;
using System.IO;

using SAS.Logic;
using SAS.Common;

namespace SAS.Web.UI
{
    /// <summary>
    /// 头像页面类
    /// </summary>
    public class Avatar : BasePage
    {
        public Avatar()
        {
            AvatarSize avatarSize;
            switch (SASRequest.GetString("size").ToLower())
            {
                case "large":
                    {
                        avatarSize = AvatarSize.Large;
                        break;
                    }
                case "medium":
                    {
                        avatarSize = AvatarSize.Medium;
                        break;
                    }
                case "small":
                    {
                        avatarSize = AvatarSize.Small;
                        break;
                    }
                default:
                    {
                        avatarSize = AvatarSize.Medium;
                        break;
                    }
            }
            string avatarUrl = Avatars.GetAvatarUrl(SASRequest.GetString("uid"), avatarSize);
            HttpContext.Current.Response.Redirect(avatarUrl);
        }
    }
}
