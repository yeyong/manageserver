using System;
using System.Collections.Generic;

using SAS.Taobao.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.app.subapp.apply
    /// </summary>
    public class AppSubappApplyRequest : INTWUploadRequest
    {
        public string CallbackUrl { get; set; }
        public FileItem Logo { get; set; }
        public string NotifyUrl { get; set; }
        public string ParentAppKey { get; set; }
        public string ServiceCode { get; set; }
        public string Title { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.app.subapp.apply";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("callback_url", this.CallbackUrl);
            parameters.Add("notify_url", this.NotifyUrl);
            parameters.Add("parent_app_key", this.ParentAppKey);
            parameters.Add("service_code", this.ServiceCode);
            parameters.Add("title", this.Title);
            return parameters;
        }

        #endregion

        #region INTWUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("logo", this.Logo);
            return parameters;
        }

        #endregion
    }
}
