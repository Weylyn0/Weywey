using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Weywey.Core.Services
{
    public static class ConfigurationService
    {
        public static string Token { get; private set; }
        public static string Prefix { get; private set; }

        public static void RunService()
        {
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("configuration.yaml")
                .Build();
                Token = config["Token"];
                Prefix = config["Prefix"];
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("configuration.yaml file not found.");
            }
        }
    }
}