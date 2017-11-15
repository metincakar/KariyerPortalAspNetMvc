using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web4._5.Startup))]
namespace web4._5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
