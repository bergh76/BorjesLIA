using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VikingsTest.Startup))]
namespace VikingsTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
