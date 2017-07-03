using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RenewedApartmentThingy.Startup))]
namespace RenewedApartmentThingy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
