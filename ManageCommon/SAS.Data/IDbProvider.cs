﻿using System;
using System.Data;
using System.Data.Common;

namespace SAS.Data
{
    public interface IDbProvider
    {
        /// <summary>
        /// 返回DbProviderFactory实例
        /// </summary>
        /// <returns></returns>
#if NET1        
		IDbProviderFactory Instance();
#else
        DbProviderFactory Instance();
#endif

        /// <summary>
        /// 检索SQL参数信息并填充
        /// </summary>
        /// <param name="cmd"></param>
        void DeriveParameters(IDbCommand cmd);

        /// <summary>
        /// 创建SQL参数
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
#if NET1  
		IDataParameter MakeParam(string ParamName, DbType DbType, Int32 Size);
#else
        DbParameter MakeParam(string ParamName, DbType DbType, Int32 Size);
#endif

        /// <summary>
        /// 是否支持全文搜索
        /// </summary>
        /// <returns></returns>
        bool IsFullTextSearchEnabled();

        /// <summary>
        /// 是否支持压缩数据库
        /// </summary>
        /// <returns></returns>
        bool IsCompactDatabase();

        /// <summary>
        /// 是否支持备份数据库
        /// </summary>
        /// <returns></returns>
        bool IsBackupDatabase();

        /// <summary>
        /// 返回刚插入记录的自增ID值, 如不支持则为""
        /// </summary>
        /// <returns></returns>
        string GetLastIdSql();
        /// <summary>
        /// 是否支持数据库优化
        /// </summary>
        /// <returns></returns>
        bool IsDbOptimize();
        /// <summary>
        /// 是否支持数据库收缩
        /// </summary>
        /// <returns></returns>
        bool IsShrinkData();
        /// <summary>
        /// 是否支持存储过程
        /// </summary>
        /// <returns></returns>
        bool IsStoreProc();
    }
}
