using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using TreasureHunt.Interfaces;

namespace TreasureHunt
{
    public class Startup
    {
        public ServiceCollection Services { get; set; }

        public Startup()
        {
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

            Services = new ServiceCollection();
            Services.AddSingleton(configuration);
            Services.AddTransient<IHuntConfiguration, HuntConfiguration>();
            Services.AddTransient<IHuntService, HuntService>();
        }
    }
}
