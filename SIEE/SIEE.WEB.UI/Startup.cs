using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIEE.WEB.UI.Startup))]
namespace SIEE.WEB.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
