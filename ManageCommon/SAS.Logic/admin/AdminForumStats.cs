using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Logic;
using SAS.Config;
using SAS.Entity;

namespace SAS.Logic
{
    /// <summary>
    /// AdminForumStatFactory 的摘要说明。
    /// 后台论坛统计功能类
    /// </summary>
    public class AdminForumStats
    {
        /// <summary>
        /// 重建指定论坛帖数
        /// </summary>
        public static void ReSetFourmTopicAPost(int fid, out int topiccount, out int postcount, out int lasttid, out string lasttitle, out string lastpost, out int lastposterid, out string lastposter, out int todaypostcount)
        {
            topiccount = 0;
            postcount = 0;
            lasttid = 0;
            lasttitle = "";
            lastpost = "";
            lastposterid = 0;
            lastposter = "";
            todaypostcount = 0;
            if (fid < 1)
                return;

            ////topiccount = Data.DataProvider.Topics.GetTopicCountOfForumWithSub(fid);
            ////postcount = GetPostsCountByFid(fid, out todaypostcount);

            ////IDataReader postreader = Data.Posts.GetLastPostByFid(fid, Posts.GetPostTableName());
            ////if (postreader.Read())
            ////{
            ////    lasttid = Utils.StrToInt(postreader["tid"], 0);
            ////    lasttitle = Topics.GetTopicInfo(lasttid).Title;//postreader["title"].ToString();
            ////    lastpost = postreader["postdatetime"].ToString();
            ////    lastposterid = Utils.StrToInt(postreader["posterid"], 0);
            ////    lastposter = postreader["poster"].ToString();
            ////}

            ////postreader.Close();
        }
    }
}
