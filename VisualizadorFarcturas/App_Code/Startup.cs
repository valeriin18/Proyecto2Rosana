using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VisualizadorFarcturas.Startup))]
namespace VisualizadorFarcturas
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
