using NtefySpotify.Models;
using NtefyWeb.DAL.Models;
using NtefyWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtefyWeb.Controllers
{
    public class EmailController : Controller
    {        
        public void CreateEmail(FullAlbum album, string recipitans)
         {
            var email = new RequestEmail
            { 
                Artist = album.Artists.First().Name,
                Title = album.Name,
                Url = album.Href,
                ImageUrl = album.Images.First().Url,
                Recipitans = recipitans
            };

            email.Send();            
        }
    }
}