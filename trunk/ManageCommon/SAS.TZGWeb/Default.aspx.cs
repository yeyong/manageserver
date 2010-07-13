using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SAS.Logic;
using SAS.Plugin;
using SAS.Plugin.TaoBao;
using SAS.Entity.Domain;
using SAS.Taobao;
using SAS.Taobao.Request;

public partial class _Default : TaoBaoPage
{
    protected long itemcount = 0;
    //protected List<ItemCat> taoitemcatlist = TaoBaos.GetItemCatCache();

    protected override void ShowPage()
    {
        NTWXmlRestClient client = new NTWXmlRestClient("http://gw.api.taobao.com/router/rest", "12005076", "64292c42ca49632200289324fba42572");
        pagetitle = "淘之够首页";
        TaobaokeItemsGetRequest tgr = new TaobaokeItemsGetRequest();
        tgr.Fields = "iid,num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,keyword_click_url";
        tgr.Nick = "yeyong2086521";
        tgr.Cid = 0;
        //tgr.Keyword = "a";
        PageList<TaobaokeItem> tbi = client.TaobaokeItemsGet(tgr);
        //itemcount = TaoBaos.GetItemCatCache().Count;
        //TaoBaos.GetItemList(50012910, "", "", "", "", "", "", "", "", "", 20, 1, out itemcount);
        itemcount = tbi.TotalResults;
    }
}
