using Microsoft.Owin;
using Owin;
using Rite.Software.Shepherdaid.Web.Frontend;

[assembly: OwinStartup(typeof(Startup))]
namespace Rite.Software.Shepherdaid.Web.Frontend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
