using EugeneForUwp.Exception;
using System.Collections.Generic;
using System.IO;

namespace EugeneForUwp.Configuration
{
    class ConfigurationFileReader
    {
        private string[] _configFileLines;
        private IDictionary<string, string> _valuesMap;

        /// <summary>
        /// Opens the config file and reads the lines
        /// </summary>
        /// <param name="configFile">The absolute path to the config file</param>
        public ConfigurationFileReader(string configFile)
        {
            if (!configFile.EndsWith(".eug")) { throw new InvalidConfigurationFileException("Invalid configuration file extension"); }
            _configFileLines = File.ReadAllLines(configFile);
            _buildKeyValueMap();
        }

        private void _buildKeyValueMap()
        {
            _valuesMap = new Dictionary<string, string>();
            foreach(var line in _configFileLines)
            {
                string[] splitted = line.Split('>');
                if (splitted.Length < 2) throw new InvalidConfigurationFileException("Configuration file is not well formatted. Make sure the last line is not blank.");
                _valuesMap.Add(splitted[0], splitted[1]);
            }
        }

        /// <summary>
        /// Gets a value from the configuration file
        /// </summary>
        /// <param name="key">The key of the configuration value</param>
        /// <returns>The value for the configuration key</returns>
        public string GetConfigurationValue(string key)
        {
            if(!_valuesMap.ContainsKey(key)) { throw new UnknownConfigurationKeyException("The key '" + key + "' is not defined in the configuration file"); }
            return _valuesMap[key];
        }
    }
}
