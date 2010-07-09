using System;
using System.Collections.Generic;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// TOP API: taobao.picture.get
    /// </summary>
    public class PictureGetRequest : INTWRequest
    {
        public string Deleted { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public string OrderBy { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<long> PictureCategoryId { get; set; }
        public Nullable<long> PictureId { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string Title { get; set; }

        #region INTWRequest Members

        public string GetApiName()
        {
            return "taobao.picture.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            NTWDictionary parameters = new NTWDictionary();
            parameters.Add("deleted", this.Deleted);
            parameters.Add("end_date", this.EndDate);
            parameters.Add("order_by", this.OrderBy);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("picture_category_id", this.PictureCategoryId);
            parameters.Add("picture_id", this.PictureId);
            parameters.Add("start_date", this.StartDate);
            parameters.Add("title", this.Title);
            return parameters;
        }

        #endregion
    }
}
