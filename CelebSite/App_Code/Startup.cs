using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CelebSite.Startup))]
namespace CelebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
