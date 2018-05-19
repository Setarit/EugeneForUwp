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
        private IDictionary<string, string> values;

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

        private async void _readRemoteFileAsync()
        {
            if (RemoteLocation == null) throw new InvalidNetworkPath("No network URL given");
            if (!RemoteLocation.EndsWith(".eug")) throw new InvalidNetworkPath("The remote file is not an Eugene file");
            values = new Dictionary<string, string>();
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
                var responseContents = response.Content.ReadAsStringAsync().Result;
                if (responseContents == null || responseContents.Length == 0) return;
                string[] lines = Regex.Split(responseContents, "\n");
                foreach (var line in lines)
                {
                    string[] keyValue = Regex.Split(line, ">");
                    try
                    {
                        values.Add(keyValue[0], keyValue[1]);
                    }
                    catch (IndexOutOfRangeException)//when the last line is empty
                    {}
                }
            }
        }

        /// <summary>
        /// Retrieves a value from the remote Eugene file
        /// </summary>
        /// <param name="key">The key of the value</param>
        /// <returns>The value for the given key</returns>
        internal string GetValue(string key)
        {
            if (!values.ContainsKey(key)) throw new UnknownConfigurationKeyException("The key '" + key + "' does not exist in the remote Eugene file");
            return values[key];
        }

        /// <summary>
        /// Reads the remote Eugene file.
        /// Needs to be called before a value is asked
        /// </summary>
        /// <exception cref="InvalidNetworkPath">If the network path is null or invalid</exception>
        public void ReadRemoteFile()
        {
            _readRemoteFileAsync();
        }

    }
}
