using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCTest001.Startup))]
namespace MVCTest001
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
