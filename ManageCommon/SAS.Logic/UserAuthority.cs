using System;
using System.Text;

using SAS.Entity;
using SAS.Config;
using SAS.Common;

namespace SAS.Logic
{
    /// <summary>
    /// 用户权限操作类
    /// </summary>
    public class UserAuthority
    {
        /// <summary>
        /// 确认当前时间是否在指定的时间列表内
        /// </summary>
        /// <param name="timelist">一个包含多个时间段的列表(格式为hh:mm-hh:mm)</param>
        /// <param name="vtime">输出参数：符合条件的第一个是时间段</param>
        /// <returns>时间段存在则返回true,否则返回false</returns>
        public static bool BetweenTime(string timelist, out string vtime)
        {
            if (!Utils.StrIsNullOrEmpty(timelist))
            {
                string[] enabledvisittime = Utils.SplitString(timelist, "\n");

                if (enabledvisittime.Length > 0)
                {
                    string starttime = "", endtime = "";
                    int s = 0, e = 0;

                    foreach (string visittime in enabledvisittime)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(visittime, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])-(([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9]))$"))
                        {
                            starttime = visittime.Substring(0, visittime.IndexOf("-"));
                            s = Utils.StrDateDiffMinutes(starttime, 0);

                            endtime = Utils.CutString(visittime, visittime.IndexOf("-") + 1, visittime.Length - (visittime.IndexOf("-") + 1));
                            e = Utils.StrDateDiffMinutes(endtime, 0);

                            if (DateTime.Parse(starttime) < DateTime.Parse(endtime)) //起始时间小于结束时间,认为未跨越0点
                            {
                                if (s > 0 && e < 0)
                                {
                                    vtime = visittime;
                                    return true;
                                }
                            }
                            else //起始时间大于结束时间,认为跨越0点
                            {
                                if ((s < 0 && e < 0) || (s > 0 && e > 0 && e > s))
                                {
                                    vtime = visittime;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            vtime = "";
            return false;
        }

        /// <summary>
        /// 确认当前时间是否在指定的时间列表内
        /// </summary>
        /// <param name="timelist">一个包含多个时间段的列表(格式为hh:mm-hh:mm)</param>
        /// <returns>时间段存在则返回true,否则返回false</returns>
        public static bool BetweenTime(string timelist)
        {
            string visittime = "";
            return BetweenTime(timelist, out visittime);
        }

        /// <summary>
        /// 发帖（主题）是否需要审核
        /// </summary>
        /// <param name="forum"></param>
        /// <param name="useradminid"></param>
        /// <param name="topicInfo"></param>
        /// <param name="userid"></param>
        /// <param name="disablepost"></param>
        /// <returns></returns>
        public static bool NeedAudit(ForumInfo forum, int useradminid, TopicInfo topicInfo, int userid, int disablepost)
        {
            bool needaudit = false; //是否需要审核

            if (BetweenTime(GeneralConfigs.GetConfig().Postmodperiods))
            {
                needaudit = true;
            }
            else
            {
                if (forum.Modnewposts == 1 && useradminid != 1)
                {
                    if (useradminid > 1)
                    {
                        if (disablepost == 1 && topicInfo.Displayorder != -2)
                        {
                            if (useradminid == 3)
                                needaudit = true;
                        }
                        else
                            needaudit = true;
                    }
                    else
                        needaudit = true;
                }
                else if (useradminid != 1 && topicInfo.Displayorder == -2)
                    needaudit = true;
            }
            return needaudit;
        }

        public static bool NeedAudit(ForumInfo forum, int useradminid, int userid)
        {
            bool needaudit = false; //是否需要审核

            if (useradminid == 1) return false;

            if (BetweenTime(GeneralConfigs.GetConfig().Postmodperiods))
            {
                needaudit = true;
            }
            else
            {
                if (forum.Modnewposts == 1)
                {
                    needaudit = true;
                }

            }
            return needaudit;
        }
    }
}
