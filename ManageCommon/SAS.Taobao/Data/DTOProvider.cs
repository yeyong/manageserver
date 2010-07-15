﻿using System;
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
    }
}
