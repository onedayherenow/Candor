using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Candor.Startup))]
namespace Candor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
