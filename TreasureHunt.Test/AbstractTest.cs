using Microsoft.Extensions.DependencyInjection;

namespace TreasureHunt.Test
{
    public class AbstractTest
    {
        protected ServiceProvider ServiceProvider { get; set; }

        protected void InitializeTest()
        {
            var startup = new Startup();
            ServiceProvider = startup.Services.BuildServiceProvider();
        }

        protected T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
