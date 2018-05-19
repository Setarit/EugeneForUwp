using EugeneForUwp.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EugeneForUwp.Configuration
{
    class Configuration
    {
        private string[] _configFileLines;
        private IDictionary<string, string> _valuesMap;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configurationFileContents">The contents in the configuration file</param>
        public Configuration(string configurationFileContents)
        {
            _configFileLines = GetLinesFromString(configurationFileContents);
            _buildKeyValueMap();
        }

        private string[] GetLinesFromString(string fileContents)
        {
            List<string> result = new List<string>();
            using (StringReader sr = new StringReader(fileContents))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            return result.ToArray();
        }

        private void _buildKeyValueMap()
        {
            _valuesMap = new Dictionary<string, string>();
            foreach (var line in _configFileLines)
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
        public string GetValue(string key)
        {
            if (!_valuesMap.ContainsKey(key)) { throw new UnknownConfigurationKeyException("The key '" + key + "' is not defined in the configuration file"); }
            return _valuesMap[key];
        }
    }
}
