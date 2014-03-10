using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fluxbytes;
using System.Linq;

namespace TracertTests
{
    [TestClass]
    public class TracertUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidIpLetterSmall()
        {
            Tracert tr = new Tracert();
            tr.Trace("a").First();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidIpLetterCapital()
        {
            Tracert tr = new Tracert();
            tr.Trace("A").First();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HopsNumberZero()
        {
            Tracert tr = new Tracert();
            tr.MaxHops = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HopsNumberNegative()
        {
            Tracert tr = new Tracert();
            tr.MaxHops = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeoutValueZero()
        {
            Tracert tr = new Tracert();
            tr.Timeout = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeoutValueNegativeNumber()
        {
            Tracert tr = new Tracert();
            tr.Timeout = -1;
        }
    }
}
