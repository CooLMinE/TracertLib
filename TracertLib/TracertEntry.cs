using System.Net;
using System.Net.NetworkInformation;

namespace Fluxbytes
{
    public class TracertEntry
    {
        /// <summary>
        /// The hop id. Represents the number of the hop.
        /// </summary>
        public int HopID { get; set; }

        /// <summary>
        /// The IP address.
        /// </summary>
        public IPAddress Address { get; set; }

        /// <summary>
        /// The hostname
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// The reply time it took for the host to receive and reply to the request in milliseconds.
        /// </summary>
        public long ReplyTime { get; set; }

        /// <summary>
        /// The reply status of the request.
        /// </summary>
        public IPStatus ReplyStatus { get; set; }

        public override string ToString()
        {
            return string.Format("{0} | {1} | {2}",
                HopID,
                Address == null ? "N/A" : string.IsNullOrEmpty(Hostname) ? Address.ToString() : Hostname + "[" + Address.ToString() + "]",
                ReplyStatus == IPStatus.TimedOut ? "Request Timed Out." : ReplyTime.ToString() + " ms"
                );
        }

        public override bool Equals(object obj)
        {
            TracertEntry o = obj as TracertEntry;
            return o != null &&
                o.HopID == this.HopID &&
                o.Address == this.Address &&
                o.Hostname == this.Hostname &&
                o.ReplyStatus == this.ReplyStatus &&
                o.ReplyTime == this.ReplyTime;
        }

        public override int GetHashCode()
        {
            return this.HopID.GetHashCode() +
                this.Address.GetHashCode() +
                this.Hostname.GetHashCode() +
                this.ReplyTime.GetHashCode() +
                this.ReplyStatus.GetHashCode();
        }
    }
}
