﻿using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Common.Generic;
using SAS.Config;
using SAS.Data;
using SAS.Entity;
using SAS.Cache;

namespace SAS.Logic
{
    /// <summary>
    /// 短消息操作类
    /// </summary>
    public class PrivateMessages
    {
        /// <summary>
        /// 负责发送新用户注册欢迎信件的用户名称, 该名称同时不允许用户注册
        /// </summary>
        public const string SystemUserName = "系统";

        /// <summary>
        /// 获得指定ID的短消息的内容
        /// </summary>
        /// <param name="pmid">短消息pmid</param>
        /// <returns>短消息内容</returns>
        public static PrivateMessageInfo GetPrivateMessageInfo(int pmid)
        {
            return pmid > 0 ? SAS.Data.DataProvider.PrivateMessages.GetPrivateMessageInfo(pmid) : null;
        }


        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(Guid userId, int folder)
        {
            return userId != new Guid("00000000-0000-0000-0000-000000000000") ? GetPrivateMessageCount(userId, folder, -1) : 0;
        }

        /// <summary>
        /// 得到当用户的短消息数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">所属文件夹(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="state">短消息状态(0:已读短消息、1:未读短消息、2:最近消息（7天内）、-1:全部短消息)</param>
        /// <returns>短消息数量</returns>
        public static int GetPrivateMessageCount(Guid userId, int folder, int state)
        {
            return userId != new Guid("00000000-0000-0000-0000-000000000000") ? SAS.Data.DataProvider.PrivateMessages.GetPrivateMessageCount(userId, folder, state) : 0;
        }


        /// <summary>
        /// 得到公共消息数量
        /// </summary>
        /// <returns>公共消息数量</returns>
        public static int GetAnnouncePrivateMessageCount()
        {
            SASCache cache = SASCache.GetCacheService();
            int announcepmcount = Utils.StrToInt(cache.RetrieveObject("/SAS/AnnouncePrivateMessageCount"), 0);

            if (announcepmcount <= 0)
            {
                announcepmcount = SAS.Data.DataProvider.PrivateMessages.GetAnnouncePrivateMessageCount();
                cache.AddObject("/SAS/AnnouncePrivateMessageCount", announcepmcount);
            }
            return announcepmcount;
        }

        /// <summary>
        /// 创建短消息
        /// </summary>
        /// <param name="privatemessageinfo">短消息内容</param>
        /// <param name="savetosentbox">设置短消息是否在发件箱保留(0为不保留, 1为保留)</param>
        /// <returns>短消息在数据库中的pmid</returns>
        public static int CreatePrivateMessage(PrivateMessageInfo privatemessageinfo, int savetosentbox)
        {
            return SAS.Data.DataProvider.PrivateMessages.CreatePrivateMessage(privatemessageinfo, savetosentbox);
        }


        /// <summary>
        /// 删除指定用户的短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmitemid">要删除的短信息列表(数组)</param>
        /// <returns>删除记录数</returns>
        public static int DeletePrivateMessage(Guid userId, string[] pmitemid)
        {
            if (!Utils.IsNumericArray(pmitemid))
                return -1;

            int reval = SAS.Data.DataProvider.PrivateMessages.DeletePrivateMessages(userId, String.Join(",", pmitemid));
            if (reval > 0)
                SAS.Data.DataProvider.Users.SetUserNewPMCount(userId, SAS.Data.DataProvider.PrivateMessages.GetNewPMCount(userId));

            return reval;
        }

        /// <summary>
        /// 删除指定用户的一条短信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pmid">要删除的短信息ID</param>
        /// <returns>删除记录数</returns>
        public static int DeletePrivateMessage(Guid userId, int pmid)
        {
            return userId != new Guid("00000000-0000-0000-0000-000000000000") ? DeletePrivateMessage(userId, new string[] { pmid.ToString() }) : 0;
        }

        /// <summary>
        /// 设置短信息状态
        /// </summary>
        /// <param name="pmid">短信息ID</param>
        /// <param name="state">状态值</param>
        /// <returns>更新记录数</returns>
        public static int SetPrivateMessageState(int pmid, byte state)
        {
            return pmid > 0 ? SAS.Data.DataProvider.PrivateMessages.SetPrivateMessageState(pmid, state) : 0;
        }

        /// <summary>
        /// 获得指定用户的短信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="folder">短信息类型(0:收件箱,1:发件箱,2:草稿箱)</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="inttype">筛选条件1为未读</param>
        /// <returns>短信息列表</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageCollection(Guid userId, int folder, int pagesize, int pageindex, int readStatus)
        {
            return (userId != new Guid("00000000-0000-0000-0000-000000000000") && pageindex > 0 && pagesize > 0) ? SAS.Data.DataProvider.PrivateMessages.GetPrivateMessageCollection(userId, folder, pagesize, pageindex, readStatus) : null;
        }

        /// <summary>
        /// 获得公共消息列表
        /// </summary>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <returns>公共消息列表</returns>
        public static List<PrivateMessageInfo> GetAnnouncePrivateMessageCollection(int pagesize, int pageindex)
        {
            if (pagesize == -1)
                return SAS.Data.DataProvider.PrivateMessages.GetAnnouncePrivateMessageCollection(-1, 0);
            return (pagesize > 0 && pageindex > 0) ? SAS.Data.DataProvider.PrivateMessages.GetAnnouncePrivateMessageCollection(pagesize, pageindex) : null;
        }

        /// <summary>
        /// 返回短标题的收件箱短消息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pagesize">每页显示短信息数</param>
        /// <param name="pageindex">当前要显示的页数</param>
        /// <param name="strwhere">筛选条件</param>
        /// <returns>收件箱短消息列表</returns>
        public static List<PrivateMessageInfo> GetPrivateMessageListForIndex(Guid userId, int pagesize, int pageindex, int inttype)
        {
            List<PrivateMessageInfo> list = GetPrivateMessageCollection(userId, 0, pagesize, pageindex, inttype);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Message = Utils.GetSubString(list[i].Message, 20, "...");
                }
            }
            return list;
        }

        /// <summary>
        /// 返回最新的一条记录ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static int GetLatestPMID(Guid userId)
        {
            List<PrivateMessageInfo> list = SAS.Data.DataProvider.PrivateMessages.GetPrivateMessageCollection(userId, 0, 1, 1, 0);
            int latestpmid = 0;

            foreach (PrivateMessageInfo info in list)
            {
                latestpmid = info.Pmid;
                break;
            }
            return latestpmid;
        }

        /// <summary>
        /// 获取删除短消息的条件
        /// </summary>
        /// <param name="isNew">是否删除新短消息</param>
        /// <param name="postDateTime">发送日期</param>
        /// <param name="msgFromList">发送者列表</param>
        /// <param name="lowerUpper">是否区分大小写</param>
        /// <param name="subject">主题</param>
        /// <param name="message">内容</param>
        /// <param name="isUpdateUserNewPm">是否更新用户短消息数</param>
        /// <returns></returns>
        public static string GetDeletePrivateMessagesCondition(bool isNew, string postDateTime, string msgFromList, bool lowerUpper, string subject, string message, bool isUpdateUserNewPm)
        {
            return Data.DataProvider.PrivateMessages.GetDeletePrivateMessagesCondition(isNew, postDateTime, msgFromList, lowerUpper, subject, message, isUpdateUserNewPm);
        }
    }
}
