using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSL.DynamicAndFriendlyUrlUsingMvc.Startup))]
namespace FSL.DynamicAndFriendlyUrlUsingMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
