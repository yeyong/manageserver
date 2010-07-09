using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.joint.img
    /// </summary>
    public class ItemJointImgRequest : INTWRequest
    {
        public Nullable<long> Id { get; set; }
        public string Iid { get; set; }
        public Nullable<bool> IsMajor { get; set; }
        public Nullable<long> NumIid { get; set; }
        public string PicPath { get; set; }
        public Nullable<int> Position { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.joint.img";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("iid", this.Iid);
            parameters.Add("is_major", this.IsMajor);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("pic_path", this.PicPath);
            parameters.Add("position", this.Position);
            return parameters;
        }

        #endregion
    }
}
