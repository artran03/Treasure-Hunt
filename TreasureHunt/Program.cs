using Microsoft.Extensions.DependencyInjection;
using System;
using TreasureHunt.Interfaces;

namespace TreasureHunt
{
    class Program
    {
        static void Main()
        {
            var startup = new Startup();
            var provider = startup.Services.BuildServiceProvider();
            var hunt = provider.GetService<IHuntConfiguration>();
            hunt.ConfigureHunt();

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    } 
}
