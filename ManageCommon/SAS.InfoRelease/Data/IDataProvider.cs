using System;
using System.Data;
using System.Data.Common;
using System.Text;

using SAS.Common.Generic;
using SAS.Entity.InfoPlatform;

namespace SAS.InfoRelease.Data
{
    public interface IDataProvider
    {
        /// <summary>
        /// 添加注册时信息
        /// </summary>
        int InsertUser(UserInfo eui);
        UserInfo GetUserByName(string lname);
    }
}
