using NtefyWeb.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL.Repository.Concrete
{
    public class RequestRepository : IRequestRepository
    {
        public RequestContext dbContext;
        public RequestRepository()
        {
            dbContext = new RequestContext();
        }

        public void AddRequest(Models.Request request)
        {
            if(request != null)
            {
                dbContext.Requests.Add(request);
            }
        }
    }
}