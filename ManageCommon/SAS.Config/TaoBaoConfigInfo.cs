using System;
using System.Text;

using SAS.Config;

namespace SAS.Config
{/// <summary>
    /// Sirius studio 配置信息
    /// </summary>
    [Serializable]
    public class TaoBaoConfigInfo : IConfigInfo
    {
        private string m_usernick = "yeyong2086521";                    //淘客昵称
        private string m_userid = "mm_13451138_0_0";                    //淘客ID
        private string m_appkey = "12005076";                           //Appkey
        private string m_appsecret = "64292c42ca49632200289324fba42572";//Appsecret

        /// <summary>
        /// 淘客昵称
        /// </summary>
        public string UserNick
        {
            get { return m_usernick; }
            set { m_usernick = value; }
        }
        /// <summary>
        /// 淘客ID
        /// </summary>
        public string UserID
        {
            get { return m_userid; }
            set { m_userid = value; }
        }
        /// <summary>
        /// Appkey
        /// </summary>
        public string AppKey
        {
            get { return m_appkey; }
            set { m_appkey = value; }
        }
        /// <summary>
        /// Appsecret
        /// </summary>
        public string AppSecret
        {
            get { return m_appsecret; }
            set { m_appsecret = value; }
        }
    }
}
