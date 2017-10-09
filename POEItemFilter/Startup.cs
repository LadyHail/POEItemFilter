using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POEItemFilter.Startup))]
namespace POEItemFilter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
