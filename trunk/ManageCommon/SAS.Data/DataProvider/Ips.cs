using System;
using System.Text;
using System.Data;

using SAS.Common;
using SAS.Entity;
using SAS.Common.Generic;

namespace SAS.Data.DataProvider
{
    public class Ips
    {
        /// <summary>
        /// 添加被禁止的ip
        /// </summary>
        /// <param name="ip1">ip段</param>
        /// <param name="ip2">ip段</param>
        /// <param name="ip3">ip段</param>
        /// <param name="ip4">ip段</param>
        /// <param name="username">添加人</param>
        /// <param name="deteline">起始时间</param>
        /// <param name="expiration">过期时间</param>
        public static void AddBannedIp(IpInfo info)
        {
            DatabaseProvider.GetInstance().AddBannedIp(info);
        }

        /// <summary>
        /// 获取被禁止ip数量
        /// </summary>
        /// <returns></returns>
        //public static int GetBannedIpCount()
        //{
        //    return DatabaseProvider.GetInstance().GetAdminCount();
        //}

        /// <summary>
        /// 获得被禁止ip列表
        /// </summary>
        /// <returns></returns>
        public static List<IpInfo> GetBannedIpList()
        {
            return GetIpInfoList(DatabaseProvider.GetInstance().GetBannedIpList());
        }

        /// <summary>
        /// 获取指定分页的禁止IP列表
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public static List<IpInfo> GetBannedIpList(int num, int pageid)
        {
            return GetIpInfoList(DatabaseProvider.GetInstance().GetBannedIpList(num, pageid));
        }

        private static List<IpInfo> GetIpInfoList(IDataReader reader)
        {
            List<IpInfo> list = new List<IpInfo>();
            while (reader.Read())
            {
                IpInfo ipinfo = new IpInfo();
                ipinfo.Id = TypeConverter.ObjectToInt(reader["bdid"], 0);
                ipinfo.Ip1 = TypeConverter.ObjectToInt(reader["ip1"], 0);
                ipinfo.Ip2 = TypeConverter.ObjectToInt(reader["ip2"], 0);
                ipinfo.Ip3 = TypeConverter.ObjectToInt(reader["ip3"], 0);
                ipinfo.Ip4 = TypeConverter.ObjectToInt(reader["ip4"], 0);
                ipinfo.Username = reader["admin"].ToString();
                ipinfo.Dateline = Convert.ToDateTime(reader["dateline"].ToString()).ToString("yyyy-MM-dd");
                ipinfo.Expiration = Convert.ToDateTime(reader["expiration"].ToString()).ToString("yyyy-MM-dd");
                list.Add(ipinfo);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 显示被禁止的ip数量
        /// </summary>
        /// <returns></returns>
        public static int GetBannedIpCount()
        {
            return DatabaseProvider.GetInstance().GetBannedIpCount();
        }

        /// <summary>
        /// 删除选中的ip地址段
        /// </summary>
        /// <param name="bannedIdList"></param>
        public static void DelBanIp(string iplist)
        {
            DatabaseProvider.GetInstance().DeleteBanIp(iplist);
        }
        
        /// <summary>
        /// 编辑banip结束时间
        /// </summary>
        /// <param name="iplist"></param>
        /// <param name="endTime"></param>
        public static void EditBanIp(int id, string endtime)
        {
            try
            {
                DateTime endTime;
                DateTime.TryParse(endtime, out endTime);
                DatabaseProvider.GetInstance().UpdateBanIpExpiration(id, endTime.ToString());
            }
            catch { }
        }

        //public static string GetBanIpPoster(int id)
        //{
        //    return DatabaseProvider.GetInstance().GetBanIpPoster(id);
        //}

    }
}
