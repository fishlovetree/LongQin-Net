using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LongQin.Startup))]
namespace LongQin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
