using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Configuration
{
    /// <summary>
    /// Collection of Default Configuration Values to be used
    /// </summary>
    public class Defaults
    {
        /// <summary>
        /// Name of the configuration file Nautilus will use
        /// </summary>
        public const string ConfigName = "Config.json";

        /// <summary>
        /// IP that Nautilus will listen for clients on 
        /// </summary>
        public const string IP = "127.0.0.1";

        /// <summary>
        /// Port that Nautilus will listen for clients on
        /// </summary>
        public const int Port = 4501;

        /// <summary>
        /// Amounts of connections that can be in the connection queue
        /// </summary>
        public const int PendingConnections = 10;
    }
}
