using EugeneForUwp.Configuration;
using EugeneForUwp.Network;

namespace EugeneForUwp
{
    public class Eugene
    {
        private ConfigurationFileReader _configFileReader;
        private Configuration.Configuration _configuration;
        private double _currentSoftwareVersion;


        /// <summary>
        /// Default constructor
        /// <param name="configFileLocation">The absolute path of the configuration file</param>
        /// <param name="currentSoftwareVersion">The current version number of the software</param>
        /// </summary>
        public Eugene(string configFileLocation, double currentSoftwareVersion)
        {
            _configFileReader = new ConfigurationFileReader(configFileLocation);
            _configuration = new Configuration.Configuration(_configFileReader.FileContents);
            _currentSoftwareVersion = currentSoftwareVersion;
        }

        /// <summary>
        /// Constructor that reads the current local software version from the local Eugene file
        /// </summary>
        /// <param name="configFileLocation">The absolute path of the configuration file</param>
        public Eugene(string configFileLocation)
        {
            _configFileReader = new ConfigurationFileReader(configFileLocation);
            _configuration = new Configuration.Configuration(_configFileReader.FileContents);
            _currentSoftwareVersion = double.Parse(_configuration.GetValue("localVersion"));
        }

        /// <summary>
        /// Checks with the configuration file and the current version is outdated
        /// </summary>
        /// <returns>True if a new version is available</returns>
        public async System.Threading.Tasks.Task<bool> CurrentVerionIsOutdatedAsync()
        {
            RemoteVersionChecker versionChecker = new RemoteVersionChecker(_configuration.GetValue("location"));
            var remoteVersion = await versionChecker.GetRemoteVersionNumberAsync();
            return remoteVersion > _currentSoftwareVersion;
        }

        /// <summary>
        /// Checks on the remote location where the new software version can be downloaded
        /// </summary>
        /// <returns>The URL where the new software version is located</returns>
        public async System.Threading.Tasks.Task<string> GetDownloadLocationAsync()
        {
            RemoteDownloadLocation downloadLocation = new RemoteDownloadLocation(_configuration.GetValue("location"));
            return await downloadLocation.GetDownloadLocationAsync();
        }
    }
}
