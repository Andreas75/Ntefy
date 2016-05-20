using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace NtefyWeb.Models.ViewModels
{
    public class RequestEmail : Email, IRequestEmail
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Recipitans { get; set; }
    }
}