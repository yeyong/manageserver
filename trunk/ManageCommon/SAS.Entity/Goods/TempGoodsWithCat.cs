using System;

namespace SAS.Entity
{
    /// <summary>
    /// 类别、产品临时存放实体（商记用）
    /// </summary>
    [Serializable]
    public class TempGoodsWithCat
    {
        private int _id = 0;            //序号
        private string _goodid = "0";   //商品ID
        private string _goodname = "";  //商品名称
        private int _catid = 0;    //类别ID
        private string _catname = "";   //类别名称
        private string _picurl = "";    //图片路径

        /// <summary>
        /// 序号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string GoodID
        {
            set { _goodid = value; }
            get { return _goodid; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName
        {
            set { _goodname = value; }
            get { return _goodname; }
        }
        /// <summary>
        /// 类别ID
        /// </summary>
        public int CatID
        {
            set { _catid = value; }
            get { return _catid; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CatName
        {
            set { _catname = value; }
            get { return _catname; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicUrl
        {
            set { _picurl = value; }
            get { return _picurl; }
        }
    }
}
