using Hangfire;
using NtefyEmail;
using NtefySpotify;
using NtefySpotify.Models;
using NtefyWeb.Business.Abstract;
using NtefyWeb.Controllers;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;
using NtefyWeb.DAL.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace NtefyWeb.Business
{
    public class SearchRequestBackgroundTask : ISearchRequestBackgroundTask
    {
        //private List<Request> Requests;
        //private ISpotifyIntegration _spotifyIntegration;
        //private IRequestRepository _requestRepository;
        //private IRecordRepository _recordRepository;
        //private List<string> recordIds = new List<string>();
        //private IAccessToken _accessToken;       

        //public SearchRequestBackgroundTask(ISpotifyIntegration spotifyIntegration, IRequestRepository requestRepo, IRecordRepository recordRepo, IAccessToken accessToken)
        //{
        //    _spotifyIntegration = spotifyIntegration;            
        //    _requestRepository = requestRepo;
        //    _recordRepository = recordRepo;
        //    _accessToken = accessToken;
            
        //}
        //private RequestRepository _requestRepository;
        private List<string> recordIds = new List<string>();
        //private AccessToken _accessToken;
        //private SpotifyIntegration _spotifyIntegration;
        //private RecordRepository _recordRepository;

        public SearchRequestBackgroundTask()
        {
            //_requestRepository = new RequestRepository();
            //_accessToken = new AccessToken();
            //_spotifyIntegration = new SpotifyIntegration();
            //_recordRepository = new RecordRepository();
        }

        public List<Request> GetAllRequestedRecords()
        {
            var requestRepository = new RequestRepository();
            return requestRepository.FilterdRequestsForRecordAndMarket();
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
                        var spotifyIntegration = new SpotifyIntegration();
                        var result = await spotifyIntegration.SerachForAlbum(request.Record.Artist, request.Record.Title, token.access_token, request.Country);
                        if (result != null)
                        {
                            foreach (var item in result.Albums.Items)
                            {
                                new RecordRepository().UpdateRecordTitle(request.Record, item.Name);
                                recordIds.Add(result.Albums.Items.FirstOrDefault().Id);
                            }                            
                        }
                    }
                }
                FillRequest(recordIds, requests, token.access_token);  
            }
                     
        }

        public async void FillRequest(List<string> recordIds, List<Request> requests, string token)
        {
            List<FullAlbum> albums = new List<FullAlbum>();
            if (recordIds.Count > 0)
            {
                var albumResults = await new SpotifyIntegration().GetMultibleAlbums(string.Join(",", recordIds), token);
                foreach (var request in requests)
                {
                    foreach (var a in albumResults.Albums)
                    {
                        var artistCompare = await StringCompare.CompareResultToRequest(request.Record.Artist, a.Artists[0].Name);
                        var titleCompare = await StringCompare.CompareResultToRequest(request.Record.Title, a.Name);
                        if (artistCompare && titleCompare && DateTime.Parse(a.ReleaseDate) <= DateTime.Now)
                        {
                            albums.Add(a);
                        }
                    }
                   
                    //var albums = albumResults.Albums.Where(x => x.Artists.Any(y => y.Name.ToLower() == request.Record.Artist.ToLower() && x.Name.ToLower() == request.Record.Title.ToLower())).ToList();
                    var album = albums.Where(x => x.AvailableMarkets.Contains(request.Country.ToUpper())).FirstOrDefault();
                    albums.Clear();
                    if (album != null)
                    {
                        Guid recordId = new RecordRepository().GetIdForRecord(new Record { Artist = request.Record.Artist, Title = request.Record.Title });
                        if (recordId != Guid.Empty)
                        {
                            var recipitans = new RequestRepository().GetAllRecipitansForAlbumRequest(recordId, request.Country);
                            Create.CreateEmail(album, recipitans);
                            new RequestRepository().SetRequestAsFilled(recordId, request.Country);
                        }
                        album = null;
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