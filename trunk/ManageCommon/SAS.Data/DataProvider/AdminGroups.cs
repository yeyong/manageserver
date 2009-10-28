using System;
using System.Data;

using SAS.Entity;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 管理组数据操作类
    /// </summary>
    public class AdminGroups
    {
        /// <summary>
        /// 获得到指定管理组信息
        /// </summary>
        /// <returns>管理组信息</returns>
        public static AdminGroupInfo[] GetAdminGroupList()
        {
            DataTable dt = DatabaseProvider.GetInstance().GetAdminGroupList();
            AdminGroupInfo[] admingroupArray = new AdminGroupInfo[dt.Rows.Count];
            int Index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                admingroupArray[Index] = LoadAdminGroupInfo(dr);
                Index++;
            }
            dt.Dispose();
            return admingroupArray;
        }

        private static AdminGroupInfo LoadAdminGroupInfo(DataRow dr)
        {
            AdminGroupInfo admingroup = new AdminGroupInfo();
            admingroup.Admingid = int.Parse(dr["pg_id"].ToString());
            admingroup.AdminGroupName = dr["pg_name"].ToString();
            admingroup.Pg_allowSelf = byte.Parse(dr["pg_allowSys"].ToString());
            admingroup.Pg_allowSys = byte.Parse(dr["pg_allowSys"].ToString());
            admingroup.Pg_status = byte.Parse(dr["pg_status"].ToString());
            admingroup.Pg_ext1 = dr["pg_ext1"].ToString();
            admingroup.Alloweditpost = byte.Parse(dr["alloweditpost"].ToString());
            admingroup.Allowstickthread = byte.Parse(dr["allowstickthread"].ToString());
            admingroup.Allowmodpost = byte.Parse(dr["allowmodpost"].ToString());
            admingroup.Allowdelpost = byte.Parse(dr["allowdelpost"].ToString());
            admingroup.Allowmassprune = byte.Parse(dr["allowmassprune"].ToString());
            //admingroup.Allowrefund = byte.Parse(dr["allowrefund"].ToString());
            admingroup.Allowcensorword = byte.Parse(dr["allowcensorword"].ToString());
            admingroup.Allowviewip = byte.Parse(dr["allowviewip"].ToString());
            admingroup.Allowbanip = byte.Parse(dr["allowbanip"].ToString());
            admingroup.Allowedituser = byte.Parse(dr["allowedituser"].ToString());
            admingroup.Allowmoduser = byte.Parse(dr["allowmoduser"].ToString());
            admingroup.Allowbanuser = byte.Parse(dr["allowbanuser"].ToString());
            admingroup.Allowpostannounce = byte.Parse(dr["allowpostannounce"].ToString());
            admingroup.Allowviewlog = byte.Parse(dr["allowviewlog"].ToString());
            admingroup.Allowviewrealname = byte.Parse(dr["allowviewrealname"].ToString());
            return admingroup;
        }

        /// <summary>
        /// 设置管理组信息
        /// </summary>
        /// <param name="admingroupsInfo">管理组信息</param>
        /// <returns>更改记录数</returns>
        public static int SetAdminGroupInfo(AdminGroupInfo admingroupsInfo)
        {
            return DatabaseProvider.GetInstance().SetAdminGroupInfo(admingroupsInfo);
        }

        /// <summary>
        /// 创建一个新的管理组信息
        /// </summary>
        /// <param name="__admingroupsInfo">要添加的管理组信息</param>
        /// <returns>更改记录数</returns>
        public static int CreateAdminGroupInfo(AdminGroupInfo admingroupsInfo)
        {
            return DatabaseProvider.GetInstance().CreateAdminGroupInfo(admingroupsInfo);
        }

        /// <summary>
        /// 删除指定的管理组信息
        /// </summary>
        /// <param name="admingid">管理组ID</param>
        /// <returns>更改记录数</returns>
        public static int DeleteAdminGroupInfo(short admingid)
        {
            return DatabaseProvider.GetInstance().DeleteAdminGroupInfo(admingid);
        }

        public static void ChangeUserAdminidByGroupid(int radminId, int groupId)
        {
            DatabaseProvider.GetInstance().UpdateUserAdminIdByGroupId(radminId, groupId);
        }
    }
}
