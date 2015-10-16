using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NtefyWeb.DAL;
using NtefyWeb.DAL.Models;

namespace NtefyWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        NtefyWeb.DAL.RequestContext dbContext = new NtefyWeb.DAL.RequestContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpRuntime.Cache.Insert("cachedAlbums", dbContext.Records.ToList(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.Zero);

        }
    }
}
