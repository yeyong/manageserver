using System;

namespace SAS.Entity
{
    /// <summary>
    /// 枚举操作
    /// </summary>
    public class EnumCatch
    {
        /// <summary>
        /// 获取企业类型名称
        /// </summary>
        /// <param name="ete"></param>
        /// <returns></returns>
        public static string GetCompanyType(EnTypeEnum ete)
        {
            return GetCompanyType(Convert.ToInt16(ete));
        }
        /// <summary>
        /// 获取企业类型名称
        /// </summary>
        /// <param name="ete"></param>
        /// <returns></returns>
        public static string GetCompanyType(int ete)
        {
            string cname = "";
            switch (ete)
            {
                case 1:
                    cname = "生产商";
                    break;
                case 2:
                    cname = "代理经销商";
                    break;
                case 3:
                    cname = "个人经销商";
                    break;
                case 4:
                    cname = "门店";
                    break;
                case 5:
                    cname = "原料商";
                    break;
                case 6:
                    cname = "分销商";
                    break;
                case 7:
                    cname = "服务站";
                    break;
                case 8:
                    cname = "其他";
                    break;
            }
            return cname;
        }
        /// <summary>
        /// 获取企业经济类型名称
        /// </summary>
        /// <param name="cte"></param>
        /// <returns></returns>
        public static string GetEnCommType(CommTypeEnum cte)
        {
            return GetEnCommType(Convert.ToInt16(cte));
        }
        /// <summary>
        /// 获取企业经济类型名称
        /// </summary>
        /// <param name="cte"></param>
        /// <returns></returns>
        public static string GetEnCommType(int cte)
        {
            string ecname = "";
            switch (cte)
            {
                case 1:
                    ecname = "有限责任公司";
                    break;
                case 2:
                    ecname = "股份有限公司";
                    break;
                case 3:
                    ecname = "国营公司";
                    break;
                case 4:
                    ecname = "集团公司";
                    break;
                case 5:
                    ecname = "合资企业";
                    break;
                case 6:
                    ecname = "外企";
                    break;
            }
            return ecname;
        }
    }

    /// <summary>
    /// 企业类型枚举
    /// </summary>
    public enum EnTypeEnum
    {
        /// <summary>
        /// 生产商
        /// </summary>
        Manufacturer = 1,
        /// <summary>
        /// 代理经销商
        /// </summary>
        Dealers = 2,
        /// <summary>
        /// 个人经销商
        /// </summary>
        IndividualDealer = 3,
        /// <summary>
        /// 门店
        /// </summary>
        Store = 4,
        /// <summary>
        /// 原料商
        /// </summary>
        MaterialSupplier = 5,
        /// <summary>
        /// 分销商
        /// </summary>
        Distributor = 6,
        /// <summary>
        /// 服务站
        /// </summary>
        Workstation = 7,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 8
    }
    /// <summary>
    /// 企业经济类型
    /// </summary>
    public enum CommTypeEnum
    {
        /// <summary>
        /// 有限责任公司
        /// </summary>
        CoLtd = 1,
        /// <summary>
        /// 股份有限公司
        /// </summary>
        CoStock = 2,
        /// <summary>
        /// 国营公司
        /// </summary>
        CoState = 3,
        /// <summary>
        /// 集团公司
        /// </summary>
        CoGroup = 4,
        /// <summary>
        /// 合资企业
        /// </summary>
        CoJoint = 5,
        /// <summary>
        /// 外企
        /// </summary>
        CoForeign = 6
    }
}
