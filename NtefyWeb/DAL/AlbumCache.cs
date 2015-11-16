using NtefyWeb.DAL.Models;
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
            HttpRuntime.Cache.Remove("cachedAlbums");         
            HttpRuntime.Cache.Insert("cachedAlbums", dbContext.Records.ToList<Record>(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
            var cachedItems = HttpRuntime.Cache["cachedAlbums"] as List<Record>;
        }

        public static List<Record> GetAllRecordsFromCache()
        {
            return HttpRuntime.Cache["cachedAlbums"] as List<Record>; 
        }

        public static Record GetRecordFromCache(string artist, string title)
        {
            var cachedRecords = HttpRuntime.Cache["cachedAlbums"] as List<Record>;
            var result = cachedRecords.FirstOrDefault(x => artist.ToLower() == x.Artist.ToLower() && title.ToLower() == x.Title.ToLower());           
            return result;
        }
    }
}