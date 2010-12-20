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
    /// 专题API操作
    /// </summary>
    public class Activitys : ActionBase
    {
        /// <summary>
        /// 活动专题信息集合
        /// </summary>
        /// <returns></returns>
        public string GetActivityList()
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

            if (!CheckRequiredParams("anums,atype"))
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return "";
            }

            int nums = GetIntParam("anums", 10);
            int atype = GetIntParam("atype", 0);

            List<SAS.Entity.ActivityInfo> ainfos = new List<SAS.Entity.ActivityInfo>();            

            if (atype == 0)
            {
                ainfos = Data.DataProvider.Activities.GetActvitiesByType(nums, SAS.Entity.ActivityType.IndexActivity);
            }
            else
            {
                ainfos = Data.DataProvider.Activities.GetActvitiesByType(nums, SAS.Entity.ActivityType.TaobaoActivity);
            }

            ActivityGetListResponse aglr = new ActivityGetListResponse();
            List<ActivityInfo> alist = new List<ActivityInfo>();

            foreach (SAS.Entity.ActivityInfo ainfo in ainfos)
            {
                ActivityInfo info = new ActivityInfo();
                info.Aid = ainfo.Id;
                info.Atitle = ainfo.Atitle;
                alist.Add(info);
            }

            aglr.Anums = alist.Count;
            aglr.ActivityList = alist.ToArray();

            if (Format == FormatType.JSON)
            {
                return JavaScriptConvert.SerializeObject(aglr);
            }
            return SerializationHelper.Serialize(aglr);
        }
    }
}
