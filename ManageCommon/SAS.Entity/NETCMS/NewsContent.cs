using System;
using System.Text;

namespace SAS.Entity
{
    /// <summary>
    /// 新闻信息实体
    /// </summary>
    [Serializable]
    public class NewsContent
    {
        private int _id = 0;
        private string _newsid = "";
        private string _newstitle = "";

        /// <summary>
        /// 新闻自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 新闻唯一编号
        /// </summary>
        public string NewsID
        {
            set { _newsid = value; }
            get { return _newsid; }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle
        {
            set { _newstitle = value; }
            get { return _newstitle; }
        }
    }
}
