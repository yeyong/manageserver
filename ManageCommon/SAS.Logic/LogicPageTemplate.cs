using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using SAS.Common;

namespace SAS.Logic
{
    /// <summary>
    /// 页面模板生成类
    /// </summary>
    public class LogicPageTemplate : PageTemplate
    {
        /// <summary>
        /// 解析特殊变量
        /// </summary>
        /// <param name="forumPath">模板路径</param>
        /// <param name="skinName">皮肤名</param>
        /// <param name="strTemplate">模板内容</param>
        /// <returns></returns>
        public override string ReplaceSpecialTemplate(string forumPath, string skinName, string strTemplate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(strTemplate);
            Match m;
            Regex r = new Regex(@"({([^\[\]/\{\}='\s]+)})", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            for (m = r.Match(strTemplate); m.Success; m = m.NextMatch())
            {
                if (m.Groups[0].ToString() == "{forumversion}")
                    sb = sb.Replace(m.Groups[0].ToString(), Utils.GetAssemblyVersion());
                else if (m.Groups[0].ToString() == "{forumproductname}")
                    sb = sb.Replace(m.Groups[0].ToString(), Utils.GetAssemblyProductName());
            }

            foreach (DataRow dr in GetTemplateVarList(forumPath, skinName).Rows)
            {
                sb = sb.Replace(dr["variablename"].ToString().Trim(), dr["variablevalue"].ToString().Trim());
            }
            return sb.ToString();
        }


        /// <summary>
        /// 获得模板变量列表
        /// </summary>
        /// <param name="forumpath">模板路径</param>
        /// <param name="skinName">皮肤名</param>
        /// <returns></returns>
        public static DataTable GetTemplateVarList(string forumpath, string skinName)
        {
            SAS.Cache.SASCache cache = Cache.SASCache.GetCacheService();
            DataTable dt = cache.RetrieveSingleObject("/SAS/" + skinName + "/TemplateVariable") as DataTable;

            if (dt == null)
            {
                DataSet dsSrc = new DataSet("template");

                string[] filename = { Utils.GetMapPath(forumpath + "templates/" + skinName + "/templatevariable.xml") };

                if (Utils.FileExists(filename[0]))
                {
                    dsSrc.ReadXml(filename[0]);
                    if (dsSrc.Tables.Count == 0)
                        dsSrc.Tables.Add(TemplateVariableTable());
                }
                else
                {
                    dsSrc.Tables.Add(TemplateVariableTable());
                }
                dt = dsSrc.Tables[0];
                cache.AddSingleObject("/SAS/" + skinName + "/TemplateVariable", dt, filename);
            }
            return dt;
        }

        /// <summary>
        /// 创建临时变量列表
        /// </summary>
        /// <returns></returns>
        private static DataTable TemplateVariableTable()
        {
            DataTable templatevariable = new DataTable("TemplateVariable");
            templatevariable.Columns.Add("id", System.Type.GetType("System.Int32"));
            templatevariable.Columns.Add("variablename", System.Type.GetType("System.String"));
            templatevariable.Columns.Add("variablevalue", System.Type.GetType("System.String"));
            return templatevariable;
        }
    }
}
