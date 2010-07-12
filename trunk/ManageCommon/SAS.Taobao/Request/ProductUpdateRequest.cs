using System;
using System.Collections.Generic;

using SAS.Entity.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.product.update
    /// </summary>
    public class ProductUpdateRequest : INTWUploadRequest
    {
        public string Binds { get; set; }
        public string Desc { get; set; }
        public FileItem Image { get; set; }
        public Nullable<bool> Major { get; set; }
        public string Name { get; set; }
        public string NativeUnkeyprops { get; set; }
        public string OuterId { get; set; }
        public string Price { get; set; }
        public Nullable<long> ProductId { get; set; }
        public string SaleProps { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.product.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("binds", this.Binds);
            parameters.Add("desc", this.Desc);
            parameters.Add("major", this.Major);
            parameters.Add("name", this.Name);
            parameters.Add("native_unkeyprops", this.NativeUnkeyprops);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("price", this.Price);
            parameters.Add("product_id", this.ProductId);
            parameters.Add("sale_props", this.SaleProps);
            return parameters;
        }

        #endregion

        #region INTWUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("image", this.Image);
            return parameters;
        }

        #endregion
    }
}
