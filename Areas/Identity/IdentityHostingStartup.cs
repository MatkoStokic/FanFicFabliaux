using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FanFicFabliaux.Areas.Identity.IdentityHostingStartup))]
namespace FanFicFabliaux.Areas.Identity
{

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(
                (context, services) => { }
                );
        }
    }
}
