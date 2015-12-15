using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL
{
    public static class RequestCache
    {
        private static RequestRepository dbContext;

        static RequestCache()
        {
            dbContext = new RequestRepository();
        }

        public static void UpDateAllCache()
        {
            HttpRuntime.Cache.Remove("cachedRequests");
            HttpRuntime.Cache.Insert("cachedRequests", dbContext.GetAllRequests(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
        }

        public static List<Request> GetAllFromCache()
        {
            return HttpRuntime.Cache["cachedRequests"] as List<Request>;
        }



        public static void UpDateAllUnfilledRequests()
        {
            HttpRuntime.Cache.Remove("cachedUnfilldRequests");
            HttpRuntime.Cache.Insert("cachedUnfilldRequests", dbContext.GetAllUnfilledRequests(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
        }

        public static List<Request> GetAllUnfilledRequests()
        {            
            return HttpRuntime.Cache["cachedUnfilldRequests"] as List<Request>;
        }
        
    }
}