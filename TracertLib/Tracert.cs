using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Fluxbytes
{
    public class Tracert
    {
        private int _maxHops;
        private int _timeout;

        /// <summary>
        /// The max number of hops. <b>Default value is 30.</b>
        /// </summary>
        public int MaxHops
        {
            get { return _maxHops; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Max hops can't be lower than 1.");
                else
                    _maxHops = value;
            }
        }

        /// <summary>
        /// The timeout value for each individual hop. <b>Default value is 5000 milliseconds.</b>
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Timeout value must be higher than 0.");
                else
                    _timeout = value;
            }
        }

        /// <summary>
        /// Tracecr Constructor.
        /// </summary>
        public Tracert()
        {
            _maxHops = 30;
            _timeout = 5000;
        }

        /// <summary>
        /// Retrieve the route details and the delay across an internet protocol network.
        /// </summary>
        /// <param name="ipOrHostname">The destination host address.</param>
        /// <returns>Returns the details for each hop, including its IP, host and the time it took to get a reply.</returns>
        public IEnumerable<TracertEntry> Trace(string ipOrHostname)
        {
            IPAddress address;

            // Ensure the argument data is a valid IP or hostname. If it is not throw an ArgumentException.
            if (!Helpers.IsValidIpOrHostname(ipOrHostname, out address))
            {
                Debug.WriteLine("Lookup failed. Parameter address is not valid.");
                throw new ArgumentException(string.Format("{0} is not a valid hostname or IP address.", ipOrHostname));
            }
            else
            {
                Ping ping = new Ping();
                PingOptions pingOptions = new PingOptions(1, true);
                Stopwatch pingReplyTime = new Stopwatch();
                PingReply reply;

                do
                {
                    // Start the stopwatch in order to measure how long it took for our host to reply.
                    pingReplyTime.Start();

                    // Ping the host.
                    reply = ping.Send(address, Timeout, new byte[] { 0 }, pingOptions);

                    // Stop the stopwatch right after we get a response from our host.
                    pingReplyTime.Stop();

                    string hostname = string.Empty;
                    if (reply.Address != null)
                    {
                        try
                        {
                            hostname = Dns.GetHostByAddress(reply.Address).HostName;    // Retrieve the hostname for the replied address.
                        }
                        catch (SocketException) { /* No host available for that address. */ }
                    }

                    // Return out TracertEntry object with all the information about the hop.
                    yield return new TracertEntry()
                    {
                        HopID = pingOptions.Ttl,
                        Address = reply.Address,
                        Hostname = hostname,
                        ReplyTime = pingReplyTime.ElapsedMilliseconds,
                        ReplyStatus = reply.Status
                    };

                    
                    pingOptions.Ttl++;

                    // Stop and reset our stopwatch back to 0.
                    pingReplyTime.Reset();
                }
                while (reply.Status != IPStatus.Success && pingOptions.Ttl <= MaxHops);
            }
        }
    }
}
