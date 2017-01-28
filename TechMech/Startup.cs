using Microsoft.Owin;
using Owin;
using TechMech;

[assembly: OwinStartup(typeof(Startup))]
namespace TechMech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
