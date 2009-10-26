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
        private int _ug_allowcusbbcode;
        private int _ug_allowvisit;
        private int _ug_allowcommunity;
        private int _ug_allowdown;
        private int _ug_allowup;
        private int _ug_allowsearch;
        private int _ug_allowavatar;
        private int _ug_allowshop;
        private int _ug_allowinvisible;
        private int _ug_allowhidecode;
        private int _ug_allowhtml;
        private int _ug_maxattachsize;
        private int _ug_maxsizeperday;
        private string _ug_attachextensions;
        private int _ug_maxspaceattachsize;
        private int _ug_maxspacephotosize;
        private int _ug_maxsigsize;
        private int _ug_pg_id;
        private string _ug_color;
        private int _ug_isSystem;
        private int m_allowsetreadperm;	//是否允许设置阅读积分权限
        private int m_allowpostattach;	//是否允许发布附件
        private int m_allowsetattachperm;	//是否允许设置下载积分限制
        private int m_stars;//星星数目
        private int m_allowpost;	//是否允许发新主题
        private int m_allowreply;	//是否允许回复
        private int m_allowpostpoll;	//是否允许发起投票
        private int m_allowvote;	//是否允许参与投票
        private int m_allownickname;	//是否允许使用昵称
        private int m_allowviewpro;	//是否允许查看用户资料
        private int m_allowviewstats;	//是否允许查看统计
        private int m_disableperiodctrl;	//是否不受时间段限制
        private int m_reasonpm;	//是否将操作理由短消息通知作者
        private int m_maxpmnum;	//短消息最多条数

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
            get { return _ug_name; }
            set
            {
                if ((_ug_color != null) && (_ug_color != string.Empty))
                    _ug_name = string.Format("<font color=\"{0}\">{1}</font>", _ug_color, value);
                else
                    _ug_name = value;
            }
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
        /// 是否允许使用bbcode0-不允许；1-允许
        /// </summary>
        public int Ug_allowcusbbcode
        {
            set { _ug_allowcusbbcode = value; }
            get { return _ug_allowcusbbcode; }
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
        /// 是否允许使用hide代码
        /// </summary>
        public int Ug_allowhidecode
        {
            set { _ug_allowhidecode = value; }
            get { return _ug_allowhidecode; }
        }

        /// <summary>
        /// 是否允许发布html贴
        /// </summary>
        public int Ug_allowhtml
        {
            set { _ug_allowhtml = value; }
            get { return _ug_allowhtml; }
        }

        /// <summary>
        /// 附件最大尺寸
        /// </summary>
        public int Ug_maxattachsize
        {
            set { _ug_maxattachsize = value; }
            get { return _ug_maxattachsize; }
        }

        /// <summary>
        /// 每天最大尺寸
        /// </summary>
        public int Ug_maxsizeperday
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

        ///<summary>
        ///是否允许设置阅读积分权限
        ///</summary>
        public int Allowsetreadperm
        {
            get { return m_allowsetreadperm; }
            set { m_allowsetreadperm = value; }
        }

        ///<summary>
        ///是否发布附件
        ///</summary>
        public int Allowpostattach
        {
            get { return m_allowpostattach; }
            set { m_allowpostattach = value; }
        }

        ///<summary>
        ///是否允许设置下载积分限制
        ///</summary>
        public int Allowsetattachperm
        {
            get { return m_allowsetattachperm; }
            set { m_allowsetattachperm = value; }
        }

        ///<summary>
        ///星星数目
        ///</summary>
        public int Stars
        {
            get { return m_stars; }
            set { m_stars = value; }
        }

        ///<summary>
        ///是否允许发帖
        ///</summary>
        public int Allowpost
        {
            get { return m_allowpost; }
            set { m_allowpost = value; }
        }

        ///<summary>
        ///是否允许回复
        ///</summary>
        public int Allowreply
        {
            get { return m_allowreply; }
            set { m_allowreply = value; }
        }

        ///<summary>
        ///是否允许发起投票
        ///</summary>
        public int Allowpostpoll
        {
            get { return m_allowpostpoll; }
            set { m_allowpostpoll = value; }
        }

        ///<summary>
        ///是否允许参与投票
        ///</summary>
        public int Allowvote
        {
            get { return m_allowvote; }
            set { m_allowvote = value; }
        }

        ///<summary>
        ///是否允许使用昵称
        ///</summary>
        public int Allownickname
        {
            get { return m_allownickname; }
            set { m_allownickname = value; }
        }

        ///<summary>
        ///是否允许查看用户资料
        ///</summary>
        public int Allowviewpro
        {
            get { return m_allowviewpro; }
            set { m_allowviewpro = value; }
        }
        ///<summary>
        ///是否允许查看统计
        ///</summary>
        public int Allowviewstats
        {
            get { return m_allowviewstats; }
            set { m_allowviewstats = value; }
        }
        ///<summary>
        ///是否不受时间段限制
        ///</summary>
        public int Disableperiodctrl
        {
            get { return m_disableperiodctrl; }
            set { m_disableperiodctrl = value; }
        }
        ///<summary>
        ///是否操作理由短消息通知作者
        ///</summary>
        public int Reasonpm
        {
            get { return m_reasonpm; }
            set { m_reasonpm = value; }
        }
        ///<summary>
        ///短消息最多条数
        ///</summary>
        public int Maxpmnum
        {
            get { return m_maxpmnum; }
            set { m_maxpmnum = value; }
        }


        #endregion Model
    }
}
