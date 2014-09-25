using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterManager.Startup))]
namespace TwitterManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
