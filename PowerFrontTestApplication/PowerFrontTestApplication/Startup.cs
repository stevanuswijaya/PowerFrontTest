using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerFrontTestApplication.Startup))]
namespace PowerFrontTestApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
