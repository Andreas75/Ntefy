using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;
using NtefyWeb.DAL.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL
{
    public class RequestCache : IRequestCache
    {
        private RequestRepository _dbContext;

        public RequestCache()
        {
            _dbContext = new RequestRepository();
        }

        public void UpDateAllCache()
        {
            HttpRuntime.Cache.Remove("cachedRequests");
            HttpRuntime.Cache.Insert("cachedRequests", _dbContext.GetAllRequests(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
        }

        public List<Request> GetAllFromCache()
        {
            return HttpRuntime.Cache["cachedRequests"] as List<Request>;
        }



        public void UpDateAllUnfilledRequests()
        {
            HttpRuntime.Cache.Remove("cachedUnfilldRequests");
            HttpRuntime.Cache.Insert("cachedUnfilldRequests", _dbContext.GetAllUnfilledRequests(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);
        }

        public List<Request> GetAllUnfilledRequests()
        {            
            return HttpRuntime.Cache["cachedUnfilldRequests"] as List<Request>;
        }
        
    }
}