using System;

namespace SAS.Entity
{
    /// <summary>
    /// 淘宝专题信息
    /// </summary>
    [Serializable]
    public class TaoBaoTopicInfo
    {
        private long m_tid = 0;
        private string m_title = "";
        private int m_type = 0;
        private int m_order = 0;
        private string m_pic = "";
        private string m_url = "";
        private int m_width = 0;
        private int m_height = 0;

        /// <summary>
        /// 淘宝专题ID
        /// </summary>
        public long Tid
        {
            set { m_tid = value; }
            get { return m_tid; }
        }
        /// <summary>
        /// 淘宝专题标题
        /// </summary>
        public string Title
        {
            set { m_title = value; }
            get { return m_title; }
        }
        /// <summary>
        /// 淘宝专题类型
        /// </summary>
        public int Type
        {
            set { m_type = value; }
            get { return m_type; }
        }
        /// <summary>
        /// 淘宝专题顺序
        /// </summary>
        public int Order
        {
            set { m_order = value; }
            get { return m_order; }
        }
        /// <summary>
        /// 淘宝专题图片
        /// </summary>
        public string Pic
        {
            set { m_pic = value; }
            get { return m_pic; }
        }
        /// <summary>
        /// 淘宝专题推广链接
        /// </summary>
        public string Url
        {
            set { m_url = value; }
            get { return m_url; }
        }
        /// <summary>
        /// 淘宝专题页面宽度
        /// </summary>
        public int Width
        {
            set { m_width = value; }
            get { return m_width; }
        }
        /// <summary>
        /// 淘宝专题页面高度
        /// </summary>
        public int Height
        {
            set { m_height = value; }
            get { return m_height; }
        }
    }
}
