using System;
using System.Collections.Generic;

using SAS.Entity.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.refund.message.add
    /// </summary>
    public class RefundMessageAddRequest : INTWUploadRequest
    {
        public string Content { get; set; }
        public FileItem Image { get; set; }
        public Nullable<long> RefundId { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.refund.message.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("content", this.Content);
            parameters.Add("refund_id", this.RefundId);
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
