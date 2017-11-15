using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web5.Startup))]
namespace web5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
