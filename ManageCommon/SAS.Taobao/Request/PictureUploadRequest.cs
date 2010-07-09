using System;
using System.Collections.Generic;

using SAS.Taobao.Domain;
using SAS.Taobao.Util;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.picture.upload
    /// </summary>
    public class PictureUploadRequest : INTWUploadRequest
    {
        public string ImageInputTitle { get; set; }
        public FileItem Img { get; set; }
        public Nullable<long> PictureCategoryId { get; set; }
        public string Title { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.picture.upload";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("image_input_title", this.ImageInputTitle);
            parameters.Add("picture_category_id", this.PictureCategoryId);
            parameters.Add("title", this.Title);
            return parameters;
        }

        #endregion

        #region INTWUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("img", this.Img);
            return parameters;
        }

        #endregion
    }
}
