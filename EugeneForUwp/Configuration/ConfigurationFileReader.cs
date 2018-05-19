using EugeneForUwp.Exception;
using System;
using System.Collections.Generic;
using System.IO;

namespace EugeneForUwp.Configuration
{
    class ConfigurationFileReader
    {
        public string FileContents { get; set; }

        /// <summary>
        /// Opens the config file and reads the lines
        /// </summary>
        /// <param name="configFile">The absolute path to the config file</param>
        public ConfigurationFileReader(string configFile)
        {
            if (!configFile.EndsWith(".eug")) { throw new InvalidConfigurationFileException("Invalid configuration file extension"); }
            FileContents = File.ReadAllText(configFile);
        }
    }
}
