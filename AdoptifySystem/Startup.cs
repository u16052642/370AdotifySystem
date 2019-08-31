using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdoptifySystem.Startup))]
namespace AdoptifySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
