using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team7_SPSUBookstore.Startup))]
namespace Team7_SPSUBookstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
