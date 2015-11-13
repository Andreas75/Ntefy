using NtefySpotify.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;
using Postal;
using System.Web.Mvc;
using System.Web.Hosting;
using NtefyEmail.Models;

namespace NtefyEmail
{
    public static class Create
    {
        public static void CreateEmail(FullAlbum album, string recipitans)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);

            var email = new RequestEmail
            {
                Artist = album.Artists.First().Name,
                Title = album.Name,
                Url = album.Href,
                ImageUrl = album.Images.First().Url,
                Recipitans = recipitans
            };

            emailService.Send(email);
        }
    }
}
