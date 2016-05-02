using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BorjesLIA.Startup))]
namespace BorjesLIA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
