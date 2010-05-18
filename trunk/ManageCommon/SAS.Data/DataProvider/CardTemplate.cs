using System;
using System.Data;
using System.Text;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 名片模板数据操作类
    /// </summary>
    public class CardTemplate
    {
        /// <summary>
        /// 添加名片模板
        /// </summary>
        /// <param name="name">模版名称</param>
        /// <param name="directory">模版目录</param>
        /// <param name="copyright">版权信息</param>
        /// <param name="author">作者</param>
        /// <param name="createdate">创建日期</param>
        /// <param name="ver">版本</param>
        /// <param name="currentfile">当前参数</param>
        public static void CreateCardTemplate(string name, string directory, string copyright, string author, string createdate, string ver, string currentfile)
        {
            DatabaseProvider.GetInstance().AddCardTemplate(name, directory, copyright, author, createdate, ver, currentfile);
        }

        /// <summary>
        /// 删除指定的名片模板项列表,
        /// </summary>
        /// <param name="templateidlist">格式为： 1,2,3</param>
        public static void DeleteCardTemplateItem(string templateidlist)
        {
            DatabaseProvider.GetInstance().DeleteCardTemplateItem(templateidlist);
        }

        /// <summary>
        /// 获得前台有效的模板列表
        /// </summary>
        /// <returns>模板列表</returns>
        public static DataTable GetValidCardTemplateList()
        {
            return DatabaseProvider.GetInstance().GetValidCardTemplateList();
        }

        /// <summary>
        /// 获得前台有效的模板ID列表
        /// </summary>
        /// <returns>模板ID列表</returns>
        public static string GetValidCardTemplateIDList()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in DatabaseProvider.GetInstance().GetValidCardTemplateIDList().Rows)
            {
                sb.Append(",");
                sb.Append(dr[0].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取名片模板列表
        /// </summary>
        /// <param name="templatePath">模版路径</param>
        /// <returns></returns>
        public static DataTable GetAllCardTemplateList()
        {
            return DatabaseProvider.GetInstance().GetAllCardTemplateList();
        }

        /// <summary>
        /// 更改名片模板参数
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="parmstr"></param>
        public static void UpdateCardTemplateParms(int tid, string parmstr)
        {
            DatabaseProvider.GetInstance().UpdateCardTemplateParm(tid, parmstr);
        }
    }
}
