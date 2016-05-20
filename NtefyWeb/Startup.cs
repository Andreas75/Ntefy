using Hangfire;
using Microsoft.Owin;
using NtefyWeb.Business;
using NtefyWeb.Business.Abstract;
using Owin;
using System;
using System.Web.SessionState;

[assembly: OwinStartupAttribute(typeof(NtefyWeb.Startup))]
namespace NtefyWeb
{
    public partial class Startup
    {
        //private ISearchRequestBackgroundTask _searchRequestBackgroundTask;        
       
        //public Startup(ISearchRequestBackgroundTask searchRequestBackgroundTask)
        //{
        //    _searchRequestBackgroundTask = searchRequestBackgroundTask;
        //}

        public void Configuration(IAppBuilder app)
        {
           
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AuthorizationFilters = new[] { new HangFireAuthorizationFilter() }
            });
            app.UseHangfireServer();

            new SearchRequestBackgroundTask().SetUpBrackgroundTask();
            
            
        }
    }
}
