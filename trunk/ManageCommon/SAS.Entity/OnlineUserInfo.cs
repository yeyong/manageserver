using System;

namespace SAS.Entity
{
    /// <summary>
    /// 在线用户信息
    /// </summary>
    public class OnlineUserInfo
    {
        #region Model
        private int _ol_id;
        private Guid _ol_ps_id;
        private string _ol_ip;
        private string _ol_name;
        private string _ol_nickname;
        private string _ol_password;
        private int _ol_pg_id;
        private int _ol_ug_id;
        private string _ol_img;
        private bool _ol_invisible;
        private int _ol_action;
        private int _ol_lastactivity;
        private string _ol_lastpostpmtime;
        private string _ol_lastsearchtime;
        private string _ol_lastupdatetime;
        private int _ol_pm_id;
        private string _ol_pm_name;
        private string _ol_verifycode;
        private int _ol_newpms;  //新短消息数
        private int _ol_newnotices;  //新通知数

        //private int _ol_onlinestate;

        /// <summary>
        /// 在线临时ID
        /// </summary>
        public int ol_id
        {
            set { _ol_id = value; }
            get { return _ol_id; }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid ol_ps_id
        {
            set { _ol_ps_id = value; }
            get { return _ol_ps_id; }
        }

        /// <summary>
        /// 在线IP
        /// </summary>
        public string ol_ip
        {
            set { _ol_ip = value; }
            get { return _ol_ip; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string ol_name
        {
            set { _ol_name = value; }
            get { return _ol_name; }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string ol_nickName
        {
            set { _ol_nickname = value; }
            get { return _ol_nickname; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string ol_password
        {
            set { _ol_password = value; }
            get { return _ol_password; }
        }

        /// <summary>
        /// 管理组ID
        /// </summary>
        public int ol_pg_id
        {
            set { _ol_pg_id = value; }
            get { return _ol_pg_id; }
        }

        /// <summary>
        /// 用户组ID
        /// </summary>
        public int ol_ug_id
        {
            set { _ol_ug_id = value; }
            get { return _ol_ug_id; }
        }

        /// <summary>
        /// 在线用户图例
        /// </summary>
        public string ol_img
        {
            set { _ol_img = value; }
            get { return _ol_img; }
        }

        /// <summary>
        /// 在线状态（是否隐身）
        /// </summary>
        public bool ol_invisible
        {
            set { _ol_invisible = value; }
            get { return _ol_invisible; }
        }

        /// <summary>
        /// 操作
        /// </summary>
        public int ol_action
        {
            set { _ol_action = value; }
            get { return _ol_action; }
        }

        /// <summary>
        /// 上一次所做的操作
        /// </summary>
        public int ol_lastactivity
        {
            set { _ol_lastactivity = value; }
            get { return _ol_lastactivity; }
        }

        /// <summary>
        /// 最后一次发送短消息时间
        /// </summary>
        public string ol_lastpostpmtime
        {
            set { _ol_lastpostpmtime = value; }
            get { return _ol_lastpostpmtime; }
        }

        /// <summary>
        /// 最后一次搜索时间
        /// </summary>
        public string ol_lastsearchtime
        {
            set { _ol_lastsearchtime = value; }
            get { return _ol_lastsearchtime; }
        }

        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public string ol_lastupdatetime
        {
            set { _ol_lastupdatetime = value; }
            get { return _ol_lastupdatetime; }
        }

        /// <summary>
        /// 最后一次所在栏目ID
        /// </summary>
        public int ol_pm_id
        {
            set { _ol_pm_id = value; }
            get { return _ol_pm_id; }
        }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ol_pm_name
        {
            set { _ol_pm_name = value; }
            get { return _ol_pm_name; }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ol_verifycode
        {
            set { _ol_verifycode = value; }
            get { return _ol_verifycode; }
        }

        ///<summary>
        ///新短消息数
        ///</summary>
        public int ol_newpms
        {
            get { return _ol_newpms; }
            set { _ol_newpms = value; }
        }
        ///<summary>
        ///新通知数
        ///</summary>
        public int ol_newnotices
        {
            get { return _ol_newnotices; }
            set { _ol_newnotices = value; }
        }

        ///// <summary>
        ///// 在线状态
        ///// </summary>
        //public int ol_onlinestate
        //{
        //    set { _ol_onlinestate = value; }
        //    get { return _ol_onlinestate; }
        //}
        #endregion Model
}
