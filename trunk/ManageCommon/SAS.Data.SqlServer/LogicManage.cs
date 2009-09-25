using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;

namespace SAS.Data.SqlServer
{
    public partial class DataProvider : IDataProvider
    {
        #region 板块处理forum操作

        /// <summary>
        /// 更新版块和用户模板Id
        /// </summary>
        /// <param name="templateIdList">模板Id列表</param>
        public void UpdateForumAndUserTemplateId(string templateIdList)
        {
            string commandText = string.Format("UPDATE [{0}forums] SET [templateid]=0 WHERE [templateid] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);

            commandText = string.Format("UPDATE [{0}personInfo] SET [ps_tempID]=0 WHERE [ps_tempID] IN ({1})",
                                                BaseConfigs.GetTablePrefix,
                                                templateIdList);
            DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
        }

        #endregion
    }
}
