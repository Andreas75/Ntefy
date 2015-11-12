using Hangfire;
using NtefySpotify;
using NtefySpotify.Models;
using NtefyWeb.Controllers;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NtefyWeb.Business
{
    public class SearchRequestBackgroundTask
    {
        private List<Request> Requests;
        private SpotifyIntegration spotifyIntegration;
        private RequestRepository requestRepo;

        public SearchRequestBackgroundTask()
        {
            spotifyIntegration = new SpotifyIntegration();
            Requests = RequestCache.GetAllFromCache();
            requestRepo = new RequestRepository();
        }

        public List<Request> GetAllRequestedRecords()
        {
            return Requests.GroupBy(x => x.RecordId).Select(y => y.FirstOrDefault()).ToList();
        }

        public async Task SearchForRequests(string userMarket, string token)
        {
            //string recipitans = string.Empty;
            var token2 = await new AccessToken().GetAccessToken();
            var availableRecords = new List<string>();            
            var requests = GetAllRequestedRecords();            
            var artists = new List<string>();
            foreach (var request in requests)
            {
                var result = await spotifyIntegration.SerachForAlbum(request.Record, token2, userMarket);
                if (result != null)
                {
                    var fullAlbum = await spotifyIntegration.GetAlbum(result.Id, token2);
                    if (fullAlbum != null)
                    {
                        Guid recordId = request.RecordId;
                        var recipitans = requestRepo.GetAllRecipitansForAlbumRequest(recordId);                        
                        new EmailController().CreateEmail(fullAlbum, recipitans);
                    }                                        
                }
            }            
        }

        public void SearchRequestsSync(string userMarket, string token)
        {           
            SearchForRequests(userMarket, token).Wait();
        }

        public void SetUpBrackgroundTask(string userMarket, string token)
        {
            RecurringJob.AddOrUpdate(() => SearchRequestsSync(userMarket, token), Cron.Minutely);
        }
    }
}