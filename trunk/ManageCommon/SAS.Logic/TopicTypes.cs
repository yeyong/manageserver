using System;
using System.Text;
using System.Data;
using SAS.Entity;

namespace SAS.Logic
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
            return Data.DataProvider.TopicTypes.GetTopicTypes(searthKeyWord);
        }

        public static DataTable GetTopicTypes()
        {
            return GetTopicTypes("");
        }
    }
}
