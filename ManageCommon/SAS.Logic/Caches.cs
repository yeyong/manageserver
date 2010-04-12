using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Entity;
using SAS.Data;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 缓存前台的一些界面HTML数据
    /// </summary>
    public class Caches
    {
        private static object lockHelper = new object();

        /// <summary>
        /// 数字正则式静态实例
        /// </summary>
        private static Regex r = new Regex("\\{(\\d+)\\}", Utils.GetRegexCompiledOptions());

        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns>禁止列表</returns>
        public static List<IpInfo> GetBannedIpList()
        {
            List<IpInfo> list = SAS.Cache.SASCache.GetCacheService().RetrieveObject("/SAS/BannedIp") as List<IpInfo>;

            if (list == null)
            {
                list = Ips.GetBannedIpList();
                SAS.Cache.SASCache.GetCacheService().AddObject("/SAS/BannedIp", list);
            }
            return list;
        }

        /// <summary>
        /// 返回脏字过滤列表
        /// </summary>
        /// <returns>返回脏字过滤列表数组</returns>
        public static string[,] GetBanWordList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string[,] str = cache.RetrieveObject("/SAS/BanWordList") as string[,];
            if (str == null)
            {
                DataTable dt = DatabaseProvider.GetInstance().GetBanWordList();
                str = new string[dt.Rows.Count, 2];
                string temp = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    temp = dt.Rows[i]["find"].ToString().Trim();
                    foreach (Match m in r.Matches(temp))
                    {
                        temp = temp.Replace(m.Groups[0].ToString(), m.Groups[0].ToString().Replace("{", ".{0,"));
                    }
                    str[i, 0] = temp.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("'", "\'").Replace("[", "\\[").Replace("]", "\\]");
                    str[i, 1] = dt.Rows[i]["replacement"].ToString().Trim();
                }
                cache.AddObject("/SAS/BanWordList", str);
                dt.Dispose();
            }
            return str;
        }

        /// <summary>
        /// 返回模板列表的下拉框html
        ///</summary>
        /// <returns>下拉框html</returns>
        public static string GetTemplateListBoxOptionsCache()
        {
            lock (lockHelper)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                string str = cache.RetrieveObject("/SAS/UI/TemplateListBoxOptions") as string;
                if (Utils.StrIsNullOrEmpty(str))
                {
                    StringBuilder sb = new StringBuilder();
                    DataTable dt = Templates.GetValidTemplateList();
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.AppendFormat("<li><a href=\"##\" onclick=\"window.location.href='{0}showtemplate.aspx?templateid={1}'\">{2}</a></li>",
                                         BaseConfigs.GetSitePath,
                                         dr["tp_id"],
                                         dr["tp_name"].ToString().Trim());
                    }
                    str = sb.ToString();
                    cache.AddObject("/SAS/UI/TemplateListBoxOptions", str);
                    dt.Dispose();
                }
                return str;
            }
        }

        /// <summary>
        /// 获得编辑器自定义按钮信息的javascript数组
        /// </summary>
        /// <returns>表情符的javascript数组</returns>
        public static string GetCustomEditButtonList()
        {
            lock (lockHelper)
            {
                SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
                string str = cache.RetrieveObject("/SAS/UI/CustomEditButtonList") as string;
                if (str == null)//此处这样判断是为了防止数据库中无记录时会将str赋值成""的情况，参见下面加载数据代码
                {
                    StringBuilder sb = new StringBuilder();
                    IDataReader dr = DatabaseProvider.GetInstance().GetCustomEditButtonList();
                    try
                    {
                        while (dr.Read())
                        {
                            //说明:[标签名,对应图标文件名,[参数1描述,参数2描述,...],[参数1默认值,参数2默认值,...]]
                            //实例["fly","swf.gif",["请输入flash网址","请输入flash宽度","请输入flash高度"],["http://","200","200"],3]
                            sb.AppendFormat(",'{0}':['", Utils.ReplaceStrToScript(dr["tag"].ToString()));
                            sb.Append(Utils.ReplaceStrToScript(dr["tag"].ToString()));
                            sb.Append("','");
                            sb.Append(Utils.ReplaceStrToScript(dr["icon"].ToString()));
                            sb.Append("','");
                            sb.Append(Utils.ReplaceStrToScript(dr["explanation"].ToString()));
                            sb.Append("',['");
                            sb.Append(Utils.ReplaceStrToScript(dr["paramsdescript"].ToString()).Replace(",", "','"));
                            sb.Append("'],['");
                            sb.Append(Utils.ReplaceStrToScript(dr["paramsdefvalue"].ToString()).Replace(",", "','"));
                            sb.Append("'],");
                            sb.Append(Utils.ReplaceStrToScript(dr["params"].ToString()));
                            sb.Append("]");
                        }
                        if (sb.Length > 0)
                            sb.Remove(0, 1);

                        str = Utils.ClearBR(sb.ToString());
                        cache.AddObject("/SAS/UI/CustomEditButtonList", str);
                    }
                    finally
                    {
                        dr.Close();
                    }
                }
                return str;
            }
        }

        /// <summary>
        /// 获得表情分类列表
        /// </summary>
        /// <returns>表情分类列表</returns>
        public static DataTable GetSmilieTypesCache()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable smilietypes = cache.RetrieveObject("/SAS/UI/SmiliesTypeList") as DataTable;
            if (smilietypes == null || smilietypes.Rows.Count == 0)
            {
                smilietypes = SAS.Data.DataProvider.Smilies.GetSmilieTypes();
                cache.AddObject("/SAS/UI/SmiliesTypeList", smilietypes);
            }
            return smilietypes;
        }

        /// <summary>
        /// 获得表情符的json数据
        /// </summary>
        /// <returns>表情符的json数据</returns>
        public static string GetSmiliesCache()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            string str = cache.RetrieveObject("/SAS/UI/SmiliesList") as string;
            if (Utils.StrIsNullOrEmpty(str))
            {
                StringBuilder builder = new StringBuilder();
                DataTable dt = SAS.Data.DataProvider.Smilies.GetSmiliesListDataTable();

                foreach (DataRow drCate in dt.Copy().Rows)
                {
                    if (drCate["smtype"].ToString() == "0")
                    {
                        builder.AppendFormat("'{0}': [\r\n", drCate["code"].ToString().Trim().Replace("'", "\\'"));
                        bool flag = false;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["smtype"].ToString() == drCate["id"].ToString())
                            {
                                builder.Append("{'code' : '");
                                builder.Append(dr["code"].ToString().Trim().Replace("'", "\\'"));
                                builder.Append("', 'url' : '");
                                builder.Append(dr["url"].ToString().Trim().Replace("'", "\\'"));
                                builder.Append("'},\r\n");
                                flag = true;
                            }
                        }
                        if (builder.Length > 0 && flag)
                            builder.Remove(builder.Length - 3, 3);
                        builder.Append("\r\n],\r\n");
                    }
                }
                builder.Remove(builder.Length - 3, 3);
                str = builder.ToString();
                cache.AddObject("/SAS/UI/SmiliesList", str);
            }
            return str;
        }

        /// <summary>
        /// 获得友情链接列表
        /// </summary>
        /// <returns>友情链接列表</returns>
        public static DataTable GetSASLinkList()
        {
            SAS.Cache.SASCache cache = SAS.Cache.SASCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/SAS/SASLinkList") as DataTable;
            if (dt == null)
            {
                dt = DatabaseProvider.GetInstance().GetSASLinks();
                if (dt != null && dt.Rows.Count > 0)
                {
                    StringBuilder linkBuilder = new StringBuilder();
                    StringBuilder logoLinkBuilder = new StringBuilder();
                    StringBuilder textLinkBuilder = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Utils.StrIsNullOrEmpty(dr["note"].ToString()))
                        {
                            if (Utils.StrIsNullOrEmpty(dr["name"].ToString()))
                                dr["name"] = "未知";

                            if (Utils.StrIsNullOrEmpty(dr["logo"].ToString()))
                                textLinkBuilder.AppendFormat("<a title=\"{0}\" href=\"{1}\" target=\"_blank\">{0}</a>\r\n", dr["name"], dr["linkurl"]);
                            else
                            {
                                logoLinkBuilder.AppendFormat("<a title=\"{0}\" href=\"{1}\" target=\"_blank\"><img alt=\"{0}\" class=\"friendlinkimg\" src=\"{2}\" /></a>\r\n",
                                                             dr["name"], dr["linkurl"], dr["logo"]);
                            }
                            dr.Delete();
                        }
                    }
                    if (logoLinkBuilder.Length > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["name"] = "$$otherlink$$";
                        dr["linkurl"] = "forumimglink";
                        dr["note"] = logoLinkBuilder.ToString();
                        dr["logo"] = "";
                        dt.Rows.Add(dr);
                    }
                    if (textLinkBuilder.Length > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["name"] = "$$otherlink$$";
                        dr["linkurl"] = "forumtxtlink";
                        dr["note"] = textLinkBuilder.ToString();
                        dr["logo"] = "";
                        dt.Rows.Add(dr);
                    }
                    dt.AcceptChanges();
                }
                cache.AddObject("/SAS/SASLinkList", dt);
            }
            return dt;
        }

        #region 后台管理重设缓存

        private static void RemoveObject(string key)
        {
            SASCache.GetCacheService().RemoveObject(key);
        }

        /// <summary>
        /// 重新设置论坛统计信息
        ///</summary>
        public static void ReSetStatistics()
        {
            RemoveObject(CacheKeys.FORUM_STATISTICS);
        }

        /// <summary>
        /// 重新设置论坛基本设置
        ///</summary>
        public static void ReSetConfig()
        {
            RemoveObject(CacheKeys.FORUM_SETTING);
        }

        /// <summary>
        /// 重新设置管理组信息
        ///</summary>
        public static void ReSetAdminGroupList()
        {
            RemoveObject(CacheKeys.FORUM_ADMIN_GROUP_LIST);
        }

        /// <summary>
        /// 重新设置用户组信息
        ///</summary>
        public static void ReSetUserGroupList()
        {
            RemoveObject(CacheKeys.FORUM_USER_GROUP_LIST);
        }

        /// <summary>
        /// 重置企业信息
        /// </summary>
        public static void ReSetCompanyList()
        {
            RemoveObject(CacheKeys.SAS_COMPANY_LIST);
        }

        /// <summary>
        /// 更新所有缓存
        /// </summary>
        public static void ReSetAllCache()
        {
            ReSetStatistics();
            ReSetConfig();
            ReSetAdminGroupList();
            ReSetUserGroupList();
            ReSetCompanyList();
        }

        #endregion
    }
}
