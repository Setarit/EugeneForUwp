using EugeneForUwp.Configuration;
using EugeneForUwp.Network;

namespace EugeneForUwp
{
    public class Eugene
    {
        private ConfigurationFileReader _configFileReader;
        private double _currentSoftwareVersion;


        /// <summary>
        /// Default constructor
        /// <param name="configFileLocation">The absolute path of the configuration file</param>
        /// <param name="currentSoftwareVersion">The current version number of the software</param>
        /// </summary>
        public Eugene(string configFileLocation, double currentSoftwareVersion)
        {
            _configFileReader = new ConfigurationFileReader(configFileLocation);
            _currentSoftwareVersion = currentSoftwareVersion;
        }

        /// <summary>
        /// Constructor that reads the current local software version from the local Eugene file
        /// </summary>
        /// <param name="configFileLocation">The absolute path of the configuration file</param>
        public Eugene(string configFileLocation)
        {
            _configFileReader = new ConfigurationFileReader(configFileLocation);
            _currentSoftwareVersion = double.Parse(_configFileReader.GetConfigurationValue("localVersion"));
        }

        /// <summary>
        /// Checks with the configuration file and the current version is outdated
        /// </summary>
        /// <returns>True if a new version is available</returns>
        public bool CurrentVerionIsOutdated()
        {
            RemoteVersionChecker versionChecker = new RemoteVersionChecker(_configFileReader.GetConfigurationValue("location"));
            var remoteVersion = versionChecker.GetRemoteVersionNumber();
            return remoteVersion > _currentSoftwareVersion;
        }

        /// <summary>
        /// Checks on the remote location where the new software version can be downloaded
        /// </summary>
        /// <returns>The URL where the new software version is located</returns>
        public string GetDownloadLocation()
        {
            RemoteDownloadLocation downloadLocation = new RemoteDownloadLocation(_configFileReader.GetConfigurationValue("location"));
            return downloadLocation.GetDownloadLocation();
        }
    }
}
