﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Config;
using SAS.Common;
using SAS.Logic;
using SAS.Entity;
using SAS.Taobao;

public partial class shopshow : TaoBaoPage
{
    protected ShopDetailInfo sinfo = new ShopDetailInfo();
    protected string sid = SASRequest.GetString("sid");

    protected override void ShowPage()
    {
        if (string.IsNullOrEmpty(sid))
        {
            AddErrLine("店铺信息展示错误！");
            SetMetaRefresh(2, LogicUtils.GetReUrl());
            return;
        }

        sinfo = TaoBaos.GetTaoBaoShopInfo(sid);

        if (sinfo == null)
        {
            AddErrLine("店铺信息展示错误！");
            SetMetaRefresh(2, LogicUtils.GetReUrl());
            return;
        }

        pagetitle = string.Format("{0}-{0}店铺详细介绍", sinfo.title);
        seokeyword = string.Format("{0}介绍,{0},{1}", sinfo.title, sinfo.nick);
        seodescription = string.Format("{0}店铺详细介绍。{1}。", sinfo.title, Utils.CutString(Utils.RemoveHtml(sinfo.shop_desc), 60));
    }

    protected string Resore(string scores)
    {
        decimal dd = decimal.Parse(scores);
        dd = decimal.Round(dd * 2, 0);
        return (10 - dd + 1).ToString();
    }
}
