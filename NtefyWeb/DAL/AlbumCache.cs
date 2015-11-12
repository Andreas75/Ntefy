﻿using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL
{
    public static class AlbumCache
    {
        private static RequestContext dbContext;

        static AlbumCache()
        {
            dbContext = new RequestContext();
        }

        public static void UpdateCache()
        {                       
            HttpRuntime.Cache.Insert("cachedAlbums", dbContext.Records.ToList<Record>(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
        }

        public static List<Record> GetAllRecordsFromCache()
        {
            return HttpRuntime.Cache["cachedAlbums"] as List<Record>; 
        }

        public static Record GetRecordFromCache(string artist, string title)
        {
            var cachedRecords = HttpRuntime.Cache["cachedAlbums"] as List<Record>;
            return cachedRecords.First(x => artist == x.Artist && title == x.Title);
        }
    }
}