using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;
using NtefySpotify;
using System.Threading.Tasks;
using System.Web.Configuration;
using NtefyWeb.Business;
using Hangfire;

namespace NtefyWeb.Controllers
{
    public class HomeController : Controller
    {
        private AccessToken accessToken;

        public ActionResult Index()
        {            
            GetAccessToken();
            return View();
        }


        public ActionResult AccessTokenCallback(string access_token, string token_type, string expires_in, string state)
        {
            if (access_token != null)
            {
                //ViewBag["token"] = access_token;
            }

            return RedirectToAction("MakeRequest");
        }

        public async Task GetAccessToken()
        {
            accessToken = new AccessToken();
            var x = await accessToken.GetAccessToken();            
            Session["accesstoken"] = x;            
        }

        public ActionResult MakeRequest()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}