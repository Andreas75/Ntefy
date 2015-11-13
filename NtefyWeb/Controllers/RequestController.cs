using Hangfire;
using NtefySpotify;
using NtefySpotify.Models;
using NtefyWeb.Business;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Concrete;
using NtefyWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public async Task<ActionResult> MakeRequest(RecordViewModel model)
        {

            var token = (string)Session["accesstoken"];
            
            if (ModelState.IsValid)
            {
                recordRepo.AddRecord(new Record { Artist = model.Artist, Title = model.Title });
                requestRepo.AddRequest(AlbumCache.GetRecordFromCache(model.Artist, model.Title).RecordId, userRepo.GetCurrentUserId());
            }           

            var albumSearch = await new SpotifyIntegration().SerachForAlbum(new Record { Artist = model.Artist, Title = model.Title }, token, userRepo.GetCurrentUserMarket());
                        
            if (albumSearch != null)
            {
                var album = await new SpotifyIntegration().GetAlbum(albumSearch.Id, token);
                var resultJson = Json(new
                {
                    message = "Request found",
                    artist = album.Artists.First().Name,
                    title = album.Name,
                    url = album.ExternalUrls["spotify"]                   
                });
                return resultJson;
            }
            else
            {
                return Json(new { message = "", artist = model.Artist, title = model.Title });

            }
            
        }


    }
}