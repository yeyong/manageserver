using System;
using System.Text;

using SAS.Common;
using SAS.Config;
using SAS.Logic;
using SAS.Common.Generic;
using Newtonsoft.Json;

namespace SAS.Web.Services.API.Actions
{
    /// <summary>
    /// 企业API数据操作动作
    /// </summary>
    public class Companys : ActionBase
    {
        /// <summary>
        /// 获取企业信息列表
        /// </summary>
        /// <returns></returns>
        public string GetCompanyList()
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

            if (!CheckRequiredParams("cnums,ordercol"))
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return "";
            }

            int cnums = GetIntParam("cnums", 10);
            string orderby = GetParam("ordercol") == null ? "" : GetParam("ordercol").ToString();

            List<SAS.Entity.Companys> clist = new List<SAS.Entity.Companys>();

            switch (orderby)
            {
                case "credit":
                    clist = SAS.Data.DataProvider.Companies.GetCompanyListByOrder(cnums, "en_credits", true);
                    break;
                default:
                    break;
            }

            CompanyGetListResponse gglr = new CompanyGetListResponse();
            List<CompanyInfo> cilist = new List<CompanyInfo>();

            foreach (SAS.Entity.Companys cmodel in clist)
            {
                CompanyInfo cinfo = new CompanyInfo();
                cinfo.Enid = cmodel.En_id;
                cinfo.Ename = cmodel.En_name;
                cinfo.Enaccesses = cmodel.En_accesses;
                cinfo.Encredits = cmodel.En_credits;

                if (cmodel.En_cataloglist.Split(',').Length > 0)
                {
                    cinfo.Encatalogid = Utils.StrToInt(cmodel.En_cataloglist.Split(',')[0], 0);
                }

                SAS.Entity.CatalogInfo catainfo = Catalogs.GetCatalogCacheInfo(cinfo.Encatalogid);
                cinfo.Encatalogname = catainfo == null ? string.Empty:catainfo.name;
                cilist.Add(cinfo);
            }

            gglr.Cnums = cilist.Count;
            gglr.CompanyList = cilist.ToArray();

            if (Format == FormatType.JSON)
            {
                return JavaScriptConvert.SerializeObject(gglr);
            }
            return SerializationHelper.Serialize(gglr);
        }
    }
}
