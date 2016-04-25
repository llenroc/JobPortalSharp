using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobPortalSharp.Startup))]
namespace JobPortalSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
