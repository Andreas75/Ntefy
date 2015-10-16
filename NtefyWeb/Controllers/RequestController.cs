using NtefyWeb.Business;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL.Repository.Concrete;
using NtefyWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtefyWeb.Controllers
{
    public class RequestController : Controller
    {
        private RequestRepository requestRepo;
        private RecordRepository recordRepo;

        public RequestController()
        {
            requestRepo = new RequestRepository();
            recordRepo = new RecordRepository();
        }       

        [HttpPost]
        public ActionResult AddRecord(RecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                recordRepo.AddRecord(new Record { Artist = model.Artist, Title = model.Title });           
            }
            return RedirectToAction("Index", "Home");
        }
    }
}