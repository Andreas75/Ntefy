using NtefySpotify.Models;
using NtefyWeb.DAL.Models;
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
        public static void CreateEmail(SimpleAlbum album, Request request)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var service = new EmailService(engines);

            var email = new RequestEmail
            {
                Title = album.Name,
                Url = album.Href
            };

            email.Send();
        }
    }
}
