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
        //private RequestContext dbContext;
        //private UserRepository _userRepository;
        //private RequestCache _requestCache;

        public RequestRepository()
        {
            //dbContext = new RequestContext();
            //_userRepository = new UserRepository();
            //_requestCache = new RequestCache();
        }

        public void AddRequest(Guid recordId, string userId)
        {
            string market = string.Empty;
            if (recordId != null)
            {
                if (userId != null)
                {
                    market = new UserRepository().GetUserMarket(userId);
                }
                var request = new Request
                {                   
                    UserId = userId,
                    RecordId = recordId,
                    RequestDate = DateTime.Now,
                    FillDate = null,
                    Country = market
                };
                var cachedRequests = new RequestCache().GetAllFromCache();
                
                var isDublicate = cachedRequests.Any(x => x.UserId == request.UserId && x.RecordId == request.RecordId);
                if (!isDublicate)
                {
                    var dbContext = new RequestContext();
                        dbContext.Requests.Add(request);
                        dbContext.SaveChanges();
                        new RequestCache().UpDateAllCache(); 
                    
                                      
                }               
            }            
        }


        public List<Request> GetAllRequestsForUser(string userId)
        {
            var requestList = new List<Request>();
            requestList = new RequestCache().GetAllFromCache();
            if(requestList.Count > 0)
            {
                return requestList.Where(x => x.UserId == userId) as List<Request>;
            }
            return requestList;
        }

        public List<Request> GetAllRequestForAlbum(Guid recordId)
        {
            var requestList = new List<Request>();
            requestList = new RequestCache().GetAllFromCache();
            if (requestList.Count > 0)
            {
                return requestList.Where(x => x.RecordId == recordId) as List<Request>;
            }
            return requestList;            
        }

        public string GetAllRecipitansForAlbumRequest(Guid recordId, string market)
        {
            var requestList = new List<Request>();
            requestList = new RequestCache().GetAllFromCache();
            var allUsers = requestList.Where(x => x.RecordId == recordId && x.Country == market).Select(y => y.UserId).ToList<string>();
            return new UserRepository().GetUsersEmail(allUsers);
        }       

        public void SetRequestAsFilled(Guid recordId, string country)
        {
            var dbContext = new RequestContext();
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
            //using(var dbContext = RequestContext.Create())
            //{
            //    return dbContext.Requests.ToList<Request>();
            //}
            var dbContext = new RequestContext();
            return dbContext.Requests.ToList<Request>();
        }
    }
}