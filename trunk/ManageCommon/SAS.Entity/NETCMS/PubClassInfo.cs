using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Entity
{
    /// <summary>
    /// 新闻栏目实体
    /// </summary>
    [Serializable]
    public class PubClassInfo
    {
        private int _id = 0;
        private string _classID = "";
        private string _classCName = "";
        private string _classEName = "";
        private string _parentID = "";
        private string _savePath = "";
        private string _saveClassframe = "";

        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string ClassID
        {
            set { _classID = value; }
            get { return _classID; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ClassCName
        {
            set { _classCName = value; }
            get { return _classCName; }
        }
        /// <summary>
        /// 栏目英文
        /// </summary>
        public string ClassEName
        {
            set { _classEName = value; }
            get { return _classEName; }
        }
        /// <summary>
        /// 父栏目
        /// </summary>
        public string ParentID
        {
            set { _parentID = value; }
            get { return _parentID; }
        }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            set { _savePath = value; }
            get { return _savePath; }
        }
        /// <summary>
        /// 保存栏目生成目录结构
        /// </summary>
        public string SaveClassframe
        {
            set { _saveClassframe = value; }
            get { return _saveClassframe; }
        }
    }
}
