using System;
using System.Text;
using System.Data;

using SAS.Common.Generic;
using SAS.Entity;
using SAS.Common;
using SAS.Config;

namespace SAS.Data.DataProvider
{
    public class Forums
    {
        /// <summary>
        /// 更新版块和用户模板Id
        /// </summary>
        /// <param name="templateIdList">模板Id列表</param>
        public static void UpdateForumAndUserTemplateId(string templateIdList)
        {
            DatabaseProvider.GetInstance().UpdateForumAndUserTemplateId(templateIdList);
        }
    }
}
