using Hangfire;
using NtefyEmail;
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
        private RecordRepository recordRepo;

        public SearchRequestBackgroundTask()
        {
            spotifyIntegration = new SpotifyIntegration();
            Requests = RequestCache.GetAllFromCache();
            requestRepo = new RequestRepository();
            recordRepo = new RecordRepository();
        }

        public List<Request> GetAllRequestedRecords()
        {
            return Requests.GroupBy(x => x.RecordId).Select(y => y.FirstOrDefault()).ToList();
        }

        public async Task SearchForRequests(string userMarket)
        {
            var recordIds = new List<string>();
            var token = await new AccessToken().GetAccessToken();
            var availableRecords = new List<string>();            
            var requests = GetAllRequestedRecords();            
            var artists = new List<string>();
            foreach (var request in requests)
            {
                var result = await spotifyIntegration.SerachForAlbum(request.Record, token, userMarket);
                if (result != null)
                {
                    recordIds.Add(result.Id);                                                         
                }
            }
            var albumResults = await spotifyIntegration.GetMultibleAlbums(string.Join(",", recordIds), token);

            foreach (var album in albumResults.Albums)
            {
                try
                {
                    Guid recordId = recordRepo.GetIdForRecord(new Record { Artist = album.Artists.First().Name, Title = album.Name });
                    var recipitans = requestRepo.GetAllRecipitansForAlbumRequest(recordId);
                    Create.CreateEmail(album, recipitans);
                }
                catch(Exception ex)
                {
                    var e = ex;
                    throw;
                }
                
            }
            
        }

        public void SearchRequestsSync(string userMarket)
        {           
            SearchForRequests(userMarket).Wait();
        }

        public void SetUpBrackgroundTask(string userMarket)
        {
            RecurringJob.AddOrUpdate(() => SearchRequestsSync(userMarket), Cron.Minutely);
        }
    }
}