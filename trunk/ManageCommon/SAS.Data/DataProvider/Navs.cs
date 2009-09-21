using System;
using System.Text;
using System.Data;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Data.DataProvider
{
    public class Navs
    {
        /// <summary>
        /// 得到自定义菜单信息
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetNavigation()
        {
            List<NavInfo> info = new List<NavInfo>();
            IDataReader reader = GetNavigation(false);
            while (reader.Read())
            {
                NavInfo m = new NavInfo();
                m.Id = TypeConverter.ObjectToInt(reader["id"], 0);
                m.Level = TypeConverter.ObjectToInt(reader["level"], 0);
                m.Name = reader["name"].ToString();
                m.Parentid = TypeConverter.ObjectToInt(reader["parentid"], 0);
                m.Target = TypeConverter.ObjectToInt(reader["target"], 0);
                m.Title = reader["title"].ToString();
                m.Type = TypeConverter.ObjectToInt(reader["navstype"], 0);
                m.Url = reader["url"].ToString();
                m.Available = TypeConverter.ObjectToInt(reader["available"], 0);
                m.Displayorder = TypeConverter.ObjectToInt(reader["displayorder"], 0);
                info.Add(m);
            }
            reader.Close();
            return info;
        }

        /// <summary>
        /// 得到自定义菜单
        /// </summary>
        /// <returns></returns>
        public static IDataReader GetNavigation(bool getAll)
        {
            return DatabaseProvider.GetInstance().GetNavigation(getAll);
        }

        /// <summary>
        /// 得到自定义菜单不重复的PARENTID
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetNavigationHasSub()
        {
            List<NavInfo> info = new List<NavInfo>();
            IDataReader reader = DatabaseProvider.GetInstance().GetNavigationHasSub();
            while (reader.Read())
            {
                NavInfo m = new NavInfo();
                m.Parentid = TypeConverter.ObjectToInt(reader["parentid"], 0);
                info.Add(m);
            }
            reader.Close();
            return info;
        }

        /// <summary>
        /// 删除导航
        /// </summary>
        /// <param name="id">菜单ID</param>
        public static void DeleteNavigation(int id)
        {
            DatabaseProvider.GetInstance().DeleteNavigation(id);
        }

        /// <summary>
        /// 添加导航菜单
        /// </summary>
        /// <param name="nav">导航菜单类</param>
        public static void InsertNavigation(NavInfo nav)
        {
            DatabaseProvider.GetInstance().InsertNavigation(nav);
        }

        /// <summary>
        /// 更新导航菜单
        /// </summary>
        /// <param name="nav">导航菜单类</param>
        public static void UpdateNavigation(NavInfo nav)
        {
            DatabaseProvider.GetInstance().UpdateNavigation(nav);
        }

    }
}
