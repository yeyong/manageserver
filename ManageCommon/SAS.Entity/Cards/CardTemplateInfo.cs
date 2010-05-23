using System;

namespace SAS.Entity
{
    /// <summary>
    /// 名片模板实体。
    /// </summary>
    [Serializable]
    public class CardTemplateInfo
    {
        #region Model
        private int _id;
        private string _directory;
        private string _name;
        private string _author;
        private string _createdate;
        private string _ver;
        private string _copyright;
        private string _currentfile;
        /// <summary>
        /// 模板ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 存放模板路径
        /// </summary>
        public string directory
        {
            set { _directory = value; }
            get { return _directory; }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 模板作者
        /// </summary>
        public string author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createdate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string ver
        {
            set { _ver = value; }
            get { return _ver; }
        }
        /// <summary>
        /// 版权
        /// </summary>
        public string copyright
        {
            set { _copyright = value; }
            get { return _copyright; }
        }
        /// <summary>
        /// 当前使用图片（动态水印背景图）|当前flash（时参）|当前js（版本）|当前silverlight文件
        /// </summary>
        public string currentfile
        {
            set { _currentfile = value; }
            get { return _currentfile; }
        }
        #endregion Model
    }
}
