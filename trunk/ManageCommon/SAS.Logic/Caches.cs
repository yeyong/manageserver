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

        #region 后台管理重设缓存

        private static void RemoveObject(string key)
        {
            SASCache.GetCacheService().RemoveObject(key);
        }

        /// <summary>
        /// 重新设置论坛基本设置
        ///</summary>
        public static void ReSetConfig()
        {
            RemoveObject(CacheKeys.FORUM_SETTING);
        }

        #endregion
    }
}
