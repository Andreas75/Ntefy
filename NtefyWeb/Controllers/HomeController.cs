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
using NtefyWeb.DAL.Repository.Concrete;
using NtefyWeb.Business.Abstract;

namespace NtefyWeb.Controllers
{
    public class HomeController : Controller
    {
        private AccessToken accessToken;
        //private ISearchRequestBackgroundTask _searchRequestBackgroundTask;

        //public HomeController(ISearchRequestBackgroundTask srbt)
        //{
        //    _searchRequestBackgroundTask = srbt;
        //}

        public async Task<ActionResult> Index()
        {
            //_searchRequestBackgroundTask.SetUpBrackgroundTask();
            Session["Bull"] = "Shit";
            return View();
        }


        public ActionResult AccessTokenCallback(string access_token, string token_type, string expires_in, string state)
        {
            return RedirectToAction("MakeRequest");
        }

        //public async Task GetAccessToken()
        //{
        //    accessToken = new AccessToken();
        //    var x = await accessToken.GetAccessToken();            
        //    Session["accesstoken"] = x;            
        //}

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