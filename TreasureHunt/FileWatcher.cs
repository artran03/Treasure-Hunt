using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TreasureHunt
{
    public class FileWatcher
    {
        public static FileSystemWatcher InitializeWatcher(IConfiguration configuration)
        {
            var watcher = new FileSystemWatcher(configuration.GetValue<string>("Paths:InputDirectory"))
            {
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size,

                Filter = "*.txt",
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            return watcher;
        }
    }
}
