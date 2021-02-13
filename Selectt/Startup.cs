using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Selectt.Startup))]
namespace Selectt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
