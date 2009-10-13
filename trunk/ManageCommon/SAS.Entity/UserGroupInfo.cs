using System;

namespace SAS.Entity
{
    /// <summary>
    /// 用户组信息（个人用户组）
    /// </summary>
    public class UserGroupInfo
    {
        #region Model
        private int _ug_id;
        private string _ug_name;
        private int _ug_scorehight;
        private int _ug_scorelow;
        private string _ug_logo;
        private int _ug_readaccess;
        private int _ug_allowvisit;
        private int _ug_allowcommunity;
        private int _ug_allowdown;
        private int _ug_allowup;
        private int _ug_allowsearch;
        private int _ug_allowavatar;
        private int _ug_allowshop;
        private int _ug_allowinvisible;
        private int _ug_maxattachsize;
        private int _ug_maxsizeperday;
        private string _ug_attachextensions;
        private int _ug_maxspaceattachsize;
        private int _ug_maxspacephotosize;
        private int _ug_maxsigsize;
        private int _ug_pg_id;
        private string _ug_color;
        private int _ug_isSystem;       

        /// <summary>
        /// 用户组ID
        /// </summary>
        public int ug_id
        {
            set { _ug_id = value; }
            get { return _ug_id; }
        }

        /// <summary>
        /// 用户组名称
        /// </summary>
        public string ug_name
        {
            set { _ug_name = value; }
            get { return _ug_name; }
        }

        /// <summary>
        /// 用户组积分下限
        /// </summary>
        public int ug_scorehight
        {
            set { _ug_scorehight = value; }
            get { return _ug_scorehight; }
        }

        /// <summary>
        /// 用户组积分上限
        /// </summary>
        public int ug_scorelow
        {
            set { _ug_scorelow = value; }
            get { return _ug_scorelow; }
        }

        /// <summary>
        /// 用户组图标
        /// </summary>
        public string ug_logo
        {
            set { _ug_logo = value; }
            get { return _ug_logo; }
        }

        /// <summary>
        /// 阅读权限
        /// </summary>
        public int ug_readaccess
        {
            set { _ug_readaccess = value; }
            get { return _ug_readaccess; }
        }

        /// <summary>
        /// 是否允许访问网站（默认1，可以访问）
        /// </summary>
        public int ug_allowvisit
        {
            set { _ug_allowvisit = value; }
            get { return _ug_allowvisit; }
        }

        /// <summary>
        /// 是否允许评论（默认1，可以）
        /// </summary>
        public int ug_allowcommunity
        {
            set { _ug_allowcommunity = value; }
            get { return _ug_allowcommunity; }
        }

        /// <summary>
        /// 是否允许下载（默认1，可以）
        /// </summary>
        public int ug_allowdown
        {
            set { _ug_allowdown = value; }
            get { return _ug_allowdown; }
        }

        /// <summary>
        /// 是否允许上传（默认1，可以）
        /// </summary>
        public int ug_allowup
        {
            set { _ug_allowup = value; }
            get { return _ug_allowup; }
        }

        /// <summary>
        /// 是否允许搜索（默认1，可以）
        /// </summary>
        public int ug_allowsearch
        {
            set { _ug_allowsearch = value; }
            get { return _ug_allowsearch; }
        }

        /// <summary>
        /// 是否允许使用头像, 0=不允许, 1=允许使用系统自带头像, 2=允许使用Url地址头像(且包括1), 3允许使用上传头像(且包括1和2)
        /// </summary>
        public int ug_allowavatar
        {
            set { _ug_allowavatar = value; }
            get { return _ug_allowavatar; }
        }

        /// <summary>
        /// 是否允许开店（默认0，不可以）
        /// </summary>
        public int ug_allowshop
        {
            set { _ug_allowshop = value; }
            get { return _ug_allowshop; }
        }

        /// <summary>
        /// 是否允许隐身（默认1，允许）
        /// </summary>
        public int ug_allowinvisible
        {
            set { _ug_allowinvisible = value; }
            get { return _ug_allowinvisible; }
        }

        /// <summary>
        /// 附件最大尺寸
        /// </summary>
        public int ug_maxattachsize
        {
            set { _ug_maxattachsize = value; }
            get { return _ug_maxattachsize; }
        }

        /// <summary>
        /// 每天最大尺寸
        /// </summary>
        public int ug_maxsizeperday
        {
            set { _ug_maxsizeperday = value; }
            get { return _ug_maxsizeperday; }
        }

        /// <summary>
        /// 签名最多字节
        /// </summary>
        public int ug_maxsigsize
        {
            set { _ug_maxsigsize = value; }
            get { return _ug_maxsigsize; }
        }

        /// <summary>
        /// 允许附件扩展类型
        /// </summary>
        public string ug_attachextensions
        {
            set { _ug_attachextensions = value; }
            get { return _ug_attachextensions; }
        }

        /// <summary>
        /// 最大附件尺寸
        /// </summary>
        public int ug_maxspaceattachsize
        {
            set { _ug_maxspaceattachsize = value; }
            get { return _ug_maxspaceattachsize; }
        }

        /// <summary>
        /// 最大照片尺寸
        /// </summary>
        public int ug_maxspacephotosize
        {
            set { _ug_maxspacephotosize = value; }
            get { return _ug_maxspacephotosize; }
        }

        /// <summary>
        /// 关联管理ID
        /// </summary>
        public int ug_pg_id
        {
            set { _ug_pg_id = value; }
            get { return _ug_pg_id; }
        }

        /// <summary>
        /// 用户名称颜色
        /// </summary>
        public string ug_color
        {
            set { _ug_color = value; }
            get { return _ug_color; }
        }

        /// <summary>
        /// 是否为系统创建
        /// </summary>
        public int ug_isSystem
        {
            set { _ug_isSystem = value; }
            get { return _ug_isSystem; }
        }
        #endregion Model
    }
}
