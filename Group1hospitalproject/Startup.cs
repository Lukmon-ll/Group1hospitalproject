using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Group1hospitalproject.Startup))]
namespace Group1hospitalproject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
