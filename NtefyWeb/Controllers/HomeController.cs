using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Abstract;

namespace NtefyWeb.Controllers
{
    public class HomeController : Controller
    {
        private IRequestRepository repository;

        public HomeController(IRequestRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var context = new RequestContext();
            //var record = new Record
            //{
            //    Artist = "Jamie xx",
            //    Title = "In Colour"
            //};
            //context.Records.Add(record);
            //context.SaveChanges();

            //var recordId = context.Records.First().RecordId;
            //if(userId != null)
            //{
            //    var request = new Request
            //    {
            //        UserId = userId,
            //        RequestDate = DateTime.Now,
            //        RecordId = recordId
            //    };
            //    context.Requests.Add(request);
            //    context.SaveChanges();
            //}
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