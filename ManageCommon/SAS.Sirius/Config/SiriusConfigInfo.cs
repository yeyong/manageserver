using System;
using System.Text;

using SAS.Config;

namespace SAS.Sirius.Config
{
    /// <summary>
    /// Sirius studio 配置信息
    /// </summary>
    [Serializable]
    public class SiriusConfigInfo :IConfigInfo
    {
        private string m_fileurladdress = "http://sirius.com";  //文件地址
        private string m_imgurladdress = "http://sirius.com";   //图片地址

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileUrlAddress
        {
            get { return m_fileurladdress; }
            set { m_fileurladdress = value; }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrlAddress
        {
            get { return m_imgurladdress; }
            set { m_imgurladdress = value; }
        }
    }
}
