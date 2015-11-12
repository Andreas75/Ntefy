using Hangfire;
using Microsoft.Owin;
using NtefyWeb.Business;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(NtefyWeb.Startup))]
namespace NtefyWeb
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard();
            app.UseHangfireServer();            
        }
    }
}
