using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Loja.Startup))]
namespace Loja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
