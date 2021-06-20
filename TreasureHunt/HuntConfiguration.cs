using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreasureHunt.Helpers;
using TreasureHunt.Interfaces;

namespace TreasureHunt
{
    public class HuntConfiguration : IHuntConfiguration
    {
        private FileSystemWatcher _fileSystemWatcher;
        private readonly IConfigurationRoot _configuration;
        private readonly IHuntService _huntService;

        public HuntConfiguration(IConfigurationRoot configuration, IHuntService huntService)
        {
            _configuration = configuration;
            _huntService = huntService;
        }

        public void ConfigureHunt()
        {
            _fileSystemWatcher = FileWatcher.InitializeWatcher(_configuration);
            _fileSystemWatcher.Created += OnFileChangedOrCreated;
            _fileSystemWatcher.Changed += OnFileChangedOrCreated;
        }

        private void OnFileChangedOrCreated(object sender, FileSystemEventArgs e)
        {
            IList<string> fileContent = FileHelper.GetFileContent(e.FullPath);
            var huntContext = _huntService.LaunchHunt(fileContent);
            GenerateResultFile(huntContext);
        }

        private void GenerateResultFile(HuntContext huntContext)
        {
            using (TextWriter tw = new StreamWriter($"{_configuration.GetValue<string>("Paths:OutputDirectory")}Result.txt"))
            {
                // Map configuration
                tw.WriteLine(huntContext.Map.ToString());

                // Mountains configuration
                foreach (var mountain in huntContext.Mountains)
                    tw.WriteLine(mountain.ToString());

                // Treasures not found
                foreach (var treasure in huntContext.Treasures.Where(t => t.TreasuresCount > 0).ToList())
                    tw.WriteLine(treasure.ToString());

                // Final Adventurer positions
                foreach (var adventurer in huntContext.Adventurers)
                    tw.WriteLine(adventurer.ToString());
            }

            Console.WriteLine($"Result file generated successfully at : { _configuration.GetValue<string>("Paths:OutputDirectory")}Result.txt");
        }
    }
}
