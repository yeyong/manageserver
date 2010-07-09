using System;

using SAS.Taobao.Request;
using SAS.Taobao.Parser;
using SAS.Taobao.Domain;

namespace SAS.Taobao
{
    /// <summary>
    /// 基于REST的XML客户端帮助类。
    /// </summary>
    /// <remarks>
    /// 带<c>session</c>参数的方法表明此方法为私有数据接口，需要传入正确的授权会话码才能访问。
    /// </remarks>
    /// <example><code>
    /// TopXmlRestClient client = new TopXmlRestClient("http://gw.api.tbsandbox.com/router/rest", "test", "test");
    /// UserGetRequest request = new UserGetRequest();
    /// request.Fields = "user_id,nick,sex,created,location";
    /// request.Nick = "tbtest520";
    /// User user = client.UserGet(request);
    /// </code></example>
    public class NTWXmlRestClient
    {
        private INTWClient client;

        public NTWXmlRestClient(string topUrl, string appKey, string appSecret)
        {
            client = new NTWRestClient(topUrl, appKey, appSecret, NTWRestClient.FORMAT_XML);
        }

        /// <summary>
        /// All PUBLIC TOP APIs
        /// </summary>
        public string GetResponse(INTWRequest request)
        {
            return client.Execute(request, new StringParser());
        }

        /// <summary>
        /// All PRIVATE TOP APIs
        /// </summary>
        public string GetResponse(INTWRequest request, string session)
        {
            return client.Execute(request, new StringParser(), session);
        }

        /// <summary>
        /// TOP API: taobao.app.subapp.apply
        /// </summary>
        public Tadget AppSubappApply(AppSubappApplyRequest request) {
            return client.Execute(request, new ObjectXmlParser<Tadget>(new ParseData(request.GetApiName(), "tadget")));
        }

        /// <summary>
        /// TOP API: taobao.app.subapp.apply
        /// </summary>
        public Tadget AppSubappApply(AppSubappApplyRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Tadget>(new ParseData(request.GetApiName(), "tadget")), session);
        }

        /// <summary>
        /// TOP API: taobao.areas.get
        /// </summary>
        public PageList<Area> AreasGet(AreasGetRequest request) {
            return client.Execute(request, new ListXmlParser<Area>(new ParseData(request.GetApiName(), "areas", "area")));
        }

        /// <summary>
        /// TOP API: taobao.areas.get
        /// </summary>
        public PageList<Area> AreasGet(AreasGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Area>(new ParseData(request.GetApiName(), "areas", "area")), session);
        }

        /// <summary>
        /// TOP API: taobao.delivery.send
        /// </summary>
        public Shipping DeliverySend(DeliverySendRequest request) {
            return client.Execute(request, new ObjectXmlParser<Shipping>(new ParseData(request.GetApiName(), "shipping")));
        }

        /// <summary>
        /// TOP API: taobao.delivery.send
        /// </summary>
        public Shipping DeliverySend(DeliverySendRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Shipping>(new ParseData(request.GetApiName(), "shipping")), session);
        }

        /// <summary>
        /// TOP API: taobao.icp.id.submit
        /// </summary>
        public void IcpIdSubmit(IcpIdSubmitRequest request) {
            client.Execute(request, new StringParser());
        }

        /// <summary>
        /// TOP API: taobao.icp.id.submit
        /// </summary>
        public void IcpIdSubmit(IcpIdSubmitRequest request, string session) {
            client.Execute(request, new StringParser(), session);
        }

        /// <summary>
        /// TOP API: taobao.icp.submit
        /// </summary>
        public void IcpSubmit(IcpSubmitRequest request) {
            client.Execute(request, new StringParser());
        }

        /// <summary>
        /// TOP API: taobao.icp.submit
        /// </summary>
        public void IcpSubmit(IcpSubmitRequest request, string session) {
            client.Execute(request, new StringParser(), session);
        }

        /// <summary>
        /// TOP API: taobao.item.add
        /// </summary>
        public Item ItemAdd(ItemAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.add
        /// </summary>
        public Item ItemAdd(ItemAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.delete
        /// </summary>
        public Item ItemDelete(ItemDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.delete
        /// </summary>
        public Item ItemDelete(ItemDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.game.add
        /// </summary>
        public Item ItemGameAdd(ItemGameAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.game.add
        /// </summary>
        public Item ItemGameAdd(ItemGameAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.get
        /// </summary>
        public Item ItemGet(ItemGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.get
        /// </summary>
        public Item ItemGet(ItemGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.img.delete
        /// </summary>
        public ItemImg ItemImgDelete(ItemImgDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemImg>(new ParseData(request.GetApiName(), "item_img")));
        }

        /// <summary>
        /// TOP API: taobao.item.img.delete
        /// </summary>
        public ItemImg ItemImgDelete(ItemImgDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemImg>(new ParseData(request.GetApiName(), "item_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.img.upload
        /// </summary>
        public ItemImg ItemImgUpload(ItemImgUploadRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemImg>(new ParseData(request.GetApiName(), "item_img")));
        }

        /// <summary>
        /// TOP API: taobao.item.img.upload
        /// </summary>
        public ItemImg ItemImgUpload(ItemImgUploadRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemImg>(new ParseData(request.GetApiName(), "item_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.joint.img
        /// </summary>
        public ItemImg ItemJointImg(ItemJointImgRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemImg>(new ParseData(request.GetApiName(), "item_img")));
        }

        /// <summary>
        /// TOP API: taobao.item.joint.img
        /// </summary>
        public ItemImg ItemJointImg(ItemJointImgRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemImg>(new ParseData(request.GetApiName(), "item_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.joint.propimg
        /// </summary>
        public PropImg ItemJointPropimg(ItemJointPropimgRequest request) {
            return client.Execute(request, new ObjectXmlParser<PropImg>(new ParseData(request.GetApiName(), "prop_img")));
        }

        /// <summary>
        /// TOP API: taobao.item.joint.propimg
        /// </summary>
        public PropImg ItemJointPropimg(ItemJointPropimgRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<PropImg>(new ParseData(request.GetApiName(), "prop_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.propimg.delete
        /// </summary>
        public PropImg ItemPropimgDelete(ItemPropimgDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<PropImg>(new ParseData(request.GetApiName(), "prop_img")));
        }

        /// <summary>
        /// TOP API: taobao.item.propimg.delete
        /// </summary>
        public PropImg ItemPropimgDelete(ItemPropimgDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<PropImg>(new ParseData(request.GetApiName(), "prop_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.propimg.upload
        /// </summary>
        public PropImg ItemPropimgUpload(ItemPropimgUploadRequest request) {
            return client.Execute(request, new ObjectXmlParser<PropImg>(new ParseData(request.GetApiName(), "prop_img")));
        }

        /// <summary>
        /// TOP API: taobao.item.propimg.upload
        /// </summary>
        public PropImg ItemPropimgUpload(ItemPropimgUploadRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<PropImg>(new ParseData(request.GetApiName(), "prop_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.recommend.add
        /// </summary>
        public Item ItemRecommendAdd(ItemRecommendAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.recommend.add
        /// </summary>
        public Item ItemRecommendAdd(ItemRecommendAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.recommend.delete
        /// </summary>
        public Item ItemRecommendDelete(ItemRecommendDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.recommend.delete
        /// </summary>
        public Item ItemRecommendDelete(ItemRecommendDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.sku.add
        /// </summary>
        public Sku ItemSkuAdd(ItemSkuAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")));
        }

        /// <summary>
        /// TOP API: taobao.item.sku.add
        /// </summary>
        public Sku ItemSkuAdd(ItemSkuAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.sku.delete
        /// </summary>
        public Sku ItemSkuDelete(ItemSkuDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")));
        }

        /// <summary>
        /// TOP API: taobao.item.sku.delete
        /// </summary>
        public Sku ItemSkuDelete(ItemSkuDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.sku.get
        /// </summary>
        public Sku ItemSkuGet(ItemSkuGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")));
        }

        /// <summary>
        /// TOP API: taobao.item.sku.get
        /// </summary>
        public Sku ItemSkuGet(ItemSkuGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.sku.update
        /// </summary>
        public Sku ItemSkuUpdate(ItemSkuUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")));
        }

        /// <summary>
        /// TOP API: taobao.item.sku.update
        /// </summary>
        public Sku ItemSkuUpdate(ItemSkuUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Sku>(new ParseData(request.GetApiName(), "sku")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.skus.get
        /// </summary>
        public PageList<Sku> ItemSkusGet(ItemSkusGetRequest request) {
            return client.Execute(request, new ListXmlParser<Sku>(new ParseData(request.GetApiName(), "skus", "sku")));
        }

        /// <summary>
        /// TOP API: taobao.item.skus.get
        /// </summary>
        public PageList<Sku> ItemSkusGet(ItemSkusGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Sku>(new ParseData(request.GetApiName(), "skus", "sku")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.update
        /// </summary>
        public Item ItemUpdate(ItemUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.update
        /// </summary>
        public Item ItemUpdate(ItemUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.update.delisting
        /// </summary>
        public Item ItemUpdateDelisting(ItemUpdateDelistingRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.update.delisting
        /// </summary>
        public Item ItemUpdateDelisting(ItemUpdateDelistingRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.update.listing
        /// </summary>
        public Item ItemUpdateListing(ItemUpdateListingRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.update.listing
        /// </summary>
        public Item ItemUpdateListing(ItemUpdateListingRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.video.delete
        /// </summary>
        public Video ItemVideoDelete(ItemVideoDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<Video>(new ParseData(request.GetApiName(), "video")));
        }

        /// <summary>
        /// TOP API: taobao.item.video.delete
        /// </summary>
        public Video ItemVideoDelete(ItemVideoDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Video>(new ParseData(request.GetApiName(), "video")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.video.upload
        /// </summary>
        public Video ItemVideoUpload(ItemVideoUploadRequest request) {
            return client.Execute(request, new ObjectXmlParser<Video>(new ParseData(request.GetApiName(), "video")));
        }

        /// <summary>
        /// TOP API: taobao.item.video.upload
        /// </summary>
        public Video ItemVideoUpload(ItemVideoUploadRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Video>(new ParseData(request.GetApiName(), "video")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.vip.add
        /// </summary>
        public Item ItemVipAdd(ItemVipAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.vip.add
        /// </summary>
        public Item ItemVipAdd(ItemVipAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.item.vip.update
        /// </summary>
        public Item ItemVipUpdate(ItemVipUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")));
        }

        /// <summary>
        /// TOP API: taobao.item.vip.update
        /// </summary>
        public Item ItemVipUpdate(ItemVipUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Item>(new ParseData(request.GetApiName(), "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemcat.features.get
        /// </summary>
        public PageList<Feature> ItemcatFeaturesGet(ItemcatFeaturesGetRequest request) {
            return client.Execute(request, new ListXmlParser<Feature>(new ParseData(request.GetApiName(), "features", "feature")));
        }

        /// <summary>
        /// TOP API: taobao.itemcat.features.get
        /// </summary>
        public PageList<Feature> ItemcatFeaturesGet(ItemcatFeaturesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Feature>(new ParseData(request.GetApiName(), "features", "feature")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemcats.authorize.get
        /// </summary>
        public SellerAuthorize ItemcatsAuthorizeGet(ItemcatsAuthorizeGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<SellerAuthorize>(new ParseData(request.GetApiName(), "seller_authorize")));
        }

        /// <summary>
        /// TOP API: taobao.itemcats.authorize.get
        /// </summary>
        public SellerAuthorize ItemcatsAuthorizeGet(ItemcatsAuthorizeGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<SellerAuthorize>(new ParseData(request.GetApiName(), "seller_authorize")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemcats.get
        /// </summary>
        public PageList<ItemCat> ItemcatsGet(ItemcatsGetRequest request) {
            return client.Execute(request, new ListXmlParser<ItemCat>(new ParseData(request.GetApiName(), "item_cats", "item_cat")));
        }

        /// <summary>
        /// TOP API: taobao.itemcats.get
        /// </summary>
        public PageList<ItemCat> ItemcatsGet(ItemcatsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ItemCat>(new ParseData(request.GetApiName(), "item_cats", "item_cat")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemextra.add
        /// </summary>
        public ItemExtra ItemextraAdd(ItemextraAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extra")));
        }

        /// <summary>
        /// TOP API: taobao.itemextra.add
        /// </summary>
        public ItemExtra ItemextraAdd(ItemextraAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extra")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemextra.get
        /// </summary>
        public ItemExtra ItemextraGet(ItemextraGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extra")));
        }

        /// <summary>
        /// TOP API: taobao.itemextra.get
        /// </summary>
        public ItemExtra ItemextraGet(ItemextraGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extra")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemextra.update
        /// </summary>
        public ItemExtra ItemextraUpdate(ItemextraUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extra")));
        }

        /// <summary>
        /// TOP API: taobao.itemextra.update
        /// </summary>
        public ItemExtra ItemextraUpdate(ItemextraUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extra")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemextras.get
        /// </summary>
        public PageList<ItemExtra> ItemextrasGet(ItemextrasGetRequest request) {
            return client.Execute(request, new ListXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extras", "item_extra")));
        }

        /// <summary>
        /// TOP API: taobao.itemextras.get
        /// </summary>
        public PageList<ItemExtra> ItemextrasGet(ItemextrasGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extras", "item_extra")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemextras.instant.search
        /// </summary>
        public PageList<ItemExtra> ItemextrasInstantSearch(ItemextrasInstantSearchRequest request) {
            return client.Execute(request, new ListXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extras", "item_extra")));
        }

        /// <summary>
        /// TOP API: taobao.itemextras.instant.search
        /// </summary>
        public PageList<ItemExtra> ItemextrasInstantSearch(ItemextrasInstantSearchRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extras", "item_extra")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemextras.search
        /// </summary>
        public PageList<ItemExtra> ItemextrasSearch(ItemextrasSearchRequest request) {
            return client.Execute(request, new ListXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extras", "item_extra")));
        }

        /// <summary>
        /// TOP API: taobao.itemextras.search
        /// </summary>
        public PageList<ItemExtra> ItemextrasSearch(ItemextrasSearchRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ItemExtra>(new ParseData(request.GetApiName(), "item_extras", "item_extra")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemprops.get
        /// </summary>
        public PageList<ItemProp> ItempropsGet(ItempropsGetRequest request) {
            return client.Execute(request, new ListXmlParser<ItemProp>(new ParseData(request.GetApiName(), "item_props", "item_prop")));
        }

        /// <summary>
        /// TOP API: taobao.itemprops.get
        /// </summary>
        public PageList<ItemProp> ItempropsGet(ItempropsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ItemProp>(new ParseData(request.GetApiName(), "item_props", "item_prop")), session);
        }

        /// <summary>
        /// TOP API: taobao.itemprops.vertical.get
        /// </summary>
        public PageList<ItemVerticalProp> ItempropsVerticalGet(ItempropsVerticalGetRequest request) {
            return client.Execute(request, new ListXmlParser<ItemVerticalProp>(new ParseData(request.GetApiName(), "item_vertical_props", "item_vertical_prop")));
        }

        /// <summary>
        /// TOP API: taobao.itemprops.vertical.get
        /// </summary>
        public PageList<ItemVerticalProp> ItempropsVerticalGet(ItempropsVerticalGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ItemVerticalProp>(new ParseData(request.GetApiName(), "item_vertical_props", "item_vertical_prop")), session);
        }

        /// <summary>
        /// TOP API: taobao.itempropvalues.get
        /// </summary>
        public PageList<PropValue> ItempropvaluesGet(ItempropvaluesGetRequest request) {
            return client.Execute(request, new ListXmlParser<PropValue>(new ParseData(request.GetApiName(), "prop_values", "prop_value")));
        }

        /// <summary>
        /// TOP API: taobao.itempropvalues.get
        /// </summary>
        public PageList<PropValue> ItempropvaluesGet(ItempropvaluesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<PropValue>(new ParseData(request.GetApiName(), "prop_values", "prop_value")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.all.get
        /// </summary>
        public PageList<Item> ItemsAllGet(ItemsAllGetRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.all.get
        /// </summary>
        public PageList<Item> ItemsAllGet(ItemsAllGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.custom.get
        /// </summary>
        public PageList<Item> ItemsCustomGet(ItemsCustomGetRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.custom.get
        /// </summary>
        public PageList<Item> ItemsCustomGet(ItemsCustomGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.download
        /// </summary>
        public PageList<Item> ItemsDownload(ItemsDownloadRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.download
        /// </summary>
        public PageList<Item> ItemsDownload(ItemsDownloadRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.get
        /// </summary>
        public PageList<Item> ItemsGet(ItemsGetRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.get
        /// </summary>
        public PageList<Item> ItemsGet(ItemsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.inventory.get
        /// </summary>
        public PageList<Item> ItemsInventoryGet(ItemsInventoryGetRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.inventory.get
        /// </summary>
        public PageList<Item> ItemsInventoryGet(ItemsInventoryGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.list.get
        /// </summary>
        public PageList<Item> ItemsListGet(ItemsListGetRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.list.get
        /// </summary>
        public PageList<Item> ItemsListGet(ItemsListGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.onsale.get
        /// </summary>
        public PageList<Item> ItemsOnsaleGet(ItemsOnsaleGetRequest request) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")));
        }

        /// <summary>
        /// TOP API: taobao.items.onsale.get
        /// </summary>
        public PageList<Item> ItemsOnsaleGet(ItemsOnsaleGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Item>(new ParseData(request.GetApiName(), "items", "item")), session);
        }

        /// <summary>
        /// TOP API: taobao.items.search
        /// </summary>
        public ItemSearch ItemsSearch(ItemsSearchRequest request) {
            return client.Execute(request, new ObjectXmlParser<ItemSearch>(new ParseData(request.GetApiName(), "item_search")));
        }

        /// <summary>
        /// TOP API: taobao.items.search
        /// </summary>
        public ItemSearch ItemsSearch(ItemsSearchRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ItemSearch>(new ParseData(request.GetApiName(), "item_search")), session);
        }

        /// <summary>
        /// TOP API: taobao.logistics.companies.get
        /// </summary>
        public PageList<LogisticsCompany> LogisticsCompaniesGet(LogisticsCompaniesGetRequest request) {
            return client.Execute(request, new ListXmlParser<LogisticsCompany>(new ParseData(request.GetApiName(), "logistics_companies", "logistics_company")));
        }

        /// <summary>
        /// TOP API: taobao.logistics.companies.get
        /// </summary>
        public PageList<LogisticsCompany> LogisticsCompaniesGet(LogisticsCompaniesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<LogisticsCompany>(new ParseData(request.GetApiName(), "logistics_companies", "logistics_company")), session);
        }

        /// <summary>
        /// TOP API: taobao.logistics.orders.detail.get
        /// </summary>
        public PageList<Shipping> LogisticsOrdersDetailGet(LogisticsOrdersDetailGetRequest request) {
            return client.Execute(request, new ListXmlParser<Shipping>(new ParseData(request.GetApiName(), "shippings", "shipping")));
        }

        /// <summary>
        /// TOP API: taobao.logistics.orders.detail.get
        /// </summary>
        public PageList<Shipping> LogisticsOrdersDetailGet(LogisticsOrdersDetailGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Shipping>(new ParseData(request.GetApiName(), "shippings", "shipping")), session);
        }

        /// <summary>
        /// TOP API: taobao.logistics.orders.get
        /// </summary>
        public PageList<Shipping> LogisticsOrdersGet(LogisticsOrdersGetRequest request) {
            return client.Execute(request, new ListXmlParser<Shipping>(new ParseData(request.GetApiName(), "shippings", "shipping")));
        }

        /// <summary>
        /// TOP API: taobao.logistics.orders.get
        /// </summary>
        public PageList<Shipping> LogisticsOrdersGet(LogisticsOrdersGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Shipping>(new ParseData(request.GetApiName(), "shippings", "shipping")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.app.subscribe
        /// </summary>
        public SubscribeMessage NotifyAppSubscribe(NotifyAppSubscribeRequest request) {
            return client.Execute(request, new ObjectXmlParser<SubscribeMessage>(new ParseData(request.GetApiName(), "subscribe_message")));
        }

        /// <summary>
        /// TOP API: taobao.notify.app.subscribe
        /// </summary>
        public SubscribeMessage NotifyAppSubscribe(NotifyAppSubscribeRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<SubscribeMessage>(new ParseData(request.GetApiName(), "subscribe_message")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.authorizemessages.get
        /// </summary>
        public PageList<AuthorizeMessage> NotifyAuthorizemessagesGet(NotifyAuthorizemessagesGetRequest request) {
            return client.Execute(request, new ListXmlParser<AuthorizeMessage>(new ParseData(request.GetApiName(), "authorize_messages", "authorize_message")));
        }

        /// <summary>
        /// TOP API: taobao.notify.authorizemessages.get
        /// </summary>
        public PageList<AuthorizeMessage> NotifyAuthorizemessagesGet(NotifyAuthorizemessagesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<AuthorizeMessage>(new ParseData(request.GetApiName(), "authorize_messages", "authorize_message")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.items.get
        /// </summary>
        public PageList<NotifyItem> NotifyItemsGet(NotifyItemsGetRequest request) {
            return client.Execute(request, new ListXmlParser<NotifyItem>(new ParseData(request.GetApiName(), "notify_items", "notify_item")));
        }

        /// <summary>
        /// TOP API: taobao.notify.items.get
        /// </summary>
        public PageList<NotifyItem> NotifyItemsGet(NotifyItemsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<NotifyItem>(new ParseData(request.GetApiName(), "notify_items", "notify_item")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.refunds.get
        /// </summary>
        public PageList<NotifyRefund> NotifyRefundsGet(NotifyRefundsGetRequest request) {
            return client.Execute(request, new ListXmlParser<NotifyRefund>(new ParseData(request.GetApiName(), "notify_refunds", "notify_refund")));
        }

        /// <summary>
        /// TOP API: taobao.notify.refunds.get
        /// </summary>
        public PageList<NotifyRefund> NotifyRefundsGet(NotifyRefundsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<NotifyRefund>(new ParseData(request.GetApiName(), "notify_refunds", "notify_refund")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.subscribemessage.get
        /// </summary>
        public SubscribeMessage NotifySubscribemessageGet(NotifySubscribemessageGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<SubscribeMessage>(new ParseData(request.GetApiName(), "subscribe_message")));
        }

        /// <summary>
        /// TOP API: taobao.notify.subscribemessage.get
        /// </summary>
        public SubscribeMessage NotifySubscribemessageGet(NotifySubscribemessageGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<SubscribeMessage>(new ParseData(request.GetApiName(), "subscribe_message")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.trades.get
        /// </summary>
        public PageList<NotifyTrade> NotifyTradesGet(NotifyTradesGetRequest request) {
            return client.Execute(request, new ListXmlParser<NotifyTrade>(new ParseData(request.GetApiName(), "notify_trades", "notify_trade")));
        }

        /// <summary>
        /// TOP API: taobao.notify.trades.get
        /// </summary>
        public PageList<NotifyTrade> NotifyTradesGet(NotifyTradesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<NotifyTrade>(new ParseData(request.GetApiName(), "notify_trades", "notify_trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.notify.user.authorize
        /// </summary>
        public AuthorizeMessage NotifyUserAuthorize(NotifyUserAuthorizeRequest request) {
            return client.Execute(request, new ObjectXmlParser<AuthorizeMessage>(new ParseData(request.GetApiName(), "authorize_message")));
        }

        /// <summary>
        /// TOP API: taobao.notify.user.authorize
        /// </summary>
        public AuthorizeMessage NotifyUserAuthorize(NotifyUserAuthorizeRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<AuthorizeMessage>(new ParseData(request.GetApiName(), "authorize_message")), session);
        }

        /// <summary>
        /// TOP API: taobao.picture.category.add
        /// </summary>
        public void PictureCategoryAdd(PictureCategoryAddRequest request) {
            client.Execute(request, new StringParser());
        }

        /// <summary>
        /// TOP API: taobao.picture.category.add
        /// </summary>
        public void PictureCategoryAdd(PictureCategoryAddRequest request, string session) {
            client.Execute(request, new StringParser(), session);
        }

        /// <summary>
        /// TOP API: taobao.picture.category.get
        /// </summary>
        public PageList<PictureCategory> PictureCategoryGet(PictureCategoryGetRequest request) {
            return client.Execute(request, new ListXmlParser<PictureCategory>(new ParseData(request.GetApiName(), "picture_categories", "picture_category")));
        }

        /// <summary>
        /// TOP API: taobao.picture.category.get
        /// </summary>
        public PageList<PictureCategory> PictureCategoryGet(PictureCategoryGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<PictureCategory>(new ParseData(request.GetApiName(), "picture_categories", "picture_category")), session);
        }

        /// <summary>
        /// TOP API: taobao.picture.delete
        /// </summary>
        public void PictureDelete(PictureDeleteRequest request) {
            client.Execute(request, new StringParser());
        }

        /// <summary>
        /// TOP API: taobao.picture.delete
        /// </summary>
        public void PictureDelete(PictureDeleteRequest request, string session) {
            client.Execute(request, new StringParser(), session);
        }

        /// <summary>
        /// TOP API: taobao.picture.get
        /// </summary>
        public PageList<Picture> PictureGet(PictureGetRequest request) {
            return client.Execute(request, new ListXmlParser<Picture>(new ParseData(request.GetApiName(), "pictures", "picture")));
        }

        /// <summary>
        /// TOP API: taobao.picture.get
        /// </summary>
        public PageList<Picture> PictureGet(PictureGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Picture>(new ParseData(request.GetApiName(), "pictures", "picture")), session);
        }

        /// <summary>
        /// TOP API: taobao.picture.upload
        /// </summary>
        public void PictureUpload(PictureUploadRequest request) {
            client.Execute(request, new StringParser());
        }

        /// <summary>
        /// TOP API: taobao.picture.upload
        /// </summary>
        public void PictureUpload(PictureUploadRequest request, string session) {
            client.Execute(request, new StringParser(), session);
        }

        /// <summary>
        /// TOP API: taobao.postage.add
        /// </summary>
        public Postage PostageAdd(PostageAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")));
        }

        /// <summary>
        /// TOP API: taobao.postage.add
        /// </summary>
        public Postage PostageAdd(PostageAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")), session);
        }

        /// <summary>
        /// TOP API: taobao.postage.delete
        /// </summary>
        public Postage PostageDelete(PostageDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")));
        }

        /// <summary>
        /// TOP API: taobao.postage.delete
        /// </summary>
        public Postage PostageDelete(PostageDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")), session);
        }

        /// <summary>
        /// TOP API: taobao.postage.get
        /// </summary>
        public Postage PostageGet(PostageGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")));
        }

        /// <summary>
        /// TOP API: taobao.postage.get
        /// </summary>
        public Postage PostageGet(PostageGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")), session);
        }

        /// <summary>
        /// TOP API: taobao.postage.update
        /// </summary>
        public Postage PostageUpdate(PostageUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")));
        }

        /// <summary>
        /// TOP API: taobao.postage.update
        /// </summary>
        public Postage PostageUpdate(PostageUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Postage>(new ParseData(request.GetApiName(), "postage")), session);
        }

        /// <summary>
        /// TOP API: taobao.postages.get
        /// </summary>
        public PageList<Postage> PostagesGet(PostagesGetRequest request) {
            return client.Execute(request, new ListXmlParser<Postage>(new ParseData(request.GetApiName(), "postages", "postage")));
        }

        /// <summary>
        /// TOP API: taobao.postages.get
        /// </summary>
        public PageList<Postage> PostagesGet(PostagesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Postage>(new ParseData(request.GetApiName(), "postages", "postage")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.add
        /// </summary>
        public Product ProductAdd(ProductAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Product>(new ParseData(request.GetApiName(), "product")));
        }

        /// <summary>
        /// TOP API: taobao.product.add
        /// </summary>
        public Product ProductAdd(ProductAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Product>(new ParseData(request.GetApiName(), "product")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.get
        /// </summary>
        public Product ProductGet(ProductGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Product>(new ParseData(request.GetApiName(), "product")));
        }

        /// <summary>
        /// TOP API: taobao.product.get
        /// </summary>
        public Product ProductGet(ProductGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Product>(new ParseData(request.GetApiName(), "product")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.img.delete
        /// </summary>
        public ProductImg ProductImgDelete(ProductImgDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<ProductImg>(new ParseData(request.GetApiName(), "product_img")));
        }

        /// <summary>
        /// TOP API: taobao.product.img.delete
        /// </summary>
        public ProductImg ProductImgDelete(ProductImgDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ProductImg>(new ParseData(request.GetApiName(), "product_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.img.upload
        /// </summary>
        public ProductImg ProductImgUpload(ProductImgUploadRequest request) {
            return client.Execute(request, new ObjectXmlParser<ProductImg>(new ParseData(request.GetApiName(), "product_img")));
        }

        /// <summary>
        /// TOP API: taobao.product.img.upload
        /// </summary>
        public ProductImg ProductImgUpload(ProductImgUploadRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ProductImg>(new ParseData(request.GetApiName(), "product_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.propimg.delete
        /// </summary>
        public ProductPropImg ProductPropimgDelete(ProductPropimgDeleteRequest request) {
            return client.Execute(request, new ObjectXmlParser<ProductPropImg>(new ParseData(request.GetApiName(), "product_prop_img")));
        }

        /// <summary>
        /// TOP API: taobao.product.propimg.delete
        /// </summary>
        public ProductPropImg ProductPropimgDelete(ProductPropimgDeleteRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ProductPropImg>(new ParseData(request.GetApiName(), "product_prop_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.propimg.upload
        /// </summary>
        public ProductPropImg ProductPropimgUpload(ProductPropimgUploadRequest request) {
            return client.Execute(request, new ObjectXmlParser<ProductPropImg>(new ParseData(request.GetApiName(), "product_prop_img")));
        }

        /// <summary>
        /// TOP API: taobao.product.propimg.upload
        /// </summary>
        public ProductPropImg ProductPropimgUpload(ProductPropimgUploadRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<ProductPropImg>(new ParseData(request.GetApiName(), "product_prop_img")), session);
        }

        /// <summary>
        /// TOP API: taobao.product.update
        /// </summary>
        public Product ProductUpdate(ProductUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Product>(new ParseData(request.GetApiName(), "product")));
        }

        /// <summary>
        /// TOP API: taobao.product.update
        /// </summary>
        public Product ProductUpdate(ProductUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Product>(new ParseData(request.GetApiName(), "product")), session);
        }

        /// <summary>
        /// TOP API: taobao.products.get
        /// </summary>
        public PageList<Product> ProductsGet(ProductsGetRequest request) {
            return client.Execute(request, new ListXmlParser<Product>(new ParseData(request.GetApiName(), "products", "product")));
        }

        /// <summary>
        /// TOP API: taobao.products.get
        /// </summary>
        public PageList<Product> ProductsGet(ProductsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Product>(new ParseData(request.GetApiName(), "products", "product")), session);
        }

        /// <summary>
        /// TOP API: taobao.products.search
        /// </summary>
        public PageList<Product> ProductsSearch(ProductsSearchRequest request) {
            return client.Execute(request, new ListXmlParser<Product>(new ParseData(request.GetApiName(), "products", "product")));
        }

        /// <summary>
        /// TOP API: taobao.products.search
        /// </summary>
        public PageList<Product> ProductsSearch(ProductsSearchRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Product>(new ParseData(request.GetApiName(), "products", "product")), session);
        }

        /// <summary>
        /// TOP API: taobao.refund.get
        /// </summary>
        public Refund RefundGet(RefundGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Refund>(new ParseData(request.GetApiName(), "refund")));
        }

        /// <summary>
        /// TOP API: taobao.refund.get
        /// </summary>
        public Refund RefundGet(RefundGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Refund>(new ParseData(request.GetApiName(), "refund")), session);
        }

        /// <summary>
        /// TOP API: taobao.refund.message.add
        /// </summary>
        public RefundMessage RefundMessageAdd(RefundMessageAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<RefundMessage>(new ParseData(request.GetApiName(), "refund_message")));
        }

        /// <summary>
        /// TOP API: taobao.refund.message.add
        /// </summary>
        public RefundMessage RefundMessageAdd(RefundMessageAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<RefundMessage>(new ParseData(request.GetApiName(), "refund_message")), session);
        }

        /// <summary>
        /// TOP API: taobao.refund.messages.get
        /// </summary>
        public PageList<RefundMessage> RefundMessagesGet(RefundMessagesGetRequest request) {
            return client.Execute(request, new ListXmlParser<RefundMessage>(new ParseData(request.GetApiName(), "refund_messages", "refund_message")));
        }

        /// <summary>
        /// TOP API: taobao.refund.messages.get
        /// </summary>
        public PageList<RefundMessage> RefundMessagesGet(RefundMessagesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<RefundMessage>(new ParseData(request.GetApiName(), "refund_messages", "refund_message")), session);
        }

        /// <summary>
        /// TOP API: taobao.refunds.apply.get
        /// </summary>
        public PageList<Refund> RefundsApplyGet(RefundsApplyGetRequest request) {
            return client.Execute(request, new ListXmlParser<Refund>(new ParseData(request.GetApiName(), "refunds", "refund")));
        }

        /// <summary>
        /// TOP API: taobao.refunds.apply.get
        /// </summary>
        public PageList<Refund> RefundsApplyGet(RefundsApplyGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Refund>(new ParseData(request.GetApiName(), "refunds", "refund")), session);
        }

        /// <summary>
        /// TOP API: taobao.refunds.receive.get
        /// </summary>
        public PageList<Refund> RefundsReceiveGet(RefundsReceiveGetRequest request) {
            return client.Execute(request, new ListXmlParser<Refund>(new ParseData(request.GetApiName(), "refunds", "refund")));
        }

        /// <summary>
        /// TOP API: taobao.refunds.receive.get
        /// </summary>
        public PageList<Refund> RefundsReceiveGet(RefundsReceiveGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Refund>(new ParseData(request.GetApiName(), "refunds", "refund")), session);
        }

        /// <summary>
        /// TOP API: taobao.sellercats.list.add
        /// </summary>
        public SellerCat SellercatsListAdd(SellercatsListAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<SellerCat>(new ParseData(request.GetApiName(), "seller_cat")));
        }

        /// <summary>
        /// TOP API: taobao.sellercats.list.add
        /// </summary>
        public SellerCat SellercatsListAdd(SellercatsListAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<SellerCat>(new ParseData(request.GetApiName(), "seller_cat")), session);
        }

        /// <summary>
        /// TOP API: taobao.sellercats.list.get
        /// </summary>
        public PageList<SellerCat> SellercatsListGet(SellercatsListGetRequest request) {
            return client.Execute(request, new ListXmlParser<SellerCat>(new ParseData(request.GetApiName(), "seller_cats", "seller_cat")));
        }

        /// <summary>
        /// TOP API: taobao.sellercats.list.get
        /// </summary>
        public PageList<SellerCat> SellercatsListGet(SellercatsListGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<SellerCat>(new ParseData(request.GetApiName(), "seller_cats", "seller_cat")), session);
        }

        /// <summary>
        /// TOP API: taobao.sellercats.list.update
        /// </summary>
        public SellerCat SellercatsListUpdate(SellercatsListUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<SellerCat>(new ParseData(request.GetApiName(), "seller_cat")));
        }

        /// <summary>
        /// TOP API: taobao.sellercats.list.update
        /// </summary>
        public SellerCat SellercatsListUpdate(SellercatsListUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<SellerCat>(new ParseData(request.GetApiName(), "seller_cat")), session);
        }

        /// <summary>
        /// TOP API: taobao.shipping.addresses.get
        /// </summary>
        public PageList<ShippingAddress> ShippingAddressesGet(ShippingAddressesGetRequest request) {
            return client.Execute(request, new ListXmlParser<ShippingAddress>(new ParseData(request.GetApiName(), "shipping_addresss", "shipping_address")));
        }

        /// <summary>
        /// TOP API: taobao.shipping.addresses.get
        /// </summary>
        public PageList<ShippingAddress> ShippingAddressesGet(ShippingAddressesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ShippingAddress>(new ParseData(request.GetApiName(), "shipping_addresss", "shipping_address")), session);
        }

        /// <summary>
        /// TOP API: taobao.shop.get
        /// </summary>
        public Shop ShopGet(ShopGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Shop>(new ParseData(request.GetApiName(), "shop")));
        }

        /// <summary>
        /// TOP API: taobao.shop.get
        /// </summary>
        public Shop ShopGet(ShopGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Shop>(new ParseData(request.GetApiName(), "shop")), session);
        }

        /// <summary>
        /// TOP API: taobao.shop.remainshowcase.get
        /// </summary>
        public Shop ShopRemainshowcaseGet(ShopRemainshowcaseGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Shop>(new ParseData(request.GetApiName(), "shop")));
        }

        /// <summary>
        /// TOP API: taobao.shop.remainshowcase.get
        /// </summary>
        public Shop ShopRemainshowcaseGet(ShopRemainshowcaseGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Shop>(new ParseData(request.GetApiName(), "shop")), session);
        }

        /// <summary>
        /// TOP API: taobao.shop.update
        /// </summary>
        public Shop ShopUpdate(ShopUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Shop>(new ParseData(request.GetApiName(), "shop")));
        }

        /// <summary>
        /// TOP API: taobao.shop.update
        /// </summary>
        public Shop ShopUpdate(ShopUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Shop>(new ParseData(request.GetApiName(), "shop")), session);
        }

        /// <summary>
        /// TOP API: taobao.shopcats.list.get
        /// </summary>
        public PageList<ShopCat> ShopcatsListGet(ShopcatsListGetRequest request) {
            return client.Execute(request, new ListXmlParser<ShopCat>(new ParseData(request.GetApiName(), "shop_cats", "shop_cat")));
        }

        /// <summary>
        /// TOP API: taobao.shopcats.list.get
        /// </summary>
        public PageList<ShopCat> ShopcatsListGet(ShopcatsListGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<ShopCat>(new ParseData(request.GetApiName(), "shop_cats", "shop_cat")), session);
        }

        /// <summary>
        /// TOP API: taobao.skus.custom.get
        /// </summary>
        public PageList<Sku> SkusCustomGet(SkusCustomGetRequest request) {
            return client.Execute(request, new ListXmlParser<Sku>(new ParseData(request.GetApiName(), "skus", "sku")));
        }

        /// <summary>
        /// TOP API: taobao.skus.custom.get
        /// </summary>
        public PageList<Sku> SkusCustomGet(SkusCustomGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Sku>(new ParseData(request.GetApiName(), "skus", "sku")), session);
        }

        /// <summary>
        /// TOP API: taobao.suites.get
        /// </summary>
        public PageList<Suite> SuitesGet(SuitesGetRequest request) {
            return client.Execute(request, new ListXmlParser<Suite>(new ParseData(request.GetApiName(), "suites", "suite")));
        }

        /// <summary>
        /// TOP API: taobao.suites.get
        /// </summary>
        public PageList<Suite> SuitesGet(SuitesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Suite>(new ParseData(request.GetApiName(), "suites", "suite")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.caturl.get
        /// </summary>
        public TaobaokeItem TaobaokeCaturlGet(TaobaokeCaturlGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_item")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.caturl.get
        /// </summary>
        public TaobaokeItem TaobaokeCaturlGet(TaobaokeCaturlGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_item")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.items.convert
        /// </summary>
        public PageList<TaobaokeItem> TaobaokeItemsConvert(TaobaokeItemsConvertRequest request) {
            return client.Execute(request, new ListXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_items", "taobaoke_item")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.items.convert
        /// </summary>
        public PageList<TaobaokeItem> TaobaokeItemsConvert(TaobaokeItemsConvertRequest request, string session) {
            return client.Execute(request, new ListXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_items", "taobaoke_item")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.items.detail.get
        /// </summary>
        public PageList<TaobaokeItemDetail> TaobaokeItemsDetailGet(TaobaokeItemsDetailGetRequest request) {
            return client.Execute(request, new ListXmlParser<TaobaokeItemDetail>(new ParseData(request.GetApiName(), "taobaoke_item_details", "taobaoke_item_detail")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.items.detail.get
        /// </summary>
        public PageList<TaobaokeItemDetail> TaobaokeItemsDetailGet(TaobaokeItemsDetailGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<TaobaokeItemDetail>(new ParseData(request.GetApiName(), "taobaoke_item_details", "taobaoke_item_detail")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.items.get
        /// </summary>
        public PageList<TaobaokeItem> TaobaokeItemsGet(TaobaokeItemsGetRequest request) {
            return client.Execute(request, new ListXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_items", "taobaoke_item")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.items.get
        /// </summary>
        public PageList<TaobaokeItem> TaobaokeItemsGet(TaobaokeItemsGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_items", "taobaoke_item")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.listurl.get
        /// </summary>
        public TaobaokeItem TaobaokeListurlGet(TaobaokeListurlGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_item")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.listurl.get
        /// </summary>
        public TaobaokeItem TaobaokeListurlGet(TaobaokeListurlGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<TaobaokeItem>(new ParseData(request.GetApiName(), "taobaoke_item")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.report.get
        /// </summary>
        public TaobaokeReport TaobaokeReportGet(TaobaokeReportGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<TaobaokeReport>(new ParseData(request.GetApiName(), "taobaoke_report")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.report.get
        /// </summary>
        public TaobaokeReport TaobaokeReportGet(TaobaokeReportGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<TaobaokeReport>(new ParseData(request.GetApiName(), "taobaoke_report")), session);
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.shops.convert
        /// </summary>
        public PageList<TaobaokeShop> TaobaokeShopsConvert(TaobaokeShopsConvertRequest request) {
            return client.Execute(request, new ListXmlParser<TaobaokeShop>(new ParseData(request.GetApiName(), "taobaoke_shops", "taobaoke_shop")));
        }

        /// <summary>
        /// TOP API: taobao.taobaoke.shops.convert
        /// </summary>
        public PageList<TaobaokeShop> TaobaokeShopsConvert(TaobaokeShopsConvertRequest request, string session) {
            return client.Execute(request, new ListXmlParser<TaobaokeShop>(new ParseData(request.GetApiName(), "taobaoke_shops", "taobaoke_shop")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.close
        /// </summary>
        public Trade TradeClose(TradeCloseRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.close
        /// </summary>
        public Trade TradeClose(TradeCloseRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.confirmfee.get
        /// </summary>
        public TradeConfirmFee TradeConfirmfeeGet(TradeConfirmfeeGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<TradeConfirmFee>(new ParseData(request.GetApiName(), "trade_confirm_fee")));
        }

        /// <summary>
        /// TOP API: taobao.trade.confirmfee.get
        /// </summary>
        public TradeConfirmFee TradeConfirmfeeGet(TradeConfirmfeeGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<TradeConfirmFee>(new ParseData(request.GetApiName(), "trade_confirm_fee")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.fullinfo.get
        /// </summary>
        public Trade TradeFullinfoGet(TradeFullinfoGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.fullinfo.get
        /// </summary>
        public Trade TradeFullinfoGet(TradeFullinfoGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.get
        /// </summary>
        public Trade TradeGet(TradeGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.get
        /// </summary>
        public Trade TradeGet(TradeGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.memo.add
        /// </summary>
        public Trade TradeMemoAdd(TradeMemoAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.memo.add
        /// </summary>
        public Trade TradeMemoAdd(TradeMemoAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.memo.update
        /// </summary>
        public Trade TradeMemoUpdate(TradeMemoUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.memo.update
        /// </summary>
        public Trade TradeMemoUpdate(TradeMemoUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.ordersku.update
        /// </summary>
        public Order TradeOrderskuUpdate(TradeOrderskuUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Order>(new ParseData(request.GetApiName(), "order")));
        }

        /// <summary>
        /// TOP API: taobao.trade.ordersku.update
        /// </summary>
        public Order TradeOrderskuUpdate(TradeOrderskuUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Order>(new ParseData(request.GetApiName(), "order")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.shippingaddress.update
        /// </summary>
        public Trade TradeShippingaddressUpdate(TradeShippingaddressUpdateRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.shippingaddress.update
        /// </summary>
        public Trade TradeShippingaddressUpdate(TradeShippingaddressUpdateRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trade.snapshot.get
        /// </summary>
        public Trade TradeSnapshotGet(TradeSnapshotGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trade.snapshot.get
        /// </summary>
        public Trade TradeSnapshotGet(TradeSnapshotGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<Trade>(new ParseData(request.GetApiName(), "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.traderate.add
        /// </summary>
        public TradeRate TraderateAdd(TraderateAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<TradeRate>(new ParseData(request.GetApiName(), "trade_rate")));
        }

        /// <summary>
        /// TOP API: taobao.traderate.add
        /// </summary>
        public TradeRate TraderateAdd(TraderateAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<TradeRate>(new ParseData(request.GetApiName(), "trade_rate")), session);
        }

        /// <summary>
        /// TOP API: taobao.traderate.list.add
        /// </summary>
        public TradeRate TraderateListAdd(TraderateListAddRequest request) {
            return client.Execute(request, new ObjectXmlParser<TradeRate>(new ParseData(request.GetApiName(), "trade_rate")));
        }

        /// <summary>
        /// TOP API: taobao.traderate.list.add
        /// </summary>
        public TradeRate TraderateListAdd(TraderateListAddRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<TradeRate>(new ParseData(request.GetApiName(), "trade_rate")), session);
        }

        /// <summary>
        /// TOP API: taobao.traderates.get
        /// </summary>
        public PageList<TradeRate> TraderatesGet(TraderatesGetRequest request) {
            return client.Execute(request, new ListXmlParser<TradeRate>(new ParseData(request.GetApiName(), "trade_rates", "trade_rate")));
        }

        /// <summary>
        /// TOP API: taobao.traderates.get
        /// </summary>
        public PageList<TradeRate> TraderatesGet(TraderatesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<TradeRate>(new ParseData(request.GetApiName(), "trade_rates", "trade_rate")), session);
        }

        /// <summary>
        /// TOP API: taobao.trades.bought.get
        /// </summary>
        public PageList<Trade> TradesBoughtGet(TradesBoughtGetRequest request) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trades.bought.get
        /// </summary>
        public PageList<Trade> TradesBoughtGet(TradesBoughtGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trades.get
        /// </summary>
        public PageList<Trade> TradesGet(TradesGetRequest request) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trades.get
        /// </summary>
        public PageList<Trade> TradesGet(TradesGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trades.sold.get
        /// </summary>
        public PageList<Trade> TradesSoldGet(TradesSoldGetRequest request) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trades.sold.get
        /// </summary>
        public PageList<Trade> TradesSoldGet(TradesSoldGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.trades.sold.increment.get
        /// </summary>
        public PageList<Trade> TradesSoldIncrementGet(TradesSoldIncrementGetRequest request) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")));
        }

        /// <summary>
        /// TOP API: taobao.trades.sold.increment.get
        /// </summary>
        public PageList<Trade> TradesSoldIncrementGet(TradesSoldIncrementGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<Trade>(new ParseData(request.GetApiName(), "trades", "trade")), session);
        }

        /// <summary>
        /// TOP API: taobao.user.detail.get
        /// </summary>
        public User UserDetailGet(UserDetailGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<User>(new ParseData(request.GetApiName(), "user")));
        }

        /// <summary>
        /// TOP API: taobao.user.detail.get
        /// </summary>
        public User UserDetailGet(UserDetailGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<User>(new ParseData(request.GetApiName(), "user")), session);
        }

        /// <summary>
        /// TOP API: taobao.user.get
        /// </summary>
        public User UserGet(UserGetRequest request) {
            return client.Execute(request, new ObjectXmlParser<User>(new ParseData(request.GetApiName(), "user")));
        }

        /// <summary>
        /// TOP API: taobao.user.get
        /// </summary>
        public User UserGet(UserGetRequest request, string session) {
            return client.Execute(request, new ObjectXmlParser<User>(new ParseData(request.GetApiName(), "user")), session);
        }

        /// <summary>
        /// TOP API: taobao.users.get
        /// </summary>
        public PageList<User> UsersGet(UsersGetRequest request) {
            return client.Execute(request, new ListXmlParser<User>(new ParseData(request.GetApiName(), "users", "user")));
        }

        /// <summary>
        /// TOP API: taobao.users.get
        /// </summary>
        public PageList<User> UsersGet(UsersGetRequest request, string session) {
            return client.Execute(request, new ListXmlParser<User>(new ParseData(request.GetApiName(), "users", "user")), session);
        }
    }
}
