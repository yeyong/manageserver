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
                case 0:
                    cname = "生产商";
                    break;
                case 1:
                    cname = "代理经销商";
                    break;
                case 2:
                    cname = "个人经销商";
                    break;
                case 3:
                    cname = "门店";
                    break;
                case 4:
                    cname = "原料商";
                    break;
                case 5:
                    cname = "分销商";
                    break;
                case 6:
                    cname = "服务站";
                    break;
                case 7:
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
                case 0:
                    ecname = "有限责任公司";
                    break;
                case 1:
                    ecname = "股份有限公司";
                    break;
                case 2:
                    ecname = "国营公司";
                    break;
                case 3:
                    ecname = "集团公司";
                    break;
                case 4:
                    ecname = "合资企业";
                    break;
                case 5:
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
        Manufacturer = 0,
        /// <summary>
        /// 代理经销商
        /// </summary>
        Dealers = 1,
        /// <summary>
        /// 个人经销商
        /// </summary>
        IndividualDealer = 2,
        /// <summary>
        /// 门店
        /// </summary>
        Store = 3,
        /// <summary>
        /// 原料商
        /// </summary>
        MaterialSupplier = 4,
        /// <summary>
        /// 分销商
        /// </summary>
        Distributor = 5,
        /// <summary>
        /// 服务站
        /// </summary>
        Workstation = 6,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 7
    }
    /// <summary>
    /// 企业经济类型
    /// </summary>
    public enum CommTypeEnum
    {
        /// <summary>
        /// 有限责任公司
        /// </summary>
        CoLtd = 0,
        /// <summary>
        /// 股份有限公司
        /// </summary>
        CoStock = 1,
        /// <summary>
        /// 国营公司
        /// </summary>
        CoState = 2,
        /// <summary>
        /// 集团公司
        /// </summary>
        CoGroup = 3,
        /// <summary>
        /// 合资企业
        /// </summary>
        CoJoint = 4,
        /// <summary>
        /// 外企
        /// </summary>
        CoForeign = 5
    }
}
