using System;
using System.Data;
using System.IO;
using System.Collections;

using SAS.Logic;
using SAS.Common;
using SAS.Config;

namespace SAS.Web.UI
{
    public class Globals
    {
        public static void BuildTemplate(string directorypath)
        {
            int templateid = Convert.ToInt32(AdminTemplates.GetAllTemplateList(Utils.GetMapPath(@"..\..\templates\")).Select("tp_directory='" + directorypath + "'")[0]["tp_id"].ToString());

            Hashtable ht = new Hashtable();
            GetTemplates("default", ht);

            if (directorypath != "default")
            {
                GetTemplates(directorypath, ht);
            }
            LogicPageTemplate forumpagetemplate = new LogicPageTemplate();

            foreach (string key in ht.Keys)
            {
                string filename = key.Split('.')[0];
                string[] template = ht[key].ToString().Split('\\');
                forumpagetemplate.GetTemplate(BaseConfigs.GetSitePath, template[0], filename, template.Length >= 2 ? template[template.Length - 1] : "", 1, templateid);
            }

        }

        private static Hashtable GetTemplates(string directorypath, Hashtable ht)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(Utils.GetMapPath("..\\..\\templates\\" + directorypath + "\\"));

            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file.Name == "images")
                    continue;
                if (file.Attributes == FileAttributes.Directory)
                {
                    GetTemplates(directorypath + "\\" + file.Name, ht);
                }
                else
                {
                    if (file != null && (file.Extension.ToLower().Equals(".htm") || file.Extension.ToLower().Equals(".config")) && !file.Name.StartsWith("_"))
                    {
                        ht[file.Name] = directorypath;
                    }
                }
            }
            return ht;
        }
    }
}
