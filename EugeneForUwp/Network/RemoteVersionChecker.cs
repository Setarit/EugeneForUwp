using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using EugeneForUwp.Exception;
using System.Text.RegularExpressions;

namespace EugeneForUwp.Network
{
    /// <summary>
    /// Checks on the given url the latest version number of the software
    /// </summary>
    class RemoteVersionChecker
    {
        private string _remoteLocation;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="remoteLocation">The url of the location where the latest software version number is stored</param>
        public RemoteVersionChecker(string remoteLocation)
        {
            _remoteLocation = remoteLocation;            
        }

        /// <summary>
        /// Connects to the remote location and checks the remote version number
        /// </summary>
        /// <exception cref="InvalidRemoteVersion">If the file on the given location is in an invalid format</exception>
        /// <returns>The remote version number</returns>
        public async Task<double> GetRemoteVersionNumberAsync()
        {
            var networkReader = RemoteFileReader.GetInstance();
            networkReader.RemoteLocation = _remoteLocation;
            await networkReader.ReadRemoteFileAsync();
            return _remoteVersionRetreiveTask(networkReader.Configuration.GetValue("version"));
        }

        private double _remoteVersionRetreiveTask(string version)
        {            
            double versionAsDouble = -1;
            version = RemoveNonDigitCharacters(version);
            try {
                versionAsDouble = double.Parse(version);
            }catch(System.Exception e)
            {
                throw new InvalidRemoteVersion(e.Message, e);
            }
            return versionAsDouble;
        }

        /// <summary>
        /// Removes the non digit characters from the version string to allow support for semantic versioning
        /// </summary>
        /// <param name="version">The version as string</param>
        /// <returns>Sanitized version as a string</returns>
        private string RemoveNonDigitCharacters(string version)
        {
            return Regex.Replace(version, @"\D", "");
        }
    }
}
