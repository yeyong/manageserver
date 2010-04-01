using System;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Config;
using SAS.Config.Provider;
using SAS.Entity;
using SAS.Logic;
using SAS.Plugin;
using SAS.Plugin.Album;

namespace SAS.Logic
{
    /// <summary>
    /// 企业页面基类
    /// </summary>
    public class CompanyPage : BasePage
    {
        /// <summary>
        /// 底部脚本
        /// </summary>
        protected internal string footscript;

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script></script>的脚本主体字符串</param>
        public void AddfootScript(string scriptfootstr)
        {
            AddfootScript(scriptfootstr, "javascript");
        }

        /// <summary>
        /// 插入脚本内容到页面head中
        /// </summary>
        /// <param name="scriptstr">不包括<script>
        /// <param name="scripttype">脚本类型(值为：vbscript或javascript,默认为javascript)</param>
        public void AddfootScript(string scriptfootstr, string scripttype)
        {
            if (!scripttype.ToLower().Equals("vbscript") && !scripttype.ToLower().Equals("vbscript"))
            {
                scripttype = "javascript";
            }
            footscript = footscript + "\r\n<script type=\"text/" + scripttype + "\">" + scriptfootstr + "</script>";
        }
    }
}
