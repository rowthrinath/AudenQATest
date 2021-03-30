using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AudenQATest.Config
{
  public  class Configuration
    {
        public static IConfigurationRoot GetConfigurationRoot()
        {
            var path = Directory.GetCurrentDirectory();
            if (!File.Exists(Path.Combine(path, "appsettings.json")))
            {
                throw new Exception($"Application settings file not found in {path}");
            }
            return new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
