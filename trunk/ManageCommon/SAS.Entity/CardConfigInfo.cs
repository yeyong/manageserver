using System;

namespace SAS.Entity
{
    /// <summary>
    /// 名片配置文件信息
    /// </summary>
    [Serializable]
    public class CardConfigInfo
    {
        #region Model
        private int _id;
        private string _ccname;
        private int _tid;
        private int _hasflash;
        private int _hasimage;
        private int _hasjs;
        private int _hassilverlight;
        private string _showparams;
        private string _createdate;
        private string _vailddate;
        private string t_name = "default";
        private string t_directory = "default";
        private string t_currentfile = "";
        /// <summary>
        /// 配置ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 配置文件名
        /// </summary>
        public string ccname
        {
            set { _ccname = value; }
            get { return _ccname; }
        }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int tid
        {
            set { _tid = value; }
            get { return _tid; }
        }
        /// <summary>
        /// 是否有flash模式名片（默认0，无）
        /// </summary>
        public int hasflash
        {
            set { _hasflash = value; }
            get { return _hasflash; }
        }
        /// <summary>
        /// 是否有图片模式名片（默认0，无）
        /// </summary>
        public int hasimage
        {
            set { _hasimage = value; }
            get { return _hasimage; }
        }
        /// <summary>
        /// 是否有JS模式名片
        /// </summary>
        public int hasjs
        {
            set { _hasjs = value; }
            get { return _hasjs; }
        }
        /// <summary>
        /// 是否有silverlight模式名片
        /// </summary>
        public int hassilverlight
        {
            set { _hassilverlight = value; }
            get { return _hassilverlight; }
        }
        /// <summary>
        /// 显示字段与位置参数（例：2|1,4|2）
        /// </summary>
        public string showparams
        {
            set { _showparams = value; }
            get { return _showparams; }
        }
        /// <summary>
        /// 配置文件创建时间
        /// </summary>
        public string createdate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public string vailddate
        {
            set { _vailddate = value; }
            get { return _vailddate; }
        }
        /// <summary>
        /// 模板名
        /// </summary>
        public string name
        {
            set { t_name = value; }
            get { return t_name; }
        }
        /// <summary>
        /// 模板路径
        /// </summary>
        public string directory
        {
            set { t_directory = value; }
            get { return t_directory; }
        }
        /// <summary>
        /// 当前文件参数
        /// </summary>
        public string currentfile
        {
            set { t_currentfile = value; }
            get { return t_currentfile; }
        }
        #endregion Model
    }
}
