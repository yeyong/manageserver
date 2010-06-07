using System;
using System.Data;
using System.Data.Common;

using SAS.Common;
using SAS.Data;
using SAS.Config;
using SAS.Entity;
using SAS.Common.Generic;

namespace SAS.Logic
{
    /// <summary>
    /// 企业统计
    /// </summary>
    public class CompaniesStats
    {
        static CompanyViewCollection<TopicView> queuedStatsList = null;

        static int queuedAllowCount = 20;

        private CompaniesStats() { }

        static CompaniesStats()
        {
            if (GeneralConfigs.GetConfig().TopicQueueStats == 1)
                SetQueueCount();
        }

        /// <summary>
        /// 追踪浏览量
        /// </summary>
        /// <param name="tv">主题浏览数对象</param>
        /// <returns>成功返回true</returns>
        public static bool Track(TopicView tv)
        {
            if (tv == null)
                return false;

            if (queuedStatsList == null)
                SetQueueCount();

            if (GeneralConfigs.GetConfig().TopicQueueStats == 1)
                return AddQuedStats(tv);
            else
                return TrackCompany(tv);
        }

        /// <summary>
        /// 追踪浏览量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewcount">浏览量</param>
        /// <returns>成功返回true</returns>
        public static bool Track(int tid, int viewcount)
        {
            if (tid < 1 || viewcount < 1)
                return false;
            TopicView tv = new TopicView();
            tv.TopicID = tid;
            tv.ViewCount = viewcount;
            return Track(tv);
        }

        #region Data

        /// <summary>
        /// 更新企业浏览量
        /// </summary>
        /// <param name="tid">主题id</param>
        /// <param name="viewcount">浏览量</param>
        /// <returns>成功返回1，否则返回0</returns>
        public static int UpdateCompanyViewCount(int tid, int viewcount)
        {
            //return SAS.Data.DataProvider.TopicStats.UpdateTopicViewCount(tid, viewcount);
            return SAS.Data.DataProvider.Companies.UpdataCompanyViewCount(tid, viewcount);
        }

        /// <summary>
        /// 追踪企业
        /// </summary>
        /// <param name="tv">主题浏览对象</param>
        /// <returns>成功返回true</returns>
        public static bool TrackCompany(TopicView tv)
        {
            return UpdateCompanyViewCount(tv.TopicID, tv.ViewCount) == 1;
        }

        #endregion

        #region 条件编译方法

        /// <summary>
        /// 设置队列长度
        /// </summary>
        public static void SetQueueCount()
        {
            if (GeneralConfigs.GetConfig().TopicQueueStatsCount > 20 && GeneralConfigs.GetConfig().TopicQueueStatsCount <= 1000)
                queuedAllowCount = GeneralConfigs.GetConfig().TopicQueueStatsCount;

            if (queuedStatsList == null)
                queuedStatsList = new CompanyViewCollection<TopicView>();
        }

        /// <summary>
        /// 清除队列，并保存队列中已有数据
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        public static bool ClearQueue(bool save)
        {
            lock (queuedStatsList.SyncRoot)
            {
                if (save)
                {
                    TopicView[] eva = new TopicView[queuedStatsList.Count];
                    queuedStatsList.CopyTo(eva, 0);
                    ClearTrackCompanyQueue(new CompanyViewCollection<TopicView>(eva));
                }
                queuedStatsList.Clear();
            }
            return true;
        }

        /// <summary>
        /// 向队列中增加统计对象
        /// </summary>
        /// <param name="tv">主题浏览数对象</param>
        /// <returns>成功返回true</returns>
        public static bool AddQuedStats(TopicView tv)
        {
            if (tv == null)
                return false;
            if (queuedAllowCount != GeneralConfigs.GetConfig().TopicQueueStatsCount || queuedStatsList == null)
            {
                SetQueueCount();
            }

            //Check for the limit
            if (queuedStatsList.ViewCount >= queuedAllowCount || queuedStatsList.Count >= 5)
            {
                //aquire the lock 
                lock (queuedStatsList.SyncRoot)
                {
                    //make sure the pool queue was not cleared during a wait for the lock
                    if (queuedStatsList.ViewCount >= queuedAllowCount || queuedStatsList.Count >= 5)
                    {
                        TopicView[] tva = new TopicView[queuedStatsList.Count];
                        queuedStatsList.CopyTo(tva, 0);
                        ClearTrackCompanyQueue(new CompanyViewCollection<TopicView>(tva));
                        queuedStatsList.Clear();
                        queuedStatsList.ViewCount = 0;

                    }
                }
            }

            bool inArray = false;
            foreach (TopicView curtv in queuedStatsList)
            {
                if (curtv.TopicID == tv.TopicID)
                {
                    curtv.ViewCount = curtv.ViewCount + tv.ViewCount;
                    inArray = true;
                    break;
                }
            }

            if (!inArray)
                queuedStatsList.Add(tv);
            queuedStatsList.ViewCount = queuedStatsList.ViewCount + 1;
            return true;
        }

        /// <summary>
        /// 清除追踪企业的队列
        /// </summary>
        /// <param name="tvc">主题浏览集合</param>
        /// <returns>成功返回true</returns>
        private static bool ClearTrackCompanyQueue(CompanyViewCollection<TopicView> tvc)
        {
            new ProcessStats(tvc).Enqueue();
            return true;
        }

        /// <summary>
        /// 追踪主题
        /// </summary>
        /// <param name="tvc">主题浏览集合</param>
        /// <returns>成功返回true</returns>
        public static bool TrackCompany(CompanyViewCollection<TopicView> tvc)
        {
            if (tvc == null)
                return false;

            foreach (TopicView tv in tvc)
            {
                UpdateCompanyViewCount(tv.TopicID, tv.ViewCount);
            }
            return true;
        }

        private class ProcessStats
        {
            public ProcessStats(CompanyViewCollection<TopicView> tvc)
            {
                _tvc = tvc;
            }

            protected CompanyViewCollection<TopicView> _tvc;

            /// <summary>
            /// 执行统计操作
            /// </summary>
            public void Enqueue()
            {
                ManagedThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Process));
            }

            /// <summary>
            /// 处理当前操作
            /// </summary>
            /// <param name="state"></param>
            private void Process(object state)
            {
                CompaniesStats.TrackCompany(this._tvc);
            }
        }

        public class CompanyViewCollection<T> : List<T> where T : TopicView
        {
            public CompanyViewCollection() : base() { }

            public CompanyViewCollection(System.Collections.Generic.IEnumerable<T> collection) : base(collection) { }

            public CompanyViewCollection(int capacity) : base(capacity) { }

            private int _viewCount = 0;

            public int ViewCount
            {
                get
                {
                    return _viewCount;
                }
                set
                {
                    _viewCount = value;
                }
            }
        }
        #endregion

        public static int GetStoredCompanyViewCount(int tid)
        {
            foreach (TopicView curtv in queuedStatsList)
            {
                if (curtv.TopicID == tid)
                {
                    return curtv.ViewCount;
                }
            }
            return 0;
        }
    }
}
