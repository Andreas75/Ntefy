using Hangfire;
using NtefySpotify;
using NtefySpotify.Models;
using NtefyWeb.Business;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Concrete;
using NtefyWeb.Models.ViewModels;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;
using System.Net.Mail;
using NtefyEmail;

namespace NtefyWeb.Controllers
{
    public class RequestController : Controller
    {
        private RequestRepository requestRepo;
        private RecordRepository recordRepo;
        private UserRepository userRepo;       

        public RequestController()
        {
            requestRepo = new RequestRepository();
            recordRepo = new RecordRepository();
            userRepo = new UserRepository();                      
        }

        public ActionResult MyRequests()
        {            
            return View();            
        }

        [HttpPost]
        public async Task<ActionResult> MakeRequest(RecordViewModel model)
        {            
            //var token = (string)Session["accesstoken"];
            var accessToken = new AccessToken();
            var token = await accessToken.GetAccessToken();
            
            if (ModelState.IsValid)
            {
                var album = await new SpotifyIntegration().SerachForAlbum(new Record { Artist = model.Artist, Title = model.Title }, token, userRepo.GetCurrentUserMarket());
                var fullAlbum = await HandleSpotifyResult.CompareFoundAlbumToRequest(model.Artist, model.Title, album.Albums.Items, token);        
                if (fullAlbum != null)
                {                   
                    if (fullAlbum.Tracks.Items.Count > 1)
                    {                        
                        var resultJson = Json(new
                        {
                            message = "Request found",
                            artist = fullAlbum.Artists.First().Name,
                            title = fullAlbum.Name,
                            image = fullAlbum.Images.Last().Url,
                            url = fullAlbum.ExternalUrls["spotify"]
                        });
                        return resultJson;
                    }
               
                }
                else
                {
                    recordRepo.AddRecord(new Record { Artist = model.Artist, Title = model.Title });
                    var recordFromCache = AlbumCache.GetRecordFromCache(model.Artist, model.Title);
                    if (recordFromCache != null)
                    {
                        requestRepo.AddRequest(recordFromCache.RecordId, userRepo.GetCurrentUserId());
                    }
                }
            }   
           
            return Json(new { message = "", artist = model.Artist, title = model.Title });

            
            
        }


    }
}