using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Master.Entity;
using Microsoft.Extensions.Caching.Memory;

namespace Master.Controller.Manager
{
    public interface IAppManager
    {
        #region - cache - 

        List<CachedNode> RetrieveCachedObjects();
        string RetrieveCache(IMemoryCache memoryCache, string tagCache);
        void SaveCache(IMemoryCache memoryCache, string cacheValue, string tagCache, object? input, string route, int minutes = 60);
        void RemoveCache(IMemoryCache memoryCache, string tagCache);
        void RemoveAllCache(IMemoryCache memoryCache, string startTag, List<string> tagLstCacheContains);
        void AddNewRequest(bool cached);
        long GetRequests(bool cached);
        void AddNewRequestTime(long milis);
        long GetRequestsMaxTime();
        long GetRequestsMinTime();
        long GetRequestsAvgTime();
        List<string> GetLastRequestsPerMInute();
        string GetStartDate();

        #endregion
    }

    public class AppManager : IAppManager
    {
        #region - cache - 

        public List<string> myTags = new List<string>();
        public List<CachedNode> myInputObjects = new List<CachedNode>();

        private long totalTime = 0;
        private long minTime = 999999999;
        private long maxTime = 0;
        private long requests = 0;
        private long requestsCached = 0;
        private int collision_status = 0;
        private Hashtable hshReqHourMin = new Hashtable();
        private List<string> lstReqHourMin = new List<string>();
        private DateTime dtStart = DateTime.Now;

        public string RetrieveCache(IMemoryCache memoryCache, string tagCache)
        {
            if (memoryCache.TryGetValue(tagCache, out string data))
            {
                return data;
            }

            return null;
        }

        public List<CachedNode> RetrieveCachedObjects()
        {
            return myInputObjects;
        }

        public void SaveCache(IMemoryCache memoryCache, string cacheValue, string tagCache, object? input, string route, int minutes = 240)
        {
            var lockWasTaken = false;
            try
            {
                lockWasTaken = Interlocked.CompareExchange(ref collision_status, 1, 0) == 0;
                if (lockWasTaken)
                {
                    memoryCache.Set(tagCache, cacheValue, DateTimeOffset.Now.AddMinutes(minutes));

                    myInputObjects.Add(new CachedNode
                    {
                        input = input,
                        tag = tagCache,
                        route = route,
                        expires = DateTime.Now.AddMinutes(minutes)
                    });

                    if (!myTags.Contains(tagCache))
                    {
                        myTags.Add(tagCache);
                    }
                }
            }
            finally
            {
                if (lockWasTaken)
                {
                    Volatile.Write(ref collision_status, 0);
                }
            }
        }

        public void RemoveCache(IMemoryCache memoryCache, string tagCache)
        {
            var lockWasTaken = false;
            try
            {
                lockWasTaken = Interlocked.CompareExchange(ref collision_status, 1, 0) == 0;
                if (lockWasTaken)
                {
                    memoryCache.Remove(tagCache);
                    myTags.Remove(tagCache);
                }
            }
            finally
            {
                if (lockWasTaken)
                {
                    Volatile.Write(ref collision_status, 0);
                }
            }
        }

        public void RemoveAllCache(IMemoryCache memoryCache, string startTag, List<string> tagLstCacheContains)
        {
            var lockWasTaken = false;
            try
            {
                lockWasTaken = Interlocked.CompareExchange(ref collision_status, 1, 0) == 0;
                if (lockWasTaken)
                {
                    foreach (var currentCacheTag in tagLstCacheContains)
                    {
                        foreach (var tagCache in myTags.Where(y => y.Contains(startTag + currentCacheTag)).ToList())
                        {
                            memoryCache.Remove(tagCache);
                            myTags.Remove(tagCache);
                        }
                    }
                }
            }
            finally
            {
                if (lockWasTaken)
                {
                    Volatile.Write(ref collision_status, 0);
                }
            }
        }

        public void AddNewRequest(bool cached)
        {
            var lockWasTaken = false;
            try
            {
                lockWasTaken = Interlocked.CompareExchange(ref collision_status, 1, 0) == 0;
                if (lockWasTaken)
                {
                    if (!cached)
                    {
                        requests++;
                    }
                    else
                    {
                        requestsCached++;
                    }

                    var tag_req_hour_min = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00");
                    if (hshReqHourMin[tag_req_hour_min] == null)
                    {
                        hshReqHourMin[tag_req_hour_min] = 0;
                        lstReqHourMin.Add(tag_req_hour_min);
                    }

                    int counter = (int)hshReqHourMin[tag_req_hour_min];
                    counter++;
                    hshReqHourMin[tag_req_hour_min] = counter;
                }
            }
            finally
            {
                if (lockWasTaken)
                {
                    Volatile.Write(ref collision_status, 0);
                }
            }
        }

        public long GetRequests(bool cached)
        {
            return !cached ? requests : requestsCached;
        }

        public void AddNewRequestTime(long milis)
        {
            var lockWasTaken = false;
            try
            {
                lockWasTaken = Interlocked.CompareExchange(ref collision_status, 1, 0) == 0;
                if (lockWasTaken)
                {
                    totalTime += milis;
                    if (milis < minTime)
                    {
                        minTime = milis;
                    }

                    if (milis > maxTime)
                    {
                        maxTime = milis;
                    }
                }
            }
            finally
            {
                if (lockWasTaken)
                {
                    Volatile.Write(ref collision_status, 0);
                }
            }
        }

        public long GetRequestsMaxTime()
        {
            return maxTime;
        }

        public long GetRequestsMinTime()
        {
            return minTime;
        }

        public long GetRequestsAvgTime()
        {
            return totalTime / requests;
        }

        public List<string> GetLastRequestsPerMInute()
        {
            if (lstReqHourMin.Count > 2)
            {
                lstReqHourMin.RemoveRange(0, lstReqHourMin.Count - 2);
            }

            var ret = new List<string>();
            foreach (var item in lstReqHourMin.OrderByDescending(y => y))
            {
                int counter = (int)hshReqHourMin[item];
                ret.Add(item + " -> " + counter + " requests/minute");
            }

            return ret;
        }

        public string GetStartDate()
        {
            return dtStart.ToString("dd/MM/yyyy HH:mm");
        }

        #endregion

    }
}
