using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SAS.Config;
using SAS.Data;

namespace SAS.Cache
{
    public interface IWebCache
    {
        //void Add(string key, object value);
        //object Get(string key);
        //void Remove(string key, bool useDependency);
    }

    public class WebCacheBase : IWebCache
    {
        //protected bool CheckParameters(string key)
        //{
        //    return !string.IsNullOrEmpty(key);
        //}

        //protected virtual CacheDependency GetCacheDependency(string key)
        //{
        //    return null;
        //}

        //protected virtual void OnBeforeAdd(string key)
        //{
        //}

        //public virtual void Add(string key, object value)
        //{
        //    if (!this.CheckParameters(key)) return;
        //    this.OnBeforeAdd(key);
        //    HttpRuntime.Cache.Insert(
        //        key,
        //        value,
        //        this.GetCacheDependency(key),
        //        System.Web.Caching.Cache.NoAbsoluteExpiration,
        //        System.Web.Caching.Cache.NoSlidingExpiration,
        //        System.Web.Caching.CacheItemPriority.Default,
        //        null);
        //}

        //public virtual object Get(string key)
        //{
        //    if (!this.CheckParameters(key)) return null;
        //    return HttpRuntime.Cache.Get(key);
        //}

        //public virtual void Remove(string key, bool useDependency)
        //{
        //    if (!this.CheckParameters(key)) return;
        //    HttpRuntime.Cache.Remove(key);
        //}
    }

    public class SqlDependencyWebCache : WebCacheBase
    {
        //private string _connectionString;

        //private SqlConnection GetConnection()
        //{
        //    return new SqlConnection(this._connectionString);
        //}

        //private SqlCommand GetSelectCommand(string key)
        //{
        //    SqlConnection conn = this.GetConnection();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "select Flag from dbo.CacheDependency where CacheKey=@CacheKey";
        //    SqlParameter sqlParam = cmd.Parameters.Add("@CacheKey", SqlDbType.VarChar, 50);
        //    sqlParam.Value = key;
        //    return cmd;
        //}

        //private SqlCommand GetUpdateCommand(string key)
        //{
        //    SqlConnection conn = this.GetConnection();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = @"if exists (select 1 from dbo.CacheDependency where CacheKey=@CacheKey)  update dbo.CacheDependency set Flag=case when isnull(flag,0)=0 then 1 else 1 end where CacheKey=@CacheKey else  insert into dbo.CacheDependency (CacheKey,Flag)  values (@CacheKey, 0)";
        //    SqlParameter sqlParam = cmd.Parameters.Add("@CacheKey", SqlDbType.VarChar, 50);
        //    sqlParam.Value = key;
        //    return cmd;
        //}

        //private void UpdateCacheDependency(string key)
        //{
        //    SqlCommand cmd = this.GetUpdateCommand(key);
        //    try
        //    {
        //        using (cmd.Connection)
        //        {
        //            cmd.Connection.Open();
        //            cmd.Prepare();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch
        //    {
        //        GC.Collect();
        //    }
        //}

        //protected override void OnBeforeAdd(string key)
        //{
        //    this.UpdateCacheDependency(key);
        //}

        //protected override CacheDependency GetCacheDependency(string key)
        //{
        //    SqlCommand cmd = this.GetSelectCommand(key);
        //    SqlCacheDependency sqlCacheDependency = new SqlCacheDependency(cmd);
        //    try
        //    {
        //        using (cmd.Connection)
        //        {
        //            cmd.Connection.Open();
        //            cmd.Prepare();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch
        //    {
        //        GC.Collect();
        //    }
        //    return sqlCacheDependency;
        //}

        //public SqlDependencyWebCache(string connectionString)
        //{
        //    this._connectionString = connectionString;
        //}

        //public override void Remove(string key, bool useDependency)
        //{
        //    if (!this.CheckParameters(key)) return;
        //    if (useDependency)
        //    {
        //        this.UpdateCacheDependency(key);
        //    }
        //    else
        //    {
        //        base.Remove(key, useDependency);
        //    }
        //}
    }
    public class WebCacheFactory
    {
        //public static IWebCache GetWebCache()
        //{
        //    string connectionString = BaseConfigs.GetDBConnectString;
        //    return new SqlDependencyWebCache(connectionString);
        //}
    }
}
