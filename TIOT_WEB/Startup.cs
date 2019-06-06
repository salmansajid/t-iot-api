using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TIOT_WEB.Startup))]
namespace TIOT_WEB
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
