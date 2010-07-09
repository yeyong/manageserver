using System.Collections.Generic;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    public class DynamicTopRequest : INTWUploadRequest
    {
        /// <summary>
        /// API名称。
        /// </summary>
        private string apiName;

        /// <summary>
        /// 文本参数。
        /// </summary>
        private NTWDictionary textParams = new NTWDictionary();

        /// <summary>
        /// 文件参数。
        /// </summary>
        private IDictionary<string, FileItem> fileParams = new Dictionary<string, FileItem>();

        public DynamicTopRequest(string apiName)
        {
            this.apiName = apiName;
        }

        public void AddTextParameter(string name, object value)
        {
            textParams.Add(name, value);
        }

        public void AddFileParameter(string name, FileItem file)
        {
            if (!string.IsNullOrEmpty(name) && file != null)
            {
                fileParams.Add(name, file);
            }
        }

        #region INTWRequest Members

        public string GetApiName()
        {
            return this.apiName;
        }

        public IDictionary<string, string> GetParameters()
        {
            return this.textParams;
        }

        #endregion

        #region INTWUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            return this.fileParams;
        }

        #endregion
    }
}
