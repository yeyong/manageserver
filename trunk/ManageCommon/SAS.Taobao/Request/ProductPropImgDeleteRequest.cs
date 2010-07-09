using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.product.propimg.delete
    /// </summary>
    public class ProductPropimgDeleteRequest : INTWRequest
    {
        public Nullable<long> Id { get; set; }
        public Nullable<long> ProductId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.product.propimg.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("product_id", this.ProductId);
            return parameters;
        }

        #endregion
    }
}
