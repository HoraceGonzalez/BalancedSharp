using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests
{
    [TestFixture]
    public class HttpWebRequestRestTests
    {
        HttpWebRequestRest rest;

        [SetUp]
        public void Setup()
        {
            rest = new HttpWebRequestRest(new DcJsonBalancedSerializer());
        }

        [Test]
        public void BuildParameters_Null_EmptyString()
        {
            string result = rest.BuildParameters(null);
            Assert.AreEqual(result, "");
        }

        [Test]
        public void BuildParameters_Empty_EmptyString()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string result = rest.BuildParameters(dic);
            Assert.AreEqual(result, "");
        }

        [Test]
        public void BuildParameters_Success()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key1", "value1");
            dic.Add("key2", "value2");
            string result = rest.BuildParameters(dic);
            Assert.AreEqual(result, "key1=value1&key2=value2");
        }
    }
}
