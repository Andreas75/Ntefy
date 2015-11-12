using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL
{
    public static class RequestCache
    {
        private static RequestContext dbContext;

        static RequestCache()
        {
            dbContext = new RequestContext();
        }

        public static void UpDateCache()
        {
            HttpRuntime.Cache.Insert("cachedRequests", dbContext.Requests.ToList<Request>(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
        }

        public static List<Request> GetAllFromCache()
        {
            return HttpRuntime.Cache["cachedRequests"] as List<Request>;
        }
        
    }
}