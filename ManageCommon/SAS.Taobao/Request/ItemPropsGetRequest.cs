using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.itemprops.get
    /// </summary>
    public class ItempropsGetRequest : INTWRequest
    {
        public string ChildPath { get; set; }
        public Nullable<long> Cid { get; set; }
        public Nullable<DateTime> Datetime { get; set; }
        public string Fields { get; set; }
        public Nullable<bool> IsColorProp { get; set; }
        public Nullable<bool> IsEnumProp { get; set; }
        public Nullable<bool> IsInputProp { get; set; }
        public Nullable<bool> IsItemProp { get; set; }
        public Nullable<bool> IsKeyProp { get; set; }
        public Nullable<bool> IsSaleProp { get; set; }
        public Nullable<long> ParentPid { get; set; }
        public Nullable<long> Pid { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.itemprops.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("child_path", this.ChildPath);
            parameters.Add("cid", this.Cid);
            parameters.Add("datetime", this.Datetime);
            parameters.Add("fields", this.Fields);
            parameters.Add("is_color_prop", this.IsColorProp);
            parameters.Add("is_enum_prop", this.IsEnumProp);
            parameters.Add("is_input_prop", this.IsInputProp);
            parameters.Add("is_item_prop", this.IsItemProp);
            parameters.Add("is_key_prop", this.IsKeyProp);
            parameters.Add("is_sale_prop", this.IsSaleProp);
            parameters.Add("parent_pid", this.ParentPid);
            parameters.Add("pid", this.Pid);
            return parameters;
        }

        #endregion
    }
}
