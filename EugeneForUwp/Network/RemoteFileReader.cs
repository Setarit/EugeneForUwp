using EugeneForUwp.Exception;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace EugeneForUwp.Network
{
    /// <summary>
    /// Reads the remote file
    /// </summary>
    internal class RemoteFileReader
    {
        public string RemoteLocation { get; set; }
        private static RemoteFileReader _remoteFileReader;
        public Configuration.Configuration Configuration { get; set; }        

        /// <summary>
        /// Returns an instance of the RemoteFileReader
        /// </summary>
        /// <returns>An instance of the RemoteFileReader</returns>
        public static RemoteFileReader GetInstance()
        {
            if(_remoteFileReader == null)
            {
                _remoteFileReader = new RemoteFileReader();
            }
            return _remoteFileReader;
        }

        private RemoteFileReader() { }

        /// <summary>
        /// Reads the remote Eugene file.
        /// Needs to be called before a value is asked
        /// </summary>
        /// <exception cref="InvalidNetworkPath">If the network path is null or invalid</exception>
        public async System.Threading.Tasks.Task ReadRemoteFileAsync()
        {
            if (RemoteLocation == null) throw new InvalidNetworkPath("No network URL given");
            if (!RemoteLocation.EndsWith(".eug")) throw new InvalidNetworkPath("The remote file is not an Eugene file");
            using (var client = new System.Net.Http.HttpClient())
            {
                var response = await client.GetAsync(RemoteLocation);
                try
                {
                    response.EnsureSuccessStatusCode();
                }catch (System.Exception e)
                {
                    throw new InvalidNetworkPath(e.Message, e);
                }
                var responseContents = await response.Content.ReadAsStringAsync();
                if (responseContents == null || responseContents.Length == 0) return;
                Configuration = new Configuration.Configuration(responseContents);                
            }
        }
    }
}
