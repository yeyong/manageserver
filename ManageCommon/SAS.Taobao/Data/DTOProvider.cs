using System;
using System.Collections;
using System.Data;
using System.Text;

using SAS.Cache;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Taobao.Data
{
    public class DTOProvider
    {
        public static CategoryInfo GetCategoryInfoEntity(IDataReader reader)
        {
            if (reader.Read())
            {
                CategoryInfo cinfo = new CategoryInfo();
                cinfo.Cid = TypeConverter.ObjectToInt(reader["cid"], 0);
                cinfo.Name = reader["name"].ToString();
                cinfo.Parentid = TypeConverter.ObjectToInt(reader["parentid"].ToString(),0);
                cinfo.Parentlist = reader["parentlist"].ToString();
                cinfo.Cg_img = reader["cg_img"].ToString();
                cinfo.Sort = TypeConverter.ObjectToInt(reader["sort"].ToString(),0);
                cinfo.Cg_prefix = reader["cg_prefix"].ToString();
                cinfo.Cg_status = TypeConverter.ObjectToInt(reader["cg_status"].ToString(),0);
                cinfo.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"].ToString(),0);
                cinfo.Haschild = reader["haschild"].ToString() == "True" ? 1 : 0;
                cinfo.Cg_relatetype = reader["cg_relatetype"].ToString();
                cinfo.Cg_relateclass = reader["cg_relateclass"].ToString();
                cinfo.Cg_relatebrand = reader["cg_relatebrand"].ToString();
                cinfo.Cg_desc = reader["cg_desc"].ToString();
                cinfo.Cg_keyword = reader["cg_keyword"].ToString();
                cinfo.Goodcount = TypeConverter.ObjectToInt(reader["goodcount"].ToString(),0);
                reader.Close();
                return cinfo;
            }
            return null;
        }

        public static List<CategoryInfo> GetCategoryListEntity(IDataReader reader)
        {
            List<CategoryInfo> categorylist = new List<CategoryInfo>();
            while (reader.Read())
            {
                CategoryInfo cinfo = new CategoryInfo();
                cinfo.Cid = TypeConverter.ObjectToInt(reader["cid"], 0);
                cinfo.Name = reader["name"].ToString();
                cinfo.Parentid = TypeConverter.ObjectToInt(reader["parentid"].ToString(), 0);
                cinfo.Parentlist = reader["parentlist"].ToString();
                cinfo.Cg_img = reader["cg_img"].ToString();
                cinfo.Sort = TypeConverter.ObjectToInt(reader["sort"].ToString(), 0);
                cinfo.Cg_prefix = reader["cg_prefix"].ToString();
                cinfo.Cg_status = TypeConverter.ObjectToInt(reader["cg_status"].ToString(), 0);
                cinfo.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"].ToString(), 0);
                cinfo.Haschild = reader["haschild"].ToString() == "True" ? 1 : 0;
                cinfo.Cg_relatetype = reader["cg_relatetype"].ToString();
                cinfo.Cg_relateclass = reader["cg_relateclass"].ToString();
                cinfo.Cg_relatebrand = reader["cg_relatebrand"].ToString();
                cinfo.Cg_desc = reader["cg_desc"].ToString();
                cinfo.Cg_keyword = reader["cg_keyword"].ToString();
                cinfo.Goodcount = TypeConverter.ObjectToInt(reader["goodcount"].ToString(), 0);
                categorylist.Add(cinfo);
            }
            reader.Close();
            return categorylist;
        }

        public static RecommendInfo GetRecommendInfoEntity(IDataReader reader)
        {
            if (reader.Read())
            {
                RecommendInfo rinfo = new RecommendInfo();

                rinfo.id = TypeConverter.ObjectToInt(reader["id"].ToString());
                rinfo.ctitle = reader["ctitle"].ToString();
                rinfo.ctype = TypeConverter.ObjectToInt(reader["ctype"].ToString());
                rinfo.relatechanel = TypeConverter.ObjectToInt(reader["relatechanel"].ToString());
                rinfo.relatecategory = TypeConverter.ObjectToInt(reader["relatecategory"].ToString());
                rinfo.ccontent = reader["ccontent"].ToString();
                rinfo.createdatetime = reader["createdatetime"].ToString();
                rinfo.updatedatetime = reader["updatedatetime"].ToString();

                reader.Close();
                return rinfo;
            }
            return null;
        }

        public static List<RecommendInfo> GetRecommendListEntity(IDataReader reader)
        {
            List<RecommendInfo> rinfolist = new List<RecommendInfo>();
            while (reader.Read())
            {
                RecommendInfo rinfo = new RecommendInfo();
                rinfo.id = TypeConverter.ObjectToInt(reader["id"].ToString());
                rinfo.ctitle = reader["ctitle"].ToString();
                rinfo.ctype = TypeConverter.ObjectToInt(reader["ctype"].ToString());
                rinfo.relatechanel = TypeConverter.ObjectToInt(reader["relatechanel"].ToString());
                rinfo.relatecategory = TypeConverter.ObjectToInt(reader["relatecategory"].ToString());
                rinfo.ccontent = reader["ccontent"].ToString();
                rinfo.createdatetime = reader["createdatetime"].ToString();
                rinfo.updatedatetime = reader["updatedatetime"].ToString();
                rinfolist.Add(rinfo);
            }
            reader.Close();
            return rinfolist;
        }

        public static List<ShopDetailInfo> GetTaoBaoShopList(IDataReader reader)
        {
            List<ShopDetailInfo> shoplist = new List<ShopDetailInfo>();
            while (reader.Read())
            {
                ShopDetailInfo sinfo = new ShopDetailInfo();
                sinfo.sid = long.Parse(reader["sid"].ToString());
                sinfo.user_id = long.Parse(reader["user_id"].ToString());
                sinfo.cid = long.Parse(reader["cid"].ToString());
                sinfo.nick = reader["nick"].ToString();
                sinfo.title = reader["title"].ToString();
                sinfo.item_score = TypeConverter.ObjectToInt(reader["item_score"].ToString());
                sinfo.service_score = TypeConverter.ObjectToInt(reader["service_score"].ToString());
                sinfo.delivery_score = TypeConverter.ObjectToInt(reader["delivery_score"].ToString());
                sinfo.shop_desc = reader["shop_desc"].ToString();
                sinfo.bulletin = reader["bulletin"].ToString();
                sinfo.pic_path = reader["pic_path"].ToString();
                sinfo.created = reader["created"].ToString();
                sinfo.modified = reader["modified"].ToString();
                sinfo.promoted_type = reader["promoted_type"].ToString();
                sinfo.consumer_protection = true;
                sinfo.shop_status = reader["shop_status"].ToString();
                sinfo.shop_type = reader["shop_type"].ToString();
                sinfo.shop_level = TypeConverter.ObjectToInt(reader["shop_level"].ToString());
                sinfo.shop_score = TypeConverter.ObjectToInt(reader["shop_score"].ToString());
                sinfo.total_num = long.Parse(reader["total_num"].ToString());
                sinfo.good_num = long.Parse(reader["good_num"].ToString());
                sinfo.shop_country = reader["shop_country"].ToString();
                sinfo.shop_province = reader["shop_province"].ToString();
                sinfo.shop_city = reader["shop_city"].ToString();
                sinfo.shop_address = reader["shop_address"].ToString();
                sinfo.commission_rate = reader["commission_rate"].ToString();
                sinfo.click_url = reader["click_url"].ToString();
                shoplist.Add(sinfo);
            }
            reader.Close();
            return shoplist;
        }

        public static GoodsBrandInfo GetGoodsBrandInfoEntity(IDataReader reader)
        {
            if (reader.Read())
            {
                GoodsBrandInfo model = new GoodsBrandInfo();

                model.id = TypeConverter.ObjectToInt(reader["id"].ToString(), 0);
                model.bname = reader["bname"].ToString();
                model.spell = reader["spell"].ToString();
                model.website = reader["website"].ToString();
                model.bcompany = reader["bcompany"].ToString();
                model.order = TypeConverter.ObjectToInt(reader["order"], 0);
                model.logo = reader["logo"].ToString();
                model.img = reader["img"].ToString();
                model.keyword = reader["keyword"].ToString();
                model.shortdesc = reader["shortdesc"].ToString();
                model.detaildesc = reader["detaildesc"].ToString();
                model.status = TypeConverter.ObjectToInt(reader["status"], 0);
                model.relateclass = reader["relateclass"].ToString();

                reader.Close();
                return model;
            }
            return null;
        }

        public static List<GoodsBrandInfo> GetGoodsBrandListEntity(IDataReader reader)
        {
            List<GoodsBrandInfo> ginfolist = new List<GoodsBrandInfo>();
            while (reader.Read())
            {
                GoodsBrandInfo model = new GoodsBrandInfo();

                model.id = TypeConverter.ObjectToInt(reader["id"].ToString(), 0);
                model.bname = reader["bname"].ToString();
                model.spell = reader["spell"].ToString();
                model.website = reader["website"].ToString();
                model.bcompany = reader["bcompany"].ToString();
                model.order = TypeConverter.ObjectToInt(reader["order"], 0);
                model.logo = reader["logo"].ToString();
                model.img = reader["img"].ToString();
                model.keyword = reader["keyword"].ToString();
                model.shortdesc = reader["shortdesc"].ToString();
                model.detaildesc = reader["detaildesc"].ToString();
                model.status = TypeConverter.ObjectToInt(reader["status"], 0);
                model.relateclass = reader["relateclass"].ToString();
                ginfolist.Add(model);
            }
            reader.Close();
            return ginfolist;
        }
    }
}
