using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agenda2868022.Startup))]
namespace Agenda2868022
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
