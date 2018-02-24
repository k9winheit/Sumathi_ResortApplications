using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SumathiResort_WebApp.Startup))]
namespace SumathiResort_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
