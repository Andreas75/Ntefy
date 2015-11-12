using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
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
        public UserRepository userRepo;

        public RequestRepository()
        {
            dbContext = new RequestContext();
            userRepo = new UserRepository();
        }

        public void AddRequest(Guid recordId, string userId)
        {
            if (recordId != null)
            {
                var request = new Request
                {                   
                    UserId = userId,
                    RecordId = recordId,
                    RequestDate = DateTime.Now
                };
                var cachedRequests = RequestCache.GetAllFromCache();
                
                var isDublicate = cachedRequests.Any(x => x.UserId == request.UserId && x.RecordId == request.RecordId);
                if (!isDublicate)
                {
                    dbContext.Requests.Add(request);
                    dbContext.SaveChanges();
                    RequestCache.UpDateCache();                   
                }              
                
            }
            
        }


        public List<Request> GetAllRequestsForUser(string userId)
        {
            var requestList = new List<Request>();
            requestList = RequestCache.GetAllFromCache();
            if(requestList.Count > 0)
            {
                return requestList.Where(x => x.UserId == userId) as List<Request>;
            }
            return requestList;
        }

        public List<Request> GetAllRequestForAlbum(Guid recordId)
        {
            var requestList = new List<Request>();
            requestList = RequestCache.GetAllFromCache();
            if (requestList.Count > 0)
            {
                return requestList.Where(x => x.RecordId == recordId) as List<Request>;
            }
            return requestList;            
        }

        public string GetAllRecipitansForAlbumRequest(Guid recordId)
        {
            var requestList = new List<Request>();
            requestList = RequestCache.GetAllFromCache();
            var users = requestList.Where(x => x.RecordId == recordId).Select(y => y.UserId).ToList<string>();
            return userRepo.GetUsersEmail(users);
        }
    }
}