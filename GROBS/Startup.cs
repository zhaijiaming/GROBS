using Microsoft.Owin;
using Owin;
[assembly: OwinStartupAttribute(typeof(GROBS.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace GROBS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //GROBS.BSL.ServiceFactory.hotload();
        }
    }
}
