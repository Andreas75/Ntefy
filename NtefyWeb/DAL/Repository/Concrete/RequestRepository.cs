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
            string market = string.Empty;
            if (recordId != null)
            {
                if (userId != null)
                {
                    market = userRepo.GetUserMarket(userId);
                }
                var request = new Request
                {                   
                    UserId = userId,
                    RecordId = recordId,
                    RequestDate = DateTime.Now,
                    FillDate = null,
                    Country = market
                };
                var cachedRequests = RequestCache.GetAllFromCache();
                
                var isDublicate = cachedRequests.Any(x => x.UserId == request.UserId && x.RecordId == request.RecordId);
                if (!isDublicate)
                {
                    dbContext.Requests.Add(request);
                    dbContext.SaveChanges();
                    RequestCache.UpDateAllCache();                    
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

        public string GetAllRecipitansForAlbumRequest(Guid recordId, string market)
        {
            var requestList = new List<Request>();
            requestList = RequestCache.GetAllFromCache();
            var allUsers = requestList.Where(x => x.RecordId == recordId && x.Country == market).Select(y => y.UserId).ToList<string>();           
            return userRepo.GetUsersEmail(allUsers);
        }       

        public void SetRequestAsFilled(Guid recordId, string country)
        {
            var requests = dbContext.Requests.Where(x => x.RecordId == recordId && x.Country.ToLower() == country.ToLower());
            foreach (var request in requests)
            {
                request.FillDate = DateTime.Now;
            }
            dbContext.SaveChanges();            
        }

        public List<Request> GetAllUnfilledRequests()
        {
            var allRequests = GetAllRequests();
            var unfilled = allRequests.Where(x => x.FillDate == null).ToList<Request>();
            return unfilled;
        }

        public List<Request> FilterdRequestsForRecordAndMarket()
        {
            var requests = GetAllUnfilledRequests();
            var filterdRequests = requests.GroupBy(x => new { x.RecordId, x.Country }).Select(y => y.First()).ToList();
            return filterdRequests;
        }

        public List<Request> GetAllRequests()
        {
            return dbContext.Requests.ToList<Request>();
        }
    }
}