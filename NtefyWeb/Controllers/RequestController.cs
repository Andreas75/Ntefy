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
using NtefyWeb.DAL.Repository.Abstract;
using NtefyWeb.Business.Abstract;

namespace NtefyWeb.Controllers
{
    public class RequestController : Controller
    {       

        public RequestController()
        {            
        }
        
        public ActionResult MyRequests()
        {            
            return View();            
        }

        [HttpPost]
        public async Task<ActionResult> MakeRequest(RecordModel model)
        {
            string token = string.Empty;
            SpotifyToken spotifyToken;
            spotifyToken = Session["accessToken"] as SpotifyToken;
            if (spotifyToken == null)
            {
                spotifyToken = await new AccessToken().GetAccessToken();
                Session["accessToken"] = spotifyToken;
                token = spotifyToken.access_token;
            }
            else
            {
                token = await new AccessToken().ValidateAccessToken(spotifyToken);
            }            
            
            if (ModelState.IsValid)
            {
                var album = await new SpotifyIntegration().SerachForAlbum(model.Artist, model.Title, token, new UserRepository().GetCurrentUserMarket());
                var fullAlbum = await new HandleSpotifyResult().CompareFoundAlbumToRequest(model.Artist, model.Title, album.Albums.Items, token);        
                if (fullAlbum != null)
                {                   
                    if (fullAlbum.Tracks.Items.Count > 1 && DateTime.Parse(fullAlbum.ReleaseDate) <= DateTime.Now)
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
                    new RecordRepository().AddRecord(new Record { Artist = model.Artist, Title = model.Title });
                    var recordFromCache = AlbumCache.GetRecordFromCache(model.Artist, model.Title);
                    if (recordFromCache != null)
                    {
                        new RequestRepository().AddRequest(recordFromCache.RecordId, new UserRepository().GetCurrentUserId());
                    }
                }
            }
           
            return Json(new { message = "", artist = model.Artist, title = model.Title });

            
            
        }


    }
}