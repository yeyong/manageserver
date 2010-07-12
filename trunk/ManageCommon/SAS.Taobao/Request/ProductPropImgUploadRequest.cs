using System;
using System.Collections.Generic;

using SAS.Entity.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.product.propimg.upload
    /// </summary>
    public class ProductPropimgUploadRequest : INTWUploadRequest
    {
        public Nullable<long> Id { get; set; }
        public FileItem Image { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<long> ProductId { get; set; }
        public string Props { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.product.propimg.upload";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("position", this.Position);
            parameters.Add("product_id", this.ProductId);
            parameters.Add("props", this.Props);
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
