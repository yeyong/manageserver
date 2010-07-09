using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.picture.category.add
    /// </summary>
    public class PictureCategoryAddRequest : INTWRequest
    {
        public string PictureCategoryName { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.picture.category.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("picture_category_name", this.PictureCategoryName);
            return parameters;
        }

        #endregion
    }
}
