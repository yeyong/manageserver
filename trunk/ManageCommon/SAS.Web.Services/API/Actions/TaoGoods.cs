using System;
using System.Text;
using System.Collections.Generic;

using SAS.Common;
using SAS.Config;
using SAS.Data;
using SAS.Plugin.TaoBao;
using Newtonsoft.Json;

namespace SAS.Web.Services.API.Actions
{
    /// <summary>
    /// 淘宝商品操作
    /// </summary>
    public class TaoGoods : ActionBase
    {
        TaoBaoPluginBase tbpb = TaoBaoPluginProvider.GetInstance();

        public string GetShangjiGoods()
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

            List<SAS.Entity.TempGoodsWithCat> tgwclist = tbpb.GetShangjiGoods();
            ShangjiGoodsGetListResponse sgglr = new ShangjiGoodsGetListResponse();
            List<TaoBaoGoodInfo> tbglist = new List<TaoBaoGoodInfo>();

            foreach (SAS.Entity.TempGoodsWithCat tgwcinfo in tgwclist)
            {
                TaoBaoGoodInfo tbginfo = new TaoBaoGoodInfo();
                tbginfo.Gid = tgwcinfo.ID;
                tbginfo.GNumiid = tgwcinfo.GoodID;
                tbginfo.GName = tgwcinfo.GoodName;
                tbginfo.Cid = tgwcinfo.CatID;
                tbginfo.CName = tgwcinfo.CatName;
                tbginfo.GPic = tgwcinfo.PicUrl;
                tbglist.Add(tbginfo);
            }

            sgglr.Anums = tbglist.Count;
            sgglr.TBGI = tbglist.ToArray();

            if (Format == FormatType.JSON)
            {
                return JavaScriptConvert.SerializeObject(sgglr);
            }
            return SerializationHelper.Serialize(sgglr);
        }
    }
}
