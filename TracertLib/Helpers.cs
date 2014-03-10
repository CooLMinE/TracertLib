using System;
using System.Net;

namespace Fluxbytes
{
    class Helpers
    {
        /// <summary>
        /// Check if the parameter is a valid IP or hostname.
        /// </summary>
        /// <param name="ipOrHostname"></param>
        /// <param name="address">The IPAddress object to set the address if the parameter is a valid IP or hostname.</param>
        /// <returns>Returns <b>true</b> if the argument is a valid IP or hostname, otherwise returns <b>false</b>.</returns>
        public static bool IsValidIpOrHostname(string ipOrHostname, out IPAddress address)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipOrHostname);

                if (hostEntry.AddressList.Length == 0)
                    throw new ArgumentException(string.Format("{0} is not a valid hostname or IP address.", ipOrHostname));
                else
                    address = hostEntry.AddressList[0];

                return true;
            }
            catch
            {
                address = null;
            }

            return false;
        }
    }
}
