using System;
using System.Data;

namespace SAS.Data.DataProvider
{
    /// <summary>
    /// 主题分类
    /// </summary>
    public class TopicTypes
    {
        /// <summary>
        /// 获取主题分类
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTopicTypes(string searthKeyWord)
        {
            return DatabaseProvider.GetInstance().GetTopicTypes(searthKeyWord);
        }
    }
}
