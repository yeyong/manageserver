using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Caching;

using SAS.Config;
using SAS.Common;

namespace SAS.ManageWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DataCacheConfigInfo dataconfig = DataCacheConfigs.GetConfig();

            if (dataconfig.EnableCaching == 1)
            {
                System.Data.SqlClient.SqlDependency.Start(BaseConfigs.GetDBConnectString);
                SqlCacheDependencyAdmin.EnableNotifications(BaseConfigs.GetDBConnectString);
                foreach (string cachetable in Utils.SplitString(dataconfig.CacheTableList, ","))
                {
                    SqlCacheDependencyAdmin.EnableTableForNotifications(BaseConfigs.GetDBConnectString, BaseConfigs.GetTablePrefix + cachetable);
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlDependency.Stop(BaseConfigs.GetDBConnectString);
        }
    }
}