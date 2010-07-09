using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.item.joint.propimg
    /// </summary>
    public class ItemJointPropimgRequest : INTWRequest
    {
        public Nullable<long> Id { get; set; }
        public string Iid { get; set; }
        public Nullable<long> NumIid { get; set; }
        public string PicPath { get; set; }
        public Nullable<int> Position { get; set; }
        public string Properties { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.item.joint.propimg";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("iid", this.Iid);
            parameters.Add("num_iid", this.NumIid);
            parameters.Add("pic_path", this.PicPath);
            parameters.Add("position", this.Position);
            parameters.Add("properties", this.Properties);
            return parameters;
        }

        #endregion
    }
}
