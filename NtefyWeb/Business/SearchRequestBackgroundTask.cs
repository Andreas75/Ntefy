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
        private List<string> recordIds = new List<string>();

        public SearchRequestBackgroundTask()
        {
            spotifyIntegration = new SpotifyIntegration();
            Requests = RequestCache.GetAllFromCache();
            requestRepo = new RequestRepository();
            recordRepo = new RecordRepository();
        }

        public List<Request> GetAllRequestedRecords()
        {
            //return requestRepo.GetAllUnfilledRequests();
            return requestRepo.FilterdRequestsForRecordAndMarket();
        }

        public async Task SearchForRequests()
        {
            recordIds.Clear();
            var token = await new AccessToken().GetAccessToken();
            var availableRecords = new List<string>();            
            var requests = GetAllRequestedRecords();
            if (requests != null && requests.Count > 0)
            {
                var artists = new List<string>();
                foreach (var request in requests)
                {
                    if (request.FillDate == null)
                    {
                        var result = await spotifyIntegration.SerachForAlbum(request.Record, token, request.Country);
                        if (result != null)
                        {
                            foreach (var item in result.Albums.Items)
                            {
                                recordRepo.UpdateRecordTitle(request.Record, item.Name);
                                recordIds.Add(result.Albums.Items.FirstOrDefault().Id);
                            }                            
                        }
                    }
                }
                FillRequest(recordIds, requests, token);  
            }
                     
        }

        public async void FillRequest(List<string> recordIds, List<Request> requests, string token)
        {
            List<FullAlbum> albums = new List<FullAlbum>();
            if (recordIds.Count > 0)
            {
                var albumResults = await spotifyIntegration.GetMultibleAlbums(string.Join(",", recordIds), token);
                foreach (var request in requests)
                {
                    foreach (var a in albumResults.Albums)
                    {
                        var artistCompare = await StringCompare.CompareResultToRequest(request.Record.Artist, a.Artists[0].Name);
                        var titleCompare = await StringCompare.CompareResultToRequest(request.Record.Title, a.Name);
                        if (artistCompare && titleCompare)
                        {
                            albums.Add(a);
                        }
                    }
                    //var albums = albumResults.Albums.Where(x => x.Artists.Any(y => y.Name.ToLower() == request.Record.Artist.ToLower() && x.Name.ToLower() == request.Record.Title.ToLower())).ToList();
                    var album = albums.Where(x => x.AvailableMarkets.Contains(request.Country.ToUpper())).FirstOrDefault();
                    if (album != null)
                    {
                        Guid recordId = recordRepo.GetIdForRecord(new Record { Artist = request.Record.Artist, Title = request.Record.Title });
                        if (recordId != Guid.Empty)
                        {
                            var recipitans = requestRepo.GetAllRecipitansForAlbumRequest(recordId, request.Country);
                            Create.CreateEmail(album, recipitans);
                            requestRepo.SetRequestAsFilled(recordId, request.Country);
                        }
                    }
                   
                }                
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public void SearchRequestsSync()
        {           
            SearchForRequests().Wait();
        }

        public void SetUpBrackgroundTask()
        {
            RecurringJob.AddOrUpdate(() => SearchRequestsSync(), "*/60 * * * *");
        }
    }
}