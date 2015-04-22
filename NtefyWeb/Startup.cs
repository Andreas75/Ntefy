using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NtefyWeb.Startup))]
namespace NtefyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
