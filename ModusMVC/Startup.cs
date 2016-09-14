using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModusMVC.Startup))]
namespace ModusMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
