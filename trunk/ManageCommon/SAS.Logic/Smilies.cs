using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 表情符操作类
    /// </summary>
    public class Smilies
    {
        public static Regex[] regexSmile = null;

        static Smilies()
        {
            InitRegexSmilies();
        }

        /// <summary>
        /// 初始化表情正则对象数组
        /// </summary>
        public static void InitRegexSmilies()
        {
            SmiliesInfo[] smiliesList = Smilies.GetSmiliesListWithInfo();

            regexSmile = new Regex[smiliesList.Length];

            for (int i = 0; i < smiliesList.Length; i++)
            {
                regexSmile[i] = new Regex(@Regex.Escape(smiliesList[i].Code), RegexOptions.None);
            }
        }

        /// <summary>
        /// 重新加载并初始化表情正则对象数组
        /// </summary>
        /// <param name="smiliesList">表情对象数组</param>
        public static void ResetRegexSmilies(SmiliesInfo[] smiliesList)
        {
            int smiliesCount = smiliesList.Length;

            // 如果数目不同则重新创建数组, 以免发生数组越界
            if (regexSmile == null || regexSmile.Length != smiliesCount)
                regexSmile = new Regex[smiliesCount];

            for (int i = 0; i < smiliesCount; i++)
            {
                regexSmile[i] = new Regex(@Regex.Escape(smiliesList[i].Code), RegexOptions.None);
            }
        }

        /// <summary>
        /// 将缓存中的表情信息返回为SmiliesInfo[],不包括表情分类
        /// </summary>
        /// <returns></returns>
        public static SmiliesInfo[] GetSmiliesListWithInfo()
        {
            SASCache cache = SASCache.GetCacheService();
            SmiliesInfo[] smiliesInfoList = cache.RetrieveObject("/SAS/UI/SmiliesListWithInfo") as SmiliesInfo[];

            if (smiliesInfoList == null)
            {
                smiliesInfoList = SAS.Data.DataProvider.Smilies.GetSmiliesListWithoutType();

                cache.AddObject("/SAS/UI/SmiliesListWithInfo", smiliesInfoList);

                //表情缓存重新加载时重新初始化表情正则对象数组
                ResetRegexSmilies(smiliesInfoList);
            }
            return smiliesInfoList;
        }

        public static SmiliesInfo GetSmiliesById(int smiliesId)
        {
            SmiliesInfo[] smiliesInfoList = GetSmiliesListWithInfo();
            foreach (SmiliesInfo smiliesInfo in smiliesInfoList)
            {
                if (smiliesInfo.Id == smiliesId)
                    return smiliesInfo;
            }
            return null;
        }

        /// <summary>
        /// 获得表情分类列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSmiliesTypes()
        {
            return Data.DataProvider.Smilies.GetSmiliesTypes();
        }


        public static DataTable GetSmilieByType(int typeId)
        {
            return typeId > 0 ? Data.DataProvider.Smilies.GetSmiliesInfoByType(typeId) : new DataTable();
        }

        /// <summary>
        /// 清理空的表情分类
        /// </summary>
        /// <returns>被清理掉的空表情分类列表</returns>
        public static string ClearEmptySmiliesType()
        {
            string emptySmilieList = "";
            DataTable smilieType = SAS.Data.DataProvider.Smilies.GetSmilieTypes();
            foreach (DataRow dr in smilieType.Rows)
            {
                if (SAS.Data.DataProvider.Smilies.GetSmiliesInfoByType(int.Parse(dr["id"].ToString())).Rows.Count == 0)
                {
                    emptySmilieList += dr["code"].ToString() + ",";
                    SAS.Data.DataProvider.Smilies.DeleteSmilies(dr["id"].ToString());
                }
            }
            return emptySmilieList.TrimEnd(',');
        }

        /// <summary>
        /// 获取表情最大Id
        /// </summary>
        /// <returns></returns>
        public static int GetMaxSmiliesId()
        {
            return Data.DataProvider.Smilies.GetMaxSmiliesId();
        }

        /// <summary>
        /// 得到表情符数据
        /// </summary>
        /// <returns>表情符数据</returns>
        public static DataTable GetSmilies()
        {
            return Data.DataProvider.Smilies.GetSmilies();
        }

        /// <summary>
        /// 检测是否有相同表情
        /// </summary>
        /// <param name="code">表情代码</param>
        /// <param name="currentid">表情ID</param>
        /// <returns></returns>
        public static bool IsExistSameSmilieCode(string code, int currentid)
        {
            foreach (DataRow dr in Data.DataProvider.Smilies.GetSmiliesListDataTable().Rows)
            {
                if (dr["code"].ToString() == code && dr["id"].ToString() != currentid.ToString())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 按类型删除表情
        /// </summary>
        /// <param name="type">类型</param>
        public static void DeleteSmilyByType(int type)
        {
            if (type > 0)
                Data.DataProvider.Smilies.DeleteSmilyByType(type);
        }
    }//class
}
