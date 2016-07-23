using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OA.UI.Startup))]
namespace OA.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
