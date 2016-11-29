using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScopoMFinance.Web.Startup))]
namespace ScopoMFinance.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
